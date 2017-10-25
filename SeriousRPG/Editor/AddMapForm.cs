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

namespace SeriousRPG.Editor {
    public partial class AddMapForm : Form {

        private MapEditorForm mapEditorForm;

        public AddMapForm(MapEditorForm mapEditorForm) {
            InitializeComponent();
            this.mapEditorForm = mapEditorForm;
        }

        private void okBtn_Click(object sender, EventArgs e) {
            int mapId;
            if (!Int32.TryParse(this.mapIdTextBox.Text, out mapId)) {
                MessageBox.Show("Invalid id value.");
                return;
            }

            int rowNum;
            if (!Int32.TryParse(this.mapHeightTextBox.Text, out rowNum)) {
                MessageBox.Show("Invalid height value.");
                return;
            }

            int colNum;
            if (!Int32.TryParse(this.mapWidthTextBox.Text, out colNum)) {
                MessageBox.Show("Invalid width value.");
                return;
            }

            if (Game.GetInstance().AddMap(mapId, this.mapNameTextBox.Text, rowNum, colNum)) {
                this.mapEditorForm.UpdateMapList();
                this.Close();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
