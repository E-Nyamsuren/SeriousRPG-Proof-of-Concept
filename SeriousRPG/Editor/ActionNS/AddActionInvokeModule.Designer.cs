namespace SeriousRPG.Editor.ActionNS {
    partial class AddActionInvokeModule {
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
            this.actionIdLabel = new System.Windows.Forms.Label();
            this.actionNameLabel = new System.Windows.Forms.Label();
            this.moduleLabel = new System.Windows.Forms.Label();
            this.actionIdTextBox = new System.Windows.Forms.TextBox();
            this.actionNameTextBox = new System.Windows.Forms.TextBox();
            this.moduleComboBox = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.actorsGroupBox = new System.Windows.Forms.GroupBox();
            this.actorDataGridView = new System.Windows.Forms.DataGridView();
            this.ActorId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActorSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.paramLabel = new System.Windows.Forms.Label();
            this.paramTextBox = new System.Windows.Forms.TextBox();
            this.actorsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.actorDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // actionIdLabel
            // 
            this.actionIdLabel.AutoSize = true;
            this.actionIdLabel.Location = new System.Drawing.Point(12, 39);
            this.actionIdLabel.Name = "actionIdLabel";
            this.actionIdLabel.Size = new System.Drawing.Size(54, 13);
            this.actionIdLabel.TabIndex = 0;
            this.actionIdLabel.Text = "Action ID:";
            // 
            // actionNameLabel
            // 
            this.actionNameLabel.AutoSize = true;
            this.actionNameLabel.Location = new System.Drawing.Point(12, 74);
            this.actionNameLabel.Name = "actionNameLabel";
            this.actionNameLabel.Size = new System.Drawing.Size(71, 13);
            this.actionNameLabel.TabIndex = 1;
            this.actionNameLabel.Text = "Action Name:";
            // 
            // moduleLabel
            // 
            this.moduleLabel.AutoSize = true;
            this.moduleLabel.Location = new System.Drawing.Point(12, 109);
            this.moduleLabel.Name = "moduleLabel";
            this.moduleLabel.Size = new System.Drawing.Size(45, 13);
            this.moduleLabel.TabIndex = 2;
            this.moduleLabel.Text = "Module:";
            // 
            // actionIdTextBox
            // 
            this.actionIdTextBox.Location = new System.Drawing.Point(99, 36);
            this.actionIdTextBox.Name = "actionIdTextBox";
            this.actionIdTextBox.Size = new System.Drawing.Size(190, 20);
            this.actionIdTextBox.TabIndex = 3;
            // 
            // actionNameTextBox
            // 
            this.actionNameTextBox.Location = new System.Drawing.Point(99, 71);
            this.actionNameTextBox.Name = "actionNameTextBox";
            this.actionNameTextBox.Size = new System.Drawing.Size(189, 20);
            this.actionNameTextBox.TabIndex = 4;
            // 
            // moduleComboBox
            // 
            this.moduleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moduleComboBox.FormattingEnabled = true;
            this.moduleComboBox.Location = new System.Drawing.Point(99, 106);
            this.moduleComboBox.Name = "moduleComboBox";
            this.moduleComboBox.Size = new System.Drawing.Size(189, 21);
            this.moduleComboBox.TabIndex = 5;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(97, 321);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(104, 29);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(232, 321);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(104, 29);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // actorsGroupBox
            // 
            this.actorsGroupBox.Controls.Add(this.actorDataGridView);
            this.actorsGroupBox.Location = new System.Drawing.Point(12, 183);
            this.actorsGroupBox.Name = "actorsGroupBox";
            this.actorsGroupBox.Size = new System.Drawing.Size(416, 123);
            this.actorsGroupBox.TabIndex = 8;
            this.actorsGroupBox.TabStop = false;
            this.actorsGroupBox.Text = "Actors";
            // 
            // actorDataGridView
            // 
            this.actorDataGridView.AllowUserToAddRows = false;
            this.actorDataGridView.AllowUserToDeleteRows = false;
            this.actorDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.actorDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ActorId,
            this.ActorName,
            this.ActorSelected});
            this.actorDataGridView.Location = new System.Drawing.Point(6, 19);
            this.actorDataGridView.Name = "actorDataGridView";
            this.actorDataGridView.Size = new System.Drawing.Size(404, 98);
            this.actorDataGridView.TabIndex = 0;
            // 
            // ActorId
            // 
            this.ActorId.HeaderText = "Id";
            this.ActorId.Name = "ActorId";
            this.ActorId.ReadOnly = true;
            // 
            // ActorName
            // 
            this.ActorName.HeaderText = "Name";
            this.ActorName.Name = "ActorName";
            this.ActorName.ReadOnly = true;
            this.ActorName.Width = 200;
            // 
            // ActorSelected
            // 
            this.ActorSelected.HeaderText = "Selected";
            this.ActorSelected.Name = "ActorSelected";
            this.ActorSelected.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ActorSelected.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ActorSelected.Width = 60;
            // 
            // paramLabel
            // 
            this.paramLabel.AutoSize = true;
            this.paramLabel.Location = new System.Drawing.Point(12, 146);
            this.paramLabel.Name = "paramLabel";
            this.paramLabel.Size = new System.Drawing.Size(63, 13);
            this.paramLabel.TabIndex = 9;
            this.paramLabel.Text = "Parameters:";
            // 
            // paramTextBox
            // 
            this.paramTextBox.Location = new System.Drawing.Point(99, 143);
            this.paramTextBox.Name = "paramTextBox";
            this.paramTextBox.Size = new System.Drawing.Size(190, 20);
            this.paramTextBox.TabIndex = 10;
            // 
            // AddActionInvokeModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 378);
            this.Controls.Add(this.paramTextBox);
            this.Controls.Add(this.paramLabel);
            this.Controls.Add(this.actorsGroupBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.moduleComboBox);
            this.Controls.Add(this.actionNameTextBox);
            this.Controls.Add(this.actionIdTextBox);
            this.Controls.Add(this.moduleLabel);
            this.Controls.Add(this.actionNameLabel);
            this.Controls.Add(this.actionIdLabel);
            this.Name = "AddActionInvokeModule";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "AddActionInvokeModule";
            this.actorsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.actorDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label actionIdLabel;
        private System.Windows.Forms.Label actionNameLabel;
        private System.Windows.Forms.Label moduleLabel;
        private System.Windows.Forms.TextBox actionIdTextBox;
        private System.Windows.Forms.TextBox actionNameTextBox;
        private System.Windows.Forms.ComboBox moduleComboBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox actorsGroupBox;
        private System.Windows.Forms.DataGridView actorDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActorId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActorName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ActorSelected;
        private System.Windows.Forms.Label paramLabel;
        private System.Windows.Forms.TextBox paramTextBox;
    }
}