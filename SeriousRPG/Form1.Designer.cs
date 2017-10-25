namespace SeriousRPG {
    partial class TestApp {
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
            this.createNewGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createNewGame
            // 
            this.createNewGame.Location = new System.Drawing.Point(70, 60);
            this.createNewGame.Name = "createNewGame";
            this.createNewGame.Size = new System.Drawing.Size(152, 47);
            this.createNewGame.TabIndex = 0;
            this.createNewGame.Text = "Create new game";
            this.createNewGame.UseVisualStyleBackColor = true;
            this.createNewGame.Click += new System.EventHandler(this.createNewGame_Click);
            // 
            // TestApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 179);
            this.Controls.Add(this.createNewGame);
            this.Name = "TestApp";
            this.Text = "Test app";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button createNewGame;
    }
}

