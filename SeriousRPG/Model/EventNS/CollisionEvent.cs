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

namespace SeriousRPG.Model.EventNS 
{
    internal class CollisionEvent : SREvent
    {
        #region Fields

        private int actorOneId;
        private int actorTwoId;

        #endregion Fields

        #region Properties

        public int ActorOneId {
            get { return this.actorOneId; }
            private set { this.actorOneId = value; }
        }

        public int ActorTwoId {
            get { return this.actorTwoId; }
            private set { this.actorTwoId = value; }
        }

        #endregion Properties

        #region Constructors

        public CollisionEvent(string description, int actorOneId, int actorTwoId) : base(description) {
            this.ActorOneId = actorOneId;
            this.ActorTwoId = actorTwoId;
        }

        public CollisionEvent(int id, string description, int actorOneId, int actorTwoId) : base(id, description) {
            this.ActorOneId = actorOneId;
            this.ActorTwoId = actorTwoId;
        }

        #endregion Constructors

        #region Methods

        public bool HasActor(int id) {
            return this.ActorOneId == id || this.ActorTwoId == id;
        }

        public bool HasActors(int idOne, int idTwo) {
            return (this.ActorOneId == idOne && this.ActorTwoId == idTwo) ||
                (this.ActorOneId == idTwo && this.ActorTwoId == idOne);
        }

        #endregion Methods
    }
}
