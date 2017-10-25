namespace SeriousRPG.Editor.AnimationNS {
    partial class SpriteSelectionForm {
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.spriteListBox = new System.Windows.Forms.ListBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox.Location = new System.Drawing.Point(222, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(510, 380);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // spriteListBox
            // 
            this.spriteListBox.FormattingEnabled = true;
            this.spriteListBox.Location = new System.Drawing.Point(12, 12);
            this.spriteListBox.Name = "spriteListBox";
            this.spriteListBox.Size = new System.Drawing.Size(204, 407);
            this.spriteListBox.TabIndex = 1;
            this.spriteListBox.SelectedIndexChanged += new System.EventHandler(this.spriteListBox_SelectedIndexChanged);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(560, 398);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(83, 23);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "Add";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(649, 398);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(83, 23);
            this.closeBtn.TabIndex = 3;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // SpriteSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 429);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.spriteListBox);
            this.Controls.Add(this.pictureBox);
            this.Name = "SpriteSelectionForm";
            this.Text = "SpriteSelectionForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ListBox spriteListBox;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button closeBtn;
    }
}