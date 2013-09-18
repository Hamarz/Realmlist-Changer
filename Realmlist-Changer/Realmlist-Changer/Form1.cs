using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Realmlist_Changer
{
    public partial class FormRealmlistChanger : Form
    {
        public FormRealmlistChanger()
        {
            InitializeComponent();
        }

        private void FormRealmlistChanger_Load(object sender, EventArgs e)
        {

        }

        private void buttonSearchDirectory_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Wtf files (*.wtf*)|*.wtf*";
            openFileDialog.FileName = "";

            if (textBoxFileDirectory.Text != "" && Directory.Exists(textBoxFileDirectory.Text))
                openFileDialog.InitialDirectory = textBoxFileDirectory.Text;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                textBoxFileDirectory.Text = openFileDialog.FileName;
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
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
