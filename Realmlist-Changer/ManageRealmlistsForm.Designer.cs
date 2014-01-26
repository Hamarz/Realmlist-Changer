namespace Realmlist_Changer
{
    partial class ManageRealmlistsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageRealmlistsForm));
            this.textBoxAccountPassword = new System.Windows.Forms.TextBox();
            this.textBoxAccountName = new System.Windows.Forms.TextBox();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxItems = new System.Windows.Forms.ComboBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxAccountPassword
            // 
            this.textBoxAccountPassword.Location = new System.Drawing.Point(181, 38);
            this.textBoxAccountPassword.Name = "textBoxAccountPassword";
            this.textBoxAccountPassword.PasswordChar = '*';
            this.textBoxAccountPassword.Size = new System.Drawing.Size(169, 20);
            this.textBoxAccountPassword.TabIndex = 2;
            // 
            // textBoxAccountName
            // 
            this.textBoxAccountName.Location = new System.Drawing.Point(12, 38);
            this.textBoxAccountName.Name = "textBoxAccountName";
            this.textBoxAccountName.Size = new System.Drawing.Size(169, 20);
            this.textBoxAccountName.TabIndex = 1;
            // 
            // buttonContinue
            // 
            this.buttonContinue.Location = new System.Drawing.Point(275, 64);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(75, 23);
            this.buttonContinue.TabIndex = 4;
            this.buttonContinue.Text = "Continue";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(12, 64);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxItems
            // 
            this.comboBoxItems.FormattingEnabled = true;
            this.comboBoxItems.Location = new System.Drawing.Point(12, 11);
            this.comboBoxItems.Name = "comboBoxItems";
            this.comboBoxItems.Size = new System.Drawing.Size(263, 21);
            this.comboBoxItems.TabIndex = 5;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(275, 10);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 6;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // ManageRealmlistsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 97);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.comboBoxItems);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.textBoxAccountPassword);
            this.Controls.Add(this.textBoxAccountName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "ManageRealmlistsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage realmlists";
            this.Load += new System.EventHandler(this.ManageRealmlistsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAccountPassword;
        private System.Windows.Forms.TextBox textBoxAccountName;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxItems;
        private System.Windows.Forms.Button buttonDelete;
    }
}