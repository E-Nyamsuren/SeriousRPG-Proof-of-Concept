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
using SeriousRPG.Model.ImageObjectNS;

namespace SeriousRPG.Model.MapNS
{
    internal class BackgroundOverlay : GenericLayer 
    {
        #region Constants

        internal const int LAYER_TYPE = 1;
        internal const string LAYER_NAME = "Background Overlay";

        #endregion Constants
        
        #region Fields

        private List<ImageObject> images;

        #endregion Fields

        #region Constructors

        internal BackgroundOverlay(int rowNum, int colNum)
            : base(BackgroundOverlay.LAYER_TYPE, BackgroundOverlay.LAYER_NAME, rowNum, colNum) {
            this.images = new List<ImageObject>();
        }

        #endregion Constructors

        #region ImageObject methods

        /// <summary>
        /// Adds ImageObject to backgdound overlay layer.
        /// </summary>
        /// <param name="imageObj"> ImageObject to be added to the background overlay.</param>
        /// <param name="x">        ImageObject's right coordinate in pixels.</param>
        /// <param name="y">        ImageObject's bottom coordinate in pixels.</param>
        /// <returns>True if ImageObject was added successfully to the layer.</returns>
        private bool AddImageObject(ImageObject imageObj, float x, float y) {
            if (imageObj == null) {
                // [TODO] error msg
                return false;
            }
            else if (!ContainsRectangle(x, y, imageObj.Width, imageObj.Height)) {
                // [TODO] error msg
                return false;
            }
            else {
                this.images.Add(imageObj);

                imageObj.X = x;
                imageObj.Y = y;

                return true;
            }
        }

        /// <summary>
        /// Adds ImageObject to backgdound overlay layer.
        /// </summary>
        /// <param name="image">    GenericImage to be added to the background overlay.</param>
        /// <param name="x">        ImageObject's right coordinate in pixels.</param>
        /// <param name="y">        ImageObject's bottom coordinate in pixels.</param>
        /// <returns>True if ImageObject was added successfully to the layer.</returns>
        internal bool AddImageObject(GenericImage image, float x, float y) {
            ImageObject imageObj = ImageObject.CreateInstance(image) as ImageObject;
            return AddImageObject(imageObj, x, y);
        }

        /// <summary>
        /// Adds ImageObject to backgdound overlay layer.
        /// </summary>
        /// <param name="genericImageId">   Id of the GenericImage to be added to the background overlay.</param>
        /// <param name="x">                ImageObject's right coordinate in pixels.</param>
        /// <param name="y">                ImageObject's bottom coordinate in pixels.</param>
        /// <returns>True if ImageObject was added successfully to the layer.</returns>
        internal bool AddImageObject(int genericImageId, float x, float y) {
            return AddImageObject(GenericImage.GetInstance(genericImageId), x, y);
        }

        internal bool RemoveImageObject(ImageObject imageObj) {
            return this.images.Remove(imageObj);
        }

        internal bool RemoveImageObject(float x, float y) {
            return this.images.Remove(GetImageObject(x, y));
        }

        internal ImageObject GetImageObject(float x, float y) {
            for (int index = this.images.Count - 1; index >= 0; index--) {
                ImageObject imageObj = this.images[index];
                if (imageObj.ContainsPoint(x, y)) {
                    return imageObj;
                }
            }

            return null;
        }

        internal int GetImageObjectId(float x, float y) {
            ImageObject imageObj = GetImageObject(x, y);
            if (imageObj != null) {
                return imageObj.Id;
            }
            else {
                return Cfg.UNASSIGNED_INT;
            }
        }

        internal List<ImageObject> CloneImageObjects() {
            List<ImageObject> cloneImages = new List<ImageObject>();

            foreach (ImageObject imageObject in this.images) {
                cloneImages.Add(imageObject.Clone());
            }

            return cloneImages;
        }

        #endregion ImageObject methods

        #region Layer methods

        override internal void Act() { }

        override internal void Draw(ICanvas canvas) {
            if (this.IsRendered) {
                foreach (ImageObject imageObj in images) {
                    canvas.DrawImage(imageObj.Image, imageObj.X, imageObj.Y, imageObj.Width, imageObj.Height);
                }
            }
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
