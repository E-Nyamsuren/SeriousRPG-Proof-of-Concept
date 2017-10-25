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

using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.ActorNS;

namespace SeriousRPG.Model.RuleNS.ActionNS
{
    public interface IAction 
    {
        /// 1. Start animation
        /// 2. Play asynch sound

        /// <summary>
        /// Id that is unique among all instances of IAction type in the game.
        /// </summary>
        int Id {
            get;
        }

        /// <summary>
        /// Description of this instance.
        /// </summary>
        string Description {
            get;
            set;
        }

        bool AddRule(IRule parentRule);

        bool RemoveRule(IRule parentRule);

        bool AddTargetActor(IActor actor);

        bool RemoveTargetActor(IActor actor);

        /// <summary>
        /// Invoke the action.
        /// </summary>
        /// <returns>Returns true if the action was invoked successfully.</returns>
        bool Invoke(IRule parentRule);

        IAction Clone(IGame cloneGame); // [TODO] should be IGame interface
    }
}
