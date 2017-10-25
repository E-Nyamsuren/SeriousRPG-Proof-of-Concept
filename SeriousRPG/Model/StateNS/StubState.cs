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

using SeriousRPG.HubNS;

namespace SeriousRPG.Model.StateNS 
{
    // [SC] to be used in the editor to form a list of available states that can be assigned to an actor; not an actual actor state
    public class StubState 
    {
        #region Constants
        
        private static Dictionary<int, StubState> abstractStates;

        #endregion Constants

        #region Fields

        /// <summary>
        /// ID of the state.
        /// </summary>
        private int id;

        /// <summary>
        /// String description of the state.
        /// </summary>
        private string name;

        /// <summary>
        /// True if it is a core abstract state that cannot be removed from editor listing.
        /// </summary>
        private bool coreState;

        #endregion Fields

        #region Properties

        /// <summary>
        /// ID getter/setter.
        /// </summary>
        internal int Id {
            get { return this.id; }
            private set { this.id = value; }
        }

        /// <summary>
        /// Name getter/setter.
        /// </summary>
        internal string Name {
            get { return this.name; }
            set {
                if (!String.IsNullOrEmpty(value)) {
                    this.name = value;
                }
                else {
                    // [TODO]
                }
            }
        }

        internal bool CoreState {
            get { return this.coreState; }
            private set { this.coreState = value; }
        }

        #endregion Properties

        #region Constructors

        static StubState() {
            if (StubState.abstractStates == null) {
                StubState.abstractStates = new Dictionary<int, StubState>();
            }

            StubState.CreateInstance((int)Hub.Reserved.STATE_UP_LEFT, "Move up-left", true);
            StubState.CreateInstance((int)Hub.Reserved.STATE_UP, "Move up", true);
            StubState.CreateInstance((int)Hub.Reserved.STATE_UP_RIGHT, "Move up-right", true);
            StubState.CreateInstance((int)Hub.Reserved.STATE_LEFT, "Move left", true);
            StubState.CreateInstance((int)Hub.Reserved.STATE_IDLE, "Idle", true);
            StubState.CreateInstance((int)Hub.Reserved.STATE_RIGHT, "Move right", true);
            StubState.CreateInstance((int)Hub.Reserved.STATE_DOWN_LEFT, "Move down-left", true);
            StubState.CreateInstance((int)Hub.Reserved.STATE_DOWN, "Move down", true);
            StubState.CreateInstance((int)Hub.Reserved.STATE_DOWN_RIGHT, "Move down-right", true);
        }

        protected StubState() { }

        protected StubState(int id, string name, bool coreState) {
            this.Id = id;
            this.Name = name;
            this.CoreState = coreState;
        }

        #endregion Constructors

        #region Static methods

        /// <summary>
        /// Factory method for creating instances of the AbstractState class.
        /// Should be called only internally.
        /// </summary>
        /// <param name="id">           State id</param>
        /// <param name="name">         State name</param>
        /// <param name="coreState">    True if state is a core state</param>
        /// <returns></returns>
        private static StubState CreateInstance(int id, string name, bool coreState) {
            if (String.IsNullOrEmpty(name)) {
                // [TODO] warning message
                return null;
            }
            else if (StubState.abstractStates.ContainsKey(id) || !Hub.IsValidId(id)) {
                // [TODO] warning message
                return null;
            }
            else {
                // [SC] register the id with the Hub
                Hub.RegisterId(id);

                return CreateInstanceHelper(id, name, coreState);
            }
        }

        private static StubState CreateInstance(string name, bool coreState) {
            if (String.IsNullOrEmpty(name)) {
                // [TODO] warning message
                return null;
            }

            return CreateInstanceHelper(Hub.GetUniqueAutoId(), name, coreState);
        }

        internal static StubState CreateInstance(int id, string name) {
            return CreateInstance(id, name, false);
        }

        internal static StubState CreateInstance(string name) {
            return CreateInstance(name, false);
        }

        private static StubState CreateInstanceHelper(int id, string name, bool coreState) {
            // [SC] create instance
            StubState state = new StubState(id, name, coreState);
            // [SC] register new instance
            StubState.abstractStates.Add(id, state);

            return state;
        }

        internal static StubState GetInstance(int id) {
            if (HasInstance(id)) {
                return StubState.abstractStates[id];
            }
            else {
                // [TODO] error msg
                return null;
            }
        }

        internal static bool HasInstance(int id) {
            return StubState.abstractStates.ContainsKey(id);
        }

        internal static bool RemoveInstance(int id) {
            // [TODO] before remove the instance need to make sure it is not used anywhere
            // [TODO] make sure it is not a core state
            throw new NotImplementedException();
        }

        internal static bool IsCoreState(int id) {
            StubState state = GetInstance(id);
            return state != null && state.CoreState;
        }

        internal static int GetInstanceCount() {
            return StubState.abstractStates.Count;
        }

        internal static List<StubState> GetAllInstances() {
            return StubState.abstractStates.Values.ToList<StubState>();
        }

        #endregion Static methods
    }
}