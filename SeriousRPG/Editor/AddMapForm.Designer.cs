namespace SeriousRPG.Editor {
    partial class AddMapForm {
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
            this.mapIdLabel = new System.Windows.Forms.Label();
            this.mapNameLabel = new System.Windows.Forms.Label();
            this.mapWidthLabel = new System.Windows.Forms.Label();
            this.mapHeightLabel = new System.Windows.Forms.Label();
            this.mapIdTextBox = new System.Windows.Forms.TextBox();
            this.mapNameTextBox = new System.Windows.Forms.TextBox();
            this.mapHeightTextBox = new System.Windows.Forms.TextBox();
            this.mapWidthTextBox = new System.Windows.Forms.TextBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mapIdLabel
            // 
            this.mapIdLabel.AutoSize = true;
            this.mapIdLabel.Location = new System.Drawing.Point(45, 43);
            this.mapIdLabel.Name = "mapIdLabel";
            this.mapIdLabel.Size = new System.Drawing.Size(45, 13);
            this.mapIdLabel.TabIndex = 0;
            this.mapIdLabel.Text = "Map ID:";
            // 
            // mapNameLabel
            // 
            this.mapNameLabel.AutoSize = true;
            this.mapNameLabel.Location = new System.Drawing.Point(30, 77);
            this.mapNameLabel.Name = "mapNameLabel";
            this.mapNameLabel.Size = new System.Drawing.Size(60, 13);
            this.mapNameLabel.TabIndex = 1;
            this.mapNameLabel.Text = "Map name:";
            // 
            // mapWidthLabel
            // 
            this.mapWidthLabel.AutoSize = true;
            this.mapWidthLabel.Location = new System.Drawing.Point(31, 151);
            this.mapWidthLabel.Name = "mapWidthLabel";
            this.mapWidthLabel.Size = new System.Drawing.Size(59, 13);
            this.mapWidthLabel.TabIndex = 2;
            this.mapWidthLabel.Text = "Map width:";
            // 
            // mapHeightLabel
            // 
            this.mapHeightLabel.AutoSize = true;
            this.mapHeightLabel.Location = new System.Drawing.Point(27, 112);
            this.mapHeightLabel.Name = "mapHeightLabel";
            this.mapHeightLabel.Size = new System.Drawing.Size(63, 13);
            this.mapHeightLabel.TabIndex = 3;
            this.mapHeightLabel.Text = "Map height:";
            // 
            // mapIdTextBox
            // 
            this.mapIdTextBox.Location = new System.Drawing.Point(103, 40);
            this.mapIdTextBox.Name = "mapIdTextBox";
            this.mapIdTextBox.Size = new System.Drawing.Size(190, 20);
            this.mapIdTextBox.TabIndex = 4;
            // 
            // mapNameTextBox
            // 
            this.mapNameTextBox.Location = new System.Drawing.Point(103, 77);
            this.mapNameTextBox.Name = "mapNameTextBox";
            this.mapNameTextBox.Size = new System.Drawing.Size(190, 20);
            this.mapNameTextBox.TabIndex = 5;
            // 
            // mapHeightTextBox
            // 
            this.mapHeightTextBox.Location = new System.Drawing.Point(103, 113);
            this.mapHeightTextBox.Name = "mapHeightTextBox";
            this.mapHeightTextBox.Size = new System.Drawing.Size(189, 20);
            this.mapHeightTextBox.TabIndex = 6;
            // 
            // mapWidthTextBox
            // 
            this.mapWidthTextBox.Location = new System.Drawing.Point(103, 151);
            this.mapWidthTextBox.Name = "mapWidthTextBox";
            this.mapWidthTextBox.Size = new System.Drawing.Size(188, 20);
            this.mapWidthTextBox.TabIndex = 7;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(55, 191);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(104, 29);
            this.okBtn.TabIndex = 8;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(216, 191);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(104, 29);
            this.cancelBtn.TabIndex = 9;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // AddMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 232);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.mapWidthTextBox);
            this.Controls.Add(this.mapHeightTextBox);
            this.Controls.Add(this.mapNameTextBox);
            this.Controls.Add(this.mapIdTextBox);
            this.Controls.Add(this.mapHeightLabel);
            this.Controls.Add(this.mapWidthLabel);
            this.Controls.Add(this.mapNameLabel);
            this.Controls.Add(this.mapIdLabel);
            this.Name = "AddMapForm";
            this.Text = "AddMapForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mapIdLabel;
        private System.Windows.Forms.Label mapNameLabel;
        private System.Windows.Forms.Label mapWidthLabel;
        private System.Windows.Forms.Label mapHeightLabel;
        private System.Windows.Forms.TextBox mapIdTextBox;
        private System.Windows.Forms.TextBox mapNameTextBox;
        private System.Windows.Forms.TextBox mapHeightTextBox;
        private System.Windows.Forms.TextBox mapWidthTextBox;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}