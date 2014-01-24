using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Realmlist_Changer.Properties;
using System.Net;
using System.Windows.Markup;
using System.Threading;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;

namespace Realmlist_Changer
{
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
        private static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private Dictionary<string /* realmlist */, Account /* accountInfo */> realmlists = new Dictionary<string /* realmlist */, Account /* accountInfo */>();
        private string xmlDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Realmlist-Changer\";
        private string xmlDirFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Realmlist-Changer\realmlist-changer.xml";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string[] splitItems = Settings.Default.SavedItems.Split(';');

            for (int i = 0; i < splitItems.Count(); ++i)
                if (splitItems[i] != String.Empty && !String.IsNullOrWhiteSpace(splitItems[i]))
                    comboBoxItems.Items.Add(splitItems[i]);

            comboBoxItems.SelectedIndex = Settings.Default.LastSelectedIndex;

            //! Set the placeholder text
            SendMessage(textBoxRealmlistFile.Handle, EM_SETCUEBANNER, 0, "Realmlist.wtf directory");
            SendMessage(textBoxWowFile.Handle, EM_SETCUEBANNER, 0, "WoW.exe directory");
            SendMessage(textBoxAccountName.Handle, EM_SETCUEBANNER, 0, "Account name");
            SendMessage(textBoxAccountPassword.Handle, EM_SETCUEBANNER, 0, "Account password");

            textBoxRealmlistFile.Text = Settings.Default.RealmlistDir;
            textBoxWowFile.Text = Settings.Default.WorldOfWarcraftDir;
            textBoxAccountName.Text = Settings.Default.AccountName;
            textBoxAccountPassword.Text = GetPasswordFromSettings();

            if (File.Exists(xmlDirFile))
            {
                using (StringReader stringReader = new StringReader(File.ReadAllText(xmlDirFile)))
                {
                    using (XmlTextReader reader = new XmlTextReader(stringReader))
                    {
                        string realmlist = String.Empty;
                        Account account = new Account(String.Empty, String.Empty);

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
                                        account.accountName = reader.ReadString();
                                        break;
                                    case "accountpassword":
                                        account.accountPassword = reader.ReadString();

                                        realmlists.Add(realmlist, account);
                                        realmlist = String.Empty;
                                        account = new Account(String.Empty, String.Empty);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonSearchDirectory_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Wtf files (*.wtf*)|*.wtf*";
            openFileDialog.FileName = "";

            if (textBoxRealmlistFile.Text != "" && Directory.Exists(textBoxRealmlistFile.Text))
                openFileDialog.InitialDirectory = textBoxRealmlistFile.Text;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                textBoxRealmlistFile.Text = openFileDialog.FileName;
        }

        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            if (comboBoxItems.Text == String.Empty || String.IsNullOrWhiteSpace(comboBoxItems.Text))
            {
                MessageBox.Show("The add item text field was left empty!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxItems.Text.Contains(" "))
            {
                MessageBox.Show("The add item text field may not contain any whitespaces!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (string item in comboBoxItems.Items)
            {
                if (item == comboBoxItems.Text)
                {
                    MessageBox.Show("The item already exists!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            int selectedIndex = comboBoxItems.Items.Add(comboBoxItems.Text);
            comboBoxItems.SelectedIndex = selectedIndex;
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
                Process process = Process.Start(textBoxWowFile.Text);

                if (String.IsNullOrWhiteSpace(textBoxAccountName.Text) || String.IsNullOrWhiteSpace(textBoxAccountPassword.Text))
                    return;

                Thread.Sleep(300);

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

                        for (int i = 0; i < textBoxAccountName.Text.Length; i++)
                        {
                            PostMessage(process.MainWindowHandle, WM_CHAR, new IntPtr(textBoxAccountName.Text[i]), IntPtr.Zero);
                            Thread.Sleep(30);
                        }

                        //! Switch to password field
                        PostMessage(process.MainWindowHandle, WM_KEYDOWN, new IntPtr(VK_TAB), IntPtr.Zero);

                        for (int i = 0; i < textBoxAccountPassword.Text.Length; i++)
                        {
                            PostMessage(process.MainWindowHandle, WM_CHAR, new IntPtr(textBoxAccountPassword.Text[i]), IntPtr.Zero);
                            Thread.Sleep(30);
                        }

                        PostMessage(process.MainWindowHandle, WM_KEYDOWN, new IntPtr(VK_RETURN), IntPtr.Zero);
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
            Settings.Default.SavedItems = String.Empty;

            foreach (string item in comboBoxItems.Items)
                Settings.Default.SavedItems += item + ";";

            Settings.Default.RealmlistDir = textBoxRealmlistFile.Text;
            Settings.Default.WorldOfWarcraftDir = textBoxWowFile.Text;
            Settings.Default.LastSelectedIndex = comboBoxItems.SelectedIndex;
            Settings.Default.AccountName = textBoxAccountName.Text;
            Settings.Default.AccountPassword = textBoxAccountPassword.Text;

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[1024];
            rng.GetBytes(buffer);
            string salt = BitConverter.ToString(buffer);
            rng.Dispose();
            Settings.Default.Entropy = salt;
            Settings.Default.AccountPassword = textBoxAccountPassword.Text.Length == 0 ? String.Empty : textBoxAccountPassword.Text.ToSecureString().EncryptString(Encoding.Unicode.GetBytes(salt));

            if (!Directory.Exists(xmlDir))
                Directory.CreateDirectory(xmlDir);

            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };

            using (XmlWriter writer = XmlWriter.Create(xmlDirFile, settings))
            {
                //writer.Settings.OmitXmlDeclaration = true;
                //writer.Settings.Indent = true;
                //writer.Settings.NewLineOnAttributes = true;
                writer.WriteStartDocument();
                writer.WriteStartElement("realms");

                foreach (string realmlist in realmlists.Keys)
                {
                    Account acc = realmlists[realmlist];
                    writer.WriteStartElement("realm");
                    writer.WriteAttributeString("realmlist", realmlist);
                    writer.WriteElementString("accountname", acc.accountName);
                    writer.WriteElementString("accountpassword", acc.accountPassword);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            Settings.Default.Save();
        }

        public string GetPasswordFromSettings()
        {
            string password = Settings.Default.AccountPassword;

            if (password.Length > 150)
                password = password.DecryptString(Encoding.Unicode.GetBytes(Settings.Default.Entropy)).ToInsecureString();

            return password;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (comboBoxItems.SelectedIndex == -1)
            {
                MessageBox.Show("You have no item selected to remove!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            realmlists.Remove(comboBoxItems.SelectedItem.ToString());
            comboBoxItems.Items.Remove(comboBoxItems.SelectedItem);
            comboBoxItems.SelectedIndex = comboBoxItems.Items.Count > 0 ? 0 : -1;

            if (comboBoxItems.Items.Count == 0)
                comboBoxItems.Text = String.Empty;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(comboBoxItems.Text))
            {
                MessageBox.Show("The given realmlist is incorrect!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (realmlists.ContainsKey(comboBoxItems.Text))
            {
                MessageBox.Show("The given realmlist already exists!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            comboBoxItems.Items.Add(comboBoxItems.Text);
            comboBoxItems.SelectedIndex = comboBoxItems.Items.Count - 1;
            realmlists.Add(comboBoxItems.Text, new Account(textBoxAccountName.Text, textBoxAccountPassword.Text));
        }

        private void buttonSearchWowDirectory_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Exe files (*.exe*)|*.exe*";
            openFileDialog.FileName = "";

            if (textBoxWowFile.Text != "" && Directory.Exists(textBoxWowFile.Text))
                openFileDialog.InitialDirectory = textBoxWowFile.Text;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                textBoxWowFile.Text = openFileDialog.FileName;
        }

        private void comboBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBoxItems.SelectedItem.ToString();

            if (!realmlists.ContainsKey(selectedItem))
                return;

            SetTextOfControl(labelOnOrOff, "<connecting...>");
            labelOnOrOff.ForeColor = Color.Black;
            labelOnOrOff.Update();

            try
            {
                IPAddress hostAddress = Dns.GetHostEntry(selectedItem).AddressList[0];
                Socket clientSocket;

                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                else if (hostAddress.AddressFamily == AddressFamily.InterNetworkV6)
                    clientSocket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                else
                    return;

                SocketAsyncEventArgs telnetSocketAsyncEventArgs = new SocketAsyncEventArgs();
                telnetSocketAsyncEventArgs.RemoteEndPoint = new IPEndPoint(hostAddress, 3724); //! Client port is always 3724 so this is safe
                telnetSocketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(telnetSocketAsyncEventArgs_Completed);
                clientSocket.ConnectAsync(telnetSocketAsyncEventArgs);

                textBoxAccountName.Text = realmlists[selectedItem].accountName;
                textBoxAccountPassword.Text = realmlists[selectedItem].accountPassword;
            }
            catch (Exception ex)
            {
                SetSelectedServerState(false);
                MessageBox.Show(ex.Message);
            }
        }

        private void telnetSocketAsyncEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                SetSelectedServerState(e.SocketError == SocketError.Success && e.LastOperation == SocketAsyncOperation.Connect);
            }
            catch (Exception ex)
            {
                SetSelectedServerState(false);
                MessageBox.Show(ex.Message);
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
    }
}
