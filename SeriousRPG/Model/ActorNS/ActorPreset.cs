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

namespace SeriousRPG.Model.ActorNS {
    public class ActorPreset 
    {
        #region Statics and Constants
        
        private static Dictionary<int, ActorPreset> actorPresets;

        #endregion Statics and Constants
        
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }

        public int PortraitId { get; set; }
        public int DefaultSpriteId { get; set; }

        public int DownSaId { get; set; }
        public int LeftSaId { get; set; }
        public int RightSaId { get; set; }
        public int UpSaId { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Static constructor
        /// </summary>
        static ActorPreset() {
            if (ActorPreset.actorPresets == null) {
                ActorPreset.actorPresets = new Dictionary<int, ActorPreset>();
            }
        }

        public ActorPreset() {
        }

        #endregion Constructors

        #region Static methods

        public static bool AddPreset(ActorPreset preset) {
            if (ActorPreset.actorPresets.ContainsKey(preset.Id)) {
                // [TODO] duplicate id error msg
                return false;
            }

            ActorPreset.actorPresets.Add(preset.Id, preset);

            return true;
        }

        internal static List<int> GetPresetIdList() {
            return ActorPreset.actorPresets.Keys.ToList<int>();
        }

        internal static ActorPreset GetPreset(int presetId) {
            if (HasPreset(presetId)) {
                return ActorPreset.actorPresets[presetId];
            }
            else {
                // [TODO] error msg
                return null;
            }
        }

        internal static bool HasPreset(int presetId) {
            return ActorPreset.actorPresets.ContainsKey(presetId);
        }

        #endregion Static methods
    }
}
