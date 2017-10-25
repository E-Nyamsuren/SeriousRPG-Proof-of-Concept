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

using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.StateNS;

namespace SeriousRPG.Model.ActorNS
{
    public interface IActor 
    {
        #region Properties

        int Id {
            get;
        }

        string Name {
            get;
            set;
        }

        string Description {
            get;
            set;
        }

        float X {
            get;
            set;
        }

        float Y {
            get;
            set;
        }

        int Width {
            get;
            set;
        }

        int Height {
            get;
            set;
        }

        int Health {
            get;
            set;
        }

        int Experience {
            get;
            set;
        }

        int Level {
            get;
            set;
        }

        GenericImage Image {
            get;
            set;
        }

        GenericImage DefaultSprite {
            get;
            set;
        }

        GenericImage Portrait {
            get;
            set;
        }

        #endregion Properties

        #region State methods

        // [TODO] reference to inter
        bool AddState(State state);

        bool RemoveState(int stateId);

        void ClearStates();

        State GetState(int stateId);

        bool HasStates();

        bool HasState(int stateId);

        bool HasCurrentState();

        bool SetCurrentState(int stateId);

        int GetCurrentStateId();

        void Animate(long currentTime);

        #endregion State methods

        #region Misc methods

        IActor Clone();

        #endregion Misc methods
    }
}
