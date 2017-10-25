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
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.ActorNS;
using SeriousRPG.Model.RuleNS.ConditionNS;
using SeriousRPG.Model.RuleNS.ActionNS;

namespace SeriousRPG.Model.RuleNS 
{
    internal class Rule : IRule
    {
        #region Fields

        private int id;

        private string description;

        private bool isTriggered = false;

        private bool canRepeat = false;

        private bool clearRuleFlag = false;

        private List<ICondition> conditions;
        private List<IAction> actions;

        //private List<IActor> actors; // [TODO]

        #endregion Fields

        #region Properties

        public int Id {
            get { return this.id; }
            private set { this.id = value; }
        }

        public string Description {
            get { return this.description; }
            set {
                if (!String.IsNullOrEmpty(value)) {
                    this.description = value;
                }
                else {
                    // [TODO]
                }
            }
        }

        public bool IsTriggered {
            get { return this.isTriggered; }
        }

        public bool CanRepeat {
            get { return this.canRepeat; }
            set { this.canRepeat = value; }
        }

        public bool ClearRuleFlag {
            get { return this.clearRuleFlag; }
            set { this.clearRuleFlag = value; }
        }

        #endregion Properties

        #region Constructors

        internal Rule(string description) {
            this.Id = Hub.GetUniqueAutoId();

            Init(description);
        }

        internal Rule(int id, string description, bool isClone) {
            if (isClone) {
                this.Id = id;
            }
            else if (Hub.IsValidId(id)) {
                this.Id = id;
                Hub.RegisterId(id);
            }
            else {
                // [TODO] error msg
                this.Id = Hub.GetUniqueAutoId();
            }

            Init(description);
        }

        /// <summary>
        /// Called by the constructors.
        /// </summary>
        /// <param name="description"></param>
        private void Init(string description) {
            this.Description = description;

            this.conditions = new List<ICondition>();
            this.actions = new List<IAction>();

            //this.actors = new List<IActor>(); // [TODO]
        }

        #endregion Constructors

        #region Finalizer

        ~Rule() {
            Hub.DeregisterId(this.Id);
        }

        #endregion Finalizer

        #region Methods for Conditions

        public bool AddCondition(ICondition condition) {
            if (condition == null) {
                // [TODO] error msg
                return false;
            }

            if (this.conditions.Find(p => p.Id == condition.Id) != null) { 
                // [TODO] duplcaite id msg
                return false;
            }

            this.conditions.Add(condition);
            condition.AddRule(this);
            return true;
        }

        // [TODO] make sure removal does not interfere with anything
        public bool RemoveCondition(ICondition condition) {
            if (condition == null) {
                // [TODO] error msg
                return false;
            }

            if (this.conditions.Remove(condition)) {
                condition.RemoveRule(this);
                return true;
            }
            else {
                return false;
            }
        }

        public bool RemoveCondition(int id) {
            return RemoveCondition(this.conditions.Find(p => p.Id == id));
        }

        public void ClearConditions() {
            foreach (ICondition cond in this.conditions) {
                RemoveCondition(cond);
            }
        }

        public bool IsSatisfied() {
            if (this.conditions.Count == 0) {
                return false;
            }

            foreach (ICondition condition in this.conditions) {
                if (!condition.IsTrue(this)) {
                    return false;
                }
            }

            return true;
        }

        #endregion Methods for Conditions

        #region Methods for Actions

        public bool AddAction(IAction action) {
            if (action == null) {
                // [TODO] error msg
                return false;
            }

            if (this.actions.Find(p => p.Id == action.Id) != null) {
                // [TODO] duplcaite id msg
                return false;
            }

            this.actions.Add(action);
            action.AddRule(this);
            return true;
        }

        public bool RemoveAction(IAction action) {
            if (action == null) {
                // [TODO] error msg
                return false;
            }

            if (this.actions.Remove(action)) {
                action.RemoveRule(this);
                return true;
            }
            else {
                return false;
            }
        }

        public bool RemoveAction(int id) {
            return RemoveAction(this.actions.Find(p => p.Id == id));
        }

        public void ClearActions() {
            foreach (IAction action in this.actions) {
                RemoveAction(action);
            }
        }

        /// <summary>
        /// Should perform all actions.
        /// </summary>
        public void TriggerActions() {
            foreach (IAction action in this.actions) {
                action.Invoke(this);
            }
            this.isTriggered = true;
        }

        #endregion Methods for Actions

        public IRule Clone(IGame cloneGame) {
            Rule cloneRule = new Rule(this.Id, this.Description, true);
            cloneRule.CanRepeat = this.CanRepeat;

            foreach (ICondition condition in this.conditions) {
                cloneRule.AddCondition(cloneGame.GetCondition<ICondition>(condition.Id));
            }

            foreach (IAction action in this.actions) {
                cloneRule.AddAction(cloneGame.GetAction<IAction>(action.Id));
            }

            return cloneRule;
        }

        public void ClearRule() {
            ClearActions();
            ClearConditions();
        }

        #region Methods for Actors

        // [TODO]
        /*public bool AddActor(IActor actor) {
            if (actor == null) {
                // [TODO] error msg
                return false;
            }

            if (this.actors.Find(p => p.Id == actor.Id) != null) {
                // [TODO] duplcaite id msg
                return false;
            }

            this.actors.Add(actor);
            return true;
        }

        public bool RemoveActor(IActor actor) {
            if (actor == null) {
                // [TODO] error msg
                return false;
            }

            return this.actors.Remove(actor);
        }

        public bool RemoveActor(int id) {
            return RemoveActor(this.actors.Find(p => p.Id == id));
        }

        public IEnumerable<IActor> GetActors() {
            return this.actors;
        }*/

        #endregion Methods for Actors
    }
}
