namespace Realmlist_Changer
{
    partial class FormRealmlistChanger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRealmlistChanger));
            this.comboBoxItems = new System.Windows.Forms.ComboBox();
            this.buttonAddItem = new System.Windows.Forms.Button();
            this.textBoxAddItem = new System.Windows.Forms.TextBox();
            this.textBoxRealmlistFile = new System.Windows.Forms.TextBox();
            this.buttonSearchDirectory = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.checkBoxLaunchWow = new System.Windows.Forms.CheckBox();
            this.textBoxWowFile = new System.Windows.Forms.TextBox();
            this.buttonSearchWowDirectory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelOnOrOff = new System.Windows.Forms.Label();
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
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(9, 118);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(80, 23);
            this.buttonUpdate.TabIndex = 8;
            this.buttonUpdate.Text = "Use Selected";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
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
            // checkBoxLaunchWow
            // 
            this.checkBoxLaunchWow.AutoSize = true;
            this.checkBoxLaunchWow.Location = new System.Drawing.Point(227, 122);
            this.checkBoxLaunchWow.Name = "checkBoxLaunchWow";
            this.checkBoxLaunchWow.Size = new System.Drawing.Size(145, 17);
            this.checkBoxLaunchWow.TabIndex = 9;
            this.checkBoxLaunchWow.Text = "Launch WoW afterwards";
            this.checkBoxLaunchWow.UseVisualStyleBackColor = true;
            this.checkBoxLaunchWow.CheckedChanged += new System.EventHandler(this.checkBoxLaunchWow_CheckedChanged);
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
            this.label1.Location = new System.Drawing.Point(95, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Server is ";
            // 
            // labelOnOrOff
            // 
            this.labelOnOrOff.AutoSize = true;
            this.labelOnOrOff.ForeColor = System.Drawing.Color.Black;
            this.labelOnOrOff.Location = new System.Drawing.Point(141, 123);
            this.labelOnOrOff.Name = "labelOnOrOff";
            this.labelOnOrOff.Size = new System.Drawing.Size(63, 13);
            this.labelOnOrOff.TabIndex = 11;
            this.labelOnOrOff.Text = "<unknown>";
            // 
            // FormRealmlistChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 149);
            this.Controls.Add(this.labelOnOrOff);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSearchWowDirectory);
            this.Controls.Add(this.textBoxWowFile);
            this.Controls.Add(this.checkBoxLaunchWow);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonUpdate);
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
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.CheckBox checkBoxLaunchWow;
        private System.Windows.Forms.TextBox textBoxWowFile;
        private System.Windows.Forms.Button buttonSearchWowDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelOnOrOff;
    }
}

