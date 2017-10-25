using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SeriousRPG.Editor.ActionNS;
using SeriousRPG.Editor.ConditionNS;

using SeriousRPG.Model.RuleNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model;

namespace SeriousRPG.Editor.RuleNS {
    public partial class RuleForm : Form {
        public RuleForm() {
            InitializeComponent();

            UpdateRuleDGV();
        }

        private void UpdateRuleDGV() {
            this.ruleGridView.Rows.Clear();

            IEnumerable<IdNamePair> rules = Game.GetInstance().GetRuleList<IRule>();
            foreach (IdNamePair ruleObj in rules) {
                int id = ruleObj.Id;
                string description = ruleObj.Name;

                this.ruleGridView.Rows.Add(id, description);
            }
        }

        private void addRuleBtn_Click(object sender, EventArgs e) {
            new AddRuleForm().ShowDialog();
            UpdateRuleDGV();
        }

        private void editRuleBtn_Click(object sender, EventArgs e) {
            // [TODO]
        }

        private void removeRuleBtn_Click(object sender, EventArgs e) {
            // [TODO]
        }

        private void manageConditionsBtn_Click(object sender, EventArgs e) {
            new ConditionForm().ShowDialog();
        }

        private void manageActionsBtn_Click(object sender, EventArgs e) {
            new ActionForm().ShowDialog();
        }

        private void quickTriggerRuleBtn_Click(object sender, EventArgs e) {
            new QuickTriggerRuleForm().ShowDialog();
            UpdateRuleDGV();
        }
    }
}
