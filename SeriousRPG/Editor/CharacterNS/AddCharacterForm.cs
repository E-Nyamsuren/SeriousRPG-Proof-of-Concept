using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SeriousRPG.HubNS;
using SeriousRPG.Model.AnimationNS;
using SeriousRPG.Model.StateNS;
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.ActorNS;

namespace SeriousRPG.Editor.CharacterNS {
    public partial class AddCharacterForm : Form {

        private static string STATIC_ACTOR = "Static actor";
        private static string DYNAMIC_ACTOR = "Dynamic actor";

        public AddCharacterForm() {
            InitializeComponent();

            // [SC] add actor types
            this.actorTypeComboBox.Items.Add(AddCharacterForm.STATIC_ACTOR);
            this.actorTypeComboBox.Items.Add(AddCharacterForm.DYNAMIC_ACTOR);
            this.actorTypeComboBox.SelectedIndex = -1;

            // [SC] load preset ids
            foreach (int id in ActorPreset.GetPresetIdList()) {
                this.presetComboBox.Items.Add(id);
            }
            this.presetComboBox.SelectedIndex = -1;
            usePresetsCheckBox_CheckedChanged(null, null);
        }

        private void createButton_Click(object sender, EventArgs e) {
            // [SC] check valid id
            int actorId;
            if (!Int32.TryParse(this.idTextBox.Text, out actorId)) {
                MessageBox.Show("Enter numeric value as Id."); // [TODO]
                return;
            }

            // [SC] check unique id
            if (!HubNS.Hub.IsValidId(actorId)) {
                MessageBox.Show("Current id is not unique."); // [TODO]
                return;
            }

            // [SC] check actor type
            if (this.actorTypeComboBox.SelectedIndex < 0) {
                MessageBox.Show("Select actor type."); // [TODO]
                return;
            }

            // [SC] check if preset is selected
            ActorPreset preset = null;
            if (this.usePresetsCheckBox.Checked) {
                if (this.presetComboBox.SelectedIndex < 0) {
                    MessageBox.Show("Select actor preset."); // [TODO]
                    return;
                }

                int presetId;
                if (!Int32.TryParse(this.presetComboBox.SelectedItem.ToString(), out presetId)) {
                    MessageBox.Show("Unable to parse the preset id");
                    return;
                }

                if (!ActorPreset.HasPreset(presetId)) {
                    MessageBox.Show("The preset does not exist.");
                    return;
                }

                preset = ActorPreset.GetPreset(presetId);
            }

            string actorType = this.actorTypeComboBox.SelectedItem as String;

            Actor actor;
            if (actorType.Equals(AddCharacterForm.STATIC_ACTOR)) {
                actor = new Actor(actorId, null, null, false);
            }
            else if (actorType.Equals(AddCharacterForm.DYNAMIC_ACTOR)) {
                actor = new DynamicActor(actorId, null, null, false);
            }
            else {
                MessageBox.Show("Unknow actor type."); // [TODO]
                return;
            }

            if (preset != null) {
                actor.Portrait = GenericImage.GetInstance(preset.PortraitId);
                actor.DefaultSprite = GenericImage.GetInstance(preset.DefaultSpriteId);

                if (actorType.Equals(AddCharacterForm.DYNAMIC_ACTOR)) {
                    actor.GetState((int)Hub.Reserved.STATE_DOWN).SetAnimation(preset.DownSaId);
                    actor.GetState((int)Hub.Reserved.STATE_LEFT).SetAnimation(preset.LeftSaId);
                    actor.GetState((int)Hub.Reserved.STATE_RIGHT).SetAnimation(preset.RightSaId);
                    actor.GetState((int)Hub.Reserved.STATE_UP).SetAnimation(preset.UpSaId);
                }
            }

            if (!Game.GetInstance().AddActor(actor)) {
                HubNS.Hub.DeregisterId(actor.Id);
                MessageBox.Show("Unknow error registering the actor."); // [TODO]
                return;
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void usePresetsCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (this.usePresetsCheckBox.Checked) {
                this.presetComboBox.Enabled = true;
            }
            else {
                this.presetComboBox.Enabled = false;
            }
        }
    }
}
