﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Realmlist_Changer.Properties;
using System.Net;
using System.Threading;
using System.Security.Cryptography;
using System.Xml;

namespace Realmlist_Changer
{
    public enum AddRealmlistErrors
    {
        AddRealmlistErrorNone = 0,
        AddRealmlistErrorAlreadyAdded = 1,
        AddRealmlistErrorInvalidRealmlist = 2,
    }

    public enum ChangeRealmlistErrors
    {
        ChangeRealmlistErrorNone = 0,
        ChangeRealmlistErrorNothingChanged = 1,
        ChangeRealmlistErrorInvalidRealmlist = 2,
        ChangeRealmlistErrorRealmlistNotFound = 3,
    }

    public partial class MainForm : Form
    {
        private const int EM_SETCUEBANNER = 0x1501;
        private const uint WM_KEYDOWN = 0x0100;
        private const uint WM_KEYUP = 0x0101;
        private const uint WM_CHAR = 0x0102;
        private const int VK_RETURN = 0x0D;
        private const int VK_TAB = 0x09;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private Dictionary<string /* realmlist */, Account /* account */> _realmlists = new Dictionary<string, Account>();
        private readonly string _xmlDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Realmlist-Changer\";
        private readonly string _xmlDirFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Realmlist-Changer\realmlist-changer.xml";
        private Socket _clientSocket;

        public Dictionary<string, Account> Realmlists
        {
            get { return _realmlists; }
            set { _realmlists = value; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //! Set the placeholder text
            SendMessage(textBoxRealmlistFile.Handle, EM_SETCUEBANNER, 0, "Realmlist.wtf directory");
            SendMessage(textBoxWowFile.Handle, EM_SETCUEBANNER, 0, "Wow.exe directory");
            SendMessage(textBoxAccountName.Handle, EM_SETCUEBANNER, 0, "Account name");
            SendMessage(textBoxAccountPassword.Handle, EM_SETCUEBANNER, 0, "Account password");
            textBoxRealmlistFile.Text = Settings.Default.RealmlistDir;
            textBoxWowFile.Text = Settings.Default.WorldOfWarcraftDir;
            checkBoxLoginToChar.Checked = Settings.Default.LoginToChar;

            if (File.Exists(_xmlDirFile))
            {
                using (var stringReader = new StringReader(File.ReadAllText(_xmlDirFile)))
                {
                    using (var reader = new XmlTextReader(stringReader))
                    {
                        string realmlist = String.Empty, accountName = String.Empty, encryptedPassword = String.Empty;

                        while (reader.Read())
                        {
                            if (reader.IsStartElement())
                            {
                                switch (reader.Name)
                                {
                                    case "realm":
                                        realmlist = reader["realmlist"];
                                        break;
                                    case "accountname":
                                        accountName = reader.ReadString();
                                        break;
                                    case "accountpassword":
                                        encryptedPassword = reader.ReadString();
                                        break;
                                    case "entropy":
                                        string accountPassword = GetDecryptedPassword(encryptedPassword, reader.ReadString());

                                        comboBoxItems.Items.Add(realmlist);
                                        _realmlists.Add(realmlist, new Account(accountName, accountPassword));
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            //! Has to be called after xml loading
            if (comboBoxItems.Items.Count > Settings.Default.LastSelectedIndex)
                comboBoxItems.SelectedIndex = Settings.Default.LastSelectedIndex;

            if (comboBoxItems.SelectedIndex != -1 && _realmlists.ContainsKey(comboBoxItems.Text))
            {
                textBoxAccountName.Text = _realmlists[comboBoxItems.Text].accountName;
                textBoxAccountPassword.Text = _realmlists[comboBoxItems.Text].accountPassword; //! Already decrypted
            }
            else
            {
                textBoxAccountName.Text = String.Empty;
                textBoxAccountPassword.Text = String.Empty; //! Already decrypted
            }
        }

        private void buttonSearchDirectory_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog {Filter = "Wtf files (*.wtf)|*.wtf", FileName = ""};

            if (textBoxRealmlistFile.Text != "" && Directory.Exists(textBoxRealmlistFile.Text))
                openFileDialog.InitialDirectory = textBoxRealmlistFile.Text;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                textBoxRealmlistFile.Text = openFileDialog.FileName;
        }

        private void buttonLaunchWow_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBoxRealmlistFile.Text))
            {
                MessageBox.Show("The realmlist.wtf file could not be located!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(textBoxWowFile.Text))
            {
                MessageBox.Show("The WoW.exe file could not be located!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var outputFile = new StreamWriter(textBoxRealmlistFile.Text, false))
                outputFile.WriteLine("set realmlist " + comboBoxItems.SelectedItem);

            try
            {
                var process = Process.Start(textBoxWowFile.Text);

                //! If no exception occurred, delete the cache folder.
                //! The reason this has its own try-catch block is because the logging in should not
                //! be stopped if the directory removing threw an exception.
                try
                {
                    var dirInfo = new DirectoryInfo(Path.GetDirectoryName(textBoxWowFile.Text) + @"\Cache");
                    dirInfo.Delete(true);
                }
                catch (Exception error)
                {
                    throw new Exception(error.Message);
                }

                //! Only attempt to login to the account page (and possibly character if checkbox is checked) if te
                //! acc info is actually given.
                if (String.IsNullOrWhiteSpace(textBoxAccountName.Text) && String.IsNullOrWhiteSpace(textBoxAccountPassword.Text))
                    return;

                Thread.Sleep(600);

                //! Run this code in a new thread so the main form does not freeze up.
                new Thread(() =>
                {
                    try
                    {
                        Thread.CurrentThread.IsBackground = true;

                        do
                        {
                            process.WaitForInputIdle();
                            process.Refresh();
                        }
                        while (process.MainWindowHandle.ToInt32() == 0);

                        Thread.Sleep(800);

                        foreach (var t in textBoxAccountName.Text)
                        {
                            SendMessage(process.MainWindowHandle, WM_CHAR, new IntPtr(t), IntPtr.Zero);
                            Thread.Sleep(30);
                        }

                        //! Switch to password field
                        if (!String.IsNullOrWhiteSpace(textBoxAccountPassword.Text))
                        {
                            SendMessage(process.MainWindowHandle, WM_KEYDOWN, new IntPtr(VK_TAB), IntPtr.Zero);

                            foreach (var t in textBoxAccountPassword.Text)
                            {
                                SendMessage(process.MainWindowHandle, WM_CHAR, new IntPtr(t), IntPtr.Zero);
                                Thread.Sleep(30);
                            }

                            //! Login to account
                            SendMessage(process.MainWindowHandle, WM_KEYUP, new IntPtr(VK_RETURN), IntPtr.Zero);
                            SendMessage(process.MainWindowHandle, WM_KEYDOWN, new IntPtr(VK_RETURN), IntPtr.Zero);

                            //! Login to char
                            if (checkBoxLoginToChar.Checked)
                            {
                                Thread.Sleep(1500);
                                SendMessage(process.MainWindowHandle, WM_KEYUP, new IntPtr(VK_RETURN), IntPtr.Zero);
                                SendMessage(process.MainWindowHandle, WM_KEYDOWN, new IntPtr(VK_RETURN), IntPtr.Zero);
                            }
                        }

                        Thread.CurrentThread.Abort();
                    }
                    catch
                    {
                        Thread.CurrentThread.Abort();
                    }
                }).Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.RealmlistDir = textBoxRealmlistFile.Text;
            Settings.Default.WorldOfWarcraftDir = textBoxWowFile.Text;
            Settings.Default.LastSelectedIndex = comboBoxItems.SelectedIndex;
            Settings.Default.LoginToChar = checkBoxLoginToChar.Checked;

            if (!Directory.Exists(_xmlDir))
                Directory.CreateDirectory(_xmlDir);

            var settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };

            using (var writer = XmlWriter.Create(_xmlDirFile, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("realms");

                foreach (string realmlist in _realmlists.Keys)
                {
                    var acc = _realmlists[realmlist];
                    writer.WriteStartElement("realm");
                    writer.WriteAttributeString("realmlist", realmlist);
                    writer.WriteElementString("accountname", acc.accountName);

                    //! Encrypt the password
                    var rng = new RNGCryptoServiceProvider();
                    var buffer = new byte[1024];
                    rng.GetBytes(buffer);
                    var salt = BitConverter.ToString(buffer);
                    rng.Dispose();
                    writer.WriteElementString("accountpassword", acc.accountPassword.Length == 0 ? String.Empty : acc.accountPassword.ToSecureString().EncryptString(Encoding.Unicode.GetBytes(salt)));
                    writer.WriteElementString("entropy", salt);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            Settings.Default.Save();
        }

        public string GetDecryptedPassword(string encryptedPassword, string entropy)
        {
            string password = encryptedPassword;

            if (password.Length > 150)
                password = password.DecryptString(Encoding.Unicode.GetBytes(entropy)).ToInsecureString();

            return password;
        }

        // This method isn't useless, it's run from ManangeRealmlistsForm.cs
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (comboBoxItems.SelectedIndex == -1)
            {
                MessageBox.Show("You have no item selected to remove!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _realmlists.Remove(comboBoxItems.SelectedItem.ToString());
            comboBoxItems.Items.Remove(comboBoxItems.SelectedItem);
            comboBoxItems.SelectedIndex = comboBoxItems.Items.Count > 0 ? 0 : -1;

            if (comboBoxItems.Items.Count == 0)
                comboBoxItems.Text = String.Empty;
        }

        private void buttonAddOrRemove_Click(object sender, EventArgs e)
        {
            using (var manageRealmlistsForm = new ManageRealmlistsForm())
                manageRealmlistsForm.ShowDialog(this);
        }

        private void buttonSearchWowDirectory_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog {Filter = "Exe files (*.exe)|*.exe", FileName = ""};

            if (textBoxWowFile.Text != "" && Directory.Exists(textBoxWowFile.Text))
                openFileDialog.InitialDirectory = textBoxWowFile.Text;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                textBoxWowFile.Text = openFileDialog.FileName;
        }

        private void comboBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxItems.SelectedIndex == -1)
                return;

            var selectedItem = comboBoxItems.SelectedItem.ToString();

            if (!_realmlists.ContainsKey(selectedItem))
                return;

            if (_clientSocket != null)
                _clientSocket.Close();

            SetTextOfControl(labelOnOrOff, "<connecting...>");
            labelOnOrOff.ForeColor = Color.Black;
            labelOnOrOff.Update();

            try
            {
                if (selectedItem != "127.0.0.1" && selectedItem != "localhost")
                {
                    var hostAddress = Dns.GetHostEntry(selectedItem).AddressList[0];

                    switch (hostAddress.AddressFamily)
                    {
                        case AddressFamily.InterNetwork:
                            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            break;
                        case AddressFamily.InterNetworkV6:
                            _clientSocket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                            break;
                        default:
                            return;
                    }

                    var telnetSocketAsyncEventArgs = new SocketAsyncEventArgs {RemoteEndPoint = new IPEndPoint(hostAddress, 3724)};
                    telnetSocketAsyncEventArgs.Completed += telnetSocketAsyncEventArgs_Completed;
                    _clientSocket.ConnectAsync(telnetSocketAsyncEventArgs);
                }
                else
                    //! If server is localhost, check if worldserver is running
                    SetSelectedServerState(Process.GetProcessesByName("worldserver").Length > 0 && Process.GetProcessesByName("authserver").Length > 0);

                textBoxAccountName.Text = _realmlists[selectedItem].accountName;
                textBoxAccountPassword.Text = _realmlists[selectedItem].accountPassword;
            }
            catch (Exception)
            {
                SetSelectedServerState(false);
                textBoxAccountName.Text = String.Empty;
                textBoxAccountPassword.Text = String.Empty;
            }
        }

        private void telnetSocketAsyncEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                SetSelectedServerState(e.SocketError == SocketError.Success && e.LastOperation == SocketAsyncOperation.Connect);
            }
            catch (Exception)
            {
                SetSelectedServerState(false);
            }
        }

        private void SetSelectedServerState(bool online)
        {
            SetTextOfControl(labelOnOrOff, online ? "online" : "offline");
            labelOnOrOff.ForeColor = online ? Color.Chartreuse : Color.Red;
        }

        private delegate void SetTextOfControlDelegate(Control control, string text);

        private void SetTextOfControl(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                Invoke(new SetTextOfControlDelegate(SetTextOfControl), new object[] { control, text });
                return;
            }

            control.Text = text;
        }

        private void readonlyField_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public AddRealmlistErrors AddRealmlist(string realmlist, Account account)
        {
            if (_realmlists.ContainsKey(realmlist))
                return AddRealmlistErrors.AddRealmlistErrorAlreadyAdded;

            if (String.IsNullOrWhiteSpace(realmlist))
                return AddRealmlistErrors.AddRealmlistErrorInvalidRealmlist;

            _realmlists.Add(realmlist, account);
            comboBoxItems.Items.Add(realmlist);
            comboBoxItems.SelectedIndex = comboBoxItems.Items.Count - 1; //! Also sets account info in event
            return AddRealmlistErrors.AddRealmlistErrorNone;
        }

        public ChangeRealmlistErrors ChangeRealmlist(string realmlist, Account account)
        {
            if (!_realmlists.ContainsKey(realmlist))
                return ChangeRealmlistErrors.ChangeRealmlistErrorRealmlistNotFound;

            if (_realmlists[realmlist].accountName == account.accountName && _realmlists[realmlist].accountPassword == account.accountPassword)
                return ChangeRealmlistErrors.ChangeRealmlistErrorNothingChanged;

            if (String.IsNullOrWhiteSpace(realmlist))
                return ChangeRealmlistErrors.ChangeRealmlistErrorInvalidRealmlist;

            _realmlists.Remove(realmlist);
            _realmlists.Add(realmlist, account);
            comboBoxItems.SelectedIndex = -1;
            comboBoxItems.SelectedIndex = comboBoxItems.Items.IndexOf(realmlist);
            return ChangeRealmlistErrors.ChangeRealmlistErrorNone;
        }

        public void RemoveRealmlist(string realmlist)
        {
            if (!_realmlists.ContainsKey(realmlist) || String.IsNullOrWhiteSpace(realmlist))
                return;

            _realmlists.Remove(realmlist);

            if (comboBoxItems.SelectedIndex != -1)
                comboBoxItems.Items.RemoveAt(comboBoxItems.SelectedIndex);

            if (comboBoxItems.Items.Count == 0)
            {
                comboBoxItems.Text = String.Empty;
                textBoxAccountName.Text = String.Empty;
                textBoxAccountPassword.Text = String.Empty;
            }
            else
                comboBoxItems.SelectedIndex = 0;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Realmlist-Changer @ 2014 Discover-", "About Realmlist-Changer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
