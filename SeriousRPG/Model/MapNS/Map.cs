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
using SeriousRPG.Model.ActorNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.RuleNS.ConditionNS;

namespace SeriousRPG.Model.MapNS
{
    internal class Map 
    {
        #region Constants

        /// <summary>
        /// Default height of a map.
        /// </summary>
        internal const int DEFAULT_ROW_NUM = 50;
        /// <summary>
        /// Default width of a map.
        /// </summary>
        internal const int DEFAULT_COL_NUM = 50;

        /// <summary>
        /// Maximum height of a map.
        /// </summary>
        internal const int MAX_ROW_NUM = 256;
        /// <summary>
        /// Maximum width of a map.
        /// </summary>
        internal const int MAX_COL_NUM = 256;

        /// <summary>
        /// Minimum height of a map.
        /// </summary>
        internal const int MIN_ROW_NUM = 1;
        /// <summary>
        /// Minimum width of a map.
        /// </summary>
        internal const int MIN_COL_NUM = 1;

        #endregion Constants

        #region Fields

        /// <summary>
        /// Map Id unique within a game.
        /// </summary>
        private int id;

        /// <summary>
        /// Map name.
        /// </summary>
        private string name;

        /// <summary>
        /// Map height in number of square cells
        /// </summary>
        private int rowNum = Map.DEFAULT_ROW_NUM;

        /// <summary>
        /// Map width in number of square cells
        /// </summary>
        private int colNum = Map.DEFAULT_COL_NUM;

        private Background bg;
        private BackgroundOverlay bgo;

        private Foreground fg;
        private ForegroundOverlay fgo;

        private RouteMap rm;

        private LogicLayer ll;

        private Game game;

        #endregion Fields

        #region Properties

        /// <summary>
        /// ID getter/setter.
        /// </summary>
        internal int Id {
            get { return this.id; }
            private set { this.id = value; }
        }

        /// <summary>
        /// Name getter/setter.
        /// </summary>
        internal string Name {
            get { return this.name; }
            set {
                if (!String.IsNullOrEmpty(value)) {
                    this.name = value;
                }
                else {
                    // [TODO]
                }
            }
        }

        internal int RowNum {
            get { return this.rowNum; }
            private set {
                if (!IsValidRowNum(value)) {
                    // [TODO] invalid value error msg
                }
                else {
                    this.rowNum = value;
                }
            }
        }

        internal int ColNum {
            get { return this.colNum; }
            private set {
                if (!IsValidColNum(value)) {
                    // [TODO] invalid value error msg
                }
                else {
                    this.colNum = value;
                }
            }
        }

        internal int PixelWidth {
            get { return this.ColNum * Tile.TILE_WIDTH; }
        }

        internal int PixelHeight {
            get { return this.RowNum * Tile.TILE_HEIGHT; }
        }

        #endregion Properties

        #region Constructors

        private Map(int id, string name, int rowNum, int colNum, Game game) {
            this.Id = id;
            this.Name = name;

            this.RowNum = rowNum;
            this.ColNum = colNum;

            this.game = game; // [TODO] what if game is null

            this.bg = new Background(this.RowNum, this.ColNum);
            this.bgo = new BackgroundOverlay(this.RowNum, this.ColNum);

            this.rm = new RouteMap(this.RowNum, this.ColNum);

            this.fg = new Foreground(this.RowNum, this.ColNum);
            this.fgo = new ForegroundOverlay(this.RowNum, this.ColNum);

            this.ll = new LogicLayer(this.RowNum, this.ColNum);
        }

        #endregion Constructors

        #region Methods

        // [TODO] rewrite
        internal void Resize(int rowNum, int colNum) {
            if (this.bg.CanResize(rowNum, colNum)
                && this.bgo.CanResize(rowNum, colNum)
                && this.fg.CanResize(rowNum, colNum)
                && this.fgo.CanResize(rowNum, colNum)
                && this.rm.CanResize(rowNum, colNum)
                && this.ll.CanResize(rowNum, colNum)
                ) {

                this.RowNum = rowNum;
                this.ColNum = colNum;

                this.bg.Resize(rowNum, colNum);
                this.bgo.Resize(rowNum, colNum);

                this.fg.Resize(rowNum, colNum);
                this.fgo.Resize(rowNum, colNum);

                this.rm.Resize(rowNum, colNum);

                this.ll.Resize(rowNum, colNum);
            }
        }

        internal bool SetLayerRenderFlag(int layerType, bool flag) {
            if (layerType == this.bg.LayerType) {
                this.bg.IsRendered = flag;
                return true;
            }
            else if (layerType == this.bgo.LayerType) {
                this.bgo.IsRendered = flag;
                return true;
            }
            else if (layerType == this.fg.LayerType) {
                this.fg.IsRendered = flag;
                return true;
            }
            else if (layerType == this.fgo.LayerType) {
                this.fgo.IsRendered = flag;
                return true;
            }
            else if (layerType == this.rm.LayerType) {
                this.rm.IsRendered = flag;
                return true;
            }
            else if (layerType == this.ll.LayerType) {
                this.ll.IsRendered = flag;
                return true;
            }
            // [TODO] error msg
            return false;
        }

        internal bool GetLayerRenderFlag(int layerType) {
            if (layerType == this.bg.LayerType) {
                return this.bg.IsRendered;
            }
            else if (layerType == this.bgo.LayerType) {
                return this.bgo.IsRendered;
            }
            else if (layerType == this.fg.LayerType) {
                return this.fg.IsRendered;
            }
            else if (layerType == this.fgo.LayerType) {
                return this.fgo.IsRendered;
            }
            else if (layerType == this.rm.LayerType) {
                return this.rm.IsRendered;
            }
            else if (layerType == this.ll.LayerType) {
                return this.ll.IsRendered;
            }
            // [TODO] error msg
            return false;
        }

        internal void Act() {
            this.bg.Act();
            this.bgo.Act();
            this.ll.Act();
            this.fg.Act();
            //this.fgo.Act(); // [TODO]
            this.rm.Act();
        }

        internal void DrawMap(ICanvas canvas) {
            this.bg.Draw(canvas);
            this.bgo.Draw(canvas);
            this.ll.Draw(canvas);
            this.fg.Draw(canvas);
            //this.fgo.Draw(canvas); // [TODO]
            this.rm.Draw(canvas);
        }

        internal void Animate(long currentTime) {
            this.fg.Animate(currentTime);
        }

        internal bool IsNoCollision(ICollidableObject clientObject) { 
            // [SC] collision detection with a route map
            if (!this.rm.IsNoCollision(clientObject)) {
                return false;
            }

            // [SC] collision detection with foreground actors
            if (!this.fg.IsNoCollision(clientObject)) {
                return false;
            }

            return true;
        }

        internal Map Clone(Game cloneGame) {
            Map cloneMap = new Map(this.Id, this.Name, this.RowNum, this.ColNum, cloneGame);

            // [SC] Cloning Background layer
            Tile[,] cloneTiles = this.bg.CloneTiles();
            for (int rowIndex = 0; rowIndex < this.RowNum; rowIndex++) {
                for (int colIndex = 0; colIndex < this.ColNum; colIndex++) {
                    if (cloneTiles[rowIndex, colIndex] != null) {
                        Tile cloneTile = cloneTiles[rowIndex, colIndex];
                        cloneMap.AddTile(cloneTile.Image.Id, cloneTile.X, cloneTile.Y);
                    }
                }
            }

            // [SC] Cloning Background Overlay layer
            List<ImageObject> cloneBgImages = this.bgo.CloneImageObjects();
            foreach(ImageObject cloneImage in cloneBgImages) {
                cloneMap.AddBackgroundImage(cloneImage.Image.Id, cloneImage.X, cloneImage.Y);
            }

            // [TODO] Cloning Foreground is not complete
            List<IActor> cloneActors = this.fg.CloneActors();
            foreach(IActor cloneActor in cloneActors) {
                cloneMap.AddActor(cloneActor.Id, cloneActor.X, cloneActor.Y);
            }
            
            // [TODO] cloning of other layers is not done

            // [SC] Cloning the Route Map
            bool[,] cloneRouteFlags = this.rm.CloneRouteFlags();
            for (int rowIndex = 0; rowIndex < this.RowNum; rowIndex++) {
                for (int colIndex = 0; colIndex < this.ColNum; colIndex++) {
                    cloneMap.SetRouteMapCellState(rowIndex, colIndex, cloneRouteFlags[rowIndex, colIndex]);
                }
            }

            // [SC] Cloning the Logic Layer
            foreach(IdNamePair rTriggerStub in this.ll.GetRegionTriggerList()) {
                RegionTrigger regionTrigger = cloneGame.GetCondition<RegionTrigger>(rTriggerStub.Id);
                cloneMap.AddRegionTrigger(regionTrigger, regionTrigger.X, regionTrigger.Y);
            }

            return cloneMap;
        }

        internal bool IsWithin(float x, float y) {
            return x >= 0 && x < this.PixelWidth && y >= 0 && y < this.PixelHeight;
        }

        /// <summary>
        /// Returns true if the specified rectangle fits within the map
        /// </summary>
        /// <param name="x">        </param>
        /// <param name="y">        BOTTOM coordinate in pixels</param>
        /// <param name="width">    </param>
        /// <param name="height">   </param>
        /// <returns></returns>
        internal bool IsWithin(float x, float y, int width, int height) {
            float maxX = x + width - 1;
            float minY = y - height + 1;

            return x >= 0 && minY >= 0 && maxX < this.PixelWidth && y < this.PixelHeight;
        }

        #endregion Methods

        #region Background methods

        internal bool AddTile(int genericImageId, float x, float y) {
            return bg.AddTile(genericImageId, x, y);
        }

        #endregion Background methods

        #region BackgroundOverlay methods

        internal bool AddBackgroundImage(int genericImageId, float x, float y) {
            return bgo.AddImageObject(genericImageId, x, y);
        }

        internal int GetBackgroundImageId(float x, float y) {
            return bgo.GetImageObjectId(x, y);
        }

        internal bool RemoveBackgroundImage(float x, float y) {
            return bgo.RemoveImageObject(x, y);
        }

        #endregion BackgroundOverlay methods

        #region Foreground methods

        internal bool AddActor(int actorId, float x, float y) {
            return fg.AddActor(this.game.GetActor<IActor>(actorId), x, y);
        }

        #endregion Foreground methods

        #region ForegroundOverlay methods
        // [TODO]
        #endregion ForegroundOverlay methods

        #region RouteMap methods

        internal bool SetRouteMapCellState(int rowIndex, int colIndex, bool cellState) {
            return rm.SetCellState(rowIndex, colIndex, cellState);
        }

        internal bool ToggleRouteMapPointState(float x, float y) {
            return rm.TogglePointState(x, y);
        }

        #endregion RouteMap methods

        #region LogicLayer methods

        internal bool AddRegionTrigger(RegionTrigger rTrigger, float x, float y) {
            return this.ll.AddRegionTrigger(rTrigger, x, y);
        }

        #endregion LogicLayer methods

        #region Static methods

        internal static Map CreateInstance(int id, string name, int rowNum, int colNum, Game game) {
            if (game == null) {
                // [TODO] error msg
                return null;
            }
            else if (String.IsNullOrEmpty(name)) {
                // [TODO] warning msg
                return null;
            }
            else if (!IsValidRowNum(rowNum) || !IsValidColNum(colNum)) {
                // [TODO] warning msg
                return null;
            }
            else {
                return new Map(id, name, rowNum, colNum, game);
            }
        }

        internal static bool IsValidRowNum(int rowNum) {
            return rowNum >= Map.MIN_ROW_NUM && rowNum <= Map.MAX_ROW_NUM;
        }

        internal static bool IsValidColNum(int colNum) {
            return colNum >= Map.MIN_COL_NUM && colNum <= Map.MAX_COL_NUM;
        }

        #endregion Static methods
    }
}
