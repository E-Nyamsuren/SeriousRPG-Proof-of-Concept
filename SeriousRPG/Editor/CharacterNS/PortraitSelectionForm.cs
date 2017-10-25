using System;
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

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.ActorNS;
using SeriousRPG.Misc;
using SeriousRPG.Model;

namespace SeriousRPG.Editor.CharacterNS {
    public partial class PortraitSelectionForm : Form {

        private CharacterWizardForm parentForm;

        public PortraitSelectionForm(CharacterWizardForm parentForm) {
            InitializeComponent();

            this.parentForm = parentForm;

            updatePortraitList();
        }

        private void updatePortraitList() {
            foreach (GenericImage image in GenericImage.GetAllInstances()) {
                if (image.Width == Actor.PORTRAIT_WIDTH && image.Height == Actor.PORTRAIT_HEIGHT) {
                    this.portraitListBox.Items.Add(new IdNamePair(image.Id, image.Name));
                }
            }
        }

        private void portraitListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.portraitListBox.SelectedIndex >= 0) {
                int portraitId = (this.portraitListBox.SelectedItem as IdNamePair).Id;

                GenericImage image = GenericImage.GetInstance(portraitId);

                this.portraitPictureBox.Image = image.Image;
            }
        }

        private void okBtn_Click(object sender, EventArgs e) {
            if (this.portraitListBox.SelectedIndex >= 0) {
                int portraitId = (this.portraitListBox.SelectedItem as IdNamePair).Id;
                this.parentForm.Portrait = GenericImage.GetInstance(portraitId);
                this.Close();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
