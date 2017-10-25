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
using System.Linq;
using System.Text;

namespace SeriousRPG.Module.AltThesaurusModuleNS 
{
    public class ExperimentData 
    {
        public string DatasetId {
            get;
            private set;
        }

        public int PlayerId {
            set;
            get;
        }

        public bool Shuffled {
            get;
            private set;
        }

        public ExperimentData(string datasetId, int playerId, bool shuffled) {
            this.DatasetId = datasetId;
            this.PlayerId = playerId;
            this.Shuffled = shuffled;
        }

        public List<TurnData> turnData = new List<TurnData>();

        public void AddTurnData(int itemId, string targetText, bool shuffled, int turnStartTime, int turnEndTime) {
            TurnData data = new TurnData();
            data.ItemId = itemId;
            data.TargetText = targetText;
            data.Shuffled = shuffled;
            data.TurnStartTime = turnStartTime;
            data.TurnEndTime = turnEndTime;
            this.turnData.Add(data);
        }
    }

    public class TurnData 
    {
        public int ItemId {
            get;
            set;
        }

        public string TargetText {
            get;
            set;
        }

        public bool Shuffled {
            get;
            set;
        }

        public int TurnStartTime {
            get;
            set;
        }

        public int TurnEndTime {
            get;
            set;
        }

        public bool ErrorMade {
            get;
            set;
        }

        public bool Recognized {
            get;
            set;
        }
    }
}
