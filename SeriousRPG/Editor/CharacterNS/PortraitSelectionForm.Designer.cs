namespace SeriousRPG.Editor.CharacterNS {
    partial class PortraitSelectionForm {
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
            this.portraitListBox = new System.Windows.Forms.ListBox();
            this.portraitPictureBox = new System.Windows.Forms.PictureBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.portraitPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // portraitListBox
            // 
            this.portraitListBox.FormattingEnabled = true;
            this.portraitListBox.Location = new System.Drawing.Point(12, 12);
            this.portraitListBox.Name = "portraitListBox";
            this.portraitListBox.Size = new System.Drawing.Size(204, 303);
            this.portraitListBox.TabIndex = 0;
            this.portraitListBox.SelectedIndexChanged += new System.EventHandler(this.portraitListBox_SelectedIndexChanged);
            // 
            // portraitPictureBox
            // 
            this.portraitPictureBox.Location = new System.Drawing.Point(221, 12);
            this.portraitPictureBox.Name = "portraitPictureBox";
            this.portraitPictureBox.Size = new System.Drawing.Size(144, 144);
            this.portraitPictureBox.TabIndex = 1;
            this.portraitPictureBox.TabStop = false;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(221, 265);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(144, 22);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(221, 293);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(144, 22);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // PortraitSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 327);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.portraitPictureBox);
            this.Controls.Add(this.portraitListBox);
            this.Name = "PortraitSelectionForm";
            this.Text = "PortraitSelectionForm";
            ((System.ComponentModel.ISupportInitialize)(this.portraitPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox portraitListBox;
        private System.Windows.Forms.PictureBox portraitPictureBox;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}