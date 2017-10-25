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

using SeriousRPG.Misc;
using SeriousRPG.HubNS;
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.ActorNS;
using SeriousRPG.Model.MapNS;
using SeriousRPG.Model.RuleNS;
using SeriousRPG.Model.RuleNS.ConditionNS;
using SeriousRPG.Model.RuleNS.ActionNS;
using SeriousRPG.Model.ModuleNS;

using System.Diagnostics; // [TODO][DELETE]

namespace SeriousRPG.Model.GameNS 
{
    internal class Game : IGame
    {
        #region Statics and Constants

        private static Dictionary<int, Game> games; // <=

        #endregion Statics and Constants

        #region Fields

        /// <summary>
        /// ID of the game.
        /// </summary>
        private int id; // <=

        /// <summary>
        /// String description of the game.
        /// </summary>
        private string name;

        /// <summary>
        /// Canvas into which the game is drawn.
        /// </summary>
        private ICanvas canvas;

        protected Player player;

        private Dictionary<int, IActor> actors;

        private Dictionary<int, ICondition> conditions;
        private Dictionary<int, IAction> actions;
        protected Dictionary<int, IRule> rules;

        private Dictionary<int, Map> maps;

        private int currentMapId;
        protected Map currentMap;

        #endregion Fields

        #region Properties

        /// <summary>
        /// ID getter/setter.
        /// </summary>
        public int Id {
            get { return this.id; }
            private set { this.id = value; } // <=
        }

        /// <summary>
        /// Name getter/setter.
        /// </summary>
        public string Name {
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

        public ICanvas Canvas {
            get { return this.canvas; }
            private set { 
                if (value != null) {
                    this.canvas = value;
                } else {
                    // [TODO] throw exception
                }
            }
        }

        internal int CurrentMapId {
            get { return this.currentMapId; }
            set {
                if (HasMap(value)) {
                    this.currentMapId = value;
                    this.currentMap = this.maps[value];
                }
                else { 
                    // [TODO] error msg
                }
            }
        }

        #endregion Properties

        #region Constructors

        static Game() { // <=
            if (Game.games == null) {
                Game.games = new Dictionary<int, Game>();
            }
        }

        protected Game() { }

        protected Game(int id, string name, ICanvas canvas) {
            this.Id = id;
            this.Name = name;
            this.Canvas = canvas;

            this.player = new Player((int)Hub.Reserved.PLAYER_ID, "Player character", GenericImage.GetPlaceholder(), false);

            this.maps = new Dictionary<int, Map>();
            this.actors = new Dictionary<int, IActor>();

            this.conditions = new Dictionary<int, ICondition>();
            this.actions = new Dictionary<int, IAction>();
            this.rules = new Dictionary<int, IRule>();
        }

        #endregion Constructors

        #region Methods

        internal RunableGame Clone(ICanvas runableCanvas) {
            // [SC] 1. 
            RunableGame cloneGame = RunableGame.CreateInstance(this.Id, this.Name, runableCanvas);

            // [SC] 2. Clone player
            cloneGame.SetPlayer(this.player.Clone());

            // [SC] 3. Clone other actors
            foreach (KeyValuePair<int, IActor> keyValuePair in this.actors) {
                IActor actor = keyValuePair.Value;
                IActor cloneActor = actor.Clone();

                cloneGame.actors.Add(cloneActor.Id, cloneActor);
            }

            // [SC] 4. Clone conditions
            foreach (KeyValuePair<int, ICondition> kayValuePair in this.conditions) {
                cloneGame.AddCondition(kayValuePair.Value.Clone(cloneGame));
            }

            // [SC] 5. Clone actions
            foreach (KeyValuePair<int, IAction> kayValuePair in this.actions) {
                cloneGame.AddAction(kayValuePair.Value.Clone(cloneGame));
            }

            // [SC] 6. Clone events
            foreach (KeyValuePair<int, IRule> kayValuePair in this.rules) {
                cloneGame.AddRule(kayValuePair.Value.Clone(cloneGame));
            }

            // [SC] 7. Clone map
            foreach (KeyValuePair<int, Map> kayValuePair in this.maps) {
                Map cloneMap = kayValuePair.Value.Clone(cloneGame);

                cloneGame.AddMap(kayValuePair.Key, cloneMap);
            }

            return cloneGame;
        }

        // [SC] used for cloning the Player class
        private void SetPlayer(Player player) {
            this.player = player;
        }

        #endregion Methods

        #region Map methods

        private bool AddMap(int id, Map map) {
            if (map != null) {
                // [SC] register new map instance
                this.maps.Add(id, map);
                return true;
            }
            else {
                return false;
            }
        }

        // [TODO] review the code, improve if necessary
        internal bool AddMap(int id, string name, int rowNum, int colNum) {
            if (HasMap(id)){
                // [TODO] error msg
                return false;
            }

            return AddMap(id, Map.CreateInstance(id, name, rowNum, colNum, this));
        }

        internal bool HasMap(int id) {
            return this.maps.ContainsKey(id);
        }

        // [TODO] review the code, improve if necessary
        internal bool AddMapItem(int mapId, int layerType, int itemId, float x, float y) {
            //Debug.WriteLine(String.Format("Game.AddMapItem: {0} {1} {2} {3} {4}", mapId, layerType, itemId, x, y)); // [TODO]

            if (this.maps.ContainsKey(mapId)) {
                Map map = this.maps[mapId];

                switch (layerType) {
                    case Background.LAYER_TYPE:
                        // [TODO]
                        return map.AddTile(itemId, x, y);
                    case BackgroundOverlay.LAYER_TYPE:
                        // [TODO]
                        return map.AddBackgroundImage(itemId, x, y);
                    case Foreground.LAYER_TYPE:
                        // [TODO]
                        return map.AddActor(itemId, x, y);
                    case ForegroundOverlay.LAYER_TYPE:
                        // [TODO]
                        return false;
                    case RouteMap.LAYER_TYPE:
                        return map.ToggleRouteMapPointState(x, y);
                    case LogicLayer.LAYER_TYPE:
                        return map.AddRegionTrigger(GetCondition<RegionTrigger>(itemId), x, y);
                    default:
                        // [TODO] error msg
                        return false;
                }
            }
            else {
                // [TODO] error msg
                return false;
            }
        }

        // [TODO]
        internal int GetMapItemId(int mapId, int layerType, float x, float y) {
            if (this.maps.ContainsKey(mapId)) {
                Map map = this.maps[mapId];

                switch (layerType) {
                    case Background.LAYER_TYPE:
                        // [TODO]
                        return Cfg.UNASSIGNED_INT;
                    case BackgroundOverlay.LAYER_TYPE:
                        return map.GetBackgroundImageId(x, y);
                    case Foreground.LAYER_TYPE:
                        // [TODO]
                        return Cfg.UNASSIGNED_INT;
                    case ForegroundOverlay.LAYER_TYPE:
                        // [TODO]
                        return Cfg.UNASSIGNED_INT;
                    case RouteMap.LAYER_TYPE:
                        // [TODO]
                        return Cfg.UNASSIGNED_INT;
                    default:
                        // [TODO] error msg
                        return Cfg.UNASSIGNED_INT;
                }
            }
            else {
                // [TODO] error msg
                return Cfg.UNASSIGNED_INT;
            }
        }

        internal bool RemoveMapItem(int mapId, int layerType, float x, float y) {
            if (this.maps.ContainsKey(mapId)) {
                Map map = this.maps[mapId];

                switch (layerType) {
                    case Background.LAYER_TYPE:
                        // [TODO]
                        return false;
                    case BackgroundOverlay.LAYER_TYPE:
                        return map.RemoveBackgroundImage(x, y);
                    case Foreground.LAYER_TYPE:
                        // [TODO]
                        return false;
                    case ForegroundOverlay.LAYER_TYPE:
                        // [TODO]
                        return false;
                    case RouteMap.LAYER_TYPE:
                        // [TODO]
                        return false;
                    default:
                        // [TODO] error msg
                        return false;
                }
            }
            else {
                // [TODO] error msg
                return false;
            }
        }

        internal int RemoveMapItem(int mapId, int layerType, int itemId, float x, float y) {
            // [TODO]
            throw new NotImplementedException("RemoveMapItem is not implemented.");
        }

        internal List<IdNamePair> GetMapList() {
            List<IdNamePair> mapList = new List<IdNamePair>();
            foreach (KeyValuePair<int, Map> entry in this.maps){
                mapList.Add(new IdNamePair(entry.Key, entry.Value.Name));
            }
            return mapList;
        }

        internal bool SetLayerRenderFlag(int mapId, int layerType, bool flag) {
            if (this.maps.ContainsKey(mapId)) {
                return this.maps[mapId].SetLayerRenderFlag(layerType, flag);
            } else {
                // [TODO] error msg
                return false;
            }
        }

        internal bool GetLayerRenderFlag(int mapId, int layerType) {
            if (this.maps.ContainsKey(mapId)) {
                return this.maps[mapId].GetLayerRenderFlag(layerType);
            }
            else {
                // [TODO] error msg
                return false;
            }
        }

        private void DrawMap(Map map) {
            if (map != null) {
                map.DrawMap(this.Canvas);
            }
            else { 
                // [TODO] error msg
            }
        }

        internal void DrawMap() {
            DrawMap(this.currentMap);
        }

        /// <summary>
        /// Returns size for the map with given Id.
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns>Returns SizeSR with 0 width and height if the map is not found.</returns>
        internal SizeSR GetMapSize(int mapId) {
            if (HasMap(mapId)) {
                Map map = this.maps[mapId];
                return new SizeSR(map.PixelWidth, map.PixelHeight);
            }
            else {
                return new SizeSR(0, 0);
            }
        }

        /// <summary>
        /// Returns size for the current map.
        /// </summary>
        /// <returns>Returns SizeSR with 0 width and height if the map is not found.</returns>
        internal SizeSR GetCurrentMapSize() {
            if (this.currentMap != null) {
                return new SizeSR(this.currentMap.PixelWidth, this.currentMap.PixelHeight);
            } else {
                return new SizeSR(0, 0);
            }
        }

        /*interal void DrawMap(int mapId, ICanvas canvas) {
            if (HasMap(mapId)) {
                DrawMap(this.maps[mapId], canvas);
            }
            else { 
                // [TODO] error msg
            }
        }*/
        
        #endregion Map methods

        #region Actor methods

        internal bool AddActor(Actor actor) {
            if (this.actors.ContainsValue(actor)) {
                // [TODO] error msg
                return false;
            }

            if (this.actors.ContainsKey(actor.Id)) {
                // [TODO] error msg
                return false;
            }

            this.actors.Add(actor.Id, actor);
            return true;
        }

        // [TODO] not complete
        private IActor GetActor(int id) {
            if (id == this.player.Id) {
                return this.player;
            }
            else if (HasActor(id)) {
                return this.actors[id];
            }
            else {
                // [TODO]
                return null;
            }
        }

        public T GetActor<T>(int id) {
            IActor actor = GetActor(id);
            if (actor != null && actor is T) {
                return (T)actor;
            }
            return default(T);
        }

        internal List<IActor> GetActorList<T>() {
            List<IActor> actorList = new List<IActor>();
            
            if (this.player is T) {
                actorList.Add(this.player);
            }

            foreach (IActor actor in this.actors.Values) {
                if (actor is T) {
                    actorList.Add(actor);
                }
            }

            return actorList;
        }

        internal IEnumerable<IdNamePair> GetActorIdList<T>() { // [TODO] List or IEnumerable
            List<IdNamePair> actorList = new List<IdNamePair>();

            if (this.player is T) { // [TODO]
                actorList.Add(new IdNamePair(player.Id, player.Name));
            }

            foreach (IActor actor in this.actors.Values) {
                if (actor is T) {
                    actorList.Add(new IdNamePair(actor.Id, actor.Name));
                }
            }

            return actorList;
        }

        // [TODO]
        //internal IEnumerable<IActor> GetAllActors() {
        //    return this.actors.Values.ToList<IActor>();
        //}

        // [TODO]
        public IPlayer GetPlayer() {
            return this.player;
        }

        internal bool SetActorDefaultSprite(int actorId, int genericImageId) {
            if (!HasActor(actorId)) {
                // [TODO] msg
                return false;
            }

            if (!GenericImage.HasInstance(genericImageId)) { 
                // [TODO] msg
                return false;
            }

            this.actors[actorId].DefaultSprite = GenericImage.GetInstance(genericImageId);
            return true;
        }

        internal bool HasActor(int actorId) {
            if (actorId == this.player.Id) {
                return true;
            }

            return this.actors.ContainsKey(actorId);
        }

        #endregion Actor methods

        #region ICondition methods

        // [SC] used for cloning
        private bool AddCondition(ICondition condition) {
            if (HasCondition(condition.Id)) {
                // [TODO] error msg
                return false;
            }

            if (String.IsNullOrEmpty(condition.Description)) {
                // [TODO] error msg
                return false;
            }

            // [SC] register new condition instance
            this.conditions.Add(condition.Id, condition);
            return true;
        }

        private ICondition GetCondition(int id) {
            if (HasCondition(id)) {
                return this.conditions[id];
            }
            else { 
                // [TODO] error msg
                return null;
            }
        }

        public T GetCondition<T>(int id) {
            ICondition condition = GetCondition(id);
            if (condition != null && (condition is T || condition.GetType().IsSubclassOf(typeof(T)))) {
                return (T)condition;
            }
            return default(T);
        }

        internal IEnumerable<IdNamePair> GetConditionList<T>() {
            List<IdNamePair> conditionList = new List<IdNamePair>();
            foreach (ICondition cond in this.conditions.Values) {
                if (cond is T || cond.GetType().IsSubclassOf(typeof(T))) {
                    conditionList.Add(new IdNamePair(cond.Id, cond.Description));
                }
            }
            return conditionList;
        }
        
        // [TODO] do not remove condition if it is in the map layer
        /*internal bool RemoveCondition(int id) {
            if (!HasCondition(id)) {
                // [TODO] error msg
                return false;
            }

            return this.conditions.Remove(id);
        }*/

        internal bool HasCondition(int id) {
            return this.conditions.ContainsKey(id);
        }

        #endregion ICondition methods

        #region IAction methods

        // [SC] used for cloning
        private bool AddAction(IAction action) {
            if (HasAction(action.Id)) {
                // [TODO] error msg
                return false;
            }

            if (String.IsNullOrEmpty(action.Description)) {
                // [TODO] error msg
                return false;
            }

            // [SC] register new action instance
            this.actions.Add(action.Id, action);
            return true;
        }

        private IAction GetAction(int id) {
            if (HasAction(id)) {
                return this.actions[id];
            }
            else {
                // [TODO] error msg
                return null;
            }
        }

        public T GetAction<T>(int id) {
            IAction action = GetAction(id);
            if (action != null && (action is T || action.GetType().IsSubclassOf(typeof(T)))) {
                return (T)action;
            }
            return default(T);
        }

        internal IEnumerable<IdNamePair> GetActionList<T>() {
            List<IdNamePair> actionList = new List<IdNamePair>();
            foreach (IAction action in this.actions.Values) {
                if (action is T || action.GetType().IsSubclassOf(typeof(T))) {
                    actionList.Add(new IdNamePair(action.Id, action.Description));
                }
            }
            return actionList;
        }

        // [TODO] do not remove condition if it is in the map layer
        /*internal bool RemoveAction(int id) {
            if (!HasAction(id)) {
                // [TODO] error msg
                return false;
            }

            return this.actions.Remove(id);
        }*/

        internal bool HasAction(int id) {
            return this.actions.ContainsKey(id);
        }

        #endregion IAction methods

        #region RegionTrigger methods

        internal RegionTrigger AddRegionTrigger(string description, int width, int height, IEnumerable<int> actorIdList) {
            RegionTrigger regionTrigger = new RegionTrigger(description, width, height);

            return AddRegionTriggerHelper(regionTrigger, actorIdList);
        }

        // [TODO] review the code, improve if necessary
        // [TODO] should i double check for id validity (it is already checked at RgionTrigger's base constructor)
        internal RegionTrigger AddRegionTrigger(int id, string description, int width, int height, IEnumerable<int> actorIdList) {
            RegionTrigger regionTrigger = new RegionTrigger(id, description, width, height, false);

            return AddRegionTriggerHelper(regionTrigger, actorIdList);
        }

        private RegionTrigger AddRegionTriggerHelper(RegionTrigger regionTrigger, IEnumerable<int> actorIdList) {
            if (actorIdList != null) {
                foreach (int actorId in actorIdList) {
                    if (HasActor(actorId)) {
                        regionTrigger.AddTargetActor(GetActor<IActor>(actorId));
                    }
                    else {
                        // [TODO] error msg
                    }
                }
            }

            if (AddCondition(regionTrigger)) {
                return regionTrigger;
            }
            else {
                return null;
            }
        }

        // [TODO] do not change region trigger if it is in the map layer
        /*internal bool ChangeRegionTrigger(int id, string name, int width, int height) {
            if (!HasRegionTrigger(id)) {
                // [TODO] error msg
                return false;
            }

            if (!String.IsNullOrEmpty(name) {
                this.regionTriggers[id].Name = name;
            }

            this.regionTriggers[id].Width = width;
            this.regionTriggers[id].Width = height;

            return true;
        }*/

        #endregion RegionTrigger methods

        #region ActionInvokeModule methods

        internal ActionInvokeModule AddActionInvokeModule(string name, Type moduleType
                                            , IEnumerable<int> actorIdList, string[] moduleParams) {
            if (moduleType.IsSubclassOf(typeof(IModule))) {
                // [TODO] error msg
                return null;
            }

            ActionInvokeModule action = new ActionInvokeModule(name, moduleType, moduleParams);

            return AddActionInvokeModuleHelper(action, actorIdList);
        }

        internal ActionInvokeModule AddActionInvokeModule(int id, string name, Type moduleType
                                            , IEnumerable<int> actorIdList, string[] moduleParams) {
            if (moduleType.IsSubclassOf(typeof(IModule))) {
                // [TODO] error msg
                return null;
            }

            ActionInvokeModule action = new ActionInvokeModule(id, name, moduleType, moduleParams, false);

            return AddActionInvokeModuleHelper(action, actorIdList);
        }

        private ActionInvokeModule AddActionInvokeModuleHelper(ActionInvokeModule action, IEnumerable<int> actorIdList) {

            if (actorIdList != null) {
                foreach (int actorId in actorIdList) {
                    if (HasActor(actorId)) {
                        action.AddTargetActor(GetActor<IActor>(actorId));
                    }
                    else {
                        // [TODO] error msg
                    }
                }

                // [TODO] warning msg
            }

            if (AddAction(action)) {
                return action;
            }
            else {
                return null;
            }
        }

        #endregion ActionInvokeModule methods

        #region IRule methods

        internal IEnumerable<IdNamePair> GetRuleList<T>() {
            List<IdNamePair> ruleList = new List<IdNamePair>();
            foreach (IRule ruleObj in this.rules.Values) {
                if (ruleObj is T || ruleObj.GetType().IsSubclassOf(typeof(T))) {
                    ruleList.Add(new IdNamePair(ruleObj.Id, ruleObj.Description));
                }
            }
            return ruleList;
        }

        // [SC] used for cloning
        private bool AddRule(IRule newRule) {
            if (HasRule(newRule.Id)) {
                // [TODO] error msg
                return false;
            }

            if (String.IsNullOrEmpty(newRule.Description)) {
                // [TODO] error msg
                return false;
            }

            // [SC] register new rule instance
            this.rules.Add(newRule.Id, newRule);
            return true;
        }

        internal Rule AddRule(string description, bool canRepeat
            , IEnumerable<int> conditionIdList, IEnumerable<int> actionIdList) {

            // [SC] there should at least one action in the rule
            if (actionIdList == null || actionIdList.Count() == 0) {
                // [TODO] error msg
                return null;
            }

            Rule newRule = new Rule(description);

            return AddRuleHelper(newRule, canRepeat, conditionIdList, actionIdList);
        }

        internal Rule AddRule(int id, string description, bool canRepeat
            , IEnumerable<int> conditionIdList, IEnumerable<int> actionIdList) {

            // [SC] there should at least one action in the rule
            if (actionIdList == null || actionIdList.Count() == 0) {
                // [TODO] error msg
                return null;
            }

            Rule newRule = new Rule(id, description, false);

            return AddRuleHelper(newRule, canRepeat, conditionIdList, actionIdList);
        }

        private Rule AddRuleHelper(Rule newRule, bool canRepeat
            , IEnumerable<int> conditionIdList, IEnumerable<int> actionIdList) {
            
            newRule.CanRepeat = canRepeat;

            foreach (int actionId in actionIdList) {
                if (HasAction(actionId)) {
                    newRule.AddAction(GetAction<IAction>(actionId));
                }
                else {
                    // [TODO] error msg
                    return null;
                }
            }

            if (conditionIdList != null) {
                foreach (int conditionId in conditionIdList) {
                    if (HasCondition(conditionId)) {
                        newRule.AddCondition(GetCondition<ICondition>(conditionId));
                    }
                    else {
                        // [TODO] error msg
                        return null;
                    }
                }
            }

            if (AddRule(newRule)) {
                return newRule;
            }
            else {
                return null;
            }
        }

        internal bool HasRule(int id) {
            return this.rules.ContainsKey(id);
        }

        #endregion IRule methods

        #region Static methods

        // [SC] change into a singleton pattern
        internal static Game CreateInstance(int id, string name, ICanvas canvas) { // <=
            if (String.IsNullOrEmpty(name)) {
                // [TODO] warning message
                return null;
            }
            else if (Game.games.ContainsKey(id)) {
                // [TODO] duplicate id error msg
                return null;
            } 
            else if (canvas == null) {
                // [TODO] null canvas error msg
                return null;
            }
            else if (Game.games.Count > 0) {
                // [TODO] no more than one game instance is allowed
                return null;
            }
            else {
                // [SC] create instance
                Game game = new Game(id, name, canvas);
                // [SC] register the instance id
                Game.games.Add(id, game);  // <=
                return game;
            }
        }

        internal static Game GetInstance() {
            if (Game.games.Count > 0) {
                return Game.games[0];
            }
            else {
                return null;
            }
        }

        internal static Game GetInstance(int id) {
            if (HasInstance(id)) {
                return Game.games[id];
            }
            else {
                // [TODO] error msg
                return null;
            }
        }

        internal static bool HasInstance(int id) {
            return Game.games.ContainsKey(id);
        }

        internal static bool RemoveInstance(int id) {
            // [TODO] before remove the instance need to make sure it is not used anywhere
            throw new NotImplementedException();
        }

        internal static int GetInstanceCount() {
            return Game.games.Count;
        }

        internal static List<Game> GetAllInstances() {
            return Game.games.Values.ToList<Game>();
        }

        #endregion Static methods
    }
}
