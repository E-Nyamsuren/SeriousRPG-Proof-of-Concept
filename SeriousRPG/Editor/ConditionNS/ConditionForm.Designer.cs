namespace SeriousRPG.Editor.ConditionNS {
    partial class ConditionForm {
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
            this.conditionGridView = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conditionTypeComboBox = new System.Windows.Forms.ComboBox();
            this.addConditionBtn = new System.Windows.Forms.Button();
            this.removeConditionBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.conditionGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // conditionGridView
            // 
            this.conditionGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conditionGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Type,
            this.Description});
            this.conditionGridView.Location = new System.Drawing.Point(12, 12);
            this.conditionGridView.Name = "conditionGridView";
            this.conditionGridView.Size = new System.Drawing.Size(743, 233);
            this.conditionGridView.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 200;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 400;
            // 
            // conditionTypeComboBox
            // 
            this.conditionTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.conditionTypeComboBox.FormattingEnabled = true;
            this.conditionTypeComboBox.Location = new System.Drawing.Point(12, 262);
            this.conditionTypeComboBox.Name = "conditionTypeComboBox";
            this.conditionTypeComboBox.Size = new System.Drawing.Size(200, 21);
            this.conditionTypeComboBox.TabIndex = 1;
            // 
            // addConditionBtn
            // 
            this.addConditionBtn.Location = new System.Drawing.Point(218, 262);
            this.addConditionBtn.Name = "addConditionBtn";
            this.addConditionBtn.Size = new System.Drawing.Size(91, 29);
            this.addConditionBtn.TabIndex = 2;
            this.addConditionBtn.Text = "Add condition";
            this.addConditionBtn.UseVisualStyleBackColor = true;
            this.addConditionBtn.Click += new System.EventHandler(this.addConditionBtn_Click);
            // 
            // removeConditionBtn
            // 
            this.removeConditionBtn.Location = new System.Drawing.Point(651, 263);
            this.removeConditionBtn.Name = "removeConditionBtn";
            this.removeConditionBtn.Size = new System.Drawing.Size(104, 29);
            this.removeConditionBtn.TabIndex = 3;
            this.removeConditionBtn.Text = "Remove condition";
            this.removeConditionBtn.UseVisualStyleBackColor = true;
            this.removeConditionBtn.Click += new System.EventHandler(this.removeConditionBtn_Click);
            // 
            // ConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 304);
            this.Controls.Add(this.removeConditionBtn);
            this.Controls.Add(this.addConditionBtn);
            this.Controls.Add(this.conditionTypeComboBox);
            this.Controls.Add(this.conditionGridView);
            this.Name = "ConditionForm";
            this.Text = "ConditionForm";
            ((System.ComponentModel.ISupportInitialize)(this.conditionGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView conditionGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.ComboBox conditionTypeComboBox;
        private System.Windows.Forms.Button addConditionBtn;
        private System.Windows.Forms.Button removeConditionBtn;
    }
}