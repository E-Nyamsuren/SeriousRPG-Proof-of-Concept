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
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using SeriousRPG.HubNS;
using SeriousRPG.ControlIO;
using SeriousRPG.Model.ModuleNS;
using SeriousRPG.Model.MapNS;

namespace SeriousRPG.Module.VideoModuleNS 
{
    internal class VideoModule : Form, IModule 
    {

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


        private Video video;
        private VideoPlayer player;

        private GameServiceContainer gameServices;
        private ContentManager contentMgr;

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

        internal VideoModule() {
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.videoModule_Paint);

            this.gameServices = new GameServiceContainer();
            this.gameServices.AddService(typeof(IGraphicsDeviceService), GraphicsDeviceService.AddRef(this.Handle, this.Width, this.Height));
            this.contentMgr = new ContentManager(this.gameServices);

            //this.video = contentMgr.Load<Video>("C:\\OU\\Game\\resources\\DemoTwoANew.mp4");
            this.video = contentMgr.Load<Video>("..\\Content\\DemoTwoANew");
            this.player = new VideoPlayer();

            player.Play(this.video);
        }

        private void videoModule_Paint(object sender, PaintEventArgs e) {

            if (this.player.State != MediaState.Stopped) {
                MemoryStream ms = new MemoryStream();

                this.player.GetTexture().SaveAsPng(ms, 1024, 576);

                Image image = Image.FromStream(ms);

                e.Graphics.DrawImage(image, 0, 0);
            }
        }

        public void Init(Hub hub, params string[] paramArray) {
            this.WasCleared = false;
        }

        public void Iterate() {
            if (this.ToClear && !this.WasCleared) {
                this.Clear();
                return;
            }
            else if (this.ToClear || this.WasCleared) {
                return;
            }
        }

        public void Animate(int tickCount) { }

        public void HandleEvents(int tickCount) { }

        public void Draw(int tickCount) { }

        public void Clear() {
            if (this.WasCleared || !this.ToClear) {
                return;
            }

            this.Hub.RunableGame.ActiveModule = null;
            this.WasCleared = true;
        }

        public new void KeyDown(ControlIO.Keys key, bool upperCase) { }

        public new void KeyUp(ControlIO.Keys key, bool upperCase) { }

        public IEnumerable<ICollidableObject> GetCollidableObjects() {
            return null;
        }

        public IEnumerable<IClickable> GetClickableObjects() {
            return null;
        }
    }
}
