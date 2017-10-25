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
    // [SC] Tile instance should be used only with Background layer
    internal class Tile : AbstractImageObject
    {
        #region Statics and Constants

        /// <summary>
        /// Tile width in pixels
        /// </summary>
        internal const int TILE_WIDTH = 48;

        /// <summary>
        /// Tile height in pixels
        /// </summary>
        internal const int TILE_HEIGHT = 48;

        #endregion Statics and Constants

        #region Constructor

        /// <summary>
        /// Private constructor to prevent instantiation from outside. 
        /// This constructor should never be used.
        /// </summary>
        private Tile() : base(null) { }

        /// <summary>
        /// This constructory should only be called via a factory method.
        /// </summary>
        /// <param name="newImage">Image of the tile</param>
        private Tile(GenericImage newImage) : base(newImage) {
        }

        private Tile(int id, GenericImage newImage, bool isClone) 
            : base(id, newImage, isClone) {
        }

        #endregion Constructor

        #region Methods

        internal Tile Clone() {
            Tile cloneTile = new Tile(this.Id, this.Image.Clone(), true);
            
            cloneTile.X = this.X;
            cloneTile.Y = this.Y;
            
            return cloneTile;
        }

        #endregion Methods

        #region Static methods

        internal static Tile CreateInstance(GenericImage newImage) {
            if (newImage == null) {
                // [TODO] warning message
                return null;
            }
            else if (newImage.Width != Tile.TILE_WIDTH || newImage.Height != Tile.TILE_HEIGHT) {
                // [TODO] warning message
                return null;
            }
            else {
                return new Tile(newImage);
            }
        }

        #endregion Static methods
    }
}
