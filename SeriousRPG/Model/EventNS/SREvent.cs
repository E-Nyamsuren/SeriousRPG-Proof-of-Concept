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

using SeriousRPG.HubNS;

namespace SeriousRPG.Model.EventNS 
{
    internal abstract class SREvent : IEvent 
    {
        #region Fields

        private int id;
        private string description;

        #endregion Fields

        #region Properties

        public int Id {
            get { return this.id; }
            private set { this.id = value; }
        }

        public string Description {
            get { return this.description; }
            set {
                if (!String.IsNullOrEmpty(value)) {
                    this.description = value;
                }
                else {
                    // [TODO]
                }
            }
        }

        #endregion Properties

        #region Constructor

        protected SREvent(string description) {
            this.Id = Hub.GetUniqueAutoId();
            this.Description = description;
        }

        protected SREvent(int id, string description) {
            if (Hub.IsValidId(id)) {
                this.Id = id;
                Hub.RegisterId(id);
            }
            else {
                // [TODO] error msg
                this.Id = Hub.GetUniqueAutoId();
            }

            this.Description = description;
        }

        #endregion Constructor

        #region Finalizer

        ~SREvent() {
            Hub.DeregisterId(this.Id);
        }

        #endregion Finalizer
    }
}
