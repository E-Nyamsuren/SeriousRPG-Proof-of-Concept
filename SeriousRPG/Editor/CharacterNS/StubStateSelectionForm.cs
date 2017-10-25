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

using SeriousRPG.Model;
using SeriousRPG.Model.StateNS;

namespace SeriousRPG.Editor.CharacterNS {
    public partial class StubStateSelectionForm : Form {

        private CharacterWizardForm parentForm;

        public StubStateSelectionForm(CharacterWizardForm parentForm) {
            InitializeComponent();

            this.parentForm = parentForm;

            updateStubStateList();
        }

        private void updateStubStateList() {
            this.stubStateListBox.Items.Clear();

            List<StubState> stubStates = StubState.GetAllInstances();
            foreach (StubState stubState in stubStates) {
                this.stubStateListBox.Items.Add(new IdNamePair(stubState.Id, stubState.Name));
            }

            this.stubStateListBox.SelectedIndex = -1;
        }

        private void addBtn_Click(object sender, EventArgs e) {
            if (this.stubStateListBox.SelectedIndex >= 0) {
                this.parentForm.addStubState((this.stubStateListBox.SelectedItem as IdNamePair).Id);
            }
        }

        private void closeBtn_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
