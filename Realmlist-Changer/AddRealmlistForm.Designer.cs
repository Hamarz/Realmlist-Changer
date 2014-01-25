namespace Realmlist_Changer
{
    partial class AddRealmlistForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddRealmlistForm));
            this.textBoxAccountPassword = new System.Windows.Forms.TextBox();
            this.textBoxAccountName = new System.Windows.Forms.TextBox();
            this.textBoxRealmlist = new System.Windows.Forms.TextBox();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
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
            // textBoxRealmlist
            // 
            this.textBoxRealmlist.Location = new System.Drawing.Point(12, 12);
            this.textBoxRealmlist.Name = "textBoxRealmlist";
            this.textBoxRealmlist.Size = new System.Drawing.Size(338, 20);
            this.textBoxRealmlist.TabIndex = 0;
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
            // AddRealmlistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 97);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.textBoxRealmlist);
            this.Controls.Add(this.textBoxAccountPassword);
            this.Controls.Add(this.textBoxAccountName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "AddRealmlistForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add a new realmlist";
            this.Load += new System.EventHandler(this.AddRealmlistForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAccountPassword;
        private System.Windows.Forms.TextBox textBoxAccountName;
        private System.Windows.Forms.TextBox textBoxRealmlist;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Button buttonCancel;
    }
}