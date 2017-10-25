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

using SeriousRPG.Misc;
using SeriousRPG.Model.ActorNS;

namespace SeriousRPG.Model.ActorNS 
{
    public interface IDynamicActor : IActor 
    {
        #region Properties

        float MaxSpeed {
            get;
            set;
        }

        float CurrSpeed {
            get;
            set;
        }
        
        PointSR DestPoint {
            get;
            set;
        }

        IActor DestActor {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        // [TODO] these are private methods
        /*void IncreaseCurrSpeedX();

        void DecreaseCurrSpeedX();

        void IncreaseCurrSpeedY();

        void DecreaseCurrSpeedY();

        void ResetCurrentSpeedX();

        void ResetCurrentSpeedY();*/

        bool Move(float targetX, float targetY, float speed, bool updateState);

        bool Move(bool updateState);

        bool MoveUp(float speed, bool updateState);

        bool MoveUp(bool updateState);

        bool MoveDown(float speed, bool updateState);

        bool MoveDown(bool updateState);

        bool MoveLeft(float speed, bool updateState);

        bool MoveLeft(bool updateState);

        bool MoveRight(float speed, bool updateState);

        bool MoveRight(bool updateState);

        void SetIdle();

        #endregion Methods
    }
}
