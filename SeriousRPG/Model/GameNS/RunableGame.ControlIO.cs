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

using System.Diagnostics;

using SeriousRPG.ControlIO;
using SeriousRPG.Model.ModuleNS;

namespace SeriousRPG.Model.GameNS
{
    internal sealed partial class RunableGame 
    {
        internal void MouseClick(SRMouseButton button, int x, int y) {
            if (this.ActiveModule != null) {
                // [SC] assume that mouse events are already handled by the UI component
                if (this.ActiveModule is IUiComponentModule) {
                    return;
                }

                IEnumerable<IClickable> objects = this.ActiveModule.GetClickableObjects();
                if (objects != null) {
                    foreach (IClickable targetObj in objects) {
                        if (targetObj.Contains(x, y)) {
                            targetObj.OnMouseClick(new SRMouseEventArgs(button, x, y));
                            return;
                        }
                    }
                }
            }
            else {
                // [TODO]
            }
        }

        internal void MouseDoubleClick(SRMouseButton button, int x, int y) {
            if (this.ActiveModule != null) {
                // [SC] assume that mouse events are already handled by the UI component
                if (this.ActiveModule is IUiComponentModule) {
                    return;
                }

                IEnumerable<IClickable> objects = this.ActiveModule.GetClickableObjects();
                if (objects != null) {
                    foreach (IClickable targetObj in objects) {
                        if (targetObj.Contains(x, y)) {
                            targetObj.OnMouseDoubleClick(new SRMouseEventArgs(button, x, y));
                            return;
                        }
                    }
                }
            }
            else {
                // [TODO]
            }
        }

        internal void MouseOver(SRMouseButton button, int x, int y) {
            if (this.ActiveModule != null) {
                // [SC] assume that mouse events are already handled by the UI component
                if (this.ActiveModule is IUiComponentModule) {
                    return;
                }

                IEnumerable<IClickable> objects = this.ActiveModule.GetClickableObjects();
                if (objects != null) {
                    foreach (IClickable targetObj in objects) {
                        if (targetObj.Contains(x, y)) {
                            targetObj.OnMouseOver(new SRMouseEventArgs(button, x, y));
                            return;
                        }
                    }
                }
            }
            else {
                // [TODO]
            }
        }

        internal void KeyDown(Keys key, bool upperCase) {

            // [TODO] verify if a special key was pressed down (e.g. game menu)

            // [SC] if there is an active module then transfer the key press to the module to handle
            if (this.ActiveModule != null) {
                this.ActiveModule.KeyDown(key, upperCase);
            }
            else { 
                switch (key) {
                    case Keys.Left:
                        this.PlayerMoveLeft();
                        break;
                    case Keys.Right:
                        this.PlayerMoveRight();
                        break;
                    case Keys.Down:
                        this.PlayerMoveDown();
                        break;
                    case Keys.Up:
                        this.PlayerMoveUp();
                        break;
                }
            }
        }

        internal void KeyUp(Keys key, bool upperCase) {
            // [TODO] verify if a special key was pressed up (e.g. game menu)

            // [SC] if there is an active module then transfer the key press to the module to handle
            if (this.ActiveModule != null) {
                this.ActiveModule.KeyUp(key, upperCase);
            }
            else {
                switch (key) {
                    case Keys.Left:
                        this.PlayerIdle();
                        break;
                    case Keys.Right:
                        this.PlayerIdle();
                        break;
                    case Keys.Down:
                        this.PlayerIdle();
                        break;
                    case Keys.Up:
                        this.PlayerIdle();
                        break;
                }
            }
        }
    }
}
