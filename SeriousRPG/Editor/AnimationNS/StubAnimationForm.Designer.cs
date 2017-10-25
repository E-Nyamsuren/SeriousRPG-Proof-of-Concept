namespace SeriousRPG.Editor.AnimationNS {
    partial class StubAnimationForm {
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
            this.saListBox = new System.Windows.Forms.ListBox();
            this.addAnimeBtn = new System.Windows.Forms.Button();
            this.removeAnimeBtn = new System.Windows.Forms.Button();
            this.saDescription = new System.Windows.Forms.Label();
            this.descrTextBox = new System.Windows.Forms.TextBox();
            this.canRepeatCheckBox = new System.Windows.Forms.CheckBox();
            this.delayLabel = new System.Windows.Forms.Label();
            this.delayTextBox = new System.Windows.Forms.TextBox();
            this.saPictureBox = new System.Windows.Forms.PictureBox();
            this.saListLabel = new System.Windows.Forms.Label();
            this.spriteListBox = new System.Windows.Forms.ListBox();
            this.spriteListLabel = new System.Windows.Forms.Label();
            this.addSpriteBtn = new System.Windows.Forms.Button();
            this.removeSpriteBtn = new System.Windows.Forms.Button();
            this.saveChangesBtn = new System.Windows.Forms.Button();
            this.editAnimeBtn = new System.Windows.Forms.Button();
            this.testAnimeBtn = new System.Windows.Forms.Button();
            this.downBtn = new System.Windows.Forms.Button();
            this.upBtn = new System.Windows.Forms.Button();
            this.cancelChangesBtn = new System.Windows.Forms.Button();
            this.idLabel = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.saPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // saListBox
            // 
            this.saListBox.FormattingEnabled = true;
            this.saListBox.Location = new System.Drawing.Point(12, 25);
            this.saListBox.Name = "saListBox";
            this.saListBox.Size = new System.Drawing.Size(143, 407);
            this.saListBox.TabIndex = 0;
            this.saListBox.SelectedIndexChanged += new System.EventHandler(this.saListBox_SelectedIndexChanged);
            // 
            // addAnimeBtn
            // 
            this.addAnimeBtn.Location = new System.Drawing.Point(12, 446);
            this.addAnimeBtn.Name = "addAnimeBtn";
            this.addAnimeBtn.Size = new System.Drawing.Size(143, 22);
            this.addAnimeBtn.TabIndex = 1;
            this.addAnimeBtn.Text = "Add animation";
            this.addAnimeBtn.UseVisualStyleBackColor = true;
            this.addAnimeBtn.Click += new System.EventHandler(this.addAnimeBtn_Click);
            // 
            // removeAnimeBtn
            // 
            this.removeAnimeBtn.Location = new System.Drawing.Point(12, 474);
            this.removeAnimeBtn.Name = "removeAnimeBtn";
            this.removeAnimeBtn.Size = new System.Drawing.Size(143, 22);
            this.removeAnimeBtn.TabIndex = 2;
            this.removeAnimeBtn.Text = "Remove animation";
            this.removeAnimeBtn.UseVisualStyleBackColor = true;
            this.removeAnimeBtn.Click += new System.EventHandler(this.removeAnimeBtn_Click);
            // 
            // saDescription
            // 
            this.saDescription.AutoSize = true;
            this.saDescription.Location = new System.Drawing.Point(376, 50);
            this.saDescription.Name = "saDescription";
            this.saDescription.Size = new System.Drawing.Size(60, 13);
            this.saDescription.TabIndex = 3;
            this.saDescription.Text = "Description";
            // 
            // descrTextBox
            // 
            this.descrTextBox.Location = new System.Drawing.Point(442, 47);
            this.descrTextBox.Name = "descrTextBox";
            this.descrTextBox.Size = new System.Drawing.Size(444, 20);
            this.descrTextBox.TabIndex = 5;
            // 
            // canRepeatCheckBox
            // 
            this.canRepeatCheckBox.AutoSize = true;
            this.canRepeatCheckBox.Location = new System.Drawing.Point(688, 80);
            this.canRepeatCheckBox.Name = "canRepeatCheckBox";
            this.canRepeatCheckBox.Size = new System.Drawing.Size(126, 17);
            this.canRepeatCheckBox.TabIndex = 6;
            this.canRepeatCheckBox.Text = "Animation can repeat";
            this.canRepeatCheckBox.UseVisualStyleBackColor = true;
            // 
            // delayLabel
            // 
            this.delayLabel.AutoSize = true;
            this.delayLabel.Location = new System.Drawing.Point(376, 81);
            this.delayLabel.Name = "delayLabel";
            this.delayLabel.Size = new System.Drawing.Size(133, 13);
            this.delayLabel.TabIndex = 7;
            this.delayLabel.Text = "Delay between sprites (ms)";
            // 
            // delayTextBox
            // 
            this.delayTextBox.Location = new System.Drawing.Point(515, 78);
            this.delayTextBox.Name = "delayTextBox";
            this.delayTextBox.Size = new System.Drawing.Size(134, 20);
            this.delayTextBox.TabIndex = 8;
            // 
            // saPictureBox
            // 
            this.saPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saPictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.saPictureBox.Location = new System.Drawing.Point(376, 116);
            this.saPictureBox.Name = "saPictureBox";
            this.saPictureBox.Size = new System.Drawing.Size(510, 380);
            this.saPictureBox.TabIndex = 9;
            this.saPictureBox.TabStop = false;
            // 
            // saListLabel
            // 
            this.saListLabel.AutoSize = true;
            this.saListLabel.Location = new System.Drawing.Point(12, 9);
            this.saListLabel.Name = "saListLabel";
            this.saListLabel.Size = new System.Drawing.Size(58, 13);
            this.saListLabel.TabIndex = 10;
            this.saListLabel.Text = "Animations";
            // 
            // spriteListBox
            // 
            this.spriteListBox.FormattingEnabled = true;
            this.spriteListBox.Location = new System.Drawing.Point(164, 116);
            this.spriteListBox.Name = "spriteListBox";
            this.spriteListBox.Size = new System.Drawing.Size(143, 316);
            this.spriteListBox.TabIndex = 11;
            this.spriteListBox.SelectedIndexChanged += new System.EventHandler(this.spriteListBox_SelectedIndexChanged);
            // 
            // spriteListLabel
            // 
            this.spriteListLabel.AutoSize = true;
            this.spriteListLabel.Location = new System.Drawing.Point(161, 100);
            this.spriteListLabel.Name = "spriteListLabel";
            this.spriteListLabel.Size = new System.Drawing.Size(39, 13);
            this.spriteListLabel.TabIndex = 12;
            this.spriteListLabel.Text = "Sprites";
            // 
            // addSpriteBtn
            // 
            this.addSpriteBtn.Location = new System.Drawing.Point(164, 446);
            this.addSpriteBtn.Name = "addSpriteBtn";
            this.addSpriteBtn.Size = new System.Drawing.Size(143, 22);
            this.addSpriteBtn.TabIndex = 13;
            this.addSpriteBtn.Text = "Add sprite";
            this.addSpriteBtn.UseVisualStyleBackColor = true;
            this.addSpriteBtn.Click += new System.EventHandler(this.addSpriteBtn_Click);
            // 
            // removeSpriteBtn
            // 
            this.removeSpriteBtn.Location = new System.Drawing.Point(164, 474);
            this.removeSpriteBtn.Name = "removeSpriteBtn";
            this.removeSpriteBtn.Size = new System.Drawing.Size(143, 22);
            this.removeSpriteBtn.TabIndex = 14;
            this.removeSpriteBtn.Text = "Remove sprite";
            this.removeSpriteBtn.UseVisualStyleBackColor = true;
            this.removeSpriteBtn.Click += new System.EventHandler(this.removeSpriteBtn_Click);
            // 
            // saveChangesBtn
            // 
            this.saveChangesBtn.Location = new System.Drawing.Point(691, 12);
            this.saveChangesBtn.Name = "saveChangesBtn";
            this.saveChangesBtn.Size = new System.Drawing.Size(91, 22);
            this.saveChangesBtn.TabIndex = 15;
            this.saveChangesBtn.Text = "Save changes";
            this.saveChangesBtn.UseVisualStyleBackColor = true;
            this.saveChangesBtn.Click += new System.EventHandler(this.saveChangesBtn_Click);
            // 
            // editAnimeBtn
            // 
            this.editAnimeBtn.Location = new System.Drawing.Point(594, 12);
            this.editAnimeBtn.Name = "editAnimeBtn";
            this.editAnimeBtn.Size = new System.Drawing.Size(91, 22);
            this.editAnimeBtn.TabIndex = 16;
            this.editAnimeBtn.Text = "Edit animation";
            this.editAnimeBtn.UseVisualStyleBackColor = true;
            this.editAnimeBtn.Click += new System.EventHandler(this.editAnimeBtn_Click);
            // 
            // testAnimeBtn
            // 
            this.testAnimeBtn.Location = new System.Drawing.Point(497, 12);
            this.testAnimeBtn.Name = "testAnimeBtn";
            this.testAnimeBtn.Size = new System.Drawing.Size(91, 22);
            this.testAnimeBtn.TabIndex = 17;
            this.testAnimeBtn.Text = "Test animation";
            this.testAnimeBtn.UseVisualStyleBackColor = true;
            this.testAnimeBtn.Click += new System.EventHandler(this.testAnimeBtn_Click);
            // 
            // downBtn
            // 
            this.downBtn.Location = new System.Drawing.Point(313, 146);
            this.downBtn.Name = "downBtn";
            this.downBtn.Size = new System.Drawing.Size(24, 22);
            this.downBtn.TabIndex = 18;
            this.downBtn.Text = "▼";
            this.downBtn.UseVisualStyleBackColor = true;
            this.downBtn.Click += new System.EventHandler(this.downBtn_Click);
            // 
            // upBtn
            // 
            this.upBtn.Location = new System.Drawing.Point(313, 116);
            this.upBtn.Name = "upBtn";
            this.upBtn.Size = new System.Drawing.Size(24, 24);
            this.upBtn.TabIndex = 19;
            this.upBtn.Text = "▲";
            this.upBtn.UseVisualStyleBackColor = true;
            this.upBtn.Click += new System.EventHandler(this.upBtn_Click);
            // 
            // cancelChangesBtn
            // 
            this.cancelChangesBtn.Location = new System.Drawing.Point(786, 12);
            this.cancelChangesBtn.Name = "cancelChangesBtn";
            this.cancelChangesBtn.Size = new System.Drawing.Size(100, 22);
            this.cancelChangesBtn.TabIndex = 20;
            this.cancelChangesBtn.Text = "Cancel changes";
            this.cancelChangesBtn.UseVisualStyleBackColor = true;
            this.cancelChangesBtn.Click += new System.EventHandler(this.cancelChangesBtn_Click);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(161, 50);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(16, 13);
            this.idLabel.TabIndex = 21;
            this.idLabel.Text = "Id";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(183, 47);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(124, 20);
            this.idTextBox.TabIndex = 22;
            // 
            // StubAnimationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 508);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.cancelChangesBtn);
            this.Controls.Add(this.upBtn);
            this.Controls.Add(this.downBtn);
            this.Controls.Add(this.testAnimeBtn);
            this.Controls.Add(this.editAnimeBtn);
            this.Controls.Add(this.saveChangesBtn);
            this.Controls.Add(this.removeSpriteBtn);
            this.Controls.Add(this.addSpriteBtn);
            this.Controls.Add(this.spriteListLabel);
            this.Controls.Add(this.spriteListBox);
            this.Controls.Add(this.saListLabel);
            this.Controls.Add(this.saPictureBox);
            this.Controls.Add(this.delayTextBox);
            this.Controls.Add(this.delayLabel);
            this.Controls.Add(this.canRepeatCheckBox);
            this.Controls.Add(this.descrTextBox);
            this.Controls.Add(this.saDescription);
            this.Controls.Add(this.removeAnimeBtn);
            this.Controls.Add(this.addAnimeBtn);
            this.Controls.Add(this.saListBox);
            this.Name = "StubAnimationForm";
            this.Text = "Animation wizard";
            ((System.ComponentModel.ISupportInitialize)(this.saPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox saListBox;
        private System.Windows.Forms.Button addAnimeBtn;
        private System.Windows.Forms.Button removeAnimeBtn;
        private System.Windows.Forms.Label saDescription;
        private System.Windows.Forms.TextBox descrTextBox;
        private System.Windows.Forms.CheckBox canRepeatCheckBox;
        private System.Windows.Forms.Label delayLabel;
        private System.Windows.Forms.TextBox delayTextBox;
        private System.Windows.Forms.PictureBox saPictureBox;
        private System.Windows.Forms.Label saListLabel;
        private System.Windows.Forms.ListBox spriteListBox;
        private System.Windows.Forms.Label spriteListLabel;
        private System.Windows.Forms.Button addSpriteBtn;
        private System.Windows.Forms.Button removeSpriteBtn;
        private System.Windows.Forms.Button saveChangesBtn;
        private System.Windows.Forms.Button editAnimeBtn;
        private System.Windows.Forms.Button testAnimeBtn;
        private System.Windows.Forms.Button downBtn;
        private System.Windows.Forms.Button upBtn;
        private System.Windows.Forms.Button cancelChangesBtn;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.TextBox idTextBox;
    }
}