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

using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.RuleNS;
using SeriousRPG.Model.ModuleNS;
using SeriousRPG.Model.MapNS;
using SeriousRPG.Model.EventNS;

using System.Diagnostics;

// [TODO] implementation is not complete

namespace SeriousRPG.Model.GameNS
{
    // [SC] a sealed class
    internal sealed partial class RunableGame : Game, IRunableGame
    {
        #region Constants and Statics

        internal const int DEFAULT_THREAD_SLEEP_TIME = 15;

        private static volatile RunableGame runableGame;        // [SC] ensure assignment berfore instance members are accessed
        private static object synchLock = new Object();         // [SC] used to lockon to prevent deadlocks

        #endregion Constants and Statics

        #region Fields

        private bool runGameFlag = true;

        private int threadSleepTime = RunableGame.DEFAULT_THREAD_SLEEP_TIME;

        private IModule activeModule; // [TODO]

        #endregion Fields

        #region Property

        internal bool RunGameFlag {
            get { return this.runGameFlag; }
            set { this.runGameFlag = value; }
        }

        internal int ThreadSleepTime {
            get { return this.threadSleepTime; }
            set {
                if (value >= 0) {
                    this.threadSleepTime = value;
                }
                else { 
                    // [TODO] error msg
                }
            }
        }

        public IModule ActiveModule {
            get { return this.activeModule; }
            set {
                if (this.activeModule != null && value != null) { 
                    // [TODO] error msg; cannot override active module
                }
                else if (value == null) {
                    if (this.activeModule != null) { // [SC] removing the active module
                        if (this.activeModule is IUiComponentModule) {
                            this.Canvas.UiModule = null;
                        }
                        this.activeModule = null;
                    }
                }
                else {
                    this.activeModule = value;
                    IUiComponentModule uiModule = this.activeModule as IUiComponentModule;
                    if (uiModule != null) {
                        this.Canvas.UiModule = uiModule;
                    }
                }
            }
        }

        #endregion Property

        #region Constructors

        private RunableGame() {}

        private RunableGame(int id, string name, ICanvas canvas) : base(id, name, canvas) { }

        #endregion Constructors

        #region Methods

        //internal void RunGame(BackgroundWorker gameWorker) {
        public void RunGame() {
            while (this.RunGameFlag) {
                Iterate();

                // [SC] sleep for a specified
                Thread.Sleep(this.ThreadSleepTime);
            }
        }

        public void Iterate() {
            if (this.RunGameFlag) {
                if (this.ActiveModule != null) {
                    this.ActiveModule.Iterate();
                }
                else {
                    // [SC] Invoke any pending events
                    HandleEvents();

                    // [SC] Change states and sprites of all actors
                    Animate();

                    // [SC] Draw the game
                    Draw();
                }
            }
        }

        public void Animate() {
            this.currentMap.Animate(Environment.TickCount);
        }

        public void HandleEvents() {
            // [SC] check if any rule should be triggered
            foreach (IRule ruleObj in this.rules.Values) {
                // [SC] Ignore the rule if it was already trigerred and cannot be repeated
                if (ruleObj.IsTriggered && !ruleObj.CanRepeat) {
                    continue;
                }

                if (ruleObj.IsSatisfied()) {
                    ruleObj.TriggerActions();

                    if (ruleObj.ClearRuleFlag) {
                        ruleObj.ClearRule();
                        this.rules.Remove(ruleObj.Id);
                    }
                }
            }

            this.currentMap.Act();
        }

        public void Draw() {
            this.Canvas.Clear();

            DrawMap();

            this.Canvas.DrawBufferToCanvas();
        }

        internal bool IsNoCollision(ICollidableObject clientObject) {
            if (this.ActiveModule != null) {
                bool noCollisionFlag = true;
                IEnumerable<ICollidableObject> objects = this.ActiveModule.GetCollidableObjects();
                if (objects != null) {
                    foreach (ICollidableObject targetObj in objects) {
                        if (targetObj.CollidesWith(clientObject)) {
                            EventRegistry.AddEvent(new CollisionEvent(
                                String.Format("{0} {1}", targetObj.Id, clientObject.Id), targetObj.Id, clientObject.Id));
                            noCollisionFlag = false;
                        }
                    }
                }
                return noCollisionFlag;
            }
            else {
                return this.currentMap.IsNoCollision(clientObject);
            }
        }

        #endregion Methods

        #region Player methods

        internal bool PlayerMoveUp() {
            return this.player.MoveUp(true);
        }

        internal bool PlayerMoveDown() {
            return this.player.MoveDown(true);
        }

        internal bool PlayerMoveLeft() {
            return this.player.MoveLeft(true);
        }

        internal bool PlayerMoveRight() {
            return this.player.MoveRight(true);
        }

        internal void PlayerIdle() {
            this.player.SetIdle();
        }

        #endregion Player methods

        #region Static methods

        public new static RunableGame CreateInstance(int id, string name, ICanvas canvas) {
            if (RunableGame.runableGame == null) {
                lock (RunableGame.synchLock) {
                    if (RunableGame.runableGame == null) {
                        RunableGame.runableGame = new RunableGame(id, name, canvas);
                    }
                }
            }

            return RunableGame.runableGame;
        }

        public new static RunableGame GetInstance() {
            lock (RunableGame.synchLock) {
                return RunableGame.runableGame;
            }
        }

        public static void ClearInstance() {
            lock (RunableGame.synchLock) {
                RunableGame.runableGame = null;
            }
        }

        #endregion Static methods
    }
}
