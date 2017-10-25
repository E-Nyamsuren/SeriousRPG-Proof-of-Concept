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
using System.Windows.Forms;
using System.Drawing; // [SC] for Point
using System.Xml;
using System.Xml.Linq;

using SeriousRPG.HubNS;
using SeriousRPG.ControlIO;
using SeriousRPG.Model.ModuleNS;
using SeriousRPG.Model.MapNS;

namespace SeriousRPG.Module.WebModuleNS
{
    class WebModule : Panel, IUiComponentModule 
    {
        #region Fields

        private const string moduleRootPath = "Module/WebModuleNS/res/";
        private static string moduleConfigFilepath = Path.Combine(moduleRootPath, "cfg.xml");

        private Uri uri = new Uri("http://www.bcogs.net");

        private WebBrowser browser;

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

        public Uri Link {
            get {
                return this.uri;
            }
        }

        #endregion Properties

        #region Constructors

        internal WebModule() {}

        #endregion Constructors

        #region Methods

        public void Init(Hub hub, params string[] paramArray) {
            this.WasCleared = false;
            this.Width = 900;
            this.Height = 530;

            this.Hub = hub;

            // [TODO]
            if (paramArray != null && paramArray.Length > 0) {
                try {
                    string xmlStr = this.Hub.Storage.Load(moduleConfigFilepath);

                    XDocument xdoc = XDocument.Parse(xmlStr);

                    XElement onlineRes = 
                        (from el in xdoc.Root.Elements("online-resource")
                        where (string)el.Attribute("id") == paramArray[0]
                        select el).SingleOrDefault();

                    this.uri = new Uri(onlineRes.Attribute("uri").Value);
                }
                catch (Exception ex) {
                    // [TODO]
                }
            }

            this.browser = new WebBrowser();
            this.browser.Width = 900;
            this.browser.Height = 500;
            this.browser.Location = new Point(0, 0);
            this.Controls.Add(browser);

            this.browser.Navigate(this.Link);// [TODO]

            Button closeBtn = new Button {
                Width = 100,
                Height = 30,
                Location = new Point(0, 500),
            };
            closeBtn.Click += new EventHandler(this.closeBtn_Click);
            this.Controls.Add(closeBtn);
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

        #endregion Methods

        #region Button event handler

        private void closeBtn_Click(object sender, EventArgs e) {
            this.ToClear = true;
        }

        #endregion Button event handler
    }
}
