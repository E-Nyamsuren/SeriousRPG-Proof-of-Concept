namespace SeriousRPG.Editor.RuleNS {
    partial class AddRuleForm {
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
            this.ruleDescriptionLabel = new System.Windows.Forms.Label();
            this.ruleIdTextBox = new System.Windows.Forms.TextBox();
            this.ruleDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.conditionGridView = new System.Windows.Forms.DataGridView();
            this.ConditionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConditionDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConditionSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.conditionGroupBox = new System.Windows.Forms.GroupBox();
            this.actionGroupBox = new System.Windows.Forms.GroupBox();
            this.actionGridView = new System.Windows.Forms.DataGridView();
            this.ActionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActionDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActionSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.canRepeatCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.conditionGridView)).BeginInit();
            this.conditionGroupBox.SuspendLayout();
            this.actionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.actionGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ruleIdLabel
            // 
            this.ruleIdLabel.AutoSize = true;
            this.ruleIdLabel.Location = new System.Drawing.Point(12, 19);
            this.ruleIdLabel.Name = "ruleIdLabel";
            this.ruleIdLabel.Size = new System.Drawing.Size(44, 13);
            this.ruleIdLabel.TabIndex = 0;
            this.ruleIdLabel.Text = "Rule Id:";
            // 
            // ruleDescriptionLabel
            // 
            this.ruleDescriptionLabel.AutoSize = true;
            this.ruleDescriptionLabel.Location = new System.Drawing.Point(12, 54);
            this.ruleDescriptionLabel.Name = "ruleDescriptionLabel";
            this.ruleDescriptionLabel.Size = new System.Drawing.Size(88, 13);
            this.ruleDescriptionLabel.TabIndex = 1;
            this.ruleDescriptionLabel.Text = "Rule Description:";
            // 
            // ruleIdTextBox
            // 
            this.ruleIdTextBox.Location = new System.Drawing.Point(116, 16);
            this.ruleIdTextBox.Name = "ruleIdTextBox";
            this.ruleIdTextBox.Size = new System.Drawing.Size(190, 20);
            this.ruleIdTextBox.TabIndex = 2;
            // 
            // ruleDescriptionTextBox
            // 
            this.ruleDescriptionTextBox.Location = new System.Drawing.Point(116, 51);
            this.ruleDescriptionTextBox.Name = "ruleDescriptionTextBox";
            this.ruleDescriptionTextBox.Size = new System.Drawing.Size(190, 20);
            this.ruleDescriptionTextBox.TabIndex = 3;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(190, 450);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(104, 29);
            this.okBtn.TabIndex = 4;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(329, 450);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(104, 29);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // conditionGridView
            // 
            this.conditionGridView.AllowUserToAddRows = false;
            this.conditionGridView.AllowUserToDeleteRows = false;
            this.conditionGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conditionGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ConditionId,
            this.ConditionDescription,
            this.ConditionSelected});
            this.conditionGridView.Location = new System.Drawing.Point(6, 19);
            this.conditionGridView.Name = "conditionGridView";
            this.conditionGridView.Size = new System.Drawing.Size(585, 120);
            this.conditionGridView.TabIndex = 6;
            // 
            // ConditionId
            // 
            this.ConditionId.HeaderText = "Id";
            this.ConditionId.Name = "ConditionId";
            this.ConditionId.ReadOnly = true;
            // 
            // ConditionDescription
            // 
            this.ConditionDescription.HeaderText = "Description";
            this.ConditionDescription.Name = "ConditionDescription";
            this.ConditionDescription.ReadOnly = true;
            this.ConditionDescription.Width = 380;
            // 
            // ConditionSelected
            // 
            this.ConditionSelected.HeaderText = "Selected";
            this.ConditionSelected.Name = "ConditionSelected";
            this.ConditionSelected.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ConditionSelected.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ConditionSelected.Width = 60;
            // 
            // conditionGroupBox
            // 
            this.conditionGroupBox.Controls.Add(this.conditionGridView);
            this.conditionGroupBox.Location = new System.Drawing.Point(12, 127);
            this.conditionGroupBox.Name = "conditionGroupBox";
            this.conditionGroupBox.Size = new System.Drawing.Size(598, 149);
            this.conditionGroupBox.TabIndex = 7;
            this.conditionGroupBox.TabStop = false;
            this.conditionGroupBox.Text = "Conditions";
            // 
            // actionGroupBox
            // 
            this.actionGroupBox.Controls.Add(this.actionGridView);
            this.actionGroupBox.Location = new System.Drawing.Point(12, 291);
            this.actionGroupBox.Name = "actionGroupBox";
            this.actionGroupBox.Size = new System.Drawing.Size(598, 147);
            this.actionGroupBox.TabIndex = 8;
            this.actionGroupBox.TabStop = false;
            this.actionGroupBox.Text = "Actions";
            // 
            // actionGridView
            // 
            this.actionGridView.AllowUserToAddRows = false;
            this.actionGridView.AllowUserToDeleteRows = false;
            this.actionGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.actionGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ActionId,
            this.ActionDescription,
            this.ActionSelected});
            this.actionGridView.Location = new System.Drawing.Point(7, 19);
            this.actionGridView.Name = "actionGridView";
            this.actionGridView.Size = new System.Drawing.Size(584, 120);
            this.actionGridView.TabIndex = 0;
            // 
            // ActionId
            // 
            this.ActionId.HeaderText = "Id";
            this.ActionId.Name = "ActionId";
            this.ActionId.ReadOnly = true;
            // 
            // ActionDescription
            // 
            this.ActionDescription.HeaderText = "Description";
            this.ActionDescription.Name = "ActionDescription";
            this.ActionDescription.ReadOnly = true;
            this.ActionDescription.Width = 380;
            // 
            // ActionSelected
            // 
            this.ActionSelected.HeaderText = "Selected";
            this.ActionSelected.Name = "ActionSelected";
            this.ActionSelected.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ActionSelected.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ActionSelected.Width = 60;
            // 
            // canRepeatCheckBox
            // 
            this.canRepeatCheckBox.AutoSize = true;
            this.canRepeatCheckBox.Location = new System.Drawing.Point(15, 90);
            this.canRepeatCheckBox.Name = "canRepeatCheckBox";
            this.canRepeatCheckBox.Size = new System.Drawing.Size(77, 17);
            this.canRepeatCheckBox.TabIndex = 10;
            this.canRepeatCheckBox.Text = "can repeat";
            this.canRepeatCheckBox.UseVisualStyleBackColor = true;
            // 
            // AddRuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 494);
            this.Controls.Add(this.canRepeatCheckBox);
            this.Controls.Add(this.actionGroupBox);
            this.Controls.Add(this.conditionGroupBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.ruleDescriptionTextBox);
            this.Controls.Add(this.ruleIdTextBox);
            this.Controls.Add(this.ruleDescriptionLabel);
            this.Controls.Add(this.ruleIdLabel);
            this.Name = "AddRuleForm";
            this.Text = "Add rule form";
            ((System.ComponentModel.ISupportInitialize)(this.conditionGridView)).EndInit();
            this.conditionGroupBox.ResumeLayout(false);
            this.actionGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.actionGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ruleIdLabel;
        private System.Windows.Forms.Label ruleDescriptionLabel;
        private System.Windows.Forms.TextBox ruleIdTextBox;
        private System.Windows.Forms.TextBox ruleDescriptionTextBox;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.DataGridView conditionGridView;
        private System.Windows.Forms.GroupBox conditionGroupBox;
        private System.Windows.Forms.GroupBox actionGroupBox;
        private System.Windows.Forms.DataGridView actionGridView;
        private System.Windows.Forms.CheckBox canRepeatCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConditionId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConditionDescription;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ConditionSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActionId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActionDescription;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ActionSelected;
    }
}