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

namespace SeriousRPG.Model.RuleNS.ActionNS
{
    internal abstract class Action : IAction
    {
        #region Fields

        /// <summary>
        /// Action's id unique within the game.
        /// </summary>
        private int id;

        /// <summary>
        /// Description of this instance.
        /// </summary>
        private string description;

        /// <summary>
        /// One or more parent rules that predicate on this action.
        /// </summary>
        protected List<IRule> parentRules;

        /// <summary>
        /// A list of actors on which the action be applied.
        /// </summary>
        protected List<IActor> targetActors;

        #endregion Fields

        #region Properties

        /// <summary>
        /// ID getter.
        /// </summary>
        public int Id {
            get { return this.id; }
            private set { this.id = value; }
        }

        /// <summary>
        /// Name getter/setter.
        /// </summary>
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

        #endregion Properties

        #region Constructors

        internal Action(string description) {
            this.Id = Hub.GetUniqueAutoId();

            Init(description);
        }

        internal Action(int id, string description, bool isClone) {
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

        private void Init(string description) {
            this.Description = description;

            this.parentRules = new List<IRule>();

            this.targetActors = new List<IActor>();
        }

        #endregion Constructors

        #region Finalizer

        ~Action() {
            Hub.DeregisterId(this.Id);
        }

        #endregion Finalizer

        #region Methods

        public bool AddRule(IRule parentRule) {
            if (parentRule == null) {
                // [TODO] error msg
                return false;
            }

            if (this.parentRules.Find(p => p.Id == parentRule.Id) != null) {
                // [TODO] duplicate rule error msg
                return false;
            }

            this.parentRules.Add(parentRule);
            parentRule.AddAction(this);
            return true;
        }

        public bool RemoveRule(IRule parentRule) {
            if (parentRule == null) {
                // [TODO] error msg
                return false;
            }

            if (this.parentRules.Remove(parentRule)) {
                parentRule.RemoveAction(this);
                return true;
            }
            else {
                return false;
            }
        }
        
        /// <summary>
        /// Add one or more actors on which the action be applied.
        /// </summary>
        /// <param name="actor">Actor</param>
        /// <returns>True if actor is successfully registered.</returns>
        public bool AddTargetActor(IActor actor) {
            if (actor == null) {
                // [TODO] error msg
                return false;
            }

            if (this.targetActors.Find(p => p.Id == actor.Id) != null) {
                // [TODO] duplcaite id msg
                return false;
            }

            this.targetActors.Add(actor);
            return false;
        }

        public bool RemoveTargetActor(IActor actor) {
            if (actor == null) {
                // [TODO] error msg
                return false;
            }

            return this.targetActors.Remove(actor);
        }

        public abstract bool Invoke(IRule parentRule);

        public abstract IAction Clone(IGame cloneGame); // [TODO] should be IGame interface

        #endregion Methods
    }
}
