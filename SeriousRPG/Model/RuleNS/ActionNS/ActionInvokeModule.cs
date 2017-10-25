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

using System.Windows.Forms;

using SeriousRPG.HubNS;

using SeriousRPG.Model.ModuleNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.ActorNS;

using SeriousRPG.Module.WebModuleNS;
using SeriousRPG.Module.VideoModuleNS;
using SeriousRPG.Module.AltThesaurusModuleNS;
using SeriousRPG.Module.CardMatchingModuleNS;
using SeriousRPG.Module.DialogueModuleNS;

namespace SeriousRPG.Model.RuleNS.ActionNS
{
    internal class ActionInvokeModule : Action 
    {
        #region Fields

        private Type moduleType;
        private string[] moduleParams;
        
        #endregion Fields

        #region Properties

        internal Type ModType {
            get { return this.moduleType; }
            set {
                this.moduleType = value;
            }
        }

        #endregion Properties

        #region Constructors

        internal ActionInvokeModule(string name, Type moduleType, string[] moduleParams) : base(name) {
            Init(moduleType, moduleParams);
        }

        internal ActionInvokeModule(int id, string name, Type moduleType, string[] moduleParams, bool isClone) : base(id, name, isClone) {
            Init(moduleType, moduleParams);
        }

        private void Init(Type moduleType, string[] moduleParams) {
            this.ModType = moduleType;
            this.moduleParams = moduleParams;
        }

        #endregion Constructors

        #region Methods

        public override bool Invoke(IRule parentRule) {
            RunableGame game = RunableGame.GetInstance();
            if (game == null) {
                // [TODO] error msg
                return false;
            }

            // [SC] make sure there is no active module currently present
            if (game.ActiveModule != null) { 
                // [TODO] error msg : cannot have more than one module active at the time
                return false;
            }

            ////////////////////////////////////////////////////////////////

            IModule module = null;

            if (this.ModType == typeof(WebModule)) { // [SC][TODO] update with new modules               
                module = new WebModule();
            }
            else if (this.ModType == typeof(VideoModule)) {
                module = new VideoModule();
            }
            else if (this.ModType == typeof(AltThesaurusModule)) {
                module = new AltThesaurusModule();
            }
            else if (this.ModType == typeof(CardMatchingModule)) {
                module = new CardMatchingModule();
            }
            else if (this.ModType == typeof(DialogueModule)) {
                module = new DialogueModule();
            }

            ////////////////////////////////////////////////////////////////

            if (module != null) {
                module.Init(Hub.GetInstance(), this.moduleParams);
                game.ActiveModule = module;
                return true;
            }
            else {
                return false;
            }
        }

        public override IAction Clone(IGame cloneGame) { // [TODO] should be IGame interface
            ActionInvokeModule cloneAction = new ActionInvokeModule(this.Id, this.Description, this.ModType, this.moduleParams, true);

            foreach (IActor actor in this.targetActors) {
                cloneAction.AddTargetActor(cloneGame.GetActor<IActor>(actor.Id));
            }

            // [TODO] should clone parent rules list here?
            return cloneAction;
        }

        #endregion Methods
    }
}
