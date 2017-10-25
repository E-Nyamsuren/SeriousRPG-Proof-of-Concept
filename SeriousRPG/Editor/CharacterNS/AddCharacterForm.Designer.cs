/*
Copyright 2017 Enkhbold Nyamsuren (http://www.bcogs.net , http://www.bcogs.info/)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

namespace SeriousRPG.Editor.CharacterNS {
    partial class AddCharacterForm {
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
            this.idLabel = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.actorTypeLabel = new System.Windows.Forms.Label();
            this.actorTypeComboBox = new System.Windows.Forms.ComboBox();
            this.createButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.usePresetsCheckBox = new System.Windows.Forms.CheckBox();
            this.presetComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(12, 22);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(16, 13);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "Id";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(101, 19);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(183, 20);
            this.idTextBox.TabIndex = 1;
            // 
            // actorTypeLabel
            // 
            this.actorTypeLabel.AutoSize = true;
            this.actorTypeLabel.Location = new System.Drawing.Point(12, 58);
            this.actorTypeLabel.Name = "actorTypeLabel";
            this.actorTypeLabel.Size = new System.Drawing.Size(55, 13);
            this.actorTypeLabel.TabIndex = 2;
            this.actorTypeLabel.Text = "Actor type";
            // 
            // actorTypeComboBox
            // 
            this.actorTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.actorTypeComboBox.FormattingEnabled = true;
            this.actorTypeComboBox.Location = new System.Drawing.Point(101, 55);
            this.actorTypeComboBox.Name = "actorTypeComboBox";
            this.actorTypeComboBox.Size = new System.Drawing.Size(183, 21);
            this.actorTypeComboBox.TabIndex = 3;
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(30, 165);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(104, 26);
            this.createButton.TabIndex = 4;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(167, 165);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(103, 26);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // usePresetsCheckBox
            // 
            this.usePresetsCheckBox.AutoSize = true;
            this.usePresetsCheckBox.Location = new System.Drawing.Point(15, 105);
            this.usePresetsCheckBox.Name = "usePresetsCheckBox";
            this.usePresetsCheckBox.Size = new System.Drawing.Size(82, 17);
            this.usePresetsCheckBox.TabIndex = 6;
            this.usePresetsCheckBox.Text = "Use presets";
            this.usePresetsCheckBox.UseVisualStyleBackColor = true;
            this.usePresetsCheckBox.CheckedChanged += new System.EventHandler(this.usePresetsCheckBox_CheckedChanged);
            // 
            // presetComboBox
            // 
            this.presetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.presetComboBox.FormattingEnabled = true;
            this.presetComboBox.Location = new System.Drawing.Point(101, 101);
            this.presetComboBox.Name = "presetComboBox";
            this.presetComboBox.Size = new System.Drawing.Size(183, 21);
            this.presetComboBox.TabIndex = 7;
            // 
            // AddCharacterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 208);
            this.Controls.Add(this.presetComboBox);
            this.Controls.Add(this.usePresetsCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.actorTypeComboBox);
            this.Controls.Add(this.actorTypeLabel);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.idLabel);
            this.Name = "AddCharacterForm";
            this.Text = "AddCharacterForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label actorTypeLabel;
        private System.Windows.Forms.ComboBox actorTypeComboBox;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox usePresetsCheckBox;
        private System.Windows.Forms.ComboBox presetComboBox;
    }
}