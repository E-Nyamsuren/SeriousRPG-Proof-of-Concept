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

namespace SeriousRPG.Model.MapNS 
{
    public interface IRenderableObject 
    {
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
        /// An image to render.
        /// </summary>
        GenericImage Image { // [TODO] use IGenericImage
            get;
        }
    }
}
