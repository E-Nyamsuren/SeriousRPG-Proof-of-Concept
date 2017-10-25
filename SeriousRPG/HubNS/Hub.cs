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

using SeriousRPG.FileIO;
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model;

namespace SeriousRPG.HubNS 
{
    // [SC] sealed class
    public sealed class Hub : IHub
    {
        #region Constants and Statics

        private static volatile Hub hub;                       // [SC] ensure assignment before instance members are accessed
        private static object synchLock = new Object();         // [SC] used to lockon to prevent deadlocks

        public const int AUTO_MIN_ID = -999999998;          // [SC] Note that 999999999 is Cfg.UNASSIGNED_INT
        public const int AUTO_MAX_ID = -2;
        private static int currentUniqueAutoId = Hub.AUTO_MIN_ID;
        private static HashSet<int> idList = new HashSet<int>();

        // [SC] reserved Ids
        internal enum Reserved : int {
            PLACEHOLDER_ID = -111,
            PORTRAIT_PLACEHOLDER_ID = -112,
            FALSE_ID = -113, 
            TRUE_ID = -114, 
            
            PLAYER_ID = -66667,

            // -224 is a default/idle state
            // -220 -221 -222
            // -223 -224 -225
            // -226 -227 -228
            STATE_UP_LEFT = -220,
            STATE_UP = -221,
            STATE_UP_RIGHT = -222,
            STATE_LEFT = -223,
            STATE_IDLE = -224,
            STATE_RIGHT = -225,
            STATE_DOWN_LEFT = -226,
            STATE_DOWN = -227,
            STATE_DOWN_RIGHT = -228,
        };

        #endregion Constants and Statics

        #region Properties

        public IStorage Storage {
            get { return FileIO.Storage.GetInstance(); }
        }

        public IRunableGame RunableGame {
            get { return SeriousRPG.Model.GameNS.RunableGame.GetInstance(); }
        }

        #endregion Properties

        #region Constructors

        private Hub() {}

        #endregion Constructors

        #region Static methods for singleton management 

        public static Hub CreateInstance() {
            if (Hub.hub == null) {
                lock (Hub.synchLock) {
                    if (Hub.hub == null) {
                        Hub.hub = new Hub();
                    }
                }
            }

            return Hub.hub;
        }

        public static Hub GetInstance() {
            return Hub.CreateInstance();
        }

        public static void ClearInstance() {
            lock (Hub.hub) {
                Hub.hub = null;
            }
        }

        #endregion Static methods for singleton management

        #region Static methods for Id tracking

        public static int GetUniqueAutoId() {
            while (Hub.currentUniqueAutoId <= Hub.AUTO_MAX_ID) {
                if (IsUniqueId(Hub.currentUniqueAutoId)) {
                    break;
                }
                else {
                    ++Hub.currentUniqueAutoId;
                }
            }

            if (Hub.currentUniqueAutoId <= Hub.AUTO_MAX_ID) {
                int newAutoId = Hub.currentUniqueAutoId++;
                RegisterId(newAutoId);
                return newAutoId;
            }
            else {
                // [TODO] error msg
                return Cfg.UNASSIGNED_INT;
            }
        }

        public static bool RegisterId(int id) {
            return idList.Add(id);
        }

        public static bool DeregisterId(int id) {
            return idList.Remove(id);
        }

        public static bool IsUniqueId(int id) {
            return !idList.Contains(id);
        }

        // [SC] returns true if the id is within the range from which auto-id is automatuically generated
        public static bool IsAutoId(int id) {
            return id >= Hub.AUTO_MIN_ID && id <= Hub.AUTO_MAX_ID;
        }

        public static bool IsReservedId(int id) {
            return Enum.IsDefined(typeof(Hub.Reserved), id);
        }

        // [SC] returns true if id is not registered and not within the scope of auto generated ids 
        public static bool IsValidId(int id) {
            return IsUniqueId(id) && (IsReservedId(id) || !IsAutoId(id));
        }

        #endregion Static methods for Id tracking
    }
}
