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

using System.Drawing; // [SC] for Brush

using SeriousRPG.Model.ModuleNS;
using SeriousRPG.Misc;

namespace SeriousRPG.Model.DrawingNS 
{
    public interface ICanvas 
    {
        // [SC] This is for a module that is UI component and relies on its built it drawing and event handling functions
        IUiComponentModule UiModule {
            get;
            set;
        }

        SizeSR SizeSR {
            get;
            set;
        }

        /// <summary>
        /// Draws given generic image instance into the canvas
        /// </summary>
        /// <param name="image">    Generic image instance to draw</param>
        /// <param name="x">        Pixel coordinate of the left corner</param>
        /// <param name="y">        Pixel coordinate of the bottom corner</param>
        /// <param name="width">    Drawing width of the generic image</param>
        /// <param name="height">   Drawing heighr of the generic image</param>
        void DrawImage(GenericImage image, float x, float y, int width, int height);

        void DrawRectangle(float x, float y, int width, int height, Brush brush);

        void DrawRectangle(float x, float y, int width, int height, Color borderColor, int borderWidth);

        void DrawText(string text, float x, float y, string font, float fontSize, Color color);

        SizeSR GetTextSize(string text, string font, float fontSize);

        void Clear();

        void DrawBufferToCanvas();
    }
}
