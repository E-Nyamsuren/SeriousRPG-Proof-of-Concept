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

namespace SeriousRPG.Model.MapNS 
{
    public interface ICollidableObject
    {
        /// [TODO] should it be unique among instance of ICollidableObject?
        /// <summary>
        /// Id of the object.
        /// </summary>
        int Id {
            get;
        }

        /// <summary>
        /// Left-most corner coordinate measured in pixels.
        /// </summary>
        float X {
            get;
        }

        /// <summary>
        /// Bottom-most corner coordinate measured in pixels.
        /// </summary>
        float Y {
            get;
        }

        /// <summary>
        /// Width in pixels.
        /// </summary>
        int Width {
            get;
        }

        /// <summary>
        /// Height in pixels.
        /// </summary>
        int Height {
            get;
        }

        /// <summary>
        /// If false then this object is not collidable. The collision detection returns false.
        /// </summary>
        bool CanCollide {
            get;
            set;
        }

        bool IgnoreCollisionWith(int id);

        bool RemoveIgnoreCollisionWith(int id);

        bool InIgnoreCollisionList(int id);

        /// <summary>
        /// Returns true if this object collides with an object passed as parameter.
        /// </summary>
        /// <param name="targetObj">Collidable object</param>
        /// <returns>True if collision is detected and false otherwie.</returns>
        bool CollidesWith(ICollidableObject targetObj);

        /// <summary>
        /// Returns true if this object collides with the specified rectangle.
        /// </summary>
        /// <param name="x">Left corner of the rectangle</param>
        /// <param name="y">Bottom corner of the rectangle </param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        /// <returns>True if collision is detected and false otherwie.</returns>
        bool CollidesWith(float x, float y, int width, int height);
    }
}
