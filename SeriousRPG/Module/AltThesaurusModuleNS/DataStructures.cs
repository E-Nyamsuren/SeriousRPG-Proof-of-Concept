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

namespace SeriousRPG.Module.AltThesaurusModuleNS 
{
    public class Concept 
    {
        public string Id {
            get;
            set;
        }

        public string Text {
            get;
            set;
        }
    }

    public class Item 
    {
        public List<Concept> concepts = new List<Concept>();

        public Concept TargetConcept {
            get;
            set;
        }

        public bool Shuffle {
            get;
            set;
        }

        public int Id {
            get;
            set;
        }
    }
}
