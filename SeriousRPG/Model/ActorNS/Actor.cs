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
using SeriousRPG.ControlIO;
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.MapNS;
using SeriousRPG.Model.StateNS;
using SeriousRPG.Model.ImageObjectNS;

namespace SeriousRPG.Model.ActorNS
{
    internal class Actor : IActor, ICollidableObject, IClickable, IRenderableObject 
    {
        #region Consts

        public const int PORTRAIT_WIDTH = 144;
        public const int PORTRAIT_HEIGHT = 144;

        #endregion Consts

        // [SC][TODO] ability to define custom states?

        #region Instance fields

        /// <summary>
        /// ID of the actor.
        /// </summary>
        private int id;

        /// <summary>
        /// Ingame name of the actor.
        /// </summary>
        private string name = Cfg.UNKNOWN_NAME;

        /// <summary>
        /// In-game description of the actor;
        /// </summary>
        private string description = Cfg.UNKNOWN_NAME;

        /// <summary>
        /// Tile's left-most drawing coordinate in pixels.
        /// </summary>
        private float x = Cfg.DEFAULT_X;

        /// <summary>
        /// Tile's bottom-most drawing coordinate in pixels.
        /// </summary>
        private float y = Cfg.DEFAULT_Y;

        private bool canCollide = true;

        private bool isClickable = true;

        private HashSet<int> ignoreColiisionList = new HashSet<int>();

        // 4 is a default/idle state.
        // 0 1 2
        // 3 4 5
        // 6 7 8
        protected List<State> states;

        /// <summary>
        /// This sprite will be used if a state does not have sprite assigned to it.
        /// </summary>
        private GenericImage defaultSprite = GenericImage.GetSpritePlaceholder();

        /// <summary>
        /// Actor's portrait.
        /// </summary>
        private GenericImage portrait = GenericImage.GetPortraitPlaceholder();

        /// <summary>
        /// Actor's health. Any positive value.
        /// </summary>
        private int health = 0;

        /// <summary>
        /// Actor's experience. Any positive value.
        /// </summary>
        private int experience = 0;

        /// <summary>
        /// Actor's level. Any positive value.
        /// </summary>
        private int level = 0;

        /// <summary>
        /// The current state of the actor.
        /// </summary>
        private State currentState;

        public event SRMouseEventHandler MouseClick;
        public event SRMouseEventHandler MouseDoubleClick;
        public event SRMouseEventHandler MouseDown;
        public event SRMouseEventHandler MouseUp;
        public event SRMouseEventHandler MouseOver;

        #endregion Instance fields

        #region Properties

        /// <summary>
        /// ID getter/setter.
        /// </summary>
        public int Id {
            get { return this.id; }
            private set { this.id = value; }
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

        /// <summary>
        /// Description getter/setter
        /// </summary>
        public string Description {
            get { return this.description; }
            set {
                if (!String.IsNullOrEmpty(value)) {
                    this.description = value;
                }
                else {
                    // [TODO]
                }
            }
        }

        public float X {
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

        public float Y {
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

        /// <summary>
        /// Actor's current width depends on Actor's state. 
        /// State's width is defined by the size of currently animated sprite.
        /// If state does not have animation then width of Actor's default sprite is returned.
        /// If the default sprite is not defined (should not happen) then standard tile width is returned.
        /// </summary>
        public int Width {
            get {
                int width = Cfg.UNASSIGNED_INT;

                if (HasCurrentState()) {
                    width = this.currentState.Width;
                }

                if (width == Cfg.UNASSIGNED_INT && this.defaultSprite != null) {
                    width = this.DefaultSprite.Width;
                }

                if (width == Cfg.UNASSIGNED_INT) {
                    width = Tile.TILE_WIDTH;
                }

                return width;
            }
            set {
                // [TODO] Non customizable Width warning msg
            }
        }

        public int Height {
            get {
                int height = Cfg.UNASSIGNED_INT;

                if (HasCurrentState()) {
                    height = this.currentState.Height;
                }

                if (height == Cfg.UNASSIGNED_INT && this.defaultSprite != null) {
                    height = this.DefaultSprite.Height;
                }

                if (height == Cfg.UNASSIGNED_INT) {
                    height = Tile.TILE_HEIGHT;
                }

                return height;
            }
            set { 
                // [TODO] Non customizable Height warning msg
            }
        }

        // [SC][TODO] Image object is too platform-specific?
        public GenericImage Image {
            get {
                GenericImage image = null;

                if (HasCurrentState()) {
                    image = this.currentState.Image; // [SC] current state still may not have image assigned to it
                }

                if (image == null && this.defaultSprite != null) {
                    image = this.defaultSprite;
                }

                return image;
            }
            set {
                // [TODO] Non customizable Image warning msg
            }
        }

        public GenericImage DefaultSprite {
            get { return this.defaultSprite; }
            set {
                if (value != null) {
                    this.defaultSprite = value;
                }
                else {
                    // [TODO]
                }
            }
        }

        public GenericImage Portrait {
            get { return this.portrait; }
            set {
                if (value != null && value.Width == Actor.PORTRAIT_WIDTH && value.Height == Actor.PORTRAIT_HEIGHT) {
                    this.portrait = value;
                }
                else {
                    // [TODO]
                }
            }
        }

        public bool CanCollide {
            get { return this.canCollide; }
            set { this.canCollide = value; }
        }

        public bool IsClickable {
            get { return this.isClickable; }
            set { this.isClickable = value; }
        }

        public int Health {
            get { return this.health; }
            set {
                if (value >= 0) {
                    this.health = value;
                }
                else {
                    // [TODO]
                    this.health = 0;
                }
            }
        }

        public int Experience {
            get { return this.experience; }
            set {
                if (value >= 0) {
                    this.experience = value;
                }
                else {
                    // [TODO]
                    this.experience = 0;
                }
            }
        }

        public int Level {
            get { return this.level; }
            set {
                if (value >= 0) {
                    this.level = value;
                }
                else {
                    // [TODO]
                    this.level = 0;
                }
            }
        }

        #endregion Properties

        #region Constructors

        internal Actor(string name, GenericImage defaultSprite) {
            this.Id = Hub.GetUniqueAutoId();

            Init(name, defaultSprite);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">               Actor's ID.</param>
        /// <param name="name">             Actor's name.</param>
        /// <param name="defaultSprite">    Default sprite.</param>
        internal Actor(int id, string name, GenericImage defaultSprite, bool isClone) {
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

            Init(name, defaultSprite);
        }

        private void Init(string name, GenericImage defaultSprite) {
            this.Name = name;
            this.DefaultSprite = defaultSprite; // [TODO] default sprite should be there no matter what

            this.states = new List<State>();

            // [SC] creating an idle state and setting it as a curret state
            State idleState = State.CreateInstance(StubState.GetInstance((int)Hub.Reserved.STATE_IDLE));
            if (AddState(idleState)) {
                this.currentState = idleState;
            }
            else {
                // [TODO]
            }
        }

        #endregion Constructors

        #region State methods

        public bool AddState(State state) {
            if (this.states.Exists(p => p.Id == state.Id)) {
                // [TODO] same Id state already exists error
                return false;
            }
            else {
                this.states.Add(state);
                return true;
            }
        }

        public bool RemoveState(int stateId) {
            if (HasCurrentState() && this.currentState.Id == stateId ) {
                // [TODO] cannot remove current state msg
                return false;
            }

            return this.states.Remove(this.states.Find(p => p.Id == stateId));
        }

        public void ClearStates() {
            this.states.Clear();
        }

        public State GetState(int stateId) {
            return this.states.Find(p => p.Id == stateId);
        }

        public List<IdNamePair> GetStates() {
            List<IdNamePair> pairs = new List<IdNamePair>(); 
            foreach (State state in this.states) {
                pairs.Add(new IdNamePair(state.Id, state.Name));
            }
            return pairs;
        }

        public bool HasStates() {
            if (this.states.Count > 0) {
                return true;
            }
            else {
                return false;
            }
        }

        public bool HasState(int stateId) {
            return this.states.Find(p => p.Id == stateId) != null;
        }

        public bool HasCurrentState() {
            if (this.currentState != null) {
                return true;
            }
            else {
                return false;
            }
        }

        public bool SetCurrentState(int stateId) {
            State state = this.states.Find(p => p.Id == stateId);

            if (state != null) {
                this.currentState = state;
                return true;
            }
            else {
                // [TODO] error msg
                return false;
            }
        }

        public int GetCurrentStateId() {
            if (HasCurrentState()) {
                return this.currentState.Id;
            }
            else {
                // [TODO] error msg
                return Cfg.UNASSIGNED_INT;
            }
        }

        public void Animate(long currentTime) {
            if (HasCurrentState()) {
                this.currentState.Animate(currentTime);
            }
            else { 
                // [TODO] error msg
            }
        }

        public bool AnimationEnded() {
            if (HasCurrentState()) {
                return this.currentState.AnimationEnded();
            }
            return true;
        }

        #endregion State method

        #region Collision detection methods

        public bool IgnoreCollisionWith(int id) {
            return this.ignoreColiisionList.Add(id); // [TODO] what if id is already present
        }

        public bool RemoveIgnoreCollisionWith(int id) {
            return this.ignoreColiisionList.Remove(id);
        }

        public bool InIgnoreCollisionList(int id) {
            return this.ignoreColiisionList.Contains(id);
        }

        public bool CollidesWith(ICollidableObject targetObj) {
            if (targetObj == this) {
                return false;
            }
            else if (InIgnoreCollisionList(targetObj.Id) || targetObj.InIgnoreCollisionList(this.Id)) {
                return false;
            }
            else {
                return CollidesWith(targetObj.X, targetObj.Y, targetObj.Width, targetObj.Height);
            }
        }

        public bool CollidesWith(float x, float y, int width, int height) {
            if (this.CanCollide) {
                float maxXOne = this.X + (this.Width - 1);
                float minYOne = this.Y - (this.Height - 1);

                float maxXTwo = x + (width - 1);
                float minYTwo = y - (height - 1);

                if ((this.X <= maxXTwo && maxXOne >= x && minYOne <= y && this.Y >= y) ||
                    (x <= maxXOne && maxXTwo >= this.X && minYTwo <= this.Y && y >= this.Y)) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }

        #endregion Collision detection methods

        #region Mouse event raisers

        public bool Contains(int x, int y) {
            return this.IsClickable &&
                this.X <= x && x < (this.X + this.Width) &&
                (this.Y - this.Height) < y && y <= this.Y;
        }

        public virtual void OnMouseClick(SRMouseEventArgs e) {
            SRMouseEventHandler handler = this.MouseClick;
            if (handler != null) {
                handler(this, e);
            }
        }

        public virtual void OnMouseDoubleClick(SRMouseEventArgs e) {
            SRMouseEventHandler handler = this.MouseDoubleClick;
            if (handler != null) {
                handler(this, e);
            }
        }

        public virtual void OnMouseDown(SRMouseEventArgs e) {
            SRMouseEventHandler handler = this.MouseDown;
            if (handler != null) {
                handler(this, e);
            }
        }

        public virtual void OnMouseUp(SRMouseEventArgs e) {
            SRMouseEventHandler handler = this.MouseUp;
            if (handler != null) {
                handler(this, e);
            }
        }

        public virtual void OnMouseOver(SRMouseEventArgs e) {
            SRMouseEventHandler handler = this.MouseOver;
            if (handler != null) {
                handler(this, e);
            }
        }

        #endregion Mouse event raisers

        #region Misc methods

        public virtual IActor Clone() {
            Actor cloneActor = new Actor(this.Id, this.Name, this.DefaultSprite.Clone(), true);

            cloneActor.Description = this.Description; // [TODO] copy string

            cloneActor.Portrait = this.Portrait.Clone();

            cloneActor.Health = this.Health;
            cloneActor.Experience = this.Experience;
            cloneActor.Level = this.Level;

            cloneActor.CanCollide = this.CanCollide;
            cloneActor.IsClickable = this.IsClickable;

            cloneActor.X = this.X;
            cloneActor.Y = this.Y;

            cloneActor.ClearStates();
            foreach (State state in this.states) {
                cloneActor.AddState(state.Clone());
            }

            if (HasCurrentState()) {
                cloneActor.SetCurrentState(GetCurrentStateId());
            }

            return cloneActor;
        }

        public override string ToString() {
            return String.Format("[{0}][{1}]", this.Id, this.Name);
        }

        #endregion Misc methods
    }
}
