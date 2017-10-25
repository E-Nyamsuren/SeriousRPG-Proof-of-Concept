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

using System.Drawing; // [SC] for Point

using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.ImageObjectNS;

namespace SeriousRPG.Model.MapNS
{
    internal class Background : GenericLayer 
    {
        #region Constants

        internal const int LAYER_TYPE = 0;
        internal const string LAYER_NAME = "Background";

        #endregion Constants

        #region Fields

        private Tile[,] tiles;

        #endregion Fields

        #region Constructors

        internal Background(int rowNum, int colNum)
            : base(Background.LAYER_TYPE, Background.LAYER_NAME, rowNum, colNum) {

            this.tiles = new Tile[this.RowNum, this.ColNum];
        }

        #endregion Constructors

        #region Tile methods

        private bool AddTile(Tile tile, int rowIndex, int colIndex) {
            if (!ContainsCell(rowIndex, colIndex)) {
                // [TODO] error msg
                return false;
            }
            else if (tile == null) {
                // [TODO] error msg
                return false;
            }
            else {
                tiles[rowIndex, colIndex] = tile;

                Point bottomLeft = GenericLayer.GetPixelPosition(rowIndex, colIndex);
                tile.X = bottomLeft.X;
                tile.Y = bottomLeft.Y;

                return true;
            }
        }

        /// <summary>
        /// Adds tile to the cell that contains the point with given X and Y pixel coordinates.
        /// </summary>
        /// <param name="image">    GenericImage from which tile is auto generated.</param>
        /// <param name="x">        Point's X coordinate in pixels.</param>
        /// <param name="y">        Point's Y coordinate in pixels.</param>
        /// <returns>True if a tile was successfully added to the layer.</returns>
        internal bool AddTile(GenericImage image, float x, float y) {
            Tile newTile = Tile.CreateInstance(image) as Tile;

            Point cellPos = GenericLayer.GetCellPosition(x, y);

            return AddTile(newTile, cellPos.Y, cellPos.X);
        }

        /// <summary>
        /// Adds tile to the cell that contains the point with given X and Y pixel coordinates.
        /// </summary>
        /// <param name="image">    Id of a GenericImage object from which tile is auto generated.</param>
        /// <param name="x">        Point's X coordinate in pixels.</param>
        /// <param name="y">        Point's Y coordinate in pixels.</param>
        /// <returns>True if a tile was successfully added to the layer.</returns>
        internal bool AddTile(int genericImageId, float x, float y) {
            return AddTile(GenericImage.GetInstance(genericImageId), x, y);
        }

        internal bool RemoveTile(int rowIndex, int colIndex) {
            if (HasTile(rowIndex, colIndex)) {
                this.tiles[rowIndex, colIndex] = null;
                return true;
            }
            else {
                // [TODO] no tile warning msg
                return false;
            }
        }

        // [TODO] should this method be available for outside classes? currently set to private.
        private Tile GetTile(int rowIndex, int colIndex) {
            if (HasTile(rowIndex, colIndex)) {
                return this.tiles[rowIndex, colIndex];
            }
            else {
                // [TODO] no tile warning msg
                return null;
            }
        }

        internal bool HasTile(int rowIndex, int colIndex) {
            if (ContainsCell(rowIndex, colIndex)) {
                if (this.tiles[rowIndex, colIndex] == null) {
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                // [TODO] warning msg
                return false;
            }
        }

        internal Tile[,] CloneTiles() {
            Tile[,] cloneTiles = new Tile[this.RowNum, this.ColNum];

            for (int rowIndex = 0; rowIndex < this.RowNum; rowIndex++) {
                for (int colIndex = 0; colIndex < this.ColNum; colIndex++) {
                    if (this.tiles[rowIndex, colIndex] != null) {
                        cloneTiles[rowIndex, colIndex] = this.tiles[rowIndex, colIndex].Clone();
                    }
                }
            }

            return cloneTiles;
        }

        #endregion Tile methods

        #region Layer methods

        override internal void Act() { }

        override internal void Draw(ICanvas canvas) {
            if (this.IsRendered) {
                for (int rowIndex = 0; rowIndex < this.RowNum; rowIndex++) {
                    for (int colIndex = 0; colIndex < this.ColNum; colIndex++) {
                        if (this.tiles[rowIndex, colIndex] != null) {
                            Tile tile = this.tiles[rowIndex, colIndex];
                            // [SC] note that tile Y coordinates represents tile's bottom corner
                            canvas.DrawImage(tile.Image, tile.X, tile.Y, tile.Width, tile.Height);
                        }
                    }
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
