namespace SeriousRPG.Editor.CharacterNS {
    partial class CharacterWizardForm {
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
            this.characterListLabel = new System.Windows.Forms.Label();
            this.charListBox = new System.Windows.Forms.ListBox();
            this.addCharBtn = new System.Windows.Forms.Button();
            this.removeCharBtn = new System.Windows.Forms.Button();
            this.portraitPictureBox = new System.Windows.Forms.PictureBox();
            this.addStateBtn = new System.Windows.Forms.Button();
            this.removeStateBtn = new System.Windows.Forms.Button();
            this.charTypeLabel = new System.Windows.Forms.Label();
            this.charPortraitLabel = new System.Windows.Forms.Label();
            this.changePortraitBtn = new System.Windows.Forms.Button();
            this.idLabel = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.descrLabel = new System.Windows.Forms.Label();
            this.descrTextBox = new System.Windows.Forms.TextBox();
            this.defaultSpriteLabel = new System.Windows.Forms.Label();
            this.defaultSpriteTextBox = new System.Windows.Forms.TextBox();
            this.changeSpriteBtn = new System.Windows.Forms.Button();
            this.viewSpriteBtn = new System.Windows.Forms.Button();
            this.canCollideCheckBox = new System.Windows.Forms.CheckBox();
            this.canClickCheckBox = new System.Windows.Forms.CheckBox();
            this.motionParamGroupBox = new System.Windows.Forms.GroupBox();
            this.destYTextBox = new System.Windows.Forms.TextBox();
            this.changeDestActorBtn = new System.Windows.Forms.Button();
            this.destActorTextBox = new System.Windows.Forms.TextBox();
            this.destActorLabel = new System.Windows.Forms.Label();
            this.destXTextBox = new System.Windows.Forms.TextBox();
            this.destPointLabel = new System.Windows.Forms.Label();
            this.startSpeedTextBox = new System.Windows.Forms.TextBox();
            this.maxSpeedTextBox = new System.Windows.Forms.TextBox();
            this.startSpeedLabel = new System.Windows.Forms.Label();
            this.maxSpeedLabel = new System.Windows.Forms.Label();
            this.editActorBtn = new System.Windows.Forms.Button();
            this.saveChangesBtn = new System.Windows.Forms.Button();
            this.cancelChangesBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.setAnimeBtn = new System.Windows.Forms.Button();
            this.stateDGV = new System.Windows.Forms.DataGridView();
            this.StateId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateDescr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartStateFlag = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AnimationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.charTypeTextBox = new System.Windows.Forms.TextBox();
            this.healthLabel = new System.Windows.Forms.Label();
            this.healthTextBox = new System.Windows.Forms.TextBox();
            this.xpTextBox = new System.Windows.Forms.TextBox();
            this.xpLabel = new System.Windows.Forms.Label();
            this.levelTextBox = new System.Windows.Forms.TextBox();
            this.levelLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.portraitPictureBox)).BeginInit();
            this.motionParamGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stateDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // characterListLabel
            // 
            this.characterListLabel.AutoSize = true;
            this.characterListLabel.Location = new System.Drawing.Point(12, 9);
            this.characterListLabel.Name = "characterListLabel";
            this.characterListLabel.Size = new System.Drawing.Size(58, 13);
            this.characterListLabel.TabIndex = 0;
            this.characterListLabel.Text = "Characters";
            // 
            // charListBox
            // 
            this.charListBox.FormattingEnabled = true;
            this.charListBox.Location = new System.Drawing.Point(12, 25);
            this.charListBox.Name = "charListBox";
            this.charListBox.Size = new System.Drawing.Size(144, 407);
            this.charListBox.TabIndex = 1;
            this.charListBox.SelectedIndexChanged += new System.EventHandler(this.charListBox_SelectedIndexChanged);
            // 
            // addCharBtn
            // 
            this.addCharBtn.Location = new System.Drawing.Point(12, 438);
            this.addCharBtn.Name = "addCharBtn";
            this.addCharBtn.Size = new System.Drawing.Size(144, 22);
            this.addCharBtn.TabIndex = 2;
            this.addCharBtn.Text = "Add character";
            this.addCharBtn.UseVisualStyleBackColor = true;
            this.addCharBtn.Click += new System.EventHandler(this.addCharBtn_Click);
            // 
            // removeCharBtn
            // 
            this.removeCharBtn.Location = new System.Drawing.Point(12, 466);
            this.removeCharBtn.Name = "removeCharBtn";
            this.removeCharBtn.Size = new System.Drawing.Size(144, 22);
            this.removeCharBtn.TabIndex = 3;
            this.removeCharBtn.Text = "Remove character";
            this.removeCharBtn.UseVisualStyleBackColor = true;
            // 
            // portraitPictureBox
            // 
            this.portraitPictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.portraitPictureBox.Location = new System.Drawing.Point(162, 25);
            this.portraitPictureBox.Name = "portraitPictureBox";
            this.portraitPictureBox.Size = new System.Drawing.Size(144, 144);
            this.portraitPictureBox.TabIndex = 4;
            this.portraitPictureBox.TabStop = false;
            // 
            // addStateBtn
            // 
            this.addStateBtn.Location = new System.Drawing.Point(6, 200);
            this.addStateBtn.Name = "addStateBtn";
            this.addStateBtn.Size = new System.Drawing.Size(100, 22);
            this.addStateBtn.TabIndex = 7;
            this.addStateBtn.Text = "Add state";
            this.addStateBtn.UseVisualStyleBackColor = true;
            this.addStateBtn.Click += new System.EventHandler(this.addStateBtn_Click);
            // 
            // removeStateBtn
            // 
            this.removeStateBtn.Location = new System.Drawing.Point(112, 200);
            this.removeStateBtn.Name = "removeStateBtn";
            this.removeStateBtn.Size = new System.Drawing.Size(100, 22);
            this.removeStateBtn.TabIndex = 8;
            this.removeStateBtn.Text = "Remove state";
            this.removeStateBtn.UseVisualStyleBackColor = true;
            this.removeStateBtn.Click += new System.EventHandler(this.removeStateBtn_Click);
            // 
            // charTypeLabel
            // 
            this.charTypeLabel.AutoSize = true;
            this.charTypeLabel.Location = new System.Drawing.Point(333, 135);
            this.charTypeLabel.Name = "charTypeLabel";
            this.charTypeLabel.Size = new System.Drawing.Size(76, 13);
            this.charTypeLabel.TabIndex = 10;
            this.charTypeLabel.Text = "Character type";
            // 
            // charPortraitLabel
            // 
            this.charPortraitLabel.AutoSize = true;
            this.charPortraitLabel.Location = new System.Drawing.Point(162, 9);
            this.charPortraitLabel.Name = "charPortraitLabel";
            this.charPortraitLabel.Size = new System.Drawing.Size(88, 13);
            this.charPortraitLabel.TabIndex = 11;
            this.charPortraitLabel.Text = "Character portrait";
            // 
            // changePortraitBtn
            // 
            this.changePortraitBtn.Location = new System.Drawing.Point(162, 175);
            this.changePortraitBtn.Name = "changePortraitBtn";
            this.changePortraitBtn.Size = new System.Drawing.Size(144, 22);
            this.changePortraitBtn.TabIndex = 12;
            this.changePortraitBtn.Text = "Change portrait";
            this.changePortraitBtn.UseVisualStyleBackColor = true;
            this.changePortraitBtn.Click += new System.EventHandler(this.changePortraitBtn_Click);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(333, 57);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(16, 13);
            this.idLabel.TabIndex = 13;
            this.idLabel.Text = "Id";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(430, 54);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(124, 20);
            this.idTextBox.TabIndex = 14;
            // 
            // descrLabel
            // 
            this.descrLabel.AutoSize = true;
            this.descrLabel.Location = new System.Drawing.Point(333, 109);
            this.descrLabel.Name = "descrLabel";
            this.descrLabel.Size = new System.Drawing.Size(60, 13);
            this.descrLabel.TabIndex = 15;
            this.descrLabel.Text = "Description";
            // 
            // descrTextBox
            // 
            this.descrTextBox.Location = new System.Drawing.Point(430, 106);
            this.descrTextBox.Name = "descrTextBox";
            this.descrTextBox.Size = new System.Drawing.Size(423, 20);
            this.descrTextBox.TabIndex = 16;
            // 
            // defaultSpriteLabel
            // 
            this.defaultSpriteLabel.AutoSize = true;
            this.defaultSpriteLabel.Location = new System.Drawing.Point(333, 162);
            this.defaultSpriteLabel.Name = "defaultSpriteLabel";
            this.defaultSpriteLabel.Size = new System.Drawing.Size(69, 13);
            this.defaultSpriteLabel.TabIndex = 17;
            this.defaultSpriteLabel.Text = "Default sprite";
            // 
            // defaultSpriteTextBox
            // 
            this.defaultSpriteTextBox.Location = new System.Drawing.Point(430, 159);
            this.defaultSpriteTextBox.Name = "defaultSpriteTextBox";
            this.defaultSpriteTextBox.Size = new System.Drawing.Size(124, 20);
            this.defaultSpriteTextBox.TabIndex = 18;
            // 
            // changeSpriteBtn
            // 
            this.changeSpriteBtn.Location = new System.Drawing.Point(690, 157);
            this.changeSpriteBtn.Name = "changeSpriteBtn";
            this.changeSpriteBtn.Size = new System.Drawing.Size(124, 22);
            this.changeSpriteBtn.TabIndex = 19;
            this.changeSpriteBtn.Text = "Change sprite";
            this.changeSpriteBtn.UseVisualStyleBackColor = true;
            this.changeSpriteBtn.Click += new System.EventHandler(this.changeSpriteBtn_Click);
            // 
            // viewSpriteBtn
            // 
            this.viewSpriteBtn.Location = new System.Drawing.Point(560, 157);
            this.viewSpriteBtn.Name = "viewSpriteBtn";
            this.viewSpriteBtn.Size = new System.Drawing.Size(124, 22);
            this.viewSpriteBtn.TabIndex = 20;
            this.viewSpriteBtn.Text = "View sprite";
            this.viewSpriteBtn.UseVisualStyleBackColor = true;
            this.viewSpriteBtn.Click += new System.EventHandler(this.viewSpriteBtn_Click);
            // 
            // canCollideCheckBox
            // 
            this.canCollideCheckBox.AutoSize = true;
            this.canCollideCheckBox.Location = new System.Drawing.Point(430, 268);
            this.canCollideCheckBox.Name = "canCollideCheckBox";
            this.canCollideCheckBox.Size = new System.Drawing.Size(78, 17);
            this.canCollideCheckBox.TabIndex = 21;
            this.canCollideCheckBox.Text = "Can collide";
            this.canCollideCheckBox.UseVisualStyleBackColor = true;
            // 
            // canClickCheckBox
            // 
            this.canClickCheckBox.AutoSize = true;
            this.canClickCheckBox.Location = new System.Drawing.Point(524, 268);
            this.canClickCheckBox.Name = "canClickCheckBox";
            this.canClickCheckBox.Size = new System.Drawing.Size(97, 17);
            this.canClickCheckBox.TabIndex = 22;
            this.canClickCheckBox.Text = "Can be clicked";
            this.canClickCheckBox.UseVisualStyleBackColor = true;
            // 
            // motionParamGroupBox
            // 
            this.motionParamGroupBox.Controls.Add(this.destYTextBox);
            this.motionParamGroupBox.Controls.Add(this.changeDestActorBtn);
            this.motionParamGroupBox.Controls.Add(this.destActorTextBox);
            this.motionParamGroupBox.Controls.Add(this.destActorLabel);
            this.motionParamGroupBox.Controls.Add(this.destXTextBox);
            this.motionParamGroupBox.Controls.Add(this.destPointLabel);
            this.motionParamGroupBox.Controls.Add(this.startSpeedTextBox);
            this.motionParamGroupBox.Controls.Add(this.maxSpeedTextBox);
            this.motionParamGroupBox.Controls.Add(this.startSpeedLabel);
            this.motionParamGroupBox.Controls.Add(this.maxSpeedLabel);
            this.motionParamGroupBox.Location = new System.Drawing.Point(586, 293);
            this.motionParamGroupBox.Name = "motionParamGroupBox";
            this.motionParamGroupBox.Size = new System.Drawing.Size(267, 165);
            this.motionParamGroupBox.TabIndex = 23;
            this.motionParamGroupBox.TabStop = false;
            this.motionParamGroupBox.Text = "Motion parameters";
            // 
            // destYTextBox
            // 
            this.destYTextBox.Location = new System.Drawing.Point(200, 80);
            this.destYTextBox.Name = "destYTextBox";
            this.destYTextBox.Size = new System.Drawing.Size(54, 20);
            this.destYTextBox.TabIndex = 9;
            // 
            // changeDestActorBtn
            // 
            this.changeDestActorBtn.Location = new System.Drawing.Point(130, 132);
            this.changeDestActorBtn.Name = "changeDestActorBtn";
            this.changeDestActorBtn.Size = new System.Drawing.Size(124, 22);
            this.changeDestActorBtn.TabIndex = 8;
            this.changeDestActorBtn.Text = "Change actor";
            this.changeDestActorBtn.UseVisualStyleBackColor = true;
            this.changeDestActorBtn.Click += new System.EventHandler(this.changeDestActorBtn_Click);
            // 
            // destActorTextBox
            // 
            this.destActorTextBox.Location = new System.Drawing.Point(130, 106);
            this.destActorTextBox.Name = "destActorTextBox";
            this.destActorTextBox.Size = new System.Drawing.Size(124, 20);
            this.destActorTextBox.TabIndex = 7;
            // 
            // destActorLabel
            // 
            this.destActorLabel.AutoSize = true;
            this.destActorLabel.Location = new System.Drawing.Point(6, 109);
            this.destActorLabel.Name = "destActorLabel";
            this.destActorLabel.Size = new System.Drawing.Size(87, 13);
            this.destActorLabel.TabIndex = 6;
            this.destActorLabel.Text = "Destination actor";
            // 
            // destXTextBox
            // 
            this.destXTextBox.Location = new System.Drawing.Point(130, 80);
            this.destXTextBox.Name = "destXTextBox";
            this.destXTextBox.Size = new System.Drawing.Size(54, 20);
            this.destXTextBox.TabIndex = 5;
            // 
            // destPointLabel
            // 
            this.destPointLabel.AutoSize = true;
            this.destPointLabel.Location = new System.Drawing.Point(6, 83);
            this.destPointLabel.Name = "destPointLabel";
            this.destPointLabel.Size = new System.Drawing.Size(111, 13);
            this.destPointLabel.TabIndex = 4;
            this.destPointLabel.Text = "Destination point (x, y)";
            // 
            // startSpeedTextBox
            // 
            this.startSpeedTextBox.Location = new System.Drawing.Point(130, 54);
            this.startSpeedTextBox.Name = "startSpeedTextBox";
            this.startSpeedTextBox.Size = new System.Drawing.Size(124, 20);
            this.startSpeedTextBox.TabIndex = 3;
            // 
            // maxSpeedTextBox
            // 
            this.maxSpeedTextBox.Location = new System.Drawing.Point(130, 28);
            this.maxSpeedTextBox.Name = "maxSpeedTextBox";
            this.maxSpeedTextBox.Size = new System.Drawing.Size(124, 20);
            this.maxSpeedTextBox.TabIndex = 2;
            // 
            // startSpeedLabel
            // 
            this.startSpeedLabel.AutoSize = true;
            this.startSpeedLabel.Location = new System.Drawing.Point(6, 57);
            this.startSpeedLabel.Name = "startSpeedLabel";
            this.startSpeedLabel.Size = new System.Drawing.Size(96, 13);
            this.startSpeedLabel.TabIndex = 1;
            this.startSpeedLabel.Text = "Start speed (pixels)";
            // 
            // maxSpeedLabel
            // 
            this.maxSpeedLabel.AutoSize = true;
            this.maxSpeedLabel.Location = new System.Drawing.Point(6, 31);
            this.maxSpeedLabel.Name = "maxSpeedLabel";
            this.maxSpeedLabel.Size = new System.Drawing.Size(94, 13);
            this.maxSpeedLabel.TabIndex = 0;
            this.maxSpeedLabel.Text = "Max speed (pixels)";
            // 
            // editActorBtn
            // 
            this.editActorBtn.Location = new System.Drawing.Point(557, 12);
            this.editActorBtn.Name = "editActorBtn";
            this.editActorBtn.Size = new System.Drawing.Size(91, 22);
            this.editActorBtn.TabIndex = 24;
            this.editActorBtn.Text = "Edit actor";
            this.editActorBtn.UseVisualStyleBackColor = true;
            this.editActorBtn.Click += new System.EventHandler(this.editActorBtn_Click);
            // 
            // saveChangesBtn
            // 
            this.saveChangesBtn.Location = new System.Drawing.Point(654, 12);
            this.saveChangesBtn.Name = "saveChangesBtn";
            this.saveChangesBtn.Size = new System.Drawing.Size(91, 22);
            this.saveChangesBtn.TabIndex = 25;
            this.saveChangesBtn.Text = "Save changes";
            this.saveChangesBtn.UseVisualStyleBackColor = true;
            this.saveChangesBtn.Click += new System.EventHandler(this.saveChangesBtn_Click);
            // 
            // cancelChangesBtn
            // 
            this.cancelChangesBtn.Location = new System.Drawing.Point(751, 12);
            this.cancelChangesBtn.Name = "cancelChangesBtn";
            this.cancelChangesBtn.Size = new System.Drawing.Size(102, 22);
            this.cancelChangesBtn.TabIndex = 26;
            this.cancelChangesBtn.Text = "Cancel changes";
            this.cancelChangesBtn.UseVisualStyleBackColor = true;
            this.cancelChangesBtn.Click += new System.EventHandler(this.cancelChangesBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.setAnimeBtn);
            this.groupBox1.Controls.Add(this.stateDGV);
            this.groupBox1.Controls.Add(this.addStateBtn);
            this.groupBox1.Controls.Add(this.removeStateBtn);
            this.groupBox1.Location = new System.Drawing.Point(165, 291);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 228);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Actor states";
            // 
            // setAnimeBtn
            // 
            this.setAnimeBtn.Location = new System.Drawing.Point(218, 200);
            this.setAnimeBtn.Name = "setAnimeBtn";
            this.setAnimeBtn.Size = new System.Drawing.Size(100, 22);
            this.setAnimeBtn.TabIndex = 9;
            this.setAnimeBtn.Text = "Set animation";
            this.setAnimeBtn.UseVisualStyleBackColor = true;
            this.setAnimeBtn.Click += new System.EventHandler(this.setAnimeBtn_Click);
            // 
            // stateDGV
            // 
            this.stateDGV.AllowUserToAddRows = false;
            this.stateDGV.AllowUserToDeleteRows = false;
            this.stateDGV.AllowUserToResizeRows = false;
            this.stateDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stateDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StateId,
            this.StateDescr,
            this.StartStateFlag,
            this.AnimationId});
            this.stateDGV.Location = new System.Drawing.Point(6, 19);
            this.stateDGV.MultiSelect = false;
            this.stateDGV.Name = "stateDGV";
            this.stateDGV.RowHeadersVisible = false;
            this.stateDGV.Size = new System.Drawing.Size(403, 176);
            this.stateDGV.TabIndex = 0;
            this.stateDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.stateDGV_CellClick);
            // 
            // StateId
            // 
            this.StateId.HeaderText = "Id";
            this.StateId.Name = "StateId";
            this.StateId.ReadOnly = true;
            // 
            // StateDescr
            // 
            this.StateDescr.HeaderText = "Description";
            this.StateDescr.Name = "StateDescr";
            this.StateDescr.ReadOnly = true;
            // 
            // StartStateFlag
            // 
            this.StartStateFlag.HeaderText = "Starting state";
            this.StartStateFlag.Name = "StartStateFlag";
            this.StartStateFlag.ReadOnly = true;
            // 
            // AnimationId
            // 
            this.AnimationId.HeaderText = "Animation Id";
            this.AnimationId.Name = "AnimationId";
            this.AnimationId.ReadOnly = true;
            // 
            // charTypeTextBox
            // 
            this.charTypeTextBox.Location = new System.Drawing.Point(430, 132);
            this.charTypeTextBox.Name = "charTypeTextBox";
            this.charTypeTextBox.Size = new System.Drawing.Size(124, 20);
            this.charTypeTextBox.TabIndex = 28;
            // 
            // healthLabel
            // 
            this.healthLabel.AutoSize = true;
            this.healthLabel.Location = new System.Drawing.Point(333, 188);
            this.healthLabel.Name = "healthLabel";
            this.healthLabel.Size = new System.Drawing.Size(38, 13);
            this.healthLabel.TabIndex = 29;
            this.healthLabel.Text = "Health";
            // 
            // healthTextBox
            // 
            this.healthTextBox.Location = new System.Drawing.Point(430, 185);
            this.healthTextBox.Name = "healthTextBox";
            this.healthTextBox.Size = new System.Drawing.Size(124, 20);
            this.healthTextBox.TabIndex = 30;
            // 
            // xpTextBox
            // 
            this.xpTextBox.Location = new System.Drawing.Point(430, 211);
            this.xpTextBox.Name = "xpTextBox";
            this.xpTextBox.Size = new System.Drawing.Size(124, 20);
            this.xpTextBox.TabIndex = 31;
            // 
            // xpLabel
            // 
            this.xpLabel.AutoSize = true;
            this.xpLabel.Location = new System.Drawing.Point(333, 214);
            this.xpLabel.Name = "xpLabel";
            this.xpLabel.Size = new System.Drawing.Size(60, 13);
            this.xpLabel.TabIndex = 32;
            this.xpLabel.Text = "Experience";
            // 
            // levelTextBox
            // 
            this.levelTextBox.Location = new System.Drawing.Point(430, 237);
            this.levelTextBox.Name = "levelTextBox";
            this.levelTextBox.Size = new System.Drawing.Size(124, 20);
            this.levelTextBox.TabIndex = 33;
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(333, 240);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(33, 13);
            this.levelLabel.TabIndex = 34;
            this.levelLabel.Text = "Level";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(333, 83);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 35;
            this.nameLabel.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(430, 80);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(254, 20);
            this.nameTextBox.TabIndex = 36;
            // 
            // CharacterWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 522);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.levelTextBox);
            this.Controls.Add(this.xpLabel);
            this.Controls.Add(this.xpTextBox);
            this.Controls.Add(this.healthTextBox);
            this.Controls.Add(this.healthLabel);
            this.Controls.Add(this.charTypeTextBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelChangesBtn);
            this.Controls.Add(this.saveChangesBtn);
            this.Controls.Add(this.editActorBtn);
            this.Controls.Add(this.motionParamGroupBox);
            this.Controls.Add(this.canClickCheckBox);
            this.Controls.Add(this.canCollideCheckBox);
            this.Controls.Add(this.viewSpriteBtn);
            this.Controls.Add(this.changeSpriteBtn);
            this.Controls.Add(this.defaultSpriteTextBox);
            this.Controls.Add(this.defaultSpriteLabel);
            this.Controls.Add(this.descrTextBox);
            this.Controls.Add(this.descrLabel);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.changePortraitBtn);
            this.Controls.Add(this.charPortraitLabel);
            this.Controls.Add(this.charTypeLabel);
            this.Controls.Add(this.portraitPictureBox);
            this.Controls.Add(this.removeCharBtn);
            this.Controls.Add(this.addCharBtn);
            this.Controls.Add(this.charListBox);
            this.Controls.Add(this.characterListLabel);
            this.Name = "CharacterWizardForm";
            this.Text = "CharacterWizardForm";
            ((System.ComponentModel.ISupportInitialize)(this.portraitPictureBox)).EndInit();
            this.motionParamGroupBox.ResumeLayout(false);
            this.motionParamGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stateDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label characterListLabel;
        private System.Windows.Forms.ListBox charListBox;
        private System.Windows.Forms.Button addCharBtn;
        private System.Windows.Forms.Button removeCharBtn;
        private System.Windows.Forms.PictureBox portraitPictureBox;
        private System.Windows.Forms.Button addStateBtn;
        private System.Windows.Forms.Button removeStateBtn;
        private System.Windows.Forms.Label charTypeLabel;
        private System.Windows.Forms.Label charPortraitLabel;
        private System.Windows.Forms.Button changePortraitBtn;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label descrLabel;
        private System.Windows.Forms.TextBox descrTextBox;
        private System.Windows.Forms.Label defaultSpriteLabel;
        private System.Windows.Forms.TextBox defaultSpriteTextBox;
        private System.Windows.Forms.Button changeSpriteBtn;
        private System.Windows.Forms.Button viewSpriteBtn;
        private System.Windows.Forms.CheckBox canCollideCheckBox;
        private System.Windows.Forms.CheckBox canClickCheckBox;
        private System.Windows.Forms.GroupBox motionParamGroupBox;
        private System.Windows.Forms.Label maxSpeedLabel;
        private System.Windows.Forms.Label startSpeedLabel;
        private System.Windows.Forms.Button changeDestActorBtn;
        private System.Windows.Forms.TextBox destActorTextBox;
        private System.Windows.Forms.Label destActorLabel;
        private System.Windows.Forms.TextBox destXTextBox;
        private System.Windows.Forms.Label destPointLabel;
        private System.Windows.Forms.TextBox startSpeedTextBox;
        private System.Windows.Forms.TextBox maxSpeedTextBox;
        private System.Windows.Forms.Button editActorBtn;
        private System.Windows.Forms.Button saveChangesBtn;
        private System.Windows.Forms.Button cancelChangesBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox charTypeTextBox;
        private System.Windows.Forms.Label healthLabel;
        private System.Windows.Forms.TextBox healthTextBox;
        private System.Windows.Forms.TextBox xpTextBox;
        private System.Windows.Forms.Label xpLabel;
        private System.Windows.Forms.TextBox levelTextBox;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.DataGridView stateDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateId;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateDescr;
        private System.Windows.Forms.DataGridViewCheckBoxColumn StartStateFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnimationId;
        private System.Windows.Forms.Button setAnimeBtn;
        private System.Windows.Forms.TextBox destYTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
    }
}