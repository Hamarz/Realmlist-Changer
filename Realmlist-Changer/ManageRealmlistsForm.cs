using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Realmlist_Changer
{
    public partial class ManageRealmlistsForm : Form
    {
        private const int EM_SETCUEBANNER = 0x1501;
        private Dictionary<string /* realmlist */, Account /* accountInfo */> realmlists = new Dictionary<string, Account>();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        public ManageRealmlistsForm()
        {
            InitializeComponent();
        }

        private void ManageRealmlistsForm_Load(object sender, EventArgs e)
        {
            realmlists = ((MainForm)Owner).Realmlists; //! Has to be called in Load event, otherwise Owner is NULL

            foreach (string realmlist in realmlists.Keys)
                comboBoxItems.Items.Add(realmlist);

            //! Set the placeholder text - sadly doesn't work for realmlists
            SendMessage(textBoxAccountName.Handle, EM_SETCUEBANNER, 0, "Account name");
            SendMessage(textBoxAccountPassword.Handle, EM_SETCUEBANNER, 0, "Account password");

            if (comboBoxItems.Items.Count > 0)
            {
                comboBoxItems.SelectedIndex = 0;
                textBoxAccountName.Text = realmlists[comboBoxItems.Text].accountName;
                textBoxAccountPassword.Text = realmlists[comboBoxItems.Text].accountPassword;
            }

            //! Focus on the realmlis combobox
            comboBoxItems.Select();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (comboBoxItems.SelectedIndex == -1)
            {
                MessageBox.Show("There is no item selected!", "Nothing selected!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ((MainForm)Owner).RemoveRealmlist(comboBoxItems.Text);
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxItems.SelectedIndex != -1)
            {
                MessageBox.Show("This realmlist already exists!", "Realmlist must be unique!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (String.IsNullOrWhiteSpace(comboBoxItems.Text) || String.IsNullOrWhiteSpace(textBoxAccountName.Text) || String.IsNullOrWhiteSpace(textBoxAccountPassword.Text))
            {
                MessageBox.Show("All fields must be filled!", "Not all fields are filled!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddRealmlistErrors error = ((MainForm)Owner).AddRealmlist(comboBoxItems.Text, new Account(textBoxAccountName.Text, textBoxAccountPassword.Text));

            switch (error)
            {
                case AddRealmlistErrors.AddRealmlistErrorAlreadyAdded:
                    MessageBox.Show("This realmlist already exists!", "Already exists!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case AddRealmlistErrors.AddRealmlistErrorInvalidRealmlist:
                    MessageBox.Show("This realmlist is incorrect!", "Incorrect realmlist!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case AddRealmlistErrors.AddRealmlistErrorInvalidAccountInfo:
                    MessageBox.Show("This account info is incorrect!", "Incorrect account info!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case AddRealmlistErrors.AddRealmlistErrorNone:
                    Close();
                    break;
            }
        }

        private void ManageRealmlistsForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxItems.SelectedIndex == -1)
            {
                MessageBox.Show("There is no item selected!", "Nothing selected!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ChangeRealmlistErrors error = ((MainForm)Owner).ChangeRealmlist(comboBoxItems.Text, new Account(textBoxAccountName.Text, textBoxAccountPassword.Text));

            switch (error)
            {
                case ChangeRealmlistErrors.ChangeRealmlistErrorRealmlistNotFound:
                    MessageBox.Show("This realmlist could not be found!", "Realmlist not found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ChangeRealmlistErrors.ChangeRealmlistErrorNothingChanged:
                    MessageBox.Show("This realmlist already had this account information!", "Nothing changed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ChangeRealmlistErrors.ChangeRealmlistErrorInvalidRealmlist:
                    MessageBox.Show("This realmlist is incorrect!", "Incorrect realmlist!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ChangeRealmlistErrors.ChangeRealmlistErrorInvalidAccountInfo:
                    MessageBox.Show("This account info is incorrect!", "Incorrect account info!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ChangeRealmlistErrors.ChangeRealmlistErrorNone:
                    Close();
                    break;
            }
        }

        private void comboBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxItems.SelectedIndex == -1)
                return;

            string selectedItem = comboBoxItems.SelectedItem.ToString();

            if (!realmlists.ContainsKey(selectedItem))
                return;

            textBoxAccountName.Text = realmlists[selectedItem].accountName;
            textBoxAccountPassword.Text = realmlists[selectedItem].accountPassword;
        }
    }
}
