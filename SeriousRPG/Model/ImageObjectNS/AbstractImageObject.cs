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
using SeriousRPG.Model.DrawingNS;

namespace SeriousRPG.Model.ImageObjectNS
{
    internal abstract class AbstractImageObject 
    {
        #region Fields

        /// <summary>
        /// Id unique among instances of the same subclass.
        /// </summary>
        private int id;

        /// <summary>
        /// Image of the object.
        /// </summary>
        private GenericImage image;

        /// <summary>
        /// Tile's left-most drawing coordinate in pixels.
        /// </summary>
        private float x = Cfg.DEFAULT_X;

        /// <summary>
        /// Tile's bottom-most drawing coordinate in pixels.
        /// </summary>
        private float y = Cfg.DEFAULT_Y;

        #endregion Fields

        #region Properties

        internal int Id {
            get { return this.id; }
            private set { this.id = value; }
        }

        internal string Name {
            get {
                if (HasGenericImage()) {
                    return this.Image.Name;
                }
                else {
                    // [TODO] error msg
                    return Cfg.UNKNOWN_NAME;
                }
            }
        }

        internal float X {
            get { return this.x; }
            set {
                if (value >= 0) {
                    this.x = value;
                }
                else {
                    // [TODO] error msg
                }
            }
        }

        internal float Y {
            get { return this.y; }
            set {
                if (value >= 0) {
                    this.y = value;
                }
                else {
                    // [TODO] error msg
                }
            }
        }

        internal int Width {
            get { return this.Image.Width; }
        }

        internal int Height {
            get { return this.Image.Height; }
        }

        internal GenericImage Image {
            get { return this.image; }
            private set {
                if (value != null) {
                    this.image = value;
                }
                else {
                    // [TODO] error msg
                }
            }
        }

        #endregion Properties

        #region Constructor

        protected AbstractImageObject(GenericImage newImage) {
            this.Id = Hub.GetUniqueAutoId();
            this.Image = newImage;
        }

        protected AbstractImageObject(int id, GenericImage newImage, bool isClone) {
            if (isClone) {
                this.Id = id;
            }
            else if (Hub.IsValidId(id)) {
                this.Id = id;
                Hub.RegisterId(id);
            }
            else {
                // [TODO] error msg
                this.Id = Hub.GetUniqueAutoId();
            }

            this.Image = newImage;
        }

        #endregion Constructor

        #region Finalizer

        ~AbstractImageObject() {
            Hub.DeregisterId(this.Id);
        }

        #endregion Finalizer

        #region Methods

        internal bool HasGenericImage() {
            if (this.Image != null) {
                return true;
            }
            else {
                return false;
            }
        }

        internal bool ContainsPoint(float x, float y) {
            float maxX = this.X + (this.Width - 1);
            float minY = this.Y - (this.Height - 1);

            return x >= this.X && x <= maxX && y >= minY && y <= this.Y;
        }

        #endregion Methods
    }
}
