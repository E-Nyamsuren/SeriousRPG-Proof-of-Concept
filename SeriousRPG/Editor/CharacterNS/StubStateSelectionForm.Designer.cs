namespace SeriousRPG.Editor.CharacterNS {
    partial class StubStateSelectionForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.stubStateListBox = new System.Windows.Forms.ListBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // stubStateListBox
            // 
            this.stubStateListBox.FormattingEnabled = true;
            this.stubStateListBox.Location = new System.Drawing.Point(12, 12);
            this.stubStateListBox.Name = "stubStateListBox";
            this.stubStateListBox.Size = new System.Drawing.Size(205, 342);
            this.stubStateListBox.TabIndex = 0;
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(223, 12);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(116, 25);
            this.addBtn.TabIndex = 1;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(223, 43);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(116, 25);
            this.closeBtn.TabIndex = 2;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // StubStateSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 365);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.stubStateListBox);
            this.Name = "StubStateSelectionForm";
            this.Text = "StubStateSelectionForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox stubStateListBox;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button closeBtn;
    }
}