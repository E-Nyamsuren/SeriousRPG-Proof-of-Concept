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
using SeriousRPG.Model.AnimationNS;

namespace SeriousRPG.Editor.AnimationNS {
    public partial class StubAnimationForm : Form {

        public StubAnimationForm() {
            InitializeComponent();

            this.removeAnimeBtn.Enabled = false; // [TODO]

            disableControls();

            updateStubAnimeList();
        }

        private void updateStubAnimeList() {
            this.saListBox.Items.Clear();

            List<StubAnimation> saList = StubAnimation.GetAllInstances();
            foreach (StubAnimation sa in saList) {
                IdNamePair pair = new IdNamePair(sa.Id, "");
                this.saListBox.Items.Add(pair);
            }

            this.saListBox.SelectedIndex = -1;
        }

        private void updateSrpiteList(StubAnimation sa) {
            this.spriteListBox.Items.Clear();

            List<GenericImage> sprites = sa.GetSprites();
            foreach (GenericImage image in sprites) {
                IdNamePair pair = new IdNamePair(image.Id, image.Name);
                this.spriteListBox.Items.Add(pair);
            }

            this.spriteListBox.SelectedIndex = -1;
        }

        private void disableControls() {
            this.upBtn.Enabled = false;
            this.downBtn.Enabled = false;
            this.addSpriteBtn.Enabled = false;
            this.removeSpriteBtn.Enabled = false;
            this.idTextBox.Enabled = false;
            this.descrTextBox.Enabled = false;
            this.delayTextBox.Enabled = false;
            this.canRepeatCheckBox.Enabled = false;
            this.saveChangesBtn.Enabled = false;
            this.cancelChangesBtn.Enabled = false;

            this.addAnimeBtn.Enabled = true;
            //this.removeAnimeBtn.Enabled = true; // [TODO]
            this.saListBox.Enabled = true;
        }

        private void enableControls() {
            this.upBtn.Enabled = true;
            this.downBtn.Enabled = true;
            this.addSpriteBtn.Enabled = true;
            this.removeSpriteBtn.Enabled = true;
            this.descrTextBox.Enabled = true;
            this.delayTextBox.Enabled = true;
            this.canRepeatCheckBox.Enabled = true;
            this.saveChangesBtn.Enabled = true;
            this.cancelChangesBtn.Enabled = true;

            this.addAnimeBtn.Enabled = false;
            //this.removeAnimeBtn.Enabled = false; // [TODO]
            this.saListBox.Enabled = false;
        }

        public void AddSprite(GenericImage image){
            if (this.addSpriteBtn.Enabled) {
                this.spriteListBox.Items.Add(new IdNamePair(image.Id, image.Name));
            }
        }

        private void saListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.saListBox.SelectedIndex >= 0) {
                int saId = (this.saListBox.SelectedItem as IdNamePair).Id;
                StubAnimation sa = StubAnimation.GetInstance(saId);

                updateSrpiteList(sa);
                this.idTextBox.Text = "" + sa.Id;
                this.descrTextBox.Text = sa.Name;
                this.delayTextBox.Text = "" + sa.SpriteDelay;
                this.canRepeatCheckBox.Checked = sa.CanRepeat;
            }
            else {
                this.idTextBox.Text = "";
                this.idTextBox.Text = "";
                this.descrTextBox.Text = "";
                this.delayTextBox.Text = "";
                this.canRepeatCheckBox.Checked = false;
                this.spriteListBox.Items.Clear();
            }
        }

        private void spriteListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.spriteListBox.SelectedIndex >= 0) {
                int spriteId = (this.spriteListBox.SelectedItem as IdNamePair).Id;

                GenericImage image = GenericImage.GetInstance(spriteId);

                this.saPictureBox.Image = image.Image;
            }
        }

        private void testAnimeBtn_Click(object sender, EventArgs e) {
            int delay;
            if (!Int32.TryParse(this.delayTextBox.Text, out delay) || delay < 0) {
                return;
            }

            List<int> spriteIds = new List<int>();
            foreach (IdNamePair pair in this.spriteListBox.Items) {
                spriteIds.Add(pair.Id);
            }

            new TestAnimeForm(spriteIds, delay).ShowDialog();
        }

        private void editAnimeBtn_Click(object sender, EventArgs e) {
            if (this.saListBox.SelectedIndex >= 0) {
                enableControls();
            }
        }

        private void saveChangesBtn_Click(object sender, EventArgs e) {
            int id;
            if (!Int32.TryParse(this.idTextBox.Text, out id)) {
                MessageBox.Show("Not saved. Invalid id.");
                return;
            }

            if (String.IsNullOrEmpty(this.descrTextBox.Text)) {
                MessageBox.Show("Not saved. Empty description.");
                return;
            }

            int delay;
            if (!Int32.TryParse(this.delayTextBox.Text, out delay) || delay < 0) {
                MessageBox.Show("Not saved. Invalid delay.");
                return;
            }

            if (this.spriteListBox.Items.Count == 0) {
                MessageBox.Show("Not saved. No sprites.");
                return;
            }

            List<GenericImage> sprites = new List<GenericImage>();
            foreach (IdNamePair pair in this.spriteListBox.Items) {
                GenericImage image = GenericImage.GetInstance(pair.Id);
                if (image == null) {
                    MessageBox.Show(String.Format("Sprite '{0}' not found.", pair.Id));
                    return;
                }
                sprites.Add(image);
            }

            StubAnimation sa = null;
            if (this.saListBox.SelectedIndex >= 0) {
                // [SC] changing existing stub animation
                int saId = (this.saListBox.SelectedItem as IdNamePair).Id;
                sa = StubAnimation.GetInstance(saId);
            }
            else { 
                // [SC] creating a new stub animation
                sa = StubAnimation.CreateInstance(id, this.descrTextBox.Text);
            }

            if (sa == null) {
                MessageBox.Show(String.Format("Unable to create or find animation '{0}'.", id));
                return;
            }

            sa.Name = this.descrTextBox.Text;
            sa.SpriteDelay = delay;
            sa.CanRepeat = this.canRepeatCheckBox.Checked;
            sa.RemoveSprites();
            foreach (GenericImage image in sprites) {
                sa.AddSprite(image);
            }

            disableControls();
            updateStubAnimeList();
        }

        private void cancelChangesBtn_Click(object sender, EventArgs e) {
            disableControls();
            saListBox_SelectedIndexChanged(null, null);
        }

        private void upBtn_Click(object sender, EventArgs e) {
            if (this.spriteListBox.SelectedIndex > 0) {
                int index = this.spriteListBox.SelectedIndex;
                Object item = this.spriteListBox.SelectedItem;
                this.spriteListBox.Items.Remove(item);
                this.spriteListBox.Items.Insert(index - 1, item);

                this.spriteListBox.SelectedIndex = index - 1;
            }
        }

        private void downBtn_Click(object sender, EventArgs e) {
            if (this.spriteListBox.SelectedIndex >= 0 &&
                this.spriteListBox.SelectedIndex < this.spriteListBox.Items.Count - 1) {
                int index = this.spriteListBox.SelectedIndex;
                Object item = this.spriteListBox.SelectedItem;
                this.spriteListBox.Items.Remove(item);
                this.spriteListBox.Items.Insert(index + 1, item);

                this.spriteListBox.SelectedIndex = index + 1;
            }
        }

        private void addSpriteBtn_Click(object sender, EventArgs e) {
            new SpriteSelectionForm(this).ShowDialog();
        }

        private void removeSpriteBtn_Click(object sender, EventArgs e) {
            if (this.spriteListBox.SelectedIndex >= 0) {
                this.spriteListBox.Items.Remove(this.spriteListBox.SelectedItem);
                this.spriteListBox.SelectedIndex = -1;
            }
        }

        private void addAnimeBtn_Click(object sender, EventArgs e) {
            this.idTextBox.Enabled = true;
            this.saListBox.SelectedIndex = -1;
            enableControls();
        }

        private void removeAnimeBtn_Click(object sender, EventArgs e) {
            // [TODO]
            if (this.saListBox.SelectedIndex >= 0) {
                int saId = (this.saListBox.SelectedItem as IdNamePair).Id;
                this.spriteListBox.Items.Remove(this.spriteListBox.SelectedItem);
                StubAnimation.RemoveInstance(saId);
                this.saListBox.SelectedIndex = -1;
            }
        }
    }
}
