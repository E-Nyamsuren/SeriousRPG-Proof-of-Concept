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

using SeriousRPG.Model.ActorNS;
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.GameNS;

namespace SeriousRPG.Module.AltThesaurusModuleNS 
{
    internal class CustomEnemy : DynamicActor 
    {
        private string targetText = "default";
        private float textX = 0;
        private float textY = 0;

        public string TargetText {
            get { return this.targetText; }
            private set {
                if (!String.IsNullOrEmpty(value)) {
                    this.targetText = value;
                }
                else { 
                    // [TODO] error msg
                }
            }
        }

        public float TextX {
            get { return this.textX; }
            set {
                if (value >= 0) {
                    this.textX = value;
                }
                else {
                    // [TODO] error msg
                }
            }
        }

        public float TextY {
            get { return this.textY; }
            set {
                if (value >= 0) {
                    this.textY = value;
                }
                else {
                    // [TODO] error msg
                }
            }
        }

        internal CustomEnemy(string name, GenericImage defaultSprite, string targetText)
            : base(name, defaultSprite) {

            Init(targetText);
        } 

        internal CustomEnemy(int id, string name, GenericImage defaultSprite, bool isClone, string targetText)
            : base(id, name, defaultSprite, isClone) {
            
            Init(targetText);
        }

        private void Init(string targetText) {
            this.TargetText = targetText;
        }
    }
}
