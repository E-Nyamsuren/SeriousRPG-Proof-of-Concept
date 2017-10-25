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

using System.Drawing;

using SeriousRPG.Model.DrawingNS;

namespace SeriousRPG.Module.CardMatchingModuleNS 
{
    class Pair
    {
        public List<Concept> concepts = new List<Concept>();

        public string Id {
            get;
            set;
        }

        public string Instruction {
            get;
            set;
        }

        public Color ConceptTextColor {
            get;
            set;
        }

        public bool IsFound {
            get;
            set;
        }

        public bool IsTarget {
            get;
            set;
        }

        public Pair() { }

        public Pair(string id, string instruction) {
            this.Id = id;
            this.Instruction = instruction;
        }
    }
}
