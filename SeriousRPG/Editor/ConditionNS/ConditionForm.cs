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

using System.Diagnostics; // [TODO]

using SeriousRPG.Model.RuleNS.ConditionNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model;

namespace SeriousRPG.Editor.ConditionNS 
{
    public partial class ConditionForm : Form 
    {
        private Dictionary<Type, ConditionTypeInfo> condTypeInfo;

        public ConditionForm() {
            InitializeComponent();

            this.condTypeInfo = new Dictionary<Type, ConditionTypeInfo>();
            AddConditionType(typeof(RegionTrigger), "Region trigger"); // [SC][TODO] update with each new condition type

            UpdateConditionTypeComboBox();

            UpdateConditionDGV();
        }

        private void addConditionBtn_Click(object sender, EventArgs e) {
            if (this.conditionTypeComboBox.SelectedIndex < 0) {
                // [TODO] warnign msg
                return;
            }

            ConditionTypeInfo conditionType = this.conditionTypeComboBox.SelectedItem as ConditionTypeInfo;

            if (conditionType.Type == typeof(RegionTrigger)) { // [SC][TODO] update with each new condition type
                new AddRegionTriggerForm().ShowDialog();
            }

            UpdateConditionDGV();
        }

        private void removeConditionBtn_Click(object sender, EventArgs e) {
            // [TODO]
        }

        private void AddConditionType(Type type, string typeDescription) {
            this.condTypeInfo.Add(type, new ConditionTypeInfo { Type = type, TypeDescription = typeDescription });
        }

        private void UpdateConditionTypeComboBox() {
            this.conditionTypeComboBox.Items.Clear();

            foreach (KeyValuePair<Type, ConditionTypeInfo> entry in this.condTypeInfo) {
                this.conditionTypeComboBox.Items.Add(entry.Value);
            }
        }

        private void UpdateConditionDGV() {
            this.conditionGridView.Rows.Clear();

            IEnumerable<IdNamePair> conditions = Game.GetInstance().GetConditionList<ICondition>();
            foreach (IdNamePair condition in conditions) {
                int id = condition.Id;
                string type = this.condTypeInfo[Game.GetInstance().GetCondition<ICondition>(id).GetType()].TypeDescription;
                string description = condition.Name;

                this.conditionGridView.Rows.Add(id, type, description);
            }
        }

        private class ConditionTypeInfo 
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
        }
    }
}
