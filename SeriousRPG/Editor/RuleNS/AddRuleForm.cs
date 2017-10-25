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

using SeriousRPG.Model.RuleNS;
using SeriousRPG.Model.RuleNS.ActionNS;
using SeriousRPG.Model.RuleNS.ConditionNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model;

namespace SeriousRPG.Editor.RuleNS {
    public partial class AddRuleForm : Form {
        public AddRuleForm() {
            InitializeComponent();

            UpdateConditionDGV();
            UpdateActionDGV();
        }

        private void UpdateConditionDGV() {
            this.conditionGridView.Rows.Clear();

            IEnumerable<IdNamePair> conditions = Game.GetInstance().GetConditionList<ICondition>();
            foreach (IdNamePair condition in conditions) {
                int id = condition.Id;
                string description = condition.Name;

                this.conditionGridView.Rows.Add(id, description, false);
            }
        }

        private void UpdateActionDGV() {
            this.actionGridView.Rows.Clear();

            IEnumerable<IdNamePair> actions = Game.GetInstance().GetActionList<IAction>();
            foreach (IdNamePair action in actions) {
                int id = action.Id;
                string description = action.Name;

                this.actionGridView.Rows.Add(id, description, false);
            }
        }

        private void okBtn_Click(object sender, EventArgs e) {
            int ruleId;
            if (!Int32.TryParse(this.ruleIdTextBox.Text, out ruleId)) {
                MessageBox.Show("Invalid id value.");
                return;
            }

            if (String.IsNullOrEmpty(this.ruleDescriptionTextBox.Text)) {
                MessageBox.Show("Empty description.");
                return;
            }

            List<int> conditionIdList = new List<int>();
            foreach (DataGridViewRow row in this.conditionGridView.Rows) {
                if (Boolean.Parse(row.Cells["ConditionSelected"].Value.ToString())) {
                    conditionIdList.Add(Int32.Parse(row.Cells["ConditionId"].Value.ToString()));
                }
            }

            List<int> actionIdList = new List<int>();
            foreach (DataGridViewRow row in this.actionGridView.Rows) {
                if (Boolean.Parse(row.Cells["ActionSelected"].Value.ToString())) {
                    actionIdList.Add(Int32.Parse(row.Cells["ActionId"].Value.ToString()));
                }
            }

            if (Game.GetInstance().AddRule(ruleId
                , this.ruleDescriptionTextBox.Text
                , this.canRepeatCheckBox.Checked
                , conditionIdList, actionIdList) != null) {
                this.Close();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
