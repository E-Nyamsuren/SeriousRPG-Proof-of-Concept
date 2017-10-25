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

using SeriousRPG.Model.MapNS;
using SeriousRPG.HubNS;
using SeriousRPG.ControlIO;

namespace SeriousRPG.Model.ModuleNS 
{
    public interface IModule 
    {
        // [SC] options: 
        //  1. Show local video
        //  2. Show VOID
        //  3. Show local picture
        //  4. Show webpage
        //  5. Play audio
        //  6. Dialogue tree
        //  7. Single question/answer

        #region Properties

        Hub Hub {
            get;
            set;
        }

        /// <summary>
        /// Set to True to automatically call module's Clear function
        /// </summary>
        bool ToClear {
            get;
            set;
        }

        /// <summary>
        /// Set to True of the module's Clear function was already called
        /// </summary>
        bool WasCleared {
            get;
        }

        #endregion Properties

        #region Methods

        // [TODO]
        void Init(Hub hub, params string[] paramArray);

        void Iterate();

        void Animate(int tickCount);

        void HandleEvents(int tickCount);

        void Draw(int tickCount);

        /// <summary>
        /// Call this method before at the end of the module use.
        /// </summary>
        void Clear();

        void KeyDown(Keys key, bool upperCase);

        void KeyUp(Keys key, bool upperCase);

        IEnumerable<ICollidableObject> GetCollidableObjects();

        IEnumerable<IClickable> GetClickableObjects();

        #endregion Methods
    }
}
