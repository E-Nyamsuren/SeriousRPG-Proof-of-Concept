using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using SeriousRPG.HubNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.RuleNS;
using SeriousRPG.Model.RuleNS.ConditionNS;
using SeriousRPG.Model.RuleNS.ActionNS;

using SeriousRPG.Editor.ActionNS;

namespace SeriousRPG.Editor.RuleNS 
{
    public partial class QuickTriggerRuleForm : Form 
    {
        private Dictionary<Type, ModuleTypeInfo> moduleTypeInfo;
        private string delimiterPattern = @"\s+";

        public QuickTriggerRuleForm() {
            InitializeComponent();

            this.moduleTypeInfo = ModuleTypeInfo.GetModuleList();

            UpdateModuleTypeComboBox();
        }

        private void UpdateModuleTypeComboBox() {
            this.moduleComboBox.Items.Clear();

            foreach (KeyValuePair<Type, ModuleTypeInfo> entry in this.moduleTypeInfo) {
                this.moduleComboBox.Items.Add(entry.Value);
            }
        }

        private void okButton_Click(object sender, EventArgs e) {
            int ruleId;
            if (!Int32.TryParse(this.ruleIdTextBox.Text, out ruleId)) {
                MessageBox.Show("Invalid id value.");
                return;
            }

            if (String.IsNullOrEmpty(this.descriptionTextBox.Text)) {
                MessageBox.Show("Empty name.");
                return;
            }

            if (this.moduleComboBox.SelectedIndex < 0) {
                MessageBox.Show("Select module type.");
                return;
            }

            ModuleTypeInfo moduleTypeInfo = this.moduleComboBox.SelectedItem as ModuleTypeInfo;

            string[] moduleParams = null;
            if (!String.IsNullOrEmpty(this.paramTextBox.Text)) {
                moduleParams = Regex.Split(this.paramTextBox.Text, delimiterPattern);
                foreach (string moduleParam in moduleParams) {
                    if (String.IsNullOrEmpty(moduleParam)) {
                        MessageBox.Show(String.Format("Invalid parameter '{0}'.", moduleParam));
                        return;
                    }
                }
            }

            List<int> actorIdList = new List<int>();
            actorIdList.Add(Game.GetInstance().GetPlayer().Id);

            /////////////////////////////////////////////////////////////////////
            // [SC] adding a region trigger condition
            RegionTrigger regionTrigger = Game.GetInstance().AddRegionTrigger(this.descriptionTextBox.Text
                , RegionTrigger.DEFAULT_WIDTH, RegionTrigger.DEFAULT_HEIGHT, actorIdList);

            if (regionTrigger == null) {
                // [TODO] error creating a region trigger
                return;
            }

            /////////////////////////////////////////////////////////////////////
            // [SC] adding invoke module action
            ActionInvokeModule aim = Game.GetInstance().AddActionInvokeModule(this.descriptionTextBox.Text
                , moduleTypeInfo.Type, actorIdList, moduleParams);

            if (aim == null) {
                // [TODO] erro creating invoke module action
                return;
            }

            /////////////////////////////////////////////////////////////////////
            // [SC] adding the rule
            List<int> conditionIdList = new List<int>();
            conditionIdList.Add(regionTrigger.Id);

            List<int> actionIdList = new List<int>();
            actionIdList.Add(aim.Id);

            SeriousRPG.Model.RuleNS.Rule newRule = Game.GetInstance().AddRule(ruleId, this.descriptionTextBox.Text
                , false, conditionIdList, actionIdList);

            if (newRule == null) {
                // [TODO] erro creating invoke module action
                return;
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
