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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SeriousRPG.Model.RuleNS.ActionNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model;

namespace SeriousRPG.Editor.ActionNS 
{
    public partial class ActionForm : Form 
    {
        private Dictionary<Type, ActionTypeInfo> actionTypeInfo;

        public ActionForm() {
            InitializeComponent();

            this.actionTypeInfo = new Dictionary<Type, ActionTypeInfo>();
            AddActionType(typeof(ActionInvokeModule), "Invoke Module"); // [SC][TODO] update with each new action type

            UpdateActionTypeComboBox();

            UpdateActionDGV();
        }

        private void addActionBtn_Click(object sender, EventArgs e) {
            if (this.actionTypeComboBox.SelectedIndex < 0) {
                // [TODO] warnign msg
                return;
            }

            ActionTypeInfo actionType = this.actionTypeComboBox.SelectedItem as ActionTypeInfo;

            if (actionType.Type == typeof(ActionInvokeModule)) { // [SC][TODO] update with each new action type
                new AddActionInvokeModule().ShowDialog();
            }

            UpdateActionDGV();
        }

        private void removeActionBtn_Click(object sender, EventArgs e) {
            // [TODO]
        }

        private void AddActionType(Type type, string typeDescription) {
            this.actionTypeInfo.Add(type, new ActionTypeInfo { Type = type, TypeDescription = typeDescription });
        }

        private void UpdateActionTypeComboBox() {
            this.actionTypeComboBox.Items.Clear();

            foreach (KeyValuePair<Type, ActionTypeInfo> entry in this.actionTypeInfo) {
                this.actionTypeComboBox.Items.Add(entry.Value);
            }
        }

        private void UpdateActionDGV() {
            this.actionGridView.Rows.Clear();

            IEnumerable<IdNamePair> actions = Game.GetInstance().GetActionList<IAction>();
            foreach (IdNamePair action in actions) {
                int id = action.Id;
                string type = this.actionTypeInfo[Game.GetInstance().GetAction<IAction>(id).GetType()].TypeDescription;
                string description = action.Name;

                this.actionGridView.Rows.Add(id, type, description);
            }
        }

        private class ActionTypeInfo {
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
        }
    }
}
