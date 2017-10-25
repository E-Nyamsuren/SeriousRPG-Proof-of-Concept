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

using SeriousRPG.Editor.RuleNS;
using SeriousRPG.Editor.AnimationNS;
using SeriousRPG.Editor.CharacterNS;

using SeriousRPG.Misc;
using SeriousRPG.Model;
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.ImageObjectNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.MapNS;
using SeriousRPG.Model.ActorNS;
using SeriousRPG.Model.RuleNS;
using SeriousRPG.Model.RuleNS.ConditionNS;
using SeriousRPG.Model.RuleNS.ActionNS;
using SeriousRPG.Model.ModuleNS;
using SeriousRPG.Module.AltThesaurusModuleNS; // [TODO]
using SeriousRPG.Module.CardMatchingModuleNS; // [TODO]
using SeriousRPG.Module.DialogueModuleNS; // [TODO]

using System.Diagnostics;

namespace SeriousRPG.Editor
{
    public partial class MapEditorForm : Form, ICanvas 
    {
        private int SelectedBgImageId {
            get;
            set;
        }

        private int SelectedActorId {
            get;
            set;
        }

        private RunableGame RunableGame {
            get;
            set;
        }

        public MapEditorForm() {
            InitializeComponent();

            layerComboBox.Items.Add(new IdNamePair(Background.LAYER_TYPE, Background.LAYER_NAME));
            layerComboBox.Items.Add(new IdNamePair(BackgroundOverlay.LAYER_TYPE, BackgroundOverlay.LAYER_NAME));
            layerComboBox.Items.Add(new IdNamePair(Foreground.LAYER_TYPE, Foreground.LAYER_NAME));
            layerComboBox.Items.Add(new IdNamePair(ForegroundOverlay.LAYER_TYPE, ForegroundOverlay.LAYER_NAME));
            layerComboBox.Items.Add(new IdNamePair(RouteMap.LAYER_TYPE, RouteMap.LAYER_NAME));
            layerComboBox.Items.Add(new IdNamePair(LogicLayer.LAYER_TYPE, LogicLayer.LAYER_NAME));
            layerComboBox.SelectedIndex = 0;
        }

        public void LoadGameResources() {
            List<GenericImage> genericImages = GenericImage.GetAllInstances();
            for (int imgIndex = 0; imgIndex < genericImages.Count; imgIndex++) {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = genericImages[imgIndex].Image;
                pictureBox.Name = "" + genericImages[imgIndex].Id;
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox.Click += new EventHandler(this.pictureBox_Click);
                this.tilePanel.Controls.Add(pictureBox);
            }
            this.SelectedBgImageId = Cfg.UNASSIGNED_INT;

            //////////////////////////////////////////////////////////////////

            UpdateActorList();

            UpdateMapList();
        }

        private void addMapBtn_Click(object sender, EventArgs e) {
            AddMapForm addMapForm = new AddMapForm(this);
            addMapForm.Show();
        }

        private void mapListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.mapListBox.SelectedIndex >= 0) {
                IdNamePair idNamePair = this.mapListBox.SelectedItem as IdNamePair;

                Game.GetInstance().CurrentMapId = idNamePair.Id;

                bgCheckBox.Checked = Game.GetInstance().GetLayerRenderFlag(idNamePair.Id, Background.LAYER_TYPE);
                bgoCheckBox.Checked = Game.GetInstance().GetLayerRenderFlag(idNamePair.Id, BackgroundOverlay.LAYER_TYPE);
                fgCheckBox.Checked = Game.GetInstance().GetLayerRenderFlag(idNamePair.Id, Foreground.LAYER_TYPE);
                fgoCheckBox.Checked = Game.GetInstance().GetLayerRenderFlag(idNamePair.Id, ForegroundOverlay.LAYER_TYPE);
                rmCheckBox.Checked = Game.GetInstance().GetLayerRenderFlag(idNamePair.Id, RouteMap.LAYER_TYPE);
                llCheckBox.Checked = Game.GetInstance().GetLayerRenderFlag(idNamePair.Id, LogicLayer.LAYER_TYPE);

                //this.mapPanel.Invalidate();  // request a delayed Repaint by the normal MessageLoop system    
                //this.mapPanel.Update();      // forces Repaint of invalidated area 
                this.mapPanel.Refresh();     // Combines Invalidate() and Update()
            }
        }

        private void mapPanel_Paint(object sender, PaintEventArgs e) {
            Game.GetInstance().DrawMap();
        }

        private void mapPanel_Click(object sender, MouseEventArgs e) {
            //Point point = panel.PointToClient(Cursor.Position); // [TODO] delete

            if (this.mapListBox.SelectedIndex < 0) {
                return;
            }

            if (this.layerComboBox.SelectedIndex < 0) {
                return;
            }

            int mapId = (this.mapListBox.SelectedItem as IdNamePair).Id;
            int layerType = (this.layerComboBox.SelectedItem as IdNamePair).Id;

            if (layerType == Background.LAYER_TYPE) {
                if (this.SelectedBgImageId != Cfg.UNASSIGNED_INT) {
                    Game.GetInstance().AddMapItem(mapId, layerType, this.SelectedBgImageId, e.X, e.Y);
                    this.mapPanel.Refresh();     // Combines Invalidate() and Update()
                    return;
                }
                else {
                    return;
                }
            } else if (layerType == BackgroundOverlay.LAYER_TYPE){
                if (this.SelectedBgImageId != Cfg.UNASSIGNED_INT) {
                    // [SC] add the new item to the background overlay
                    Game.GetInstance().AddMapItem(mapId, layerType, this.SelectedBgImageId, e.X, e.Y);
                    this.mapPanel.Refresh();
                    return;
                } else {
                    // [SC] remove any background overlay image that was clicked on
                    Game.GetInstance().RemoveMapItem(mapId, layerType, e.X, e.Y);
                    this.mapPanel.Refresh();
                    return;
                }
            } else if (layerType == Foreground.LAYER_TYPE){
                if (this.SelectedActorId != Cfg.UNASSIGNED_INT) {
                    Game.GetInstance().AddMapItem(mapId, layerType, this.SelectedActorId, e.X, e.Y);
                    this.mapPanel.Refresh();
                    return;
                }
                else {
                    return;
                }
            } else if (layerType == ForegroundOverlay.LAYER_TYPE){
                return; // [TODO]
            } else if (layerType == RouteMap.LAYER_TYPE){
                Game.GetInstance().AddMapItem(mapId, layerType, Cfg.UNASSIGNED_INT, e.X, e.Y);
                this.mapPanel.Refresh();     // Combines Invalidate() and Update()
                return;
            }
            else if (layerType == LogicLayer.LAYER_TYPE) {
                Game.GetInstance().AddMapItem(mapId, layerType, (this.regionTriggerListBox.SelectedItem as IdNamePair).Id, e.X, e.Y);
                this.mapPanel.Refresh();     // Combines Invalidate() and Update()
                return;
            }
            else {
                // [TODO] error msg
                return;
            }        
        }

        private void Test_Click(object sender, EventArgs e) {
            TestForm testForm = new TestForm();

            int mapId = (this.mapListBox.SelectedItem as IdNamePair).Id;

            RunableGame.ClearInstance();
            this.RunableGame = Game.GetInstance().Clone(testForm);
            this.RunableGame.CurrentMapId = mapId;

            testForm.SizeSR = this.RunableGame.GetCurrentMapSize();

            // [TODO] need to reset these layers after testing is done
            // [TODO] need to check wether these flags are woring properly; logic layer does not;
            // [TODO] does not work if the code is moved to RunableGame.RunGame
            this.RunableGame.SetLayerRenderFlag(mapId, Background.LAYER_TYPE, true);
            this.RunableGame.SetLayerRenderFlag(mapId, BackgroundOverlay.LAYER_TYPE, true);
            this.RunableGame.SetLayerRenderFlag(mapId, Foreground.LAYER_TYPE, true);
            this.RunableGame.SetLayerRenderFlag(mapId, ForegroundOverlay.LAYER_TYPE, true);
            this.RunableGame.SetLayerRenderFlag(mapId, LogicLayer.LAYER_TYPE, false);
            this.RunableGame.SetLayerRenderFlag(mapId, RouteMap.LAYER_TYPE, false);

            // [Thesaurus][TODO]
            //ActionInvokeModule actionInvoke = new ActionInvokeModule(333, "thesaurus test"
                // [SC] [0] thesaurus id
            //    , typeof(AltThesaurusModule), new string[] { "2" }, false);
            //actionInvoke.Invoke(null);

            // [CardMatchingModule][TODO]
            //ActionInvokeModule actionInvoke = new ActionInvokeModule(333, "card matching module test"
            //    // [SC] [0] number of target pairs, [1] the number of pairs; [2] target set id
            //    , typeof(CardMatchingModule), new string[] { "1", "6", "1" }, false);
            //actionInvoke.Invoke(null);

            // [DialogueModule][TODO]
            //ActionInvokeModule actionInvoke = new ActionInvokeModule(333, "dialogue module test"
            //    , typeof(DialogueModule), new string[] { "1", "" + (int)HubNS.Hub.Reserved.PLAYER_ID, "55" }, false);
            //actionInvoke.Invoke(null);

            testForm.Show();
        }

        ////////////////////////////////////////////////////////////////

        private void pictureBox_Click(object sender, EventArgs e) {
            PictureBox pictureBox = sender as PictureBox;
            
            int imageId;
            if (Int32.TryParse(pictureBox.Name, out imageId)) {
                if (this.SelectedBgImageId == imageId) {
                    this.SelectedBgImageId = Cfg.UNASSIGNED_INT;
                    pictureBox.BorderStyle = BorderStyle.None;
                }
                else {
                    if (this.SelectedBgImageId != Cfg.UNASSIGNED_INT) {
                        PictureBox prevPictureBox = this.tilePanel.Controls.Find("" + this.SelectedBgImageId, true).FirstOrDefault() as PictureBox;
                        prevPictureBox.BorderStyle = BorderStyle.None;
                    }

                    this.SelectedBgImageId = imageId;
                    pictureBox.BorderStyle = BorderStyle.Fixed3D;
                }
            }
            else {
                MessageBox.Show("Cannot parse GenericImage's id from picture box's name.");
            }
        }

        private void actorPictureBox_Click(object sender, EventArgs e) {
            PictureBox pictureBox = sender as PictureBox;

            int actorId;
            if (Int32.TryParse(pictureBox.Name, out actorId)) {

                if (this.SelectedActorId == actorId) {
                    this.SelectedActorId = Cfg.UNASSIGNED_INT;
                }
                else {
                    this.SelectedActorId = actorId;
                }
            }
            else {
                MessageBox.Show("Cannot parse Actor's id from picture box's name.");
            }
        }

        internal void UpdateActorList() {
            List<IActor> actors = Game.GetInstance().GetActorList<IActor>();

            this.actorsPanel.Controls.Clear();

            foreach (IActor actor in actors) {
                PictureBox actorPictureBox = new PictureBox();
                actorPictureBox.Image = actor.Image.Image;
                actorPictureBox.Name = "" + actor.Id;
                actorPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                actorPictureBox.Click += new EventHandler(this.actorPictureBox_Click);
                this.actorsPanel.Controls.Add(actorPictureBox);
            }

            this.SelectedActorId = Cfg.UNASSIGNED_INT;
        }

        /// <summary>
        /// Update the listbox with available maps
        /// </summary>
        internal void UpdateMapList() {
            List<IdNamePair> mapList = Game.GetInstance().GetMapList();
            this.mapListBox.Items.Clear(); // [TODO] need to update the drawing panel
            foreach (IdNamePair idNamePair in mapList) {
                this.mapListBox.Items.Add(idNamePair);
            }
            if (this.mapListBox.Items.Count > 0) {
                this.mapListBox.SelectedIndex = 0;
            }
        }

        internal void UpdateRegionTriggerList() {
            IEnumerable<IdNamePair> regionTriggerList = Game.GetInstance().GetConditionList<RegionTrigger>();
            this.regionTriggerListBox.Items.Clear(); // [TODO] need to update the drawing panel
            foreach (IdNamePair idNamePair in regionTriggerList) {
                this.regionTriggerListBox.Items.Add(idNamePair);
            }
            if (this.regionTriggerListBox.Items.Count > 0) {
                this.regionTriggerListBox.SelectedIndex = 0;
            }
        }

        private void bgCheckBox_CheckedChanged(object sender, EventArgs e) {
            IdNamePair idNamePair = this.mapListBox.SelectedItem as IdNamePair;
            Game.GetInstance().SetLayerRenderFlag(idNamePair.Id, Background.LAYER_TYPE, this.bgCheckBox.Checked);  
            this.mapPanel.Refresh();
        }

        private void bgoCheckBox_CheckedChanged(object sender, EventArgs e) {
            IdNamePair idNamePair = this.mapListBox.SelectedItem as IdNamePair;
            Game.GetInstance().SetLayerRenderFlag(idNamePair.Id, BackgroundOverlay.LAYER_TYPE, this.bgoCheckBox.Checked);  
            this.mapPanel.Refresh();
        }

        private void fgCheckBox_CheckedChanged(object sender, EventArgs e) {
            IdNamePair idNamePair = this.mapListBox.SelectedItem as IdNamePair;
            Game.GetInstance().SetLayerRenderFlag(idNamePair.Id, Foreground.LAYER_TYPE, this.fgCheckBox.Checked);  
            this.mapPanel.Refresh();
        }

        private void fgoCheckBox_CheckedChanged(object sender, EventArgs e) {
            IdNamePair idNamePair = this.mapListBox.SelectedItem as IdNamePair;
            Game.GetInstance().SetLayerRenderFlag(idNamePair.Id, ForegroundOverlay.LAYER_TYPE, this.fgoCheckBox.Checked);  
            this.mapPanel.Refresh();
        }

        private void rmCheckBox_CheckedChanged(object sender, EventArgs e) {
            IdNamePair idNamePair = this.mapListBox.SelectedItem as IdNamePair;
            Game.GetInstance().SetLayerRenderFlag(idNamePair.Id, RouteMap.LAYER_TYPE, this.rmCheckBox.Checked);  
            this.mapPanel.Refresh();
        }

        private void llCheckBox_CheckedChanged(object sender, EventArgs e) {
            IdNamePair idNamePair = this.mapListBox.SelectedItem as IdNamePair;
            Game.GetInstance().SetLayerRenderFlag(idNamePair.Id, LogicLayer.LAYER_TYPE, this.llCheckBox.Checked);  
            this.mapPanel.Refresh();
        }

        private void animationWizardBtn_Click(object sender, EventArgs e) {
            new StubAnimationForm().ShowDialog();
        }

        private void characterWizardBtn_Click(object sender, EventArgs e) {
            new CharacterWizardForm().ShowDialog();
            UpdateActorList();
        }

        private void ruleWizardBtn_Click(object sender, EventArgs e) {
            new RuleForm().ShowDialog();
            UpdateRegionTriggerList();
        }

        #region ICanvas methods

        public IUiComponentModule UiModule {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public SizeSR SizeSR {
            get {
                return new SizeSR(this.ClientSize.Width, this.ClientSize.Height);
            }
            set {
                if (value != null) {
                    this.ClientSize = new Size(value.Width, value.Height);
                }
            }
        }

        public void DrawImage(GenericImage image, float x, float y, int width, int height) {
            Graphics g = this.mapPanel.CreateGraphics();
            g.DrawImage(image.Image, x, y - (height - 1), width, height);
            g.Dispose();
        }

        public void DrawRectangle(float x, float y, int width, int height, Brush brush) {
            Graphics g = this.mapPanel.CreateGraphics();
            g.FillRectangle(brush, x, y - (height - 1), width, width);
            g.Dispose();
        }

        public void DrawRectangle(float x, float y, int width, int height, Color borderColor, int borderWidth) {
            Graphics g = this.mapPanel.CreateGraphics();
            g.DrawRectangle(new Pen(borderColor, borderWidth), x, y - (height - 1), width, height);
            g.Dispose();
        }

        public void DrawText(string text, float x, float y, string font, float fontSize, Color color) {
            Graphics g = this.mapPanel.CreateGraphics();
            Font drawFont = new Font(font, fontSize);
            SolidBrush drawBrush = new SolidBrush(color);
            StringFormat drawFormat = new StringFormat();
            SizeF textSize = g.MeasureString(text, drawFont);
            g.DrawString(text, drawFont, drawBrush, x, y - (textSize.Height - 1), drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            g.Dispose();
        }

        public SizeSR GetTextSize(string text, string font, float fontSize) {
            Graphics g = this.mapPanel.CreateGraphics();
            Font drawFont = new Font(font, fontSize);
            SizeF textSize = g.MeasureString(text, drawFont);
            return new SizeSR((int)textSize.Width, (int)textSize.Height);
        }

        public void Clear() {
            Graphics g = this.mapPanel.CreateGraphics();
            g.Dispose();
        }

        public void DrawBufferToCanvas() {

        }

        #endregion ICanvas methods
    }
}
