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

namespace SeriousRPG.Model.ImageObjectNS
{
    internal class ImageObject : AbstractImageObject 
    {
        #region Constructors

        /// <summary>
        /// Private constructor to prevent instantiation from outside. 
        /// This constructor should never be used.
        /// </summary>
        private ImageObject() : base(null) { }

        private ImageObject(GenericImage newImage)
            : base(newImage) {
        }
        
        private ImageObject(int id, GenericImage newImage, bool isClone)
            : base(id, newImage, isClone) {
        }

        #endregion Constructors

        #region Methods

        internal ImageObject Clone() {
            ImageObject cloneImageObject = new ImageObject(this.Id, this.Image.Clone(), true);

            cloneImageObject.X = this.X;
            cloneImageObject.Y = this.Y;

            return cloneImageObject;
        }

        #endregion Methods

        #region Static methods

        internal static ImageObject CreateInstance(GenericImage newImage) {
            if (newImage == null) {
                // [TODO] warning message
                return null;
            }
            else {
                return new ImageObject(newImage);
            }
        }

        #endregion Static methods
    }
}
