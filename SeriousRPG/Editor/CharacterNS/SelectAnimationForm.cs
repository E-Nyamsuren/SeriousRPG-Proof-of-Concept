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

using System.Threading;

using SeriousRPG.Model;
using SeriousRPG.Model.AnimationNS;
using SeriousRPG.Model.DrawingNS;

namespace SeriousRPG.Editor.CharacterNS {
    public partial class SelectAnimationForm : Form {

        private CharacterWizardForm parentForm;
        private BackgroundWorker bw;

        private int delay = 0;
        private int currIndex = 0;
        private List<GenericImage> images = new List<GenericImage>();

        public SelectAnimationForm(CharacterWizardForm parentForm, bool select, int selectedAnimeId) {
            this.parentForm = parentForm;

            InitializeComponent();

            updateStubAnimeList(select, selectedAnimeId);
        }

        private void updateStubAnimeList(bool select, int selectedAnimeId) {
            this.animeListBox.Items.Clear();

            List<StubAnimation> saList = StubAnimation.GetAllInstances();
            foreach (StubAnimation sa in saList) {
                IdNamePair pair = new IdNamePair(sa.Id, "");
                int index = this.animeListBox.Items.Add(pair);

                if (selectedAnimeId == pair.Id) {
                    this.animeListBox.SelectedIndex = index;
                }
            }
        }

        private void animeListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.animeListBox.SelectedIndex >= 0) {
                int saId = (this.animeListBox.SelectedItem as IdNamePair).Id;
                StubAnimation sa = StubAnimation.GetInstance(saId);

                if (this.bw == null) {
                    this.bw = new BackgroundWorker();
                    this.bw.WorkerSupportsCancellation = true;

                    // what to do in the background thread
                    bw.DoWork += new DoWorkEventHandler(
                        delegate(object o, DoWorkEventArgs args) {
                            BackgroundWorker b = o as BackgroundWorker;

                            while (true) {
                                if (b.CancellationPending) {
                                    args.Cancel = true;
                                    break;
                                }

                                this.animePictureBox.Image = images[currIndex++].Image;
                                if (currIndex >= images.Count) {
                                    currIndex = 0;
                                }

                                Thread.Sleep(this.delay);
                            }
                        }
                    );
                }
                else {
                    this.bw.CancelAsync();
                    waitThreadStop();
                }

                this.delay = sa.SpriteDelay;
                this.currIndex = 0;
                this.images = sa.GetSprites();
                bw.RunWorkerAsync();
            }
        }

        private void waitThreadStop () {
            while (true) {
                if (!this.bw.IsBusy) {
                    break;
                }

                Application.DoEvents();

                Thread.Sleep(50);
            }
        }

        private void okBtn_Click(object sender, EventArgs e) {
            if (this.animeListBox.SelectedIndex < 0) {
                return;
            }

            int saId = (this.animeListBox.SelectedItem as IdNamePair).Id;

            this.parentForm.StubAnimationId = saId;

            this.bw.CancelAsync();
            waitThreadStop();

            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e) {
            this.bw.CancelAsync();
            waitThreadStop();

            this.Close();
        }
    }
}
