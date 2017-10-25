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
using System.Linq;
using System.Text;

using System.IO;
using System.Xml.Linq;

using System.Drawing; // [TODO] Color

using SeriousRPG.Model.ModuleNS;
using SeriousRPG.Model.MapNS; // ICollidableObject
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.ActorNS;
using SeriousRPG.Model.StateNS;
using SeriousRPG.Model.AnimationNS;
using SeriousRPG.Model;
using SeriousRPG.HubNS;
using SeriousRPG.Misc;
using SeriousRPG.ControlIO;

using System.Diagnostics;

namespace SeriousRPG.Module.DialogueModuleNS
{
    class DialogueModule : IModule
    {
        #region Const and static fields

        private const int SCENE_WIDTH = 768;
        private const int SCENE_HEIGHT = 480;

        private const int SCENARIO_FILE_ID = 62000;
        private const int BG_IMAGE_ID = 62001;

        private const string moduleRootPath = "Module/DialogueModule/res/";
        private static string moduleConfigFilepath = Path.Combine(moduleRootPath, "cfg.xml");

        #endregion Const and static fields

        #region Actor related fields and consts

        private static PointSR ACTOR_ONE_POS = new PointSR(16, 255);
        private static PointSR ACTOR_ONE_NAME_POS = new PointSR(16, 79);

        private static PointSR ACTOR_TWO_POS = new PointSR(608, 255);
        private static PointSR ACTOR_TWO_NAME_POS = new PointSR(752, 79); // [SC] bottom-right corner

        private static Color ACTOR_ONE_COLOR = Color.Green;
        private static Color ACTOR_TWO_COLOR = Color.Blue;

        private static PointSR ACTOR_ONE_HH_POS = new PointSR(8, 263);
        private static PointSR ACTOR_TWO_HH_POS = new PointSR(600, 263);

        private IActor actorOne;
        private IActor actorTwo;

        #endregion Actor related fields and consts

        #region Text related fields and consts

        public const string textFont = "Arial";
        public const float textFontSize = 18;
        public const float vSpacing = 2;

        private static PointSR TEXT_POS = new PointSR(16, 288); // [SC] top-left corner
        private static SizeSR TEXT_SIZE = new SizeSR(736, 176);

        #endregion Text related fields and consts

        #region Button related fields and consts

        private static int PREV_BTN_ID = 62011;
        private static int NEXT_BTN_ID = 62021;
        private static int RESTART_BTN_ID = 62031;
        private static int CLOSE_BTN_ID = 62041;

        private static PointSR PREV_BTN_POS = new PointSR(296, 255);
        private static PointSR NEXT_BTN_POS = new PointSR(424, 255);
        private static PointSR RESTART_BTN_POS = new PointSR(360, 255);
        private static PointSR CLOSE_BTN_POS = new PointSR(192, 255);

        private Actor prevBtn;
        private Actor nextBtn;
        private Actor restartBtn;
        private Actor closeBtn;

        private int overStubStateId;

        #endregion Button related fields and consts

        #region Fields

        private List<ICollidableObject> cObjects;
        private List<IClickable> clickableObjects;

        private GenericImage bgImage = null;

        private SizeSR prevSize; // [SC] canvas size set for the world map

        private Dialogue dialogue = new Dialogue();

        private DialogueNode currNode;
        private string[] currentText;

        #endregion Fields

        #region Properties

        public Hub Hub {
            get;
            set;
        }

        public bool ToClear {
            get;
            set;
        }

        public bool WasCleared {
            get;
            private set;
        }

        #endregion Properties

        public void Init(Hub hub, params string[] paramArray) {
            this.WasCleared = false;
            this.cObjects = new List<ICollidableObject>();
            this.clickableObjects = new List<IClickable>();
            this.Hub = hub;

            // [SC] [0] dialogue id, [1] actor one id; [2] actor two id
            if (paramArray == null || paramArray.Length < 1 || paramArray.Length != 3) {
                // [TODO] critical error; invalid number of params, expected 2 params for thesaurus and distractors
            }

            // [SC] loading a config data
            XDocument configXdoc;
            IEnumerable<XElement> resourceElems = null;
            try {
                string xmlStr = this.Hub.Storage.Load(moduleConfigFilepath);
                configXdoc = XDocument.Parse(xmlStr);
                resourceElems = configXdoc.Root.Elements("local-resource");
            }
            catch (Exception ex) {
                // [TODO] critical error
                Debug.WriteLine("Error reading config xml.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            // [SC] load xml with scenarios data
            XDocument scenariosXdoc = null;
            try {
                string scenariosFilename = (from el in resourceElems
                                            where (string)el.Attribute("rid") == "" + DialogueModule.SCENARIO_FILE_ID
                                            select el).SingleOrDefault().Value;

                string xmlStr = this.Hub.Storage.Load(Path.Combine(moduleRootPath, scenariosFilename));
                scenariosXdoc = XDocument.Parse(xmlStr);

                // [TODO] verify that the scenarioXdoc has a valid structure
            }
            catch (Exception ex) {
                // [TODO] critical error
                Debug.WriteLine("Error reading scenario xml.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            // [SC] load all image files
            try {
                IEnumerable<XElement> imageElems = from el in resourceElems where (string)el.Attribute("type") == "image" select el;
                foreach (XElement imageElem in imageElems) {
                    int imageId = Int32.Parse(imageElem.Attribute("rid").Value);
                    string imageDescription = imageElem.Attribute("description").Value;
                    string imageFilename = imageElem.Value;

                    if (!GenericImage.HasInstance(imageId)) {
                        GenericImage.CreateInstance(imageId, imageDescription, Path.Combine(moduleRootPath, imageFilename));
                    }
                }
            }
            catch (Exception ex) {
                // [TODO] NON-critical error
                Debug.WriteLine("Error reading images.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            // [SC] load dialogue nodes and parse into this.dialogue object
            try {
                XElement xDialogue = (from el in scenariosXdoc.Root.Elements("Dialogue")
                                      where (string)el.Attribute("did") == paramArray[0]
                                      select el).SingleOrDefault();

                foreach (XElement xPara in xDialogue.Elements("Para")) {
                    this.dialogue.AddNode(Int32.Parse(xPara.Attribute("pos").Value),
                        Int32.Parse(xPara.Attribute("aindex").Value),
                        xPara.Value);
                }
            }
            catch (Exception ex) {
                // [TODO] critical error
                Debug.WriteLine("Error reading concepts and pairs from xml.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            // [SC] parse actor Ids and retrive actor instances
            if (paramArray.Length > 1) {
                int actorOneId, actorTwoId;
                if (Int32.TryParse(paramArray[1], out actorOneId)) {
                    this.actorOne = this.Hub.RunableGame.GetActor<IActor>(actorOneId);
                }

                if (Int32.TryParse(paramArray[2], out actorTwoId)) {
                    this.actorTwo = this.Hub.RunableGame.GetActor<IActor>(actorTwoId);
                }
            }

            this.bgImage = GenericImage.GetInstance(DialogueModule.BG_IMAGE_ID);

            // [SC] resizing the canvas
            ICanvas canvas = this.Hub.RunableGame.Canvas;
            this.prevSize = canvas.SizeSR;
            canvas.SizeSR = new SizeSR(DialogueModule.SCENE_WIDTH, DialogueModule.SCENE_HEIGHT);

            createModuleAssets();

            this.currNode = this.dialogue.GetNextNode();
            formatText();
        }

        public void createModuleAssets() {
            StubState overStubState = StubState.CreateInstance("over state");
            this.overStubStateId = overStubState.Id;
            
            this.prevBtn = new Actor("previous button", null);
            this.prevBtn.GetState((int)Hub.Reserved.STATE_IDLE).SetDefaultImage(DialogueModule.PREV_BTN_ID);
            this.prevBtn.AddState(State.CreateInstance(overStubState));
            this.prevBtn.GetState(this.overStubStateId).SetDefaultImage(DialogueModule.PREV_BTN_ID + 1);
            this.prevBtn.SetCurrentState((int)Hub.Reserved.STATE_IDLE);
            this.prevBtn.X = DialogueModule.PREV_BTN_POS.X;
            this.prevBtn.Y = DialogueModule.PREV_BTN_POS.Y;
            this.prevBtn.MouseClick += navBtnMouseClicked;
            this.clickableObjects.Add(this.prevBtn);

            this.nextBtn = new Actor("next button", null);
            this.nextBtn.GetState((int)Hub.Reserved.STATE_IDLE).SetDefaultImage(DialogueModule.NEXT_BTN_ID);
            this.nextBtn.AddState(State.CreateInstance(overStubState));
            this.nextBtn.GetState(this.overStubStateId).SetDefaultImage(DialogueModule.NEXT_BTN_ID + 1);
            this.nextBtn.SetCurrentState((int)Hub.Reserved.STATE_IDLE);
            this.nextBtn.X = DialogueModule.NEXT_BTN_POS.X;
            this.nextBtn.Y = DialogueModule.NEXT_BTN_POS.Y;
            this.nextBtn.MouseClick += navBtnMouseClicked;
            this.clickableObjects.Add(this.nextBtn);

            this.restartBtn = new Actor("restart button", null);
            this.restartBtn.GetState((int)Hub.Reserved.STATE_IDLE).SetDefaultImage(DialogueModule.RESTART_BTN_ID);
            this.restartBtn.AddState(State.CreateInstance(overStubState));
            this.restartBtn.GetState(this.overStubStateId).SetDefaultImage(DialogueModule.RESTART_BTN_ID + 1);
            this.restartBtn.SetCurrentState((int)Hub.Reserved.STATE_IDLE);
            this.restartBtn.X = DialogueModule.RESTART_BTN_POS.X;
            this.restartBtn.Y = DialogueModule.RESTART_BTN_POS.Y;
            this.restartBtn.MouseClick += navBtnMouseClicked;
            this.clickableObjects.Add(this.restartBtn);

            this.closeBtn = new Actor("close button", null);
            this.closeBtn.GetState((int)Hub.Reserved.STATE_IDLE).SetDefaultImage(DialogueModule.CLOSE_BTN_ID);
            this.closeBtn.AddState(State.CreateInstance(overStubState));
            this.closeBtn.GetState(this.overStubStateId).SetDefaultImage(DialogueModule.CLOSE_BTN_ID + 1);
            this.closeBtn.SetCurrentState((int)Hub.Reserved.STATE_IDLE);
            this.closeBtn.X = DialogueModule.CLOSE_BTN_POS.X;
            this.closeBtn.Y = DialogueModule.CLOSE_BTN_POS.Y;
            this.closeBtn.MouseClick += navBtnMouseClicked;
            this.clickableObjects.Add(this.closeBtn);
        }

        private void navBtnMouseClicked(object sender, SRMouseEventArgs e) {
            if (sender == this.prevBtn) {
                this.currNode = this.dialogue.GetPrevNode();
                formatText();
            }
            else if (sender == this.nextBtn) {
                this.currNode = this.dialogue.GetNextNode();
                formatText();
            }
            else if (sender == this.restartBtn) {
                this.dialogue.ResetDialogue();
                this.currNode = this.dialogue.GetNextNode();
                formatText();
            }
            else if (sender == this.closeBtn){
                this.ToClear = true;
            }
        }

        private void formatText() {
            ICanvas canvas = this.Hub.RunableGame.Canvas;
            
            string[] lines = this.currNode.Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            
            if (String.IsNullOrEmpty(lines[0])) {
                lines[0] = null;
            }

            if (String.IsNullOrEmpty(lines[lines.Length - 1])) {
                lines[lines.Length - 1] = null;
            }

            List<string> fLines = new List<string>();
            foreach (string line in lines) {
                if (line == null) continue;

                fLines.Add(line);

                /*SizeSR lineSize = canvas.GetTextSize(line, this.textFont, this.textFontSize);
                if (lineSize.Width > DialogueModule.TEXT_SIZE.Width) {

                }
                else {
                    fLines.Add(line);
                }*/ // [TODO]
            }

            this.currentText = fLines.ToArray();
        }

        public void Iterate() {
            if (this.ToClear && !this.WasCleared) {
                this.Clear();
                return;
            }
            else if (this.ToClear || this.WasCleared) {
                return;
            }

            int tickCount = Environment.TickCount;

            HandleEvents(tickCount);

            Animate(tickCount);

            Draw(tickCount);
        }

        public void HandleEvents(int tickCount) {
        }

        public void Animate(int tickCount) {
        }

        public void Draw(int tickCount) {
            ICanvas canvas = this.Hub.RunableGame.Canvas;

            canvas.Clear();

            // [SC] draw background image
            canvas.DrawImage(this.bgImage, 0, this.bgImage.Height - 1, this.bgImage.Width, this.bgImage.Height);

            canvas.DrawImage(this.prevBtn.Image, this.prevBtn.X, this.prevBtn.Y, this.prevBtn.Width, this.prevBtn.Height);
            canvas.DrawImage(this.nextBtn.Image, this.nextBtn.X, this.nextBtn.Y, this.nextBtn.Width, this.nextBtn.Height);
            canvas.DrawImage(this.restartBtn.Image, this.restartBtn.X, this.restartBtn.Y, this.restartBtn.Width, this.restartBtn.Height);
            canvas.DrawImage(this.closeBtn.Image, this.closeBtn.X, this.closeBtn.Y, this.closeBtn.Width, this.closeBtn.Height);

            if (this.actorOne != null) {
                canvas.DrawImage(this.actorOne.Portrait
                    , DialogueModule.ACTOR_ONE_POS.X, DialogueModule.ACTOR_ONE_POS.Y
                    , this.actorOne.Portrait.Width, this.actorOne.Portrait.Height);

                canvas.DrawText(this.actorOne.Name
                    , DialogueModule.ACTOR_ONE_NAME_POS.X, DialogueModule.ACTOR_ONE_NAME_POS.Y
                    , "Arial", 22, Color.Black);
            }

            if (this.actorTwo != null) {
                canvas.DrawImage(this.actorTwo.Portrait
                    , DialogueModule.ACTOR_TWO_POS.X, DialogueModule.ACTOR_TWO_POS.Y
                    , this.actorTwo.Portrait.Width, this.actorTwo.Portrait.Height);

                SizeSR nameSize = canvas.GetTextSize(this.actorTwo.Name, "Arial", 22);

                canvas.DrawText(this.actorTwo.Name
                    , DialogueModule.ACTOR_TWO_NAME_POS.X - nameSize.Width
                    , DialogueModule.ACTOR_TWO_NAME_POS.Y
                    , "Arial", 22, Color.Black);
            }

            if (this.currentText != null && this.currentText.Length > 0) {
                SizeSR textSize = canvas.GetTextSize("test", DialogueModule.textFont, DialogueModule.textFontSize);
                float currY = DialogueModule.TEXT_POS.Y;

                PointSR hhPos = DialogueModule.ACTOR_ONE_HH_POS;
                Color textColor = DialogueModule.ACTOR_ONE_COLOR;
                if (this.currNode.ActorIndex == 2) {
                    textColor = DialogueModule.ACTOR_TWO_COLOR;
                    hhPos = DialogueModule.ACTOR_TWO_HH_POS;
                }
                canvas.DrawRectangle(hhPos.X, hhPos.Y, 160, 160, textColor, 16);

                foreach(string line in this.currentText) {
                    if (line != null) {
                        currY += textSize.Height;

                        canvas.DrawText(line, DialogueModule.TEXT_POS.X, currY
                            , DialogueModule.textFont, DialogueModule.textFontSize, textColor);

                        currY += DialogueModule.vSpacing;
                    }
                }
            }
            
            canvas.DrawBufferToCanvas();
        }

        /// <summary>
        /// Call this method before at the end of the module use.
        /// </summary>
        public void Clear() {
            if (this.WasCleared || !this.ToClear) {
                return;
            }

            // [SC][TODO] Unload all images?

            // [SC] restore the original size of the canvas
            ICanvas canvas = this.Hub.RunableGame.Canvas;
            canvas.SizeSR = this.prevSize;

            // [SC] prevents the game from transferring control loop to the module
            this.Hub.RunableGame.ActiveModule = null;

            // [SC] indicate that the module was cleared
            this.WasCleared = true;
        }

        public void KeyDown(Keys key, bool upperCase) {
            if (key == Keys.Escape || key == Keys.Delete) {
                this.ToClear = true;
                Debug.WriteLine("ESCAPE Pressed.");
            }
            else if (key == Keys.Left) {
                this.currNode = this.dialogue.GetPrevNode();
                formatText();
            }
            else if (key == Keys.Right) {
                this.currNode = this.dialogue.GetNextNode();
                formatText();
            }
        }

        public void KeyUp(Keys key, bool upperCase) { }

        public IEnumerable<ICollidableObject> GetCollidableObjects() {
            return this.cObjects;
        }

        public IEnumerable<IClickable> GetClickableObjects() {
            return this.clickableObjects;
        }
    }
}
