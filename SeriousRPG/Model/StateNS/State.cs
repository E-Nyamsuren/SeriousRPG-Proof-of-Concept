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
using SeriousRPG.Model.AnimationNS;

namespace SeriousRPG.Model.StateNS
{
    public class State : StubState 
    {
        #region Fields

        private Animation animation;

        private GenericImage defaultImage; // [SC] used if no animation is assigned

        #endregion Fields

        #region Properties

        internal int Width {
            get {
                if (HasAnimation()) {
                    return this.animation.Width;
                } else {
                    // [TODO] error msg
                    return Cfg.UNASSIGNED_INT;
                }
            }
        }

        internal int Height {
            get {
                if (HasAnimation()) {
                    return this.animation.Height;
                } else {
                    // [TODO] error msg
                    return Cfg.UNASSIGNED_INT;
                }
            }
        }

        internal GenericImage Image {
            get {
                if (HasAnimation()) {
                    return this.animation.Image;
                } else {
                    return this.defaultImage;
                }
            }
        }

        #endregion Properties

        #region Constructors

        private State(StubState state) 
            : base(state.Id, state.Name, state.CoreState) {
        }

        #endregion Constructors

        #region Default image methods

        public GenericImage GetDefaultImage() {
            return this.defaultImage;
        }

        public bool SetDefaultImage(int imageId) {
            return SetDefaultImage(GenericImage.GetInstance(imageId));
        }

        public bool SetDefaultImage(GenericImage image) {
            if (image == null) {
                // [TODO] warning message
                return false;
            }

            if (image.Image == null) { 
                // [TODO] warning message
            }

            this.defaultImage = image;
            return true;
        }

        public bool HasDefaultImage() {
            if (this.defaultImage != null && this.defaultImage.Image != null) {
                return true;
            }
            else {
                return false;
            }
        }

        #endregion Default image methods

        #region Animation methods

        internal int GetAnimationId() {
            if (HasAnimation()) {
                return this.animation.Id;
            }
            else {
                return Cfg.UNASSIGNED_INT;
            }
        }

        private bool SetAnimation(StubAnimation stubAnimation) {
            if (stubAnimation != null) {
                this.animation = Animation.CreateInstance(stubAnimation);
                return true;
            }
            else {
                // [TODO] error msg
                return false;
            }
        }

        internal bool SetAnimation(int stubAnimationId) {
            return SetAnimation(StubAnimation.GetInstance(stubAnimationId));
        }

        private Animation GetAnimation() {
            return this.animation;
        }

        internal bool HasAnimation() {
            return this.animation != null;
        }

        internal void Animate(long currentTime) {
            if (HasAnimation()) {
                this.animation.Animate(currentTime);
            }
            else { 
                // [TODO] error msg
            }
        }

        internal void ResetAnimation() {
            if (HasAnimation()) {
                this.animation.ResetSpriteIndex();
            } else {
                // [TODO] error msg
            }
        }

        internal bool AnimationEnded() {
            if (HasAnimation()) {
                return this.animation.AnimationEnded();
            }
            else {
                return true;
            }
        }

        #endregion Animation methods

        #region Methods

        internal State Clone() {
            State cloneState = new State(StubState.GetInstance(this.Id));

            if (HasAnimation()) {
                cloneState.SetAnimation(this.animation.Clone());
            }

            if (HasDefaultImage()) {
                cloneState.SetDefaultImage(this.defaultImage);
            }

            return cloneState;
        }

        #endregion Methods

        #region Static methods

        internal static State CreateInstance(StubState state) {
            if (state == null) {
                // [TODO] warnign msg
                return null;
            }

            return new State(state);
        }

        #endregion Static methods
    }
}
