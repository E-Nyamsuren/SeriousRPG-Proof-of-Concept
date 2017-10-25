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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SeriousRPG.Model;
using SeriousRPG.Model.DrawingNS;

namespace SeriousRPG.Editor.CharacterNS {
    public partial class SpriteSelectionForm : Form {

        private CharacterWizardForm parentForm;

        public SpriteSelectionForm(CharacterWizardForm parentForm) {
            InitializeComponent();

            this.parentForm = parentForm;

            updateSpriteList();
        }

        private void updateSpriteList() {
            foreach (GenericImage image in GenericImage.GetAllInstances()) {
                this.spriteListBox.Items.Add(new IdNamePair(image.Id, image.Name));
            }
        }

        private void spriteListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.spriteListBox.SelectedIndex >= 0) {
                int spriteId = (this.spriteListBox.SelectedItem as IdNamePair).Id;

                GenericImage image = GenericImage.GetInstance(spriteId);

                this.pictureBox.Image = image.Image;
            }
        }

        private void addBtn_Click(object sender, EventArgs e) {
            if (this.spriteListBox.SelectedIndex >= 0) {
                int spriteId = (this.spriteListBox.SelectedItem as IdNamePair).Id;
                GenericImage image = GenericImage.GetInstance(spriteId);

                this.parentForm.DefaultSprite = image;

                this.Close();
            }
        }

        private void closeBtn_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
