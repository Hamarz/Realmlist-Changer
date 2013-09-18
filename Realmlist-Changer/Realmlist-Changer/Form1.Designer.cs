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
            this.textBoxFileDirectory = new System.Windows.Forms.TextBox();
            this.buttonSearchDirectory = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxItems
            // 
            this.comboBoxItems.FormattingEnabled = true;
            this.comboBoxItems.Location = new System.Drawing.Point(9, 38);
            this.comboBoxItems.Name = "comboBoxItems";
            this.comboBoxItems.Size = new System.Drawing.Size(363, 21);
            this.comboBoxItems.TabIndex = 2;
            // 
            // buttonAddItem
            // 
            this.buttonAddItem.Location = new System.Drawing.Point(297, 65);
            this.buttonAddItem.Name = "buttonAddItem";
            this.buttonAddItem.Size = new System.Drawing.Size(75, 20);
            this.buttonAddItem.TabIndex = 3;
            this.buttonAddItem.Text = "Add";
            this.buttonAddItem.UseVisualStyleBackColor = true;
            this.buttonAddItem.Click += new System.EventHandler(this.buttonAddItem_Click);
            // 
            // textBoxAddItem
            // 
            this.textBoxAddItem.Location = new System.Drawing.Point(9, 65);
            this.textBoxAddItem.Name = "textBoxAddItem";
            this.textBoxAddItem.Size = new System.Drawing.Size(282, 20);
            this.textBoxAddItem.TabIndex = 4;
            // 
            // textBoxFileDirectory
            // 
            this.textBoxFileDirectory.Location = new System.Drawing.Point(9, 12);
            this.textBoxFileDirectory.Name = "textBoxFileDirectory";
            this.textBoxFileDirectory.Size = new System.Drawing.Size(337, 20);
            this.textBoxFileDirectory.TabIndex = 5;
            // 
            // buttonSearchDirectory
            // 
            this.buttonSearchDirectory.Location = new System.Drawing.Point(347, 12);
            this.buttonSearchDirectory.Name = "buttonSearchDirectory";
            this.buttonSearchDirectory.Size = new System.Drawing.Size(25, 20);
            this.buttonSearchDirectory.TabIndex = 6;
            this.buttonSearchDirectory.Text = "...";
            this.buttonSearchDirectory.UseVisualStyleBackColor = true;
            this.buttonSearchDirectory.Click += new System.EventHandler(this.buttonSearchDirectory_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(12, 91);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(80, 23);
            this.buttonUpdate.TabIndex = 7;
            this.buttonUpdate.Text = "Use Selected";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(98, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Launch WoW";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // FormRealmlistChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 120);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonSearchDirectory);
            this.Controls.Add(this.textBoxFileDirectory);
            this.Controls.Add(this.textBoxAddItem);
            this.Controls.Add(this.buttonAddItem);
            this.Controls.Add(this.comboBoxItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormRealmlistChanger";
            this.Text = "Realmlist Changer";
            this.Load += new System.EventHandler(this.FormRealmlistChanger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxItems;
        private System.Windows.Forms.Button buttonAddItem;
        private System.Windows.Forms.TextBox textBoxAddItem;
        private System.Windows.Forms.TextBox textBoxFileDirectory;
        private System.Windows.Forms.Button buttonSearchDirectory;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button button1;
    }
}

