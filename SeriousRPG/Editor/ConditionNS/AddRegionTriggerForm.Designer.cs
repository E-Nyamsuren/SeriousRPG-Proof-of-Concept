namespace SeriousRPG.Editor.ConditionNS {
    partial class AddRegionTriggerForm {
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.regionIdTextBox = new System.Windows.Forms.TextBox();
            this.regionNameTextBox = new System.Windows.Forms.TextBox();
            this.regionHeightTextBox = new System.Windows.Forms.TextBox();
            this.regionWidthTextBox = new System.Windows.Forms.TextBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.actorsGroupBox = new System.Windows.Forms.GroupBox();
            this.actorDataGridView = new System.Windows.Forms.DataGridView();
            this.ActorId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActorSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.actorsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.actorDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Region ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Region Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Region width:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Region height:";
            // 
            // regionIdTextBox
            // 
            this.regionIdTextBox.Location = new System.Drawing.Point(114, 36);
            this.regionIdTextBox.Name = "regionIdTextBox";
            this.regionIdTextBox.Size = new System.Drawing.Size(190, 20);
            this.regionIdTextBox.TabIndex = 4;
            // 
            // regionNameTextBox
            // 
            this.regionNameTextBox.Location = new System.Drawing.Point(114, 72);
            this.regionNameTextBox.Name = "regionNameTextBox";
            this.regionNameTextBox.Size = new System.Drawing.Size(190, 20);
            this.regionNameTextBox.TabIndex = 5;
            // 
            // regionHeightTextBox
            // 
            this.regionHeightTextBox.Location = new System.Drawing.Point(114, 108);
            this.regionHeightTextBox.Name = "regionHeightTextBox";
            this.regionHeightTextBox.Size = new System.Drawing.Size(190, 20);
            this.regionHeightTextBox.TabIndex = 6;
            // 
            // regionWidthTextBox
            // 
            this.regionWidthTextBox.Location = new System.Drawing.Point(114, 145);
            this.regionWidthTextBox.Name = "regionWidthTextBox";
            this.regionWidthTextBox.Size = new System.Drawing.Size(190, 20);
            this.regionWidthTextBox.TabIndex = 7;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(98, 312);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(104, 29);
            this.okBtn.TabIndex = 8;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(232, 312);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(104, 29);
            this.cancelBtn.TabIndex = 9;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // actorsGroupBox
            // 
            this.actorsGroupBox.Controls.Add(this.actorDataGridView);
            this.actorsGroupBox.Location = new System.Drawing.Point(12, 177);
            this.actorsGroupBox.Name = "actorsGroupBox";
            this.actorsGroupBox.Size = new System.Drawing.Size(416, 123);
            this.actorsGroupBox.TabIndex = 10;
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
            this.actorDataGridView.Size = new System.Drawing.Size(403, 94);
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
            this.ActorSelected.Width = 60;
            // 
            // AddRegionTriggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 352);
            this.Controls.Add(this.actorsGroupBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.regionWidthTextBox);
            this.Controls.Add(this.regionHeightTextBox);
            this.Controls.Add(this.regionNameTextBox);
            this.Controls.Add(this.regionIdTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddRegionTriggerForm";
            this.Text = "AddRegionTriggerForm";
            this.actorsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.actorDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox regionIdTextBox;
        private System.Windows.Forms.TextBox regionNameTextBox;
        private System.Windows.Forms.TextBox regionHeightTextBox;
        private System.Windows.Forms.TextBox regionWidthTextBox;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.GroupBox actorsGroupBox;
        private System.Windows.Forms.DataGridView actorDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActorId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActorName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ActorSelected;
    }
}