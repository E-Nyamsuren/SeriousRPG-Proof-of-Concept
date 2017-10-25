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

using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.ActorNS;
using SeriousRPG.Model;

namespace SeriousRPG.Editor.ConditionNS 
{
    public partial class AddRegionTriggerForm : Form 
    {
        public AddRegionTriggerForm() {
            InitializeComponent();

            UpdateActorsDGV();
        }

        private void okBtn_Click(object sender, EventArgs e) {
            int regionId;
            if (!Int32.TryParse(this.regionIdTextBox.Text, out regionId)) {
                MessageBox.Show("Invalid id value.");
                return;
            }

            int width;
            if (!Int32.TryParse(this.regionWidthTextBox.Text, out width)) {
                MessageBox.Show("Invalid width value.");
                return;
            }

            int height;
            if (!Int32.TryParse(this.regionHeightTextBox.Text, out height)) {
                MessageBox.Show("Invalid height value.");
                return;
            }

            List<int> actorIdList = new List<int>();
            foreach (DataGridViewRow row in this.actorDataGridView.Rows) {
                if (Boolean.Parse(row.Cells["ActorSelected"].Value.ToString())) {
                    actorIdList.Add(Int32.Parse(row.Cells["ActorId"].Value.ToString()));
                }
            }

            if (Game.GetInstance().AddRegionTrigger(regionId, this.regionNameTextBox.Text, width, height, actorIdList) != null) {
                this.Close();
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

        private void cancelBtn_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
