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
    public partial class AddRealmlistForm : Form
    {
        private const int EM_SETCUEBANNER = 0x1501;
        private Dictionary<string /* realmlist */, Account /* accountInfo */> realmlists = new Dictionary<string, Account>();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        public AddRealmlistForm()
        {
            InitializeComponent();
        }

        private void AddRealmlistForm_Load(object sender, EventArgs e)
        {
            realmlists = ((MainForm)Owner).Realmlists; //! Has to be called in Load event, otherwise Owner is NULL

            //! Set the placeholder text
            SendMessage(textBoxRealmlist.Handle, EM_SETCUEBANNER, 0, "Realmlist of the server");
            SendMessage(textBoxAccountName.Handle, EM_SETCUEBANNER, 0, "Account name");
            SendMessage(textBoxAccountPassword.Handle, EM_SETCUEBANNER, 0, "Account password");

            //! Focus on the continue button so the realmlist textbox placeholder text is visible
            buttonContinue.Select();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            AddRealmlistErrors error = ((MainForm)Owner).AddRealmlist(textBoxRealmlist.Text, new Account(textBoxAccountName.Text, textBoxAccountPassword.Text));

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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
