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
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.ImageObjectNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.StateNS;

namespace SeriousRPG.Model.ActorNS
{
    internal class Player : DynamicActor, IPlayer
    {
        #region Constructors

        internal Player(string name, GenericImage defaultSprite) 
            : base(name, defaultSprite) {
        }

        internal Player(int id, string name, GenericImage defaultSprite, bool isClone) 
            : base (id, name, defaultSprite, isClone) {
        }

        #endregion Constructors

        #region Methods

        internal new Player Clone() {
            Player clonePlayer = new Player(this.Id, this.Name, this.DefaultSprite.Clone(), true);

            clonePlayer.Description = this.Description; // [TODO] copy string
            
            clonePlayer.Portrait = this.Portrait.Clone();

            clonePlayer.Health = this.Health;
            clonePlayer.Experience = this.Experience;
            clonePlayer.Level = this.Level;

            clonePlayer.CanCollide = this.CanCollide;
            clonePlayer.IsClickable = this.IsClickable;

            clonePlayer.X = this.X;
            clonePlayer.Y = this.Y;
            
            clonePlayer.ClearStates();
            foreach (State state in this.states) {
                clonePlayer.AddState(state.Clone());
            }

            if (HasCurrentState()) {
                clonePlayer.SetCurrentState(GetCurrentStateId());
            }

            clonePlayer.MaxSpeed = this.MaxSpeed;
            clonePlayer.CurrSpeed = this.CurrSpeed;
            clonePlayer.DestPoint.X = this.DestPoint.X;
            clonePlayer.DestPoint.Y = this.DestPoint.Y;
            // clonePlayer.DestActor // [TODO]

            return clonePlayer;
        }

        #endregion Methods
    }
}
