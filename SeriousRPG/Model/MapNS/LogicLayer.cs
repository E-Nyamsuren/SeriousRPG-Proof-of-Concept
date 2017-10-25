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

using System.Drawing; // [TODO] for Brush ad Brushes

using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.RuleNS.ConditionNS;

namespace SeriousRPG.Model.MapNS 
{
    internal class LogicLayer : GenericLayer
    {
        #region Constants

        internal const int LAYER_TYPE = 5;
        internal const string LAYER_NAME = "Logic Layer";

        internal static Brush TRIGGER_BRUSH = Brushes.Aquamarine;

        #endregion Constants
        
        #region Fields

        private List<RegionTrigger> regionTriggers;

        #endregion Fields

        #region Constructors

        internal LogicLayer(int rowNum, int colNum)
            : base(BackgroundOverlay.LAYER_TYPE, BackgroundOverlay.LAYER_NAME, rowNum, colNum) {
            this.regionTriggers = new List<RegionTrigger>();
        }

        #endregion Constructors

        #region RegionTrigger methods

        /// <summary>
        /// Adds a region trigger at the specified point.
        /// </summary>
        /// <param name="rTrigger"> RegionTrigger instance</param>
        /// <param name="x">        Right coordinate in pixels.</param>
        /// <param name="y">        Bottom coordinate in pixels.</param>
        /// <returns>True if the trigger was added successfully and false otherwise.</returns>
        internal bool AddRegionTrigger(RegionTrigger rTrigger, float x, float y) {
            if (rTrigger == null) {
                // [TODO] error msg
                return false;
            }
            else if (!ContainsRectangle(x, y, rTrigger.Width, rTrigger.Height)) {
                // [TODO] error msg
                return false;
            }
            else if (this.regionTriggers.Find(p => p.Id == rTrigger.Id) != null) {
                // [TODO] duplicate Id error msg
                rTrigger.X = x;
                rTrigger.Y = y;
                return false;
            }
            else {
                this.regionTriggers.Add(rTrigger);

                rTrigger.X = x;
                rTrigger.Y = y;

                return true;
            }
        }

        private RegionTrigger GetRegionTrigger(float x, float y) {
            for (int index = this.regionTriggers.Count - 1; index >= 0; index--) {
                RegionTrigger rTrigger = this.regionTriggers[index];
                if (rTrigger.ContainsPoint(x, y)) {
                    return rTrigger;
                }
            }

            return null;
        }

        internal int GetRegionTriggerId(float x, float y) {
            RegionTrigger rTrigger = GetRegionTrigger(x, y);
            if (rTrigger != null) {
                return rTrigger.Id;
            }
            else {
                return Cfg.UNASSIGNED_INT;
            }
        }

        internal List<IdNamePair> GetRegionTriggerList() {
            List<IdNamePair> regionTriggerList = new List<IdNamePair>();
            foreach (RegionTrigger rTrigger in this.regionTriggers) {
                regionTriggerList.Add(new IdNamePair(rTrigger.Id, rTrigger.Description));
            }
            return regionTriggerList;
        }

        /// <summary>
        /// Removes a region trigger at the specified point.
        /// </summary>
        /// <param name="x">        Right coordinate in pixels.</param>
        /// <param name="y">        Bottom coordinate in pixels.</param>
        /// <returns>True if the trigger was removed successfully and false otherwise.</returns>
        internal bool RemoveRegionTrigger(float x, float y) {
            return this.regionTriggers.Remove(GetRegionTrigger(x, y));
        }

        // [DELETE][TODO] remove if not necessary
        /*internal List<RegionTrigger> CloneRegionTriggers() {
            List<RegionTrigger> regionTriggerClones = new List<RegionTrigger>();
            foreach (RegionTrigger rTrigger in this.regionTriggers) {
                regionTriggerClones.Add(rTrigger.Clone());
            }
            return regionTriggerClones;
        }*/
        
        #endregion RegionTrigger methods

        #region Layer methods

        override internal void Act() { }

        override internal void Draw(ICanvas canvas) {
            if (this.IsRendered) {
                foreach (RegionTrigger rTrigger in this.regionTriggers) {
                    canvas.DrawRectangle(rTrigger.X, rTrigger.Y, rTrigger.Width, rTrigger.Height, LogicLayer.TRIGGER_BRUSH);
                }
            }
        }

        override internal void Resize(int rowNum, int colNum) {
            throw new NotImplementedException(); // [TODO]
        }

        override internal bool CanResize(int rowNum, int colNum) {
            throw new NotImplementedException(); // [TODO]
        }

        #endregion Layer methods
    }
}
