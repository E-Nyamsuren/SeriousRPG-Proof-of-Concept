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

using SeriousRPG.Model.DrawingNS;

namespace SeriousRPG.Model.MapNS
{
    internal class ForegroundOverlay : GenericLayer 
    {
        #region Constants

        internal const int LAYER_TYPE = 3;
        internal const string LAYER_NAME = "Foreground Overlay";

        #endregion Constants

        #region Constructors

        internal ForegroundOverlay(int rowNum, int colNum)
            : base(ForegroundOverlay.LAYER_TYPE, ForegroundOverlay.LAYER_NAME, rowNum, colNum) {
        
        }

        #endregion Constructors

        #region ImageObject methods

        // [TODO]

        #endregion ImageObject methods

        #region Layer methods

        override internal void Act() { }

        override internal void Draw(ICanvas canvas) {
            throw new NotImplementedException(); // [TODO]
        }

        override internal void Resize(int rowNum, int colNum) {
            throw new NotImplementedException(); // [TODO]
        }

        override internal bool CanResize(int rowNum, int colNum) {
            throw new NotImplementedException(); // [TODO]
        }

        #endregion Layer methods
    }
}
