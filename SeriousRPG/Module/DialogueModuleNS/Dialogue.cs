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

using System.Diagnostics; // [TODO]

namespace SeriousRPG.Module.DialogueModuleNS
{
    class Dialogue 
    {
        private List<DialogueNode> dialogue = new List<DialogueNode>();

        private int currentIndex = -1;

        private bool sorted = false;

        public Dialogue(){}

        public bool AddNode(int position, int actorIndex, string text) { 
            if (actorIndex != 1 && actorIndex != 2) {
                return false;
            }

            if (text == null) {
                return false;
            }

            if (this.dialogue.Find(p => p.Position == position) != null) {
                return false;
            }

            // [SC] should not add new nodes one the Dialogue is used
            if (this.sorted) {
                return false;
            }

            this.dialogue.Add(new DialogueNode { 
                Position = position,
                ActorIndex = actorIndex,
                Text = text
            });

            return true;
        }

        public DialogueNode GetNextNode() {
            if (!this.sorted) {
                this.dialogue = this.dialogue.OrderBy(p => p.Position).ToList();
                this.sorted = true;
            }

            if (this.dialogue.Count == 0) {
                return null;
            }

            if (this.currentIndex == this.dialogue.Count - 1) {
                return this.dialogue[this.currentIndex];
            }

            int oldIndex = ++this.currentIndex;

            return this.dialogue[oldIndex];
        }

        public DialogueNode GetPrevNode() {
            if (!this.sorted) {
                this.dialogue = this.dialogue.OrderBy(p => p.Position).ToList();
                this.sorted = true;
            }

            if (this.dialogue.Count == 0) {
                return null;
            }

            if (this.currentIndex <= 0) {
                this.currentIndex = 0;
                return this.dialogue[this.currentIndex];
            }

            int oldIndex = --currentIndex;

            return this.dialogue[oldIndex];
        }

        public void ResetDialogue() {
            this.currentIndex = -1;
        }
    }

    class DialogueNode
    {
        public int Position {
            get;
            set;
        }

        public int ActorIndex {
            get;
            set;
        }

        public String Text {
            get;
            set;
        }
    }
}
