namespace SeriousRPG.Editor.RuleNS {
    partial class QuickTriggerRuleForm {
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
            this.ruleIdLabel = new System.Windows.Forms.Label();
            this.ruleIdTextBox = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.moduleLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.moduleComboBox = new System.Windows.Forms.ComboBox();
            this.paramTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ruleIdLabel
            // 
            this.ruleIdLabel.AutoSize = true;
            this.ruleIdLabel.Location = new System.Drawing.Point(12, 32);
            this.ruleIdLabel.Name = "ruleIdLabel";
            this.ruleIdLabel.Size = new System.Drawing.Size(46, 13);
            this.ruleIdLabel.TabIndex = 0;
            this.ruleIdLabel.Text = "Rule ID:";
            // 
            // ruleIdTextBox
            // 
            this.ruleIdTextBox.Location = new System.Drawing.Point(87, 29);
            this.ruleIdTextBox.Name = "ruleIdTextBox";
            this.ruleIdTextBox.Size = new System.Drawing.Size(189, 20);
            this.ruleIdTextBox.TabIndex = 1;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(12, 66);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(63, 13);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "Description:";
            // 
            // moduleLabel
            // 
            this.moduleLabel.AutoSize = true;
            this.moduleLabel.Location = new System.Drawing.Point(12, 102);
            this.moduleLabel.Name = "moduleLabel";
            this.moduleLabel.Size = new System.Drawing.Size(45, 13);
            this.moduleLabel.TabIndex = 3;
            this.moduleLabel.Text = "Module:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Parameters:";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(87, 63);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(189, 20);
            this.descriptionTextBox.TabIndex = 5;
            // 
            // moduleComboBox
            // 
            this.moduleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moduleComboBox.FormattingEnabled = true;
            this.moduleComboBox.Location = new System.Drawing.Point(87, 99);
            this.moduleComboBox.Name = "moduleComboBox";
            this.moduleComboBox.Size = new System.Drawing.Size(189, 21);
            this.moduleComboBox.TabIndex = 6;
            // 
            // paramTextBox
            // 
            this.paramTextBox.Location = new System.Drawing.Point(87, 136);
            this.paramTextBox.Name = "paramTextBox";
            this.paramTextBox.Size = new System.Drawing.Size(189, 20);
            this.paramTextBox.TabIndex = 7;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(27, 188);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(104, 29);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(157, 188);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(104, 29);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // QuickTriggerRuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 225);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.paramTextBox);
            this.Controls.Add(this.moduleComboBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.moduleLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.ruleIdTextBox);
            this.Controls.Add(this.ruleIdLabel);
            this.Name = "QuickTriggerRuleForm";
            this.Text = "QuickTriggerRuleForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ruleIdLabel;
        private System.Windows.Forms.TextBox ruleIdTextBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label moduleLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.ComboBox moduleComboBox;
        private System.Windows.Forms.TextBox paramTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}