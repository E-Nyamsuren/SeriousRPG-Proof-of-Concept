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
using SeriousRPG.Model.RuleNS.ConditionNS;
using SeriousRPG.Model.RuleNS.ActionNS;

namespace SeriousRPG.Model.RuleNS 
{
    public interface IRule 
    {
        #region Properties

        int Id {
            get;
        }

        string Description {
            get;
            set;
        }

        // [SC] true if the rule was already triggered
        bool IsTriggered {
            get;
        }

        // [SC] true if rule can be reset and retrigered again
        bool CanRepeat {
            get;
            set;
        }

        // [SC] if true then the rule will be automatically cleared and removed after execution
        bool ClearRuleFlag {
            get;
            set;
        }

        #endregion Properties

        #region ICondition methods

        bool AddCondition(ICondition condition);

        bool RemoveCondition(ICondition condition);

        bool RemoveCondition(int id);

        // [SC] remove all conditions from this rule
        void ClearConditions();

        /// <summary>
        /// Return true if all conditions in this rule are satisfied.
        /// </summary>
        /// <returns>bool</returns>
        bool IsSatisfied();

        #endregion ICondition methods

        #region IAction methods

        bool AddAction(IAction action);

        bool RemoveAction(IAction action);

        bool RemoveAction(int id);

        // [SC] remove all actions from this rule
        void ClearActions();

        /// <summary>
        /// Should perform all actions.
        /// </summary>
        void TriggerActions();

        #endregion IAction methods

        #region IActor methods

        /*bool AddActor(IActor actor); // [TODO]

        bool RemoveActor(IActor actor);

        bool RemoveActor(int id);

        IEnumerable<IActor> GetActors();*/

        #endregion IActor methods

        IRule Clone(IGame cloneGame); // [TODO] should be IGame interface

        // [SC] clear the rule of all conditions and actions
        void ClearRule();

        // [SC] Rule options: 
        //  1. If rule triggers invoke module
        //  2. If rule triggers change value(s)
        //  3. If rule triggers trigger another rule

        // [SC] Rule trigger conditions:
        //  1. No trigger (can be directly triggered by another rule only)
        //  2. Player reaches a region of interest (roi)
        //  3. Player's certain action
        //  4. Player's certain condition
    }
}
