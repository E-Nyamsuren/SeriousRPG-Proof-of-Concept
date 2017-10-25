namespace SeriousRPG.Editor.ActionNS {
    partial class ActionForm {
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
            this.actionGridView = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addActionBtn = new System.Windows.Forms.Button();
            this.removeActionBtn = new System.Windows.Forms.Button();
            this.actionTypeComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.actionGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // actionGridView
            // 
            this.actionGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.actionGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Type,
            this.Description});
            this.actionGridView.Location = new System.Drawing.Point(12, 12);
            this.actionGridView.Name = "actionGridView";
            this.actionGridView.ReadOnly = true;
            this.actionGridView.Size = new System.Drawing.Size(743, 233);
            this.actionGridView.TabIndex = 0;
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
            // addActionBtn
            // 
            this.addActionBtn.Location = new System.Drawing.Point(218, 262);
            this.addActionBtn.Name = "addActionBtn";
            this.addActionBtn.Size = new System.Drawing.Size(91, 29);
            this.addActionBtn.TabIndex = 1;
            this.addActionBtn.Text = "Add action";
            this.addActionBtn.UseVisualStyleBackColor = true;
            this.addActionBtn.Click += new System.EventHandler(this.addActionBtn_Click);
            // 
            // removeActionBtn
            // 
            this.removeActionBtn.Location = new System.Drawing.Point(664, 262);
            this.removeActionBtn.Name = "removeActionBtn";
            this.removeActionBtn.Size = new System.Drawing.Size(91, 29);
            this.removeActionBtn.TabIndex = 2;
            this.removeActionBtn.Text = "Remove action";
            this.removeActionBtn.UseVisualStyleBackColor = true;
            this.removeActionBtn.Click += new System.EventHandler(this.removeActionBtn_Click);
            // 
            // actionTypeComboBox
            // 
            this.actionTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.actionTypeComboBox.FormattingEnabled = true;
            this.actionTypeComboBox.Location = new System.Drawing.Point(12, 262);
            this.actionTypeComboBox.Name = "actionTypeComboBox";
            this.actionTypeComboBox.Size = new System.Drawing.Size(200, 21);
            this.actionTypeComboBox.TabIndex = 3;
            // 
            // ActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 304);
            this.Controls.Add(this.actionTypeComboBox);
            this.Controls.Add(this.removeActionBtn);
            this.Controls.Add(this.addActionBtn);
            this.Controls.Add(this.actionGridView);
            this.Name = "ActionForm";
            this.Text = "ActionForm";
            ((System.ComponentModel.ISupportInitialize)(this.actionGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView actionGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.Button addActionBtn;
        private System.Windows.Forms.Button removeActionBtn;
        private System.Windows.Forms.ComboBox actionTypeComboBox;
    }
}