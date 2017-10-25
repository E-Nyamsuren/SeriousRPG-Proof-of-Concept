using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

using System.Linq;
using System.Text;
using System.Windows.Forms;

using SeriousRPG.Model;
using SeriousRPG.Misc;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.ActorNS;
using SeriousRPG.Model.StateNS;
using SeriousRPG.Model.DrawingNS;

using System.Diagnostics;

namespace SeriousRPG.Editor.CharacterNS {
    public partial class CharacterWizardForm : Form {

        public GenericImage Portrait {
            get;
            set;
        }

        public GenericImage DefaultSprite {
            get;
            set;
        }

        public int StubAnimationId {
            get;
            set;
        }

        public IActor DestinationActor { // [TODO]
            get;
            set;
        }

        public CharacterWizardForm() {
            InitializeComponent();

            this.StubAnimationId = Cfg.UNASSIGNED_INT;

            this.idTextBox.Enabled = false;
            this.charTypeTextBox.Enabled = false;
            this.defaultSpriteTextBox.Enabled = false;
            this.destActorTextBox.Enabled = false;

            this.removeCharBtn.Enabled = false; // [TODO]

            disableControls();

            updateCharList();
        }

        private void updateCharList() {
            this.charListBox.Items.Clear();

            IEnumerable<IdNamePair> pairs = Game.GetInstance().GetActorIdList<IActor>();

            foreach (IdNamePair pair in pairs) {
                this.charListBox.Items.Add(pair);
            }

            this.charListBox.SelectedIndex = -1;
        }

        private void enableControls() {
            if (this.charListBox.SelectedIndex >= 0) {
                this.changePortraitBtn.Enabled = true;
                this.nameTextBox.Enabled = true;
                this.descrTextBox.Enabled = true;
                this.changeSpriteBtn.Enabled = true;
                this.healthTextBox.Enabled = true;
                this.xpTextBox.Enabled = true;
                this.levelTextBox.Enabled = true;
                this.canCollideCheckBox.Enabled = true;
                this.canClickCheckBox.Enabled = true;

                this.stateDGV.Enabled = true;
                this.addStateBtn.Enabled = true;
                this.removeStateBtn.Enabled = true;
                this.setAnimeBtn.Enabled = true;

                int actorId = (this.charListBox.SelectedItem as IdNamePair).Id;
                Actor actor = Game.GetInstance().GetActor<Actor>(actorId);

                if (actor is DynamicActor) { // [TODO] if character is dynamic actor
                    this.maxSpeedTextBox.Enabled = true;
                    this.startSpeedTextBox.Enabled = true;
                    this.destXTextBox.Enabled = true;
                    this.destYTextBox.Enabled = true;
                    //this.changeDestActorBtn.Enabled = true; // [TODO]
                }

                this.saveChangesBtn.Enabled = true;
                this.cancelChangesBtn.Enabled = true;

                this.charListBox.Enabled = false;
                this.addCharBtn.Enabled = false;
                this.removeCharBtn.Enabled = false;
            }
        }

        private void disableControls() {
            this.changePortraitBtn.Enabled = false;
            this.nameTextBox.Enabled = false;
            this.descrTextBox.Enabled = false;
            this.changeSpriteBtn.Enabled = false;
            this.healthTextBox.Enabled = false;
            this.xpTextBox.Enabled = false;
            this.levelTextBox.Enabled = false;
            this.canCollideCheckBox.Enabled = false;
            this.canClickCheckBox.Enabled = false;

            this.stateDGV.Enabled = false;
            this.addStateBtn.Enabled = false;
            this.removeStateBtn.Enabled = false;
            this.setAnimeBtn.Enabled = false;

            this.maxSpeedTextBox.Enabled = false;
            this.startSpeedTextBox.Enabled = false;
            this.destXTextBox.Enabled = false;
            this.destYTextBox.Enabled = false;
            this.changeDestActorBtn.Enabled = false;

            this.saveChangesBtn.Enabled = false;
            this.cancelChangesBtn.Enabled = false;

            this.charListBox.Enabled = true;
            this.addCharBtn.Enabled = true;
            //this.removeCharBtn.Enabled = true; // [TODO]
        }

        private void charListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.charListBox.SelectedIndex >= 0) {
                int actorId = (this.charListBox.SelectedItem as IdNamePair).Id;
                Actor actor = Game.GetInstance().GetActor<Actor>(actorId);

                this.idTextBox.Text = "" + actor.Id;
                this.nameTextBox.Text = actor.Name; 
                this.descrTextBox.Text = actor.Description;
                this.charTypeTextBox.Text = "Static actor";
                this.healthTextBox.Text = "" + actor.Health;
                this.xpTextBox.Text = "" + actor.Experience;
                this.levelTextBox.Text = "" + actor.Level;
                this.canCollideCheckBox.Checked = actor.CanCollide;
                this.canClickCheckBox.Checked = actor.IsClickable;
                
                if (actor.Portrait != null) {
                    this.portraitPictureBox.Image = actor.Portrait.Image;
                    this.Portrait = actor.Portrait;
                }
                else {
                    this.Portrait = null;
                    this.portraitPictureBox.Image = null;
                }

                if (actor.DefaultSprite != null) {
                    this.defaultSpriteTextBox.Text = "" + actor.DefaultSprite.Id;
                    this.DefaultSprite = actor.DefaultSprite;
                }
                else {
                    this.defaultSpriteTextBox.Text = "";
                    this.DefaultSprite = null;
                }
                
                if (actor is DynamicActor) {
                    this.charTypeTextBox.Text = "Dynamic actor";
                    this.maxSpeedTextBox.Text = "" + (actor as DynamicActor).MaxSpeed;
                    this.startSpeedTextBox.Text = "" + (actor as DynamicActor).CurrSpeed;

                    PointSR destPoint = (actor as DynamicActor).DestPoint;
                    if (destPoint != null) {
                        this.destXTextBox.Text = "" + destPoint.X;
                        this.destYTextBox.Text = "" + destPoint.Y;
                    }
                    else {
                        this.destXTextBox.Text = "";
                        this.destYTextBox.Text = "";
                    }

                    IActor destActor = (actor as DynamicActor).DestActor;
                    if (destActor != null) {
                        this.destActorTextBox.Text = destActor.X + "," + destActor.Y;
                    }
                    else {
                        this.destActorTextBox.Text = "";
                    }
                }

                List<IdNamePair> states = actor.GetStates();
                int currentStateId = actor.GetCurrentStateId();
                this.stateDGV.Rows.Clear();
                foreach (IdNamePair pair in states) {
                    State state = actor.GetState(pair.Id);
                    if (state.HasAnimation()) {
                        this.stateDGV.Rows.Add(state.Id, state.Name, state.Id == currentStateId, state.GetAnimationId());
                    }
                    else {
                        this.stateDGV.Rows.Add(state.Id, state.Name, state.Id == currentStateId);
                    }
                }
            }
            else {
                this.idTextBox.Text = "";
                this.nameTextBox.Text = "";
                this.descrTextBox.Text = "";
                this.charTypeTextBox.Text = "";
                this.defaultSpriteTextBox.Text = "";
                this.healthTextBox.Text = "";
                this.xpTextBox.Text = "";
                this.levelTextBox.Text = "";
                this.canCollideCheckBox.Checked = false;
                this.canClickCheckBox.Checked = false;

                this.stateDGV.Rows.Clear();

                this.charTypeTextBox.Text = "";
                this.maxSpeedTextBox.Text = "";
                this.startSpeedTextBox.Text = "";

                this.Portrait = null;
                this.DefaultSprite = null;

                this.stateDGV.Rows.Clear();
            }
        }

        private void viewSpriteBtn_Click(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(this.defaultSpriteTextBox.Text)) {
                return;
            }

            int imageId;
            if (!Int32.TryParse(this.defaultSpriteTextBox.Text, out imageId)) {
                return;
            }

            GenericImage image = GenericImage.GetInstance(imageId);

            if (image == null) {
                return;
            }

            PictureBox box = new PictureBox();
            box.Location = new Point(0, 0);
            box.ClientSize = new Size(image.Width, image.Height);
            box.Image = image.Image;

            Form form = new Form();
            form.ClientSize = new Size(box.Width, box.Height);
            form.Controls.Add(box);
            form.ShowDialog();
        }

        private void editActorBtn_Click(object sender, EventArgs e) {
            enableControls();
        }

        private void saveChangesBtn_Click(object sender, EventArgs e) {
            // [SC] verify the portrait
            if (this.Portrait == null) {
                MessageBox.Show("Add a portrait."); // [TODO]
                return;
            }
            // [SC] verify the name
            if (String.IsNullOrEmpty(this.nameTextBox.Text)) {
                MessageBox.Show("Add a name."); // [TODO]
                return;
            }
            // [SC] verify the description
            if (String.IsNullOrEmpty(this.descrTextBox.Text)) {
                MessageBox.Show("Add a description."); // [TODO]
                return;
            }
            // [SC] verify the default sprite
            if (this.DefaultSprite == null) {
                MessageBox.Show("Add a default sprite."); // [TODO]
                return;
            }
            // [SC] verify health
            int health;
            if (!Int32.TryParse(this.healthTextBox.Text, out health) || health < 0) {
                MessageBox.Show("Health should be 0 or higher."); // [TODO]
                return;
            }
            // [SC] verify xp
            int xp;
            if (!Int32.TryParse(this.xpTextBox.Text, out xp) || xp < 0) {
                MessageBox.Show("Experience should be 0 or higher."); // [TODO]
                return;
            }
            // [SC] verify level
            int level;
            if (!Int32.TryParse(this.levelTextBox.Text, out level) || level < 0) {
                MessageBox.Show("Level should be 0 or higher."); // [TODO]
                return;
            }

            // [SC] verify has states
            if (this.stateDGV.Rows.Count == 0) {
                MessageBox.Show("Actor should have at least one state."); // [TODO]
                return;
            }

            // [SC] verify has a starting state
            bool hasStartState = false;
            foreach(DataGridViewRow row in this.stateDGV.Rows) {
                if (Boolean.Parse(row.Cells["StartStateFlag"].Value.ToString())) {
                    hasStartState = true;
                }
            }
            if (!hasStartState) {
                MessageBox.Show("Set starting state for the actor."); // [TODO]
                return;
            }

            int maxSpeed = 0;
            int startSpeed = 0;
            int destX = 0;
            int destY = 0;
            // [SC] for dynamic actors
            if (this.charTypeTextBox.Text.Equals("Dynamic actor")) {
                // [SC] verify max speed
                if (!Int32.TryParse(this.maxSpeedTextBox.Text, out maxSpeed) || maxSpeed < 0) {
                    MessageBox.Show("Maximum speed should be 0 or higher."); // [TODO]
                    return;
                }
                // [SC] verify start speed
                if (!Int32.TryParse(this.startSpeedTextBox.Text, out startSpeed) || startSpeed < 0) {
                    MessageBox.Show("Start speed should be 0 or higher."); // [TODO]
                    return;
                }
                // [SC] verify dest x
                if (!Int32.TryParse(this.destXTextBox.Text, out destX)) {
                    MessageBox.Show("Destination x-coordinate is invalid."); // [TODO]
                    return;
                }
                // [SC] verify dest y
                if (!Int32.TryParse(this.destYTextBox.Text, out destY)) {
                    MessageBox.Show("Destination y-coordinate is invalid."); // [TODO]
                    return;
                }
            }

            int actorId = (this.charListBox.SelectedItem as IdNamePair).Id;
            Actor actor = Game.GetInstance().GetActor<Actor>(actorId);
            
            actor.Portrait = this.Portrait; // [SC] update the portrait
            actor.Name = this.nameTextBox.Text; // [SC] update the name
            actor.Description = this.descrTextBox.Text; // [SC] update description
            actor.DefaultSprite = this.DefaultSprite; // [SC] update sprite
            actor.Health = health; // [SC] update helath
            actor.Experience = xp; // [SC] update xp
            actor.Level = level; // [SC] update level
            actor.CanCollide = this.canCollideCheckBox.Checked; // [SC] update can collide
            actor.IsClickable = this.canClickCheckBox.Checked; // [SC] update is clickable

            // [SC] update states
            actor.ClearStates(); // [SC] remove all existing states
            foreach (DataGridViewRow row in this.stateDGV.Rows) {
                // [SC] create a new State instance
                int stateId = Int32.Parse(row.Cells["StateId"].Value.ToString());
                State newState = State.CreateInstance(StubState.GetInstance(stateId));
                actor.AddState(newState);

                // [SC] set start state
                bool startState = Boolean.Parse(row.Cells["StartStateFlag"].Value.ToString());
                if (startState) {
                    actor.SetCurrentState(stateId);
                }

                // [SC] set animation Id
                if (row.Cells["AnimationId"].Value != null) {
                    string animeIdStr = row.Cells["AnimationId"].Value.ToString();
                    if (!String.IsNullOrEmpty(animeIdStr) && !String.IsNullOrWhiteSpace(animeIdStr)) {
                        newState.SetAnimation(Int32.Parse(animeIdStr));
                    }
                }
            }

            // [SC] for dynamic actors
            if (actor is DynamicActor) {
                (actor as DynamicActor).MaxSpeed = maxSpeed; // [SC] update max speed
                (actor as DynamicActor).CurrSpeed = startSpeed;
                (actor as DynamicActor).DestPoint.X = destX;
                (actor as DynamicActor).DestPoint.Y = destY;
                (actor as DynamicActor).DestActor = this.DestinationActor;
            }

            disableControls();
            charListBox_SelectedIndexChanged(null, null);
        }

        private void cancelChangesBtn_Click(object sender, EventArgs e) {
            disableControls();
            charListBox_SelectedIndexChanged(null, null);
        }

        private void changePortraitBtn_Click(object sender, EventArgs e) {
            new PortraitSelectionForm(this).ShowDialog();
            if (this.Portrait != null) {
                this.portraitPictureBox.Image = this.Portrait.Image;
            }
        }

        private void changeSpriteBtn_Click(object sender, EventArgs e) {
            new SpriteSelectionForm(this).ShowDialog();
            if (this.DefaultSprite != null) {
                this.defaultSpriteTextBox.Text = "" + this.DefaultSprite.Id;
            }
        }

        private void changeDestActorBtn_Click(object sender, EventArgs e) {
            // [TODO]
            // - dest actor should not be the self
            // - dest actor should not be actor in a different map
        }

        private void stateDGV_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (this.stateDGV.Columns[e.ColumnIndex].Name.Equals("StartStateFlag")) {

                if (!Boolean.Parse(this.stateDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())) {
                    this.stateDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;

                    foreach(DataGridViewRow row in this.stateDGV.Rows) {
                        if (row.Index != e.RowIndex) {
                            row.Cells[e.ColumnIndex].Value = false;
                        }
                    }
                }
            }
        }

        private void setAnimeBtn_Click(object sender, EventArgs e) {
            if (this.stateDGV.SelectedCells.Count == 1) { 
                int rowIndex = this.stateDGV.SelectedCells[0].RowIndex;


                int animationId;
                bool select;
                if (this.stateDGV.Rows[rowIndex].Cells["AnimationId"].Value == null ||
                    !Int32.TryParse(this.stateDGV.Rows[rowIndex].Cells["AnimationId"].Value.ToString(), out animationId)) {
                    animationId = 0;
                    select = false;
                }
                else {
                    select = true;
                }

                new SelectAnimationForm(this, select, animationId).ShowDialog();

                if (this.StubAnimationId == Cfg.UNASSIGNED_INT) {
                    return;
                }

                this.stateDGV.Rows[rowIndex].Cells["AnimationId"].Value = this.StubAnimationId;
                this.StubAnimationId = Cfg.UNASSIGNED_INT;
            }
        }

        private void removeStateBtn_Click(object sender, EventArgs e) {
            if (this.stateDGV.SelectedCells.Count == 1) {
                int rowIndex = this.stateDGV.SelectedCells[0].RowIndex;
                this.stateDGV.Rows.RemoveAt(rowIndex);
            }
        }

        private void addStateBtn_Click(object sender, EventArgs e) {
            new StubStateSelectionForm(this).ShowDialog();
        }

        public bool addStubState(int stubStateId) {
            foreach (DataGridViewRow row in this.stateDGV.Rows) {
                int stateId = Int32.Parse(row.Cells["StateId"].Value.ToString());
                if (stateId == stubStateId) {
                    // [SC] actor already has the state
                    return false;
                }
            }

            StubState newStubState = StubState.GetInstance(stubStateId);
            this.stateDGV.Rows.Add(newStubState.Id, newStubState.Name, false);
            return true;
        }

        private void addCharBtn_Click(object sender, EventArgs e) {
            new AddCharacterForm().ShowDialog();
            updateCharList();
        }
    }
}
