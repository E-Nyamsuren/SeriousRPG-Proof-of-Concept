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

using SeriousRPG.Model.EventNS;
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.ActorNS;

namespace SeriousRPG.Model.MapNS
{
    internal class Foreground : GenericLayer 
    {
        #region Constants

        internal const int LAYER_TYPE = 2;
        internal const string LAYER_NAME = "Foreground";

        #endregion Constants

        #region Fields

        private List<IActor> actors;

        #endregion Fields

        #region Constructors

        internal Foreground(int rowNum, int colNum)
            : base(Foreground.LAYER_TYPE, Foreground.LAYER_NAME, rowNum, colNum) {
            actors = new List<IActor>();
        }

        #endregion Constructors

        #region Actor methods

        /// <summary>
        /// Adds actor to be rendered in the foreground layer. Uses BOTTOM-left anchor point.
        /// </summary>
        /// <param name="actor">    Actor object to render.</param>
        /// <param name="x">        Left coordinate in pixels.</param>
        /// <param name="y">        Bottom coordinate in pixels.</param>
        /// <returns></returns>
        internal bool AddActor(IActor actor, float x, float y) {
            if (actor == null) {
                // [TODO] error msg
                return false;
            }
            else if (!ContainsRectangle(x, y, actor.Width, actor.Height)) {
                // [TODO] error msg
                return false;
            }
            else if (actors.Contains(actor)) {
                actor.X = x;
                actor.Y = y;
                return true;
            }
            else {
                actor.X = x;
                actor.Y = y;
                actors.Add(actor);
                return true;
            }
        }

        internal List<IActor> CloneActors() {
            List<IActor> cloneActors = new List<IActor>();
            foreach (IActor actor in this.actors) {
                cloneActors.Add(actor.Clone());
            }
            return cloneActors;
        }

        internal bool IsNoCollision(ICollidableObject clientObject) {
            bool noCollisionFlag = true;
            foreach (ICollidableObject actor in this.actors) {
                if (actor.CollidesWith(clientObject)) {
                    EventRegistry.AddEvent(new CollisionEvent(
                        String.Format("{0} {1}", actor.Id, clientObject.Id), actor.Id, clientObject.Id));
                    noCollisionFlag = false;
                }
            }
            return noCollisionFlag;
        }

        #endregion Actor methods

        #region Layer methods

        override internal void Act() {
            foreach (IActor actor in this.actors) {
                if (!(actor is DynamicActor)) {
                    continue;
                }

                if (actor is Player) {
                    continue;
                }

                (actor as DynamicActor).Move(true);
            }
        }

        override internal void Draw(ICanvas canvas) {
            if (this.IsRendered) {
                foreach (IActor actor in actors) {
                    canvas.DrawImage(actor.Image, actor.X, actor.Y, actor.Width, actor.Height);
                }
            }
        }

        internal void Animate(long currentTime) {
            foreach (IActor actor in actors) {
                actor.Animate(currentTime);
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
