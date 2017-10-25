using System;
using System.Collections.Generic;
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

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using SeriousRPG.Model.ActorNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model;

namespace SeriousRPG.Editor.ActionNS 
{
    public partial class AddActionInvokeModule : Form 
    {
        private Dictionary<Type, ModuleTypeInfo> moduleTypeInfo;
        private string delimiterPattern = @"\s+";

        public AddActionInvokeModule() {
            InitializeComponent();

            this.moduleTypeInfo = ModuleTypeInfo.GetModuleList();

            UpdateModuleTypeComboBox();
            UpdateActorsDGV();
        }

        private void UpdateModuleTypeComboBox() {
            this.moduleComboBox.Items.Clear();

            foreach (KeyValuePair<Type, ModuleTypeInfo> entry in this.moduleTypeInfo) {
                this.moduleComboBox.Items.Add(entry.Value);
            }
        }

        private void UpdateActorsDGV() {
            this.actorDataGridView.Rows.Clear();

            IEnumerable<IdNamePair> actors = Game.GetInstance().GetActorIdList<IDynamicActor>();
            foreach (IdNamePair actor in actors) {
                int id = actor.Id;
                string name = actor.Name;
                this.actorDataGridView.Rows.Add(id, name, false);
            }
        }

        private void okButton_Click(object sender, EventArgs e) {
            int actionId;
            if (!Int32.TryParse(this.actionIdTextBox.Text, out actionId)) {
                MessageBox.Show("Invalid id value.");
                return;
            }

            if (String.IsNullOrEmpty(this.actionNameTextBox.Text)) {
                MessageBox.Show("Empty name.");
                return;
            }

            if (this.moduleComboBox.SelectedIndex < 0) {
                MessageBox.Show("Select module type.");
                return;
            }

            ModuleTypeInfo moduleTypeInfo = this.moduleComboBox.SelectedItem as ModuleTypeInfo;

            List<int> actorIdList = new List<int>();
            foreach (DataGridViewRow row in this.actorDataGridView.Rows) {
                if (Boolean.Parse(row.Cells["ActorSelected"].Value.ToString())) {
                    actorIdList.Add(Int32.Parse(row.Cells["ActorId"].Value.ToString()));
                }
            }

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

            if (Game.GetInstance().AddActionInvokeModule(actionId
                , this.actionNameTextBox.Text
                , moduleTypeInfo.Type, actorIdList, moduleParams) != null) {
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
