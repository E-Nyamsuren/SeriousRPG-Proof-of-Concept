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

namespace SeriousRPG.ControlIO 
{
    public enum SRMouseButton 
    {
        Left,
        Middle,
        None,
        Right
    }

    public class SRMouseEventArgs : EventArgs
    {
        public SRMouseButton Button { // [SC] can be null; e.g. mouse over
            get;
            private set;
        }

        public int X {
            get;
            private set;
            // [TODO] value check
        }

        public int Y {
            get;
            private set;
            // [TODO] value check
        }

        public SRMouseEventArgs(SRMouseButton button, int x, int y) {
            this.Button = button;
            this.X = x;
            this.Y = y;
        }
    }

    public delegate void SRMouseEventHandler (
        object sender,
        SRMouseEventArgs e
    );
}
