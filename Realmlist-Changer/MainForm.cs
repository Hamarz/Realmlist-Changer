using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Realmlist_Changer.Properties;

namespace Realmlist_Changer
{
    public partial class FormRealmlistChanger : Form
    {
        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        public FormRealmlistChanger()
        {
            InitializeComponent();
        }

        private void FormRealmlistChanger_Load(object sender, EventArgs e)
        {
            string[] splitItems = Settings.Default.SavedItems.Split(';');

            for (int i = 0; i < splitItems.Count(); ++i)
                if (splitItems[i] != String.Empty && !String.IsNullOrWhiteSpace(splitItems[i]))
                    comboBoxItems.Items.Add(splitItems[i]);

            comboBoxItems.SelectedIndex = comboBoxItems.Items.Count > 0 ? 0 : -1;

            //! Set the placeholder text
            SendMessage(textBoxRealmlistFile.Handle, EM_SETCUEBANNER, 0, "Realmlist.wtf directory");
            SendMessage(textBoxWowFile.Handle, EM_SETCUEBANNER, 0, "WoW.exe directory");

            textBoxRealmlistFile.Text = Settings.Default.RealmlistDir;
            textBoxWowFile.Text = Settings.Default.WorldOfWarcraftDir;
            checkBoxLaunchWow.Checked = Settings.Default.LaunchWowSelected;
        }

        private void buttonSearchDirectory_Click(object sender, EventArgs e)
        {
        OpenFileDialogGoto:
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Wtf files (*.wtf*)|*.wtf*";
            openFileDialog.FileName = "";

            if (textBoxRealmlistFile.Text != "" && Directory.Exists(textBoxRealmlistFile.Text))
                openFileDialog.InitialDirectory = textBoxRealmlistFile.Text;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //! Shouldn't be possible, but still
                if (Path.GetExtension(openFileDialog.FileName) != ".wtf")
                {
                    MessageBox.Show("The extension of the selected file has to be '.wtf'!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto OpenFileDialogGoto;
                }

                textBoxRealmlistFile.Text = openFileDialog.FileName;
            }
        }

        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            if (textBoxAddItem.Text == String.Empty || String.IsNullOrWhiteSpace(textBoxAddItem.Text))
            {
                MessageBox.Show("The add item text field was left empty!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (string item in comboBoxItems.Items)
            {
                if (item == textBoxAddItem.Text)
                {
                    MessageBox.Show("The item already exists!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            comboBoxItems.Items.Add(textBoxAddItem.Text);
            textBoxAddItem.Text = String.Empty;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBoxRealmlistFile.Text))
            {
                MessageBox.Show("The realmlist.wtf file could not be located!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var outputFile = new StreamWriter(textBoxRealmlistFile.Text, false))
                outputFile.WriteLine("set realmlist " + comboBoxItems.SelectedItem);

            if (checkBoxLaunchWow.Checked)
            {
                if (!File.Exists(textBoxWowFile.Text))
                {
                    MessageBox.Show("The WoW.exe file could not be located!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    Process.Start(textBoxWowFile.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FormRealmlistChanger_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.SavedItems = String.Empty;

            foreach (string item in comboBoxItems.Items)
                Settings.Default.SavedItems += item + ";";

            Settings.Default.RealmlistDir = textBoxRealmlistFile.Text;
            Settings.Default.WorldOfWarcraftDir = textBoxWowFile.Text;
            Settings.Default.LaunchWowSelected = checkBoxLaunchWow.Checked;
            Settings.Default.Save();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (comboBoxItems.SelectedIndex == -1)
            {
                MessageBox.Show("You have no item selected to remove!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            comboBoxItems.Items.Remove(comboBoxItems.SelectedItem);
            comboBoxItems.SelectedIndex = comboBoxItems.Items.Count > 0 ? 0 : -1;

            if (comboBoxItems.Items.Count == 0)
                comboBoxItems.Text = String.Empty;
        }

        private void checkBoxLaunchWow_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLaunchWow.Checked && (textBoxWowFile.Text == String.Empty || String.IsNullOrWhiteSpace(textBoxWowFile.Text)))
            {
                MessageBox.Show("You have no wow directory selected to launch!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkBoxLaunchWow.Checked = false;
            }
        }

        private void buttonSearchWowDirectory_Click(object sender, EventArgs e)
        {
        OpenFileDialogGoto:
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Exe files (*.exe*)|*.exe*";
            openFileDialog.FileName = "";

            if (textBoxWowFile.Text != "" && Directory.Exists(textBoxWowFile.Text))
                openFileDialog.InitialDirectory = textBoxWowFile.Text;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //! Shouldn't be possible, but still
                if (Path.GetExtension(openFileDialog.FileName) != ".exe")
                {
                    MessageBox.Show("The extension of the selected file has to be '.exe'!", "Something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto OpenFileDialogGoto;
                }

                textBoxWowFile.Text = openFileDialog.FileName;
            }
        }

        private void comboBoxItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
