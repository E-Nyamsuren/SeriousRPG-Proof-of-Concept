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

namespace SeriousRPG.Model.AnimationNS 
{
    // [SC] to be used in the editor to form a list of available animation that can be assigned to a state; not an actual animation
    internal class StubAnimation
    {
        #region Constants and Statics

        internal const int DEFAULT_SPRITE_DELAY = 200;

        internal const int DEFAULT_SPRITE_INDEX = 0;

        private static Dictionary<int, StubAnimation> stubAnimations; // <=

        #endregion Constants and Statics

        #region Fields

        /// <summary>
        /// Last time in milliseconds the Animation method was called. 0 if the method was not called at all.
        /// </summary>
        private long previousTime = 0;

        /// <summary>
        /// ID of the animation.
        /// </summary>
        private int id; // <=

        /// <summary>
        /// String description of the animation.
        /// </summary>
        private string name;

        /// <summary>
        /// A delay between sprite tansitions measured in milliseconds.
        /// </summary>
        private int spriteDelay = StubAnimation.DEFAULT_SPRITE_DELAY;

        /// <summary>
        /// Index of current sprite.
        /// </summary>
        private int spriteIndex = StubAnimation.DEFAULT_SPRITE_INDEX;

        /// <summary>
        /// If true animation will restart when the last sprite is reached.
        /// </summary>
        private bool canRepeat = true;

        /// <summary>
        /// List of sprites.
        /// </summary>
        protected List<GenericImage> sprites;

        #endregion Fields

        #region Properties

        /// <summary>
        /// ID getter/setter.
        /// </summary>
        public int Id {
            get { return this.id; }
            protected set { this.id = value; }
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

        /// <summary>
        /// Sprite tansition delay getter/setter.
        /// </summary>
        internal int SpriteDelay {
            get { return this.spriteDelay; }
            set {
                if (value >= 0) {
                    this.spriteDelay = value;
                }
                else {
                    // [TODO] error msg
                }
            }
        }

        /// <summary>
        /// Sprite index getter/setter.
        /// </summary>
        internal int SpriteIndex {
            get { return this.spriteIndex; }
            set {
                if (value >= 0 && value < GetSpriteCount()) {
                    this.spriteIndex = value;
                }
                else {
                    // [TODO] some error message?
                }
            }
        }

        /// <summary>
        /// Repeatition flag getter/setter.
        /// </summary>
        internal bool CanRepeat {
            get { return this.canRepeat; }
            set { this.canRepeat = value; }
        }

        internal int Width {
            get {
                if (HasSprites()) {
                    return GetCurrentSprite().Width;
                }
                else {
                    return Cfg.UNASSIGNED_INT;
                }
            }
        }

        internal int Height {
            get {
                if (HasSprites()) {
                    return GetCurrentSprite().Height;
                }
                else {
                    return Cfg.UNASSIGNED_INT;
                }
            }
        }

        internal GenericImage Image {
            get {
                if (HasSprites()) {
                    return GetCurrentSprite();
                }
                else {
                    return null;
                }
            }
        }

        #endregion Properties

        #region Constructors

        static StubAnimation() { // <=
            if (StubAnimation.stubAnimations == null) {
                StubAnimation.stubAnimations = new Dictionary<int, StubAnimation>();
            }
        }

        protected StubAnimation() { }

        protected StubAnimation(int id, string name) {
            this.Id = id;
            this.Name = name;
            this.sprites = new List<GenericImage>();
        }

        #endregion Constructors

        #region Methods

        // [SC] need to be overriden by the Animation class
        virtual internal bool AddSprite(GenericImage sprite) {
            // [TODO] what if GenericImage does not contain Image data

            if (sprite == null) {
                // [TODO] error message
                return false;
            }

            this.sprites.Add(sprite);

            return true;
        }

        // [SC] need to be overriden by the Animation class
        virtual internal bool AddSprite(GenericImage sprite, int spriteIndex) {
            // [TODO] what if GenericImage does not contain Image data

            if (sprite == null) {
                // [TODO] error message
                return false;
            }

            if (spriteIndex > GetSpriteCount()) {
                // [TODO] error message
                return false;
            }

            this.sprites.Insert(spriteIndex, sprite);

            return true;
        }

        // [SC] need to be overriden by the Animation class
        virtual internal bool RemoveSprites() {
            if (HasSprites()) {
                this.sprites.Clear();
                return true;
            }
            else {
                // [TODO] error msg
                return false;
            }
        }

        // [SC] need to be overriden by the Animation class
        virtual internal bool RemoveSpriteAt(int spriteIndex) {
            if (GetSpriteCount() > spriteIndex) {
                this.sprites.RemoveAt(spriteIndex);
                return true;
            }
            else {
                // [TODO] error msg
                return false;
            }
        }

        internal int GetSpriteCount() {
            return this.sprites.Count;
        }

        internal bool HasSprites() {
            if (GetSpriteCount() == 0) {
                return false;
            }
            else {
                return true;
            }
        }

        internal void ResetSpriteIndex() {
            this.SpriteIndex = StubAnimation.DEFAULT_SPRITE_INDEX;
        }

        internal GenericImage GetCurrentSprite() {
            if (HasSprites()) {
                return this.sprites[this.SpriteIndex];
            }
            else {
                // [TODO] error msg
                return null;
            }
        }

        internal GenericImage GetNextSprite() {
            if (HasSprites()) {
                if (this.SpriteIndex + 1 >= GetSpriteCount()) {
                    if (this.CanRepeat) {
                        this.SpriteIndex = StubAnimation.DEFAULT_SPRITE_INDEX;
                    }
                }
                else {
                    ++this.SpriteIndex;
                }

                return this.sprites[this.SpriteIndex];
            }
            else {
                // [TODO] error msg
                return null;
            }
        }

        // [SC] should be called by the Animation class only
        protected internal List<GenericImage> GetSprites() {
            return this.sprites;
        }

        internal void Animate(long currentTime) {
            if (this.previousTime > 0) {
                if (currentTime - this.previousTime > this.SpriteDelay) {
                    GetNextSprite();
                    this.previousTime = currentTime;
                }
            }
            else {
                this.previousTime = currentTime;
            }
        }

        // [SC] return true if there are no sprites or if the last sprite was reached, and false otherwise
        internal bool AnimationEnded() {
            if (HasSprites()) {
                return this.SpriteIndex == GetSpriteCount() - 1;
            }
            return true;
        }

        #endregion Methods

        #region Static methods

        internal static StubAnimation CreateInstance(string name) {
            if (String.IsNullOrEmpty(name)) {
                // [TODO] warning message
                return null;
            }

            return CreateInstanceHelper(Hub.GetUniqueAutoId(), name);
        } 


        internal static StubAnimation CreateInstance(int id, string name) { // <=
            if (String.IsNullOrEmpty(name)) {
                // [TODO] warning message
                return null;
            }
            else if (StubAnimation.stubAnimations.ContainsKey(id) || !Hub.IsValidId(id)) {
                // [TODO] duplicate id error msg
                return null;
            }
            else {
                // [SC] register the id with the Hub
                Hub.RegisterId(id);

                return CreateInstanceHelper(id, name);
            }
        }

        private static StubAnimation CreateInstanceHelper(int id, string name) {
            // [SC] create new instance
            StubAnimation animation = new StubAnimation(id, name);
            // [SC] register new instance
            StubAnimation.stubAnimations.Add(id, animation);

            return animation;
        }

        internal static StubAnimation GetInstance(int id) {
            if (HasInstance(id)) {
                return StubAnimation.stubAnimations[id];
            }
            else {
                // [TODO] error msg
                return null;
            }
        }

        internal static bool HasInstance(int id) {
            return StubAnimation.stubAnimations.ContainsKey(id);
        }

        internal static bool RemoveInstance(int id) {
            // [TODO] before remove the instance need to make sure it is not used anywhere
            throw new NotImplementedException();
        }

        internal static int GetInstanceCount() {
            return StubAnimation.stubAnimations.Count;
        }

        internal static List<StubAnimation> GetAllInstances() {
            return StubAnimation.stubAnimations.Values.ToList<StubAnimation>();
        }

        #endregion Static methods
    }
}
