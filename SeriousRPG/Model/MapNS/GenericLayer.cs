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
    internal abstract class GenericLayer 
    {
        #region Fields

        /// <summary>
        /// Use this layer identifier if reflection for dynamically identifying subclasses is not supported.
        /// </summary>
        private int layerType;

        /// <summary>
        /// Name of the layer.
        /// </summary>
        private string layerName;

        /// <summary>
        /// Layer height in number of square cells
        /// </summary>
        private int rowNum;

        /// <summary>
        /// Layer width in number of square cells
        /// </summary>
        private int colNum;

        /// <summary>
        /// Flag indicating whether the layer should be rendered.
        /// </summary>
        private bool isRendered = true;

        #endregion Fields

        #region Properties

        internal int LayerType {
            get { return this.layerType; }
            private set { this.layerType = value; }
        }

        internal string LayerName {
            get { return this.layerName; }
            set {
                if (!String.IsNullOrEmpty(value)) {
                    this.layerName = value;
                }
                else {
                    // [TODO]
                }
            }
        }

        internal int RowNum {
            get { return this.rowNum; }
            set {
                if (value > 0) {
                    this.rowNum = value;
                }
                else {
                    // [TODO]
                }
            }
        }

        internal int ColNum {
            get { return this.colNum; }
            set {
                if (value > 0) {
                    this.colNum = value;
                }
                else {
                    // [TODO]
                }
            }
        }

        internal int PixelWidth {
            get { return this.ColNum * Tile.TILE_WIDTH; }
        }

        internal int PixelHeight {
            get { return this.RowNum * Tile.TILE_HEIGHT; }
        }

        internal bool IsRendered {
            get { return this.isRendered; }
            set { this.isRendered = value; }
        }

        #endregion Properties

        #region Constructors

        private GenericLayer() { }

        protected GenericLayer(int layerType, string layerName, int rowNum, int colNum) {
            this.LayerType = layerType;
            this.LayerName = layerName;
            this.RowNum = rowNum;
            this.ColNum = colNum;
        }

        #endregion Constructors

        #region Methods

        abstract internal void Act();

        abstract internal void Draw(ICanvas canvas);

        abstract internal void Resize(int rowNum, int colNum);

        abstract internal bool CanResize(int rowNum, int colNum);

        /// <summary>
        /// Returns true if given cell's indices are within layer boundaries.
        /// </summary>
        /// <param name="rowIndex">Cell's row index.</param>
        /// <param name="colIndex">Cell's column index.</param>
        /// <returns></returns>
        internal bool ContainsCell(int rowIndex, int colIndex) {
            return rowIndex >= 0 && rowIndex < this.RowNum && colIndex >= 0 && colIndex < this.ColNum;
        }

        /// <summary>
        /// Given a point in pixel coordinates, return true if the point is inside the layout.
        /// </summary>
        /// <param name="x">X coordinate in pixels</param>
        /// <param name="y">Y coordinate in pixels</param>
        /// <returns>boolean</returns>
        internal bool ContainsPoint(float x, float y) {
            return x >= 0 && x < this.PixelWidth && y >= 0 && y < this.PixelHeight;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">        Left coordinate in pixels.</param>
        /// <param name="y">        BOTTOM coordinate in pixels.</param>
        /// <param name="width">    Width in pixels.</param>
        /// <param name="height">   Height in pixels.</param>
        /// <returns></returns>
        internal bool ContainsRectangle(float x, float y, int width, int height) {
            return x >= 0 && (x + width - 1) < this.PixelWidth
                && (y - (height - 1)) >= 0 && y < this.PixelHeight;
        }

        #endregion Mathods

        #region Static methods

        /// <summary>
        /// Returns a BOTTOM-LEFT pixel coordinate for a given cell position.
        /// </summary>
        /// <param name="rowIndex">     Cell's row index</param>
        /// <param name="colIndex">     Cell's column index</param>
        /// <returns>An integer array with x and y coordinates.</returns>
        internal static Point GetPixelPosition(int rowIndex, int colIndex) {
            int x = Tile.TILE_WIDTH * colIndex;
            int y = Tile.TILE_HEIGHT * (rowIndex + 1) - 1;

            return new Point { X = x, Y = y };
        }

        /// <summary>
        /// Given a point in pixel coordinates, calculates row and column indices of the cell containing the point.
        /// </summary>
        /// <param name="x">X coordinate in pixels</param>
        /// <param name="y">Y coordinate in pixels</param>
        /// <returns>Point where X is column index and Y is a row index.</returns>
        internal static Point GetCellPosition(float x, float y) {
            int rowIndex = (int)(y / Tile.TILE_HEIGHT);
            int colIndex = (int)(x / Tile.TILE_WIDTH);

            return new Point { X = colIndex, Y = rowIndex };
        }

        #endregion Static methods 
    }
}
