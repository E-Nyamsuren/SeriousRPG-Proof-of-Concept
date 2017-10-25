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

using System.Threading;

using SeriousRPG.Model.DrawingNS;

namespace SeriousRPG.Editor.AnimationNS {
    public partial class TestAnimeForm : Form {
        public TestAnimeForm(List<int> spriteIds, int delay) {
            InitializeComponent();

            if (delay < 0 || spriteIds == null || spriteIds.Count == 0) {
                return;
            }

            List<GenericImage> images = new List<GenericImage>();
            foreach (int spriteId in spriteIds) {
                GenericImage image = GenericImage.GetInstance(spriteId);
                if (image != null) {
                    images.Add(image);
                } else {
                    return;
                }
            }

            int currIndex = 0;

            BackgroundWorker bw = new BackgroundWorker();

            bw.WorkerReportsProgress = true;

            // what to do in the background thread
            bw.DoWork += new DoWorkEventHandler(
                delegate(object o, DoWorkEventArgs args) {
                    BackgroundWorker b = o as BackgroundWorker;

                    while (true) {
                        this.pictureBox.Image = images[currIndex++].Image;
                        if (currIndex >= images.Count) {
                            currIndex = 0;
                        }

                        //b.ReportProgress();
                        Thread.Sleep(delay);
                    }
                }
            );

            bw.RunWorkerAsync();
        }
    }
}
