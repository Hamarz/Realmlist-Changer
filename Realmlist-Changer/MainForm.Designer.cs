namespace Realmlist_Changer
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.comboBoxItems = new System.Windows.Forms.ComboBox();
            this.buttonAddItem = new System.Windows.Forms.Button();
            this.textBoxAddItem = new System.Windows.Forms.TextBox();
            this.textBoxRealmlistFile = new System.Windows.Forms.TextBox();
            this.buttonSearchDirectory = new System.Windows.Forms.Button();
            this.buttonLaunchWow = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.textBoxWowFile = new System.Windows.Forms.TextBox();
            this.buttonSearchWowDirectory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelOnOrOff = new System.Windows.Forms.Label();
            this.textBoxAccountName = new System.Windows.Forms.TextBox();
            this.textBoxAccountPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBoxItems
            // 
            this.comboBoxItems.FormattingEnabled = true;
            this.comboBoxItems.Location = new System.Drawing.Point(9, 91);
            this.comboBoxItems.Name = "comboBoxItems";
            this.comboBoxItems.Size = new System.Drawing.Size(288, 21);
            this.comboBoxItems.TabIndex = 6;
            this.comboBoxItems.SelectedIndexChanged += new System.EventHandler(this.comboBoxItems_SelectedIndexChanged);
            this.comboBoxItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxItems_KeyPress);
            // 
            // buttonAddItem
            // 
            this.buttonAddItem.Location = new System.Drawing.Point(297, 63);
            this.buttonAddItem.Name = "buttonAddItem";
            this.buttonAddItem.Size = new System.Drawing.Size(75, 22);
            this.buttonAddItem.TabIndex = 5;
            this.buttonAddItem.Text = "Add";
            this.buttonAddItem.UseVisualStyleBackColor = true;
            this.buttonAddItem.Click += new System.EventHandler(this.buttonAddItem_Click);
            // 
            // textBoxAddItem
            // 
            this.textBoxAddItem.Location = new System.Drawing.Point(9, 64);
            this.textBoxAddItem.Name = "textBoxAddItem";
            this.textBoxAddItem.Size = new System.Drawing.Size(288, 20);
            this.textBoxAddItem.TabIndex = 4;
            // 
            // textBoxRealmlistFile
            // 
            this.textBoxRealmlistFile.Location = new System.Drawing.Point(9, 12);
            this.textBoxRealmlistFile.Name = "textBoxRealmlistFile";
            this.textBoxRealmlistFile.Size = new System.Drawing.Size(338, 20);
            this.textBoxRealmlistFile.TabIndex = 0;
            // 
            // buttonSearchDirectory
            // 
            this.buttonSearchDirectory.Location = new System.Drawing.Point(347, 11);
            this.buttonSearchDirectory.Name = "buttonSearchDirectory";
            this.buttonSearchDirectory.Size = new System.Drawing.Size(25, 22);
            this.buttonSearchDirectory.TabIndex = 1;
            this.buttonSearchDirectory.Text = "...";
            this.buttonSearchDirectory.UseVisualStyleBackColor = true;
            this.buttonSearchDirectory.Click += new System.EventHandler(this.buttonSearchDirectory_Click);
            // 
            // buttonLaunchWow
            // 
            this.buttonLaunchWow.Location = new System.Drawing.Point(273, 141);
            this.buttonLaunchWow.Name = "buttonLaunchWow";
            this.buttonLaunchWow.Size = new System.Drawing.Size(99, 23);
            this.buttonLaunchWow.TabIndex = 8;
            this.buttonLaunchWow.Text = "Launch WoW!";
            this.buttonLaunchWow.UseVisualStyleBackColor = true;
            this.buttonLaunchWow.Click += new System.EventHandler(this.buttonLaunchWow_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(297, 90);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 7;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // textBoxWowFile
            // 
            this.textBoxWowFile.Location = new System.Drawing.Point(9, 38);
            this.textBoxWowFile.Name = "textBoxWowFile";
            this.textBoxWowFile.Size = new System.Drawing.Size(338, 20);
            this.textBoxWowFile.TabIndex = 2;
            // 
            // buttonSearchWowDirectory
            // 
            this.buttonSearchWowDirectory.Location = new System.Drawing.Point(347, 37);
            this.buttonSearchWowDirectory.Name = "buttonSearchWowDirectory";
            this.buttonSearchWowDirectory.Size = new System.Drawing.Size(25, 22);
            this.buttonSearchWowDirectory.TabIndex = 3;
            this.buttonSearchWowDirectory.Text = "...";
            this.buttonSearchWowDirectory.UseVisualStyleBackColor = true;
            this.buttonSearchWowDirectory.Click += new System.EventHandler(this.buttonSearchWowDirectory_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Server status is ";
            // 
            // labelOnOrOff
            // 
            this.labelOnOrOff.AutoSize = true;
            this.labelOnOrOff.ForeColor = System.Drawing.Color.Black;
            this.labelOnOrOff.Location = new System.Drawing.Point(90, 146);
            this.labelOnOrOff.Name = "labelOnOrOff";
            this.labelOnOrOff.Size = new System.Drawing.Size(63, 13);
            this.labelOnOrOff.TabIndex = 11;
            this.labelOnOrOff.Text = "<unknown>";
            // 
            // textBoxAccountName
            // 
            this.textBoxAccountName.Location = new System.Drawing.Point(9, 118);
            this.textBoxAccountName.Name = "textBoxAccountName";
            this.textBoxAccountName.Size = new System.Drawing.Size(181, 20);
            this.textBoxAccountName.TabIndex = 12;
            // 
            // textBoxAccountPassword
            // 
            this.textBoxAccountPassword.Location = new System.Drawing.Point(191, 118);
            this.textBoxAccountPassword.Name = "textBoxAccountPassword";
            this.textBoxAccountPassword.PasswordChar = '*';
            this.textBoxAccountPassword.Size = new System.Drawing.Size(181, 20);
            this.textBoxAccountPassword.TabIndex = 12;
            // 
            // FormRealmlistChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 171);
            this.Controls.Add(this.textBoxAccountPassword);
            this.Controls.Add(this.textBoxAccountName);
            this.Controls.Add(this.labelOnOrOff);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSearchWowDirectory);
            this.Controls.Add(this.textBoxWowFile);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonLaunchWow);
            this.Controls.Add(this.buttonSearchDirectory);
            this.Controls.Add(this.textBoxRealmlistFile);
            this.Controls.Add(this.textBoxAddItem);
            this.Controls.Add(this.buttonAddItem);
            this.Controls.Add(this.comboBoxItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormRealmlistChanger";
            this.Text = "Realmlist Changer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRealmlistChanger_FormClosing);
            this.Load += new System.EventHandler(this.FormRealmlistChanger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxItems;
        private System.Windows.Forms.Button buttonAddItem;
        private System.Windows.Forms.TextBox textBoxAddItem;
        private System.Windows.Forms.TextBox textBoxRealmlistFile;
        private System.Windows.Forms.Button buttonSearchDirectory;
        private System.Windows.Forms.Button buttonLaunchWow;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.TextBox textBoxWowFile;
        private System.Windows.Forms.Button buttonSearchWowDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelOnOrOff;
        private System.Windows.Forms.TextBox textBoxAccountName;
        private System.Windows.Forms.TextBox textBoxAccountPassword;
    }
}

