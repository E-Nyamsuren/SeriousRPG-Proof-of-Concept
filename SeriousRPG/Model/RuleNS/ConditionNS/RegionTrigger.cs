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

using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.ActorNS;

namespace SeriousRPG.Model.RuleNS.ConditionNS
{
    internal class RegionTrigger : Condition 
    {
        #region Constants

        public const int MIN_WIDTH = 1;
        public const int MIN_HEIGHT = 1;

        public const int DEFAULT_WIDTH = 48;
        public const int DEFAULT_HEIGHT = 48;

        #endregion Constants

        #region Fields

        private int width = RegionTrigger.DEFAULT_WIDTH;
        private int height = RegionTrigger.DEFAULT_HEIGHT;

        private float x = Cfg.DEFAULT_X;
        private float y = Cfg.DEFAULT_Y;
        
        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets/sets region width in pixels.
        /// </summary>
        internal int Width {
            get { return this.width; }
            set {
                if (value >= RegionTrigger.MIN_WIDTH) {
                    this.width = value;
                }
                else { 
                    // [TODO] error msg
                }
            }
        }

        /// <summary>
        /// Gets/sets region height in pixels.
        /// </summary>
        internal int Height {
            get { return this.height; }
            set {
                if (value >= RegionTrigger.MIN_HEIGHT) {
                    this.height = value;
                }
                else {
                    // [TODO] error msg
                }
            }
        }

        /// <summary>
        /// Pixel coordinate of the left corners.
        /// </summary>
        internal float X {
            get { return this.x; }
            set {
                if (value >= 0) {
                    this.x = value;
                }
                else {
                    // [TODO] error msg
                }
            }
        }

        /// <summary>
        /// Pixel coordinate of the bottom corners.
        /// </summary>
        internal float Y {
            get { return this.y; }
            set {
                if (value >= 0) {
                    this.y = value;
                }
                else {
                    // [TODO] error msg
                }
            }
        }

        #endregion Properties

        #region Constructors

        internal RegionTrigger(string description, int width, int height) 
            : base(description) { 
            
            Init(width, height);
        }

        /// <summary>
        /// Should only be called by the Game instance
        /// </summary>
        internal RegionTrigger(int id, string description, int width, int height, bool isClone)
            : base(id, description, isClone) {

            Init(width, height);
        }

        private void Init(int width, int height) {
            this.Width = width;
            this.Height = height;
        }

        #endregion Constructors

        #region Methods
        
        public override bool IsTrue(IRule parentRule) {
            if (parentRule != null) {
                foreach (IActor actor in this.targetActors) {
                    if (Overlaps(actor.X, actor.Y, actor.Width, actor.Height)) {
                        return true;
                    }
                }
            }

            return false;
        }

        internal bool ContainsPoint(float x, float y) {
            float maxX = this.X + (this.Width - 1);
            float minY = this.Y - (this.Height - 1);

            return x >= this.X && x <= maxX && y >= minY && y <= this.Y;
        }

        /// <summary>
        /// Returns true of the region trigger overlaps with specified rectangle.
        /// </summary>
        /// <param name="x">        Right corner coordinate in pixels.</param>
        /// <param name="y">        BOTTOM corner coordinate in pixels.</param>
        /// <param name="width">    Width of the rectangle.</param>
        /// <param name="height">   Height of the rectangle.</param>
        /// <returns>Boolean</returns>
        internal bool Overlaps(float x, float y, int width, int height) {
            float maxXOne = this.X + (this.Width - 1);
            float minYOne = this.Y - (this.Height - 1);

            float maxXTwo = x + (width - 1);
            float minYTwo = y - (height - 1);

            if ((this.X <= maxXTwo && maxXOne >= x && minYOne <= y && this.Y >= y) ||
                (x <= maxXOne && maxXTwo >= this.X && minYTwo <= this.Y && y >= this.Y)) {
                return true;
            }
            else {
                return false;
            }
        }

        public override ICondition Clone(IGame cloneGame) { // [TODO] should be IGame interface
            RegionTrigger cloneRegionTrigger = new RegionTrigger(this.Id, this.Description, this.Width, this.Height, true);
            cloneRegionTrigger.X = this.X;
            cloneRegionTrigger.Y = this.Y;

            foreach (IActor actor in this.targetActors) {
                cloneRegionTrigger.AddTargetActor(cloneGame.GetActor<IActor>(actor.Id));
            }

            // [TODO] should clone parent rules list here?
            return cloneRegionTrigger;
        }

        #endregion Methods
    }
}
