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

using SeriousRPG.Module.WebModuleNS;
using SeriousRPG.Module.VideoModuleNS;
using SeriousRPG.Module.AltThesaurusModuleNS;
using SeriousRPG.Module.CardMatchingModuleNS;
using SeriousRPG.Module.DialogueModuleNS;

namespace SeriousRPG.Editor.ActionNS 
{
    public class ModuleTypeInfo 
    {
        public Type Type {
            get;
            set;
        }

        public string TypeDescription {
            get;
            set;
        }

        public override string ToString() {
            return this.TypeDescription;
        }

        public static Dictionary<Type, ModuleTypeInfo> GetModuleList() {
            Dictionary<Type, ModuleTypeInfo> moduleTypeInfo = new Dictionary<Type, ModuleTypeInfo>();
            AddModuleType(typeof(VideoModule), "Video module", moduleTypeInfo); // [SC][TODO] update with each new module type
            AddModuleType(typeof(WebModule), "Web module", moduleTypeInfo); // [SC][TODO] update with each new module type
            AddModuleType(typeof(AltThesaurusModule), "Thesaurus invaders module", moduleTypeInfo); // [SC][TODO] update with each new module type
            AddModuleType(typeof(CardMatchingModule), "Cardmatching module", moduleTypeInfo); // [SC][TODO] update with each new module type
            AddModuleType(typeof(DialogueModule), "Dialogue module", moduleTypeInfo); // [SC][TODO] update with each new module type

            return moduleTypeInfo;
        }

        private static void AddModuleType(Type type, string typeDescription, Dictionary<Type, ModuleTypeInfo> moduleTypeInfo) {
            moduleTypeInfo.Add(type, new ModuleTypeInfo { Type = type, TypeDescription = typeDescription });
        }
    }
}
