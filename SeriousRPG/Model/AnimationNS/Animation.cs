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
    // [SC] Animation class is necessary since different states using the same animation may have different sprite indices 
    internal class Animation : StubAnimation
    {
        #region Constructors

        /// <summary>
        /// Assign auto-generated Id to the instance.
        /// </summary>
        /// <param name="stubAnimation">StubAnimation on which this animation is based</param>
        private Animation(StubAnimation stubAnimation)
            : base(stubAnimation.Id, stubAnimation.Name) {
            
            this.SpriteDelay = stubAnimation.SpriteDelay;
            this.CanRepeat = stubAnimation.CanRepeat;

            // [TODO] what if stub animation does not contain sprites
            this.sprites = stubAnimation.GetSprites();
        }

        #endregion Constructors

        #region Methods

        internal Animation Clone() {
            Animation cloneAnimation = new Animation(StubAnimation.GetInstance(this.Id));

            cloneAnimation.SpriteDelay = this.SpriteDelay;
            cloneAnimation.SpriteIndex = this.SpriteIndex;
            cloneAnimation.sprites = this.sprites;

            return cloneAnimation;
        }

        override internal bool AddSprite(GenericImage sprite) {
            // [TODO] warning msg
            return false;
        }

        override internal bool AddSprite(GenericImage sprite, int spriteIndex) {
            // [TODO] warning msg
            return false;
        }

        override internal bool RemoveSprites() {
            // [TODO] warning msg
            return false;
        }

        override internal bool RemoveSpriteAt(int spriteIndex) {
            // [TODO] warning msg
            return false;
        }

        #endregion Methods

        #region Static methods

        internal static Animation CreateInstance(StubAnimation stubAnimation) {
            if (stubAnimation == null) {
                // [TODO] warnign msg
                return null;
            }

            return new Animation(stubAnimation);
        }

        #endregion Static methods
    }
}
