namespace SeriousRPG.Editor.CharacterNS {
    partial class SelectAnimationForm {
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
            this.animePictureBox = new System.Windows.Forms.PictureBox();
            this.animeListBox = new System.Windows.Forms.ListBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.animePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // animePictureBox
            // 
            this.animePictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.animePictureBox.Location = new System.Drawing.Point(161, 12);
            this.animePictureBox.Name = "animePictureBox";
            this.animePictureBox.Size = new System.Drawing.Size(510, 380);
            this.animePictureBox.TabIndex = 0;
            this.animePictureBox.TabStop = false;
            // 
            // animeListBox
            // 
            this.animeListBox.FormattingEnabled = true;
            this.animeListBox.Location = new System.Drawing.Point(12, 12);
            this.animeListBox.Name = "animeListBox";
            this.animeListBox.Size = new System.Drawing.Size(143, 407);
            this.animeListBox.TabIndex = 1;
            this.animeListBox.SelectedIndexChanged += new System.EventHandler(this.animeListBox_SelectedIndexChanged);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(379, 397);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(143, 22);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(528, 397);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(143, 22);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // SelectAnimationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 428);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.animeListBox);
            this.Controls.Add(this.animePictureBox);
            this.Name = "SelectAnimationForm";
            this.Text = "SelectAnimationForm";
            ((System.ComponentModel.ISupportInitialize)(this.animePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox animePictureBox;
        private System.Windows.Forms.ListBox animeListBox;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}