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

using System.Drawing; // for Point

using SeriousRPG.HubNS;
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.ImageObjectNS;

// [SC] it will be more efficient if coordinates of all cells' corners were precalculated

namespace SeriousRPG.Model.MapNS
{
    internal class RouteMap : GenericLayer 
    {
        #region Constants

        internal const int LAYER_TYPE = 4;
        internal const string LAYER_NAME = "Route Map";

        internal const bool DEFAULT_ROUTE_STATE = true;

        #endregion Constants

        #region Fields

        private bool[,] routeFlags;

        #endregion Fields

        #region Constructors

        internal RouteMap(int rowNum, int colNum)
            : base(RouteMap.LAYER_TYPE, RouteMap.LAYER_NAME, rowNum, colNum) {

            this.routeFlags = new bool[this.RowNum, this.ColNum];

            for (int rowIndex = 0; rowIndex < this.RowNum; rowIndex++) {
                for (int colIndex = 0; colIndex < this.ColNum; colIndex++) {
                    this.routeFlags[rowIndex, colIndex] = RouteMap.DEFAULT_ROUTE_STATE;
                }
            }
        }

        #endregion Constructors

        #region Route methods

        internal bool GetCellState(int rowIndex, int colIndex) {
            if (ContainsCell(rowIndex, colIndex)) {
                return this.routeFlags[rowIndex, colIndex];
            }
            else {
                return false;
            }
        }

        internal bool SetCellState(int rowIndex, int colIndex, bool state) {
            if (ContainsCell(rowIndex, colIndex)) {
                this.routeFlags[rowIndex, colIndex] = state;
                return true;
            }
            else {
                return false;
            }
        }

        internal bool ToggleCellState(int rowIndex, int colIndex) {
            if (ContainsCell(rowIndex, colIndex)) {
                this.routeFlags[rowIndex, colIndex] = !this.routeFlags[rowIndex, colIndex];
                return true;
            }
            else {
                return false;
            }
        }

        internal bool TogglePointState(float x, float y) {
            if (ContainsPoint(x, y)) {
                Point cell = GenericLayer.GetCellPosition(x, y);
                return ToggleCellState(cell.Y, cell.X);
            }
            else {
                return false;
            }
        }

        internal bool[,] CloneRouteFlags(){
            bool[,] cloneRouteFlags = new bool[this.RowNum, this.ColNum];
            for (int rowIndex = 0; rowIndex < this.RowNum; rowIndex++) {
                for (int colIndex = 0; colIndex < this.ColNum; colIndex++) {
                    cloneRouteFlags[rowIndex, colIndex] = this.routeFlags[rowIndex, colIndex];
                }
            }
            return cloneRouteFlags;
        }

        // [SC] returns true if the given rectangle does not collide with

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">        Left corner coordinate in pixels</param>
        /// <param name="y">        BOTTOM corner coordinate in pixels</param>
        /// <param name="width">    Width of the rectangle in pixels</param>
        /// <param name="height">   Height of the rectangle in pixels</param>
        /// <returns></returns>
        internal bool IsNoCollision(ICollidableObject clientObj) {
            // [SC] first, check if the rectangle is within map boundaries
            if (!ContainsRectangle(clientObj.X, clientObj.Y, clientObj.Width, clientObj.Height)) {
                return false;
            }

            // [SC] left-top corner cell
            Point topLeftCell = GetCellPosition(clientObj.X, clientObj.Y - clientObj.Height + 1); 

            // [SC] right-bottom corner cell
            Point rightBottomCell = GetCellPosition(clientObj.X + clientObj.Width - 1, clientObj.Y);

            // [SC] check all route map cells that the rectangle covers
            for (int rowIndex = topLeftCell.Y; rowIndex <= rightBottomCell.Y; rowIndex++) {
                for (int colIndex = topLeftCell.X; colIndex <= rightBottomCell.X; colIndex++) {
                    if (!this.routeFlags[rowIndex, colIndex]) {
                        return false;
                    }
                }
            }

            return true;
        }

        // [TODO] Resize and CanResize methods

        #endregion Route methods

        #region Layer methods

        override internal void Act() { }

        override internal void Draw(ICanvas canvas) {
            if (this.IsRendered) {
                for (int rowIndex = 0; rowIndex < this.RowNum; rowIndex++) {
                    for (int colIndex = 0; colIndex < this.ColNum; colIndex++) {
                        Point pixelPos = GenericLayer.GetPixelPosition(rowIndex, colIndex);
                        GenericImage stateImg;

                        if (this.routeFlags[rowIndex, colIndex]) {
                            stateImg = GenericImage.GetInstance((int)Hub.Reserved.TRUE_ID);
                        }
                        else {
                            stateImg = GenericImage.GetInstance((int)Hub.Reserved.FALSE_ID);
                        }

                        canvas.DrawImage(stateImg, pixelPos.X, pixelPos.Y, Tile.TILE_WIDTH, Tile.TILE_HEIGHT);
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
