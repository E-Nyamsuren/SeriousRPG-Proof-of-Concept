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

using SeriousRPG.Model.EventNS;
using SeriousRPG.Model.GameNS;

namespace SeriousRPG.Model.RuleNS.ActionNS 
{
    internal class ActionRemoveEvent : Action 
    {
        #region Fields

        private int eventId = Cfg.UNASSIGNED_INT;

        #endregion Fields

        #region Properties

        public int EventId {
            get { return this.eventId; }
            private set { this.eventId = value; }
        }

        #endregion Properties

        #region Constructors

        internal ActionRemoveEvent(string description, int eventId) 
            : base(description) {
            
            Init(eventId);
        }

        internal ActionRemoveEvent(int id, string description, int eventId, bool isClone) 
            : base(id, description, isClone) {
            
            Init(eventId);
        }

        private void Init(int eventId) {
            this.EventId = eventId;
        }

        #endregion Constructors

        #region Methods

        public override bool Invoke(IRule parentRule) {
            return EventRegistry.RemoveEvent(this.EventId);
        }

        public override IAction Clone(IGame cloneGame) { // [TODO] should be IGame interface
            return new ActionRemoveEvent(this.Id, this.Description, this.EventId, true);
        }

        #endregion Methods
    }
}
