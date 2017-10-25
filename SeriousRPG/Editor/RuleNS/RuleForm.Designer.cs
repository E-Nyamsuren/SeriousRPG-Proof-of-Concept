namespace SeriousRPG.Editor.RuleNS {
    partial class RuleForm {
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
            this.ruleGridView = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addRuleBtn = new System.Windows.Forms.Button();
            this.editRuleBtn = new System.Windows.Forms.Button();
            this.removeRuleBtn = new System.Windows.Forms.Button();
            this.manageConditionsBtn = new System.Windows.Forms.Button();
            this.manageActionsBtn = new System.Windows.Forms.Button();
            this.condActionGroupBox = new System.Windows.Forms.GroupBox();
            this.rulesGroupBox = new System.Windows.Forms.GroupBox();
            this.quickTriggerRuleBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ruleGridView)).BeginInit();
            this.condActionGroupBox.SuspendLayout();
            this.rulesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ruleGridView
            // 
            this.ruleGridView.AllowUserToAddRows = false;
            this.ruleGridView.AllowUserToDeleteRows = false;
            this.ruleGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ruleGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Description});
            this.ruleGridView.Location = new System.Drawing.Point(6, 19);
            this.ruleGridView.Name = "ruleGridView";
            this.ruleGridView.RowHeadersVisible = false;
            this.ruleGridView.Size = new System.Drawing.Size(532, 233);
            this.ruleGridView.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 400;
            // 
            // addRuleBtn
            // 
            this.addRuleBtn.Location = new System.Drawing.Point(6, 258);
            this.addRuleBtn.Name = "addRuleBtn";
            this.addRuleBtn.Size = new System.Drawing.Size(91, 29);
            this.addRuleBtn.TabIndex = 1;
            this.addRuleBtn.Text = "Add rule";
            this.addRuleBtn.UseVisualStyleBackColor = true;
            this.addRuleBtn.Click += new System.EventHandler(this.addRuleBtn_Click);
            // 
            // editRuleBtn
            // 
            this.editRuleBtn.Location = new System.Drawing.Point(103, 258);
            this.editRuleBtn.Name = "editRuleBtn";
            this.editRuleBtn.Size = new System.Drawing.Size(91, 29);
            this.editRuleBtn.TabIndex = 2;
            this.editRuleBtn.Text = "Edit rule";
            this.editRuleBtn.UseVisualStyleBackColor = true;
            this.editRuleBtn.Click += new System.EventHandler(this.editRuleBtn_Click);
            // 
            // removeRuleBtn
            // 
            this.removeRuleBtn.Location = new System.Drawing.Point(447, 259);
            this.removeRuleBtn.Name = "removeRuleBtn";
            this.removeRuleBtn.Size = new System.Drawing.Size(91, 28);
            this.removeRuleBtn.TabIndex = 3;
            this.removeRuleBtn.Text = "Remove rule";
            this.removeRuleBtn.UseVisualStyleBackColor = true;
            this.removeRuleBtn.Click += new System.EventHandler(this.removeRuleBtn_Click);
            // 
            // manageConditionsBtn
            // 
            this.manageConditionsBtn.Location = new System.Drawing.Point(6, 21);
            this.manageConditionsBtn.Name = "manageConditionsBtn";
            this.manageConditionsBtn.Size = new System.Drawing.Size(117, 28);
            this.manageConditionsBtn.TabIndex = 4;
            this.manageConditionsBtn.Text = "Manage conditions";
            this.manageConditionsBtn.UseVisualStyleBackColor = true;
            this.manageConditionsBtn.Click += new System.EventHandler(this.manageConditionsBtn_Click);
            // 
            // manageActionsBtn
            // 
            this.manageActionsBtn.Location = new System.Drawing.Point(129, 21);
            this.manageActionsBtn.Name = "manageActionsBtn";
            this.manageActionsBtn.Size = new System.Drawing.Size(117, 28);
            this.manageActionsBtn.TabIndex = 5;
            this.manageActionsBtn.Text = "Manage actions";
            this.manageActionsBtn.UseVisualStyleBackColor = true;
            this.manageActionsBtn.Click += new System.EventHandler(this.manageActionsBtn_Click);
            // 
            // condActionGroupBox
            // 
            this.condActionGroupBox.Controls.Add(this.manageActionsBtn);
            this.condActionGroupBox.Controls.Add(this.manageConditionsBtn);
            this.condActionGroupBox.Location = new System.Drawing.Point(11, 15);
            this.condActionGroupBox.Name = "condActionGroupBox";
            this.condActionGroupBox.Size = new System.Drawing.Size(544, 63);
            this.condActionGroupBox.TabIndex = 6;
            this.condActionGroupBox.TabStop = false;
            this.condActionGroupBox.Text = "Conditions and actions";
            // 
            // rulesGroupBox
            // 
            this.rulesGroupBox.Controls.Add(this.quickTriggerRuleBtn);
            this.rulesGroupBox.Controls.Add(this.addRuleBtn);
            this.rulesGroupBox.Controls.Add(this.editRuleBtn);
            this.rulesGroupBox.Controls.Add(this.ruleGridView);
            this.rulesGroupBox.Controls.Add(this.removeRuleBtn);
            this.rulesGroupBox.Location = new System.Drawing.Point(11, 88);
            this.rulesGroupBox.Name = "rulesGroupBox";
            this.rulesGroupBox.Size = new System.Drawing.Size(544, 350);
            this.rulesGroupBox.TabIndex = 7;
            this.rulesGroupBox.TabStop = false;
            this.rulesGroupBox.Text = "Rules";
            // 
            // quickTriggerRuleBtn
            // 
            this.quickTriggerRuleBtn.Location = new System.Drawing.Point(6, 314);
            this.quickTriggerRuleBtn.Name = "quickTriggerRuleBtn";
            this.quickTriggerRuleBtn.Size = new System.Drawing.Size(153, 29);
            this.quickTriggerRuleBtn.TabIndex = 4;
            this.quickTriggerRuleBtn.Text = "Quick Trigger/Invoke Rule";
            this.quickTriggerRuleBtn.UseVisualStyleBackColor = true;
            this.quickTriggerRuleBtn.Click += new System.EventHandler(this.quickTriggerRuleBtn_Click);
            // 
            // RuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 447);
            this.Controls.Add(this.rulesGroupBox);
            this.Controls.Add(this.condActionGroupBox);
            this.Name = "RuleForm";
            this.Text = "Rule form";
            ((System.ComponentModel.ISupportInitialize)(this.ruleGridView)).EndInit();
            this.condActionGroupBox.ResumeLayout(false);
            this.rulesGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ruleGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.Button addRuleBtn;
        private System.Windows.Forms.Button editRuleBtn;
        private System.Windows.Forms.Button removeRuleBtn;
        private System.Windows.Forms.Button manageConditionsBtn;
        private System.Windows.Forms.Button manageActionsBtn;
        private System.Windows.Forms.GroupBox condActionGroupBox;
        private System.Windows.Forms.GroupBox rulesGroupBox;
        private System.Windows.Forms.Button quickTriggerRuleBtn;
    }
}