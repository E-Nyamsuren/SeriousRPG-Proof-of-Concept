namespace SeriousRPG.Editor {
    partial class MapEditorForm {
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
            this.mapListBox = new System.Windows.Forms.ListBox();
            this.selectMapLabel = new System.Windows.Forms.Label();
            this.selectLayerLabel = new System.Windows.Forms.Label();
            this.addMapBtn = new System.Windows.Forms.Button();
            this.characterWizardBtn = new System.Windows.Forms.Button();
            this.mapPanel = new System.Windows.Forms.Panel();
            this.layerComboBox = new System.Windows.Forms.ComboBox();
            this.tilePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.imagesTabPage = new System.Windows.Forms.TabPage();
            this.actorsTabPage = new System.Windows.Forms.TabPage();
            this.eventCondTab = new System.Windows.Forms.TabPage();
            this.regionTriggerListBox = new System.Windows.Forms.ListBox();
            this.Test = new System.Windows.Forms.Button();
            this.bgCheckBox = new System.Windows.Forms.CheckBox();
            this.bgoCheckBox = new System.Windows.Forms.CheckBox();
            this.fgCheckBox = new System.Windows.Forms.CheckBox();
            this.fgoCheckBox = new System.Windows.Forms.CheckBox();
            this.rmCheckBox = new System.Windows.Forms.CheckBox();
            this.llCheckBox = new System.Windows.Forms.CheckBox();
            this.ruleWizardBtn = new System.Windows.Forms.Button();
            this.animationWizardBtn = new System.Windows.Forms.Button();
            this.actorsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl.SuspendLayout();
            this.imagesTabPage.SuspendLayout();
            this.actorsTabPage.SuspendLayout();
            this.eventCondTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapListBox
            // 
            this.mapListBox.FormattingEnabled = true;
            this.mapListBox.Location = new System.Drawing.Point(12, 455);
            this.mapListBox.Name = "mapListBox";
            this.mapListBox.Size = new System.Drawing.Size(216, 95);
            this.mapListBox.TabIndex = 3;
            this.mapListBox.SelectedIndexChanged += new System.EventHandler(this.mapListBox_SelectedIndexChanged);
            // 
            // selectMapLabel
            // 
            this.selectMapLabel.AutoSize = true;
            this.selectMapLabel.Location = new System.Drawing.Point(9, 439);
            this.selectMapLabel.Name = "selectMapLabel";
            this.selectMapLabel.Size = new System.Drawing.Size(61, 13);
            this.selectMapLabel.TabIndex = 4;
            this.selectMapLabel.Text = "Select Map";
            // 
            // selectLayerLabel
            // 
            this.selectLayerLabel.AutoSize = true;
            this.selectLayerLabel.Location = new System.Drawing.Point(12, 557);
            this.selectLayerLabel.Name = "selectLayerLabel";
            this.selectLayerLabel.Size = new System.Drawing.Size(90, 13);
            this.selectLayerLabel.TabIndex = 6;
            this.selectLayerLabel.Text = "Select Map Layer";
            // 
            // addMapBtn
            // 
            this.addMapBtn.Location = new System.Drawing.Point(258, 12);
            this.addMapBtn.Name = "addMapBtn";
            this.addMapBtn.Size = new System.Drawing.Size(103, 30);
            this.addMapBtn.TabIndex = 8;
            this.addMapBtn.Text = "Add Map";
            this.addMapBtn.UseVisualStyleBackColor = true;
            this.addMapBtn.Click += new System.EventHandler(this.addMapBtn_Click);
            // 
            // characterWizardBtn
            // 
            this.characterWizardBtn.Location = new System.Drawing.Point(476, 12);
            this.characterWizardBtn.Name = "characterWizardBtn";
            this.characterWizardBtn.Size = new System.Drawing.Size(106, 30);
            this.characterWizardBtn.TabIndex = 10;
            this.characterWizardBtn.Text = "Character Wizard";
            this.characterWizardBtn.UseVisualStyleBackColor = true;
            this.characterWizardBtn.Click += new System.EventHandler(this.characterWizardBtn_Click);
            // 
            // mapPanel
            // 
            this.mapPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.mapPanel.Location = new System.Drawing.Point(258, 95);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(877, 637);
            this.mapPanel.TabIndex = 11;
            this.mapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mapPanel_Paint);
            this.mapPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapPanel_Click);
            // 
            // layerComboBox
            // 
            this.layerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerComboBox.FormattingEnabled = true;
            this.layerComboBox.Location = new System.Drawing.Point(12, 573);
            this.layerComboBox.Name = "layerComboBox";
            this.layerComboBox.Size = new System.Drawing.Size(215, 21);
            this.layerComboBox.TabIndex = 12;
            // 
            // tilePanel
            // 
            this.tilePanel.AutoScroll = true;
            this.tilePanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tilePanel.Location = new System.Drawing.Point(6, 6);
            this.tilePanel.Name = "tilePanel";
            this.tilePanel.Size = new System.Drawing.Size(196, 328);
            this.tilePanel.TabIndex = 13;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.imagesTabPage);
            this.tabControl.Controls.Add(this.actorsTabPage);
            this.tabControl.Controls.Add(this.eventCondTab);
            this.tabControl.Location = new System.Drawing.Point(12, 73);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(216, 363);
            this.tabControl.TabIndex = 13;
            // 
            // imagesTabPage
            // 
            this.imagesTabPage.Controls.Add(this.tilePanel);
            this.imagesTabPage.Location = new System.Drawing.Point(4, 22);
            this.imagesTabPage.Name = "imagesTabPage";
            this.imagesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.imagesTabPage.Size = new System.Drawing.Size(208, 337);
            this.imagesTabPage.TabIndex = 0;
            this.imagesTabPage.Text = "Images";
            this.imagesTabPage.UseVisualStyleBackColor = true;
            // 
            // actorsTabPage
            // 
            this.actorsTabPage.Controls.Add(this.actorsPanel);
            this.actorsTabPage.Location = new System.Drawing.Point(4, 22);
            this.actorsTabPage.Name = "actorsTabPage";
            this.actorsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.actorsTabPage.Size = new System.Drawing.Size(208, 337);
            this.actorsTabPage.TabIndex = 1;
            this.actorsTabPage.Text = "Actors";
            this.actorsTabPage.UseVisualStyleBackColor = true;
            // 
            // eventCondTab
            // 
            this.eventCondTab.Controls.Add(this.regionTriggerListBox);
            this.eventCondTab.Location = new System.Drawing.Point(4, 22);
            this.eventCondTab.Name = "eventCondTab";
            this.eventCondTab.Padding = new System.Windows.Forms.Padding(3);
            this.eventCondTab.Size = new System.Drawing.Size(208, 337);
            this.eventCondTab.TabIndex = 2;
            this.eventCondTab.Text = "Event Conditions";
            this.eventCondTab.UseVisualStyleBackColor = true;
            // 
            // regionTriggerListBox
            // 
            this.regionTriggerListBox.FormattingEnabled = true;
            this.regionTriggerListBox.Location = new System.Drawing.Point(0, 10);
            this.regionTriggerListBox.Name = "regionTriggerListBox";
            this.regionTriggerListBox.Size = new System.Drawing.Size(208, 316);
            this.regionTriggerListBox.TabIndex = 0;
            // 
            // Test
            // 
            this.Test.Location = new System.Drawing.Point(1032, 12);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(103, 30);
            this.Test.TabIndex = 14;
            this.Test.Text = "Test";
            this.Test.UseVisualStyleBackColor = true;
            this.Test.Click += new System.EventHandler(this.Test_Click);
            // 
            // bgCheckBox
            // 
            this.bgCheckBox.AutoSize = true;
            this.bgCheckBox.Location = new System.Drawing.Point(12, 609);
            this.bgCheckBox.Name = "bgCheckBox";
            this.bgCheckBox.Size = new System.Drawing.Size(84, 17);
            this.bgCheckBox.TabIndex = 15;
            this.bgCheckBox.Text = "Background";
            this.bgCheckBox.UseVisualStyleBackColor = true;
            this.bgCheckBox.CheckedChanged += new System.EventHandler(this.bgCheckBox_CheckedChanged);
            // 
            // bgoCheckBox
            // 
            this.bgoCheckBox.AutoSize = true;
            this.bgoCheckBox.Location = new System.Drawing.Point(97, 609);
            this.bgoCheckBox.Name = "bgoCheckBox";
            this.bgoCheckBox.Size = new System.Drawing.Size(121, 17);
            this.bgoCheckBox.TabIndex = 16;
            this.bgoCheckBox.Text = "Background overlay";
            this.bgoCheckBox.UseVisualStyleBackColor = true;
            this.bgoCheckBox.CheckedChanged += new System.EventHandler(this.bgoCheckBox_CheckedChanged);
            // 
            // fgCheckBox
            // 
            this.fgCheckBox.AutoSize = true;
            this.fgCheckBox.Location = new System.Drawing.Point(11, 632);
            this.fgCheckBox.Name = "fgCheckBox";
            this.fgCheckBox.Size = new System.Drawing.Size(80, 17);
            this.fgCheckBox.TabIndex = 17;
            this.fgCheckBox.Text = "Foreground";
            this.fgCheckBox.UseVisualStyleBackColor = true;
            this.fgCheckBox.CheckedChanged += new System.EventHandler(this.fgCheckBox_CheckedChanged);
            // 
            // fgoCheckBox
            // 
            this.fgoCheckBox.AutoSize = true;
            this.fgoCheckBox.Location = new System.Drawing.Point(97, 632);
            this.fgoCheckBox.Name = "fgoCheckBox";
            this.fgoCheckBox.Size = new System.Drawing.Size(117, 17);
            this.fgoCheckBox.TabIndex = 18;
            this.fgoCheckBox.Text = "Foreground overlay";
            this.fgoCheckBox.UseVisualStyleBackColor = true;
            this.fgoCheckBox.CheckedChanged += new System.EventHandler(this.fgoCheckBox_CheckedChanged);
            // 
            // rmCheckBox
            // 
            this.rmCheckBox.AutoSize = true;
            this.rmCheckBox.Location = new System.Drawing.Point(11, 655);
            this.rmCheckBox.Name = "rmCheckBox";
            this.rmCheckBox.Size = new System.Drawing.Size(79, 17);
            this.rmCheckBox.TabIndex = 19;
            this.rmCheckBox.Text = "Route Map";
            this.rmCheckBox.UseVisualStyleBackColor = true;
            this.rmCheckBox.CheckedChanged += new System.EventHandler(this.rmCheckBox_CheckedChanged);
            // 
            // llCheckBox
            // 
            this.llCheckBox.AutoSize = true;
            this.llCheckBox.Location = new System.Drawing.Point(97, 655);
            this.llCheckBox.Name = "llCheckBox";
            this.llCheckBox.Size = new System.Drawing.Size(77, 17);
            this.llCheckBox.TabIndex = 20;
            this.llCheckBox.Text = "Logic layer";
            this.llCheckBox.UseVisualStyleBackColor = true;
            this.llCheckBox.CheckedChanged += new System.EventHandler(this.llCheckBox_CheckedChanged);
            // 
            // ruleWizardBtn
            // 
            this.ruleWizardBtn.Location = new System.Drawing.Point(367, 12);
            this.ruleWizardBtn.Name = "ruleWizardBtn";
            this.ruleWizardBtn.Size = new System.Drawing.Size(103, 30);
            this.ruleWizardBtn.TabIndex = 22;
            this.ruleWizardBtn.Text = "Rule Wizard";
            this.ruleWizardBtn.UseVisualStyleBackColor = true;
            this.ruleWizardBtn.Click += new System.EventHandler(this.ruleWizardBtn_Click);
            // 
            // animationWizardBtn
            // 
            this.animationWizardBtn.Location = new System.Drawing.Point(588, 12);
            this.animationWizardBtn.Name = "animationWizardBtn";
            this.animationWizardBtn.Size = new System.Drawing.Size(103, 30);
            this.animationWizardBtn.TabIndex = 24;
            this.animationWizardBtn.Text = "Animation Wizard";
            this.animationWizardBtn.UseVisualStyleBackColor = true;
            this.animationWizardBtn.Click += new System.EventHandler(this.animationWizardBtn_Click);
            // 
            // actorsPanel
            // 
            this.actorsPanel.AutoSize = true;
            this.actorsPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.actorsPanel.Location = new System.Drawing.Point(6, 6);
            this.actorsPanel.Name = "actorsPanel";
            this.actorsPanel.Size = new System.Drawing.Size(196, 325);
            this.actorsPanel.TabIndex = 0;
            // 
            // MapEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 744);
            this.Controls.Add(this.animationWizardBtn);
            this.Controls.Add(this.ruleWizardBtn);
            this.Controls.Add(this.llCheckBox);
            this.Controls.Add(this.rmCheckBox);
            this.Controls.Add(this.fgoCheckBox);
            this.Controls.Add(this.fgCheckBox);
            this.Controls.Add(this.bgoCheckBox);
            this.Controls.Add(this.bgCheckBox);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.layerComboBox);
            this.Controls.Add(this.mapPanel);
            this.Controls.Add(this.characterWizardBtn);
            this.Controls.Add(this.addMapBtn);
            this.Controls.Add(this.selectLayerLabel);
            this.Controls.Add(this.selectMapLabel);
            this.Controls.Add(this.mapListBox);
            this.Name = "MapEditorForm";
            this.Text = "Map Editor";
            this.tabControl.ResumeLayout(false);
            this.imagesTabPage.ResumeLayout(false);
            this.actorsTabPage.ResumeLayout(false);
            this.actorsTabPage.PerformLayout();
            this.eventCondTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox mapListBox;
        private System.Windows.Forms.Label selectMapLabel;
        private System.Windows.Forms.Label selectLayerLabel;
        private System.Windows.Forms.Button addMapBtn;
        private System.Windows.Forms.Button characterWizardBtn;
        private System.Windows.Forms.Panel mapPanel;
        private System.Windows.Forms.ComboBox layerComboBox;
        private System.Windows.Forms.FlowLayoutPanel tilePanel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage imagesTabPage;
        private System.Windows.Forms.TabPage actorsTabPage;
        private System.Windows.Forms.Button Test;
        private System.Windows.Forms.CheckBox bgCheckBox;
        private System.Windows.Forms.CheckBox bgoCheckBox;
        private System.Windows.Forms.CheckBox fgCheckBox;
        private System.Windows.Forms.CheckBox fgoCheckBox;
        private System.Windows.Forms.CheckBox rmCheckBox;
        private System.Windows.Forms.TabPage eventCondTab;
        private System.Windows.Forms.CheckBox llCheckBox;
        private System.Windows.Forms.ListBox regionTriggerListBox;
        private System.Windows.Forms.Button ruleWizardBtn;
        private System.Windows.Forms.Button animationWizardBtn;
        private System.Windows.Forms.FlowLayoutPanel actorsPanel;
    }
}