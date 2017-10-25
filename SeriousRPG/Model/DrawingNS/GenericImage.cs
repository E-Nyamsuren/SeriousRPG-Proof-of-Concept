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

/// No changes to the Image object should be done once it is loaded from a file.
/// Assumes that the same Image instance can be used in different GenericImage instances
/// If changes are to be made then a new GenericImage instance should be created with a copy of the image.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

using SeriousRPG.HubNS;
using SeriousRPG.FileIO;

namespace SeriousRPG.Model.DrawingNS
{
    // [SC] sealed to prevent inheritance
    public sealed class GenericImage 
    {
        #region Statics and Constants
        
        private static Dictionary<int, GenericImage> genericImages;

        #endregion Statics and Constants

        #region Fields

        /// <summary>
        /// Id unique among all generic images.
        /// </summary>
        private int id;

        /// <summary>
        /// Short description of the image.
        /// </summary>
        private string name;

        /// <summary>
        /// Image object
        /// </summary>
        private Image image;

        /// <summary>
        /// Path to a file from which the image was read
        /// </summary>
        private string filepath;

        #endregion Fields

        #region Properties

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

        internal string Filepath {
            get { return this.filepath; }
            private set {
                if (!String.IsNullOrEmpty(value)) {
                    this.filepath = value;
                }
                else {
                    // [TODO] error msg
                }
            }
        }

        internal Image Image {
            get { return this.image; } // [TODO] should it be private to prevent passing the reference outside of this class?
            private set {
                if (value != null) {
                    this.image = value;
                }
                else {
                    // [TODO] error msg
                }
            }
        }

        /// <summary>
        /// Returns image width in pixels.
        /// </summary>
        internal int Width {
            get {
                if (this.image != null) {
                    return this.image.Width;
                }
                else {
                    // [TODO] error msg
                    return Cfg.UNASSIGNED_INT;
                }
            }
        }

        /// <summary>
        /// Returns image height in pixels.
        /// </summary>
        internal int Height {
            get {
                if (this.image != null) {
                    return this.image.Height;
                }
                else {
                    // [TODO] error msg
                    return Cfg.UNASSIGNED_INT;
                }
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Static constructor
        /// </summary>
        static GenericImage() {
            if (GenericImage.genericImages == null) {
                GenericImage.genericImages = new Dictionary<int, GenericImage>();

                // [SC] creating a generic placeholder GenericImage with a predefined Id
                // [TODO] filepath
                GenericImage placeholder = GenericImage.CreateInstance((int)Hub.Reserved.PLACEHOLDER_ID, "placeholder", "resources/placeholder.png");
                if (placeholder == null) {
                    // [SC] intentionally throwing an exception, placeholder files should be always available
                    throw new NullReferenceException("Unable to load resource: placeholder.png.");
                }

                // [TODO] filepath
                GenericImage portraitPlaceholder = GenericImage.CreateInstance((int)Hub.Reserved.PORTRAIT_PLACEHOLDER_ID, "portrait_placeholder", "resources/portrait_placeholder.png");
                if (portraitPlaceholder == null) {
                    // [SC] intentionally throwing an exception, placeholder files should be always available
                    throw new NullReferenceException("Unable to load resource: portrait_placeholder.png.");
                }

                // [SC] creating a placeholder for FALSE GenericImage with a predefined Id
                // [TODO] filepath
                GenericImage falseImg = GenericImage.CreateInstance((int)Hub.Reserved.FALSE_ID, "false", "resources/0.png");
                if (falseImg == null) {
                    // [SC] intentionally throwing an exception, placeholder files should be always available
                    throw new NullReferenceException("Unable to load resource: 0.png.");
                }

                // [SC] creating a placeholder for TRUE GenericImage with a predefined Id
                // [TODO] filepath
                GenericImage trueImg = GenericImage.CreateInstance((int)Hub.Reserved.TRUE_ID, "true", "resources/1.png");
                if (trueImg == null) {
                    // [SC] intentionally throwing an exception, placeholder files should be always available
                    throw new NullReferenceException("Unable to load resource: 1.png.");
                }
            }
        }

        // [TODO][TEST] make sure instantiation is not possible
        /// <summary>
        /// A private constructor to prevent any instantiation
        /// </summary>
        private GenericImage() { }

        /// Called by the factory method to create instances of GenericImage class
        /// </summary>
        /// <param name="id">Id unique among instance of GenericImage class.</param>
        /// <param name="name">Short description of the image.</param>
        /// <param name="newImage">A non-null instance of Image</param>
        /// <param name="filepath">Path to file from which image was loaded.</param>
        private GenericImage(int id, string name, Image newImage, String filepath) {
            this.Id = id;
            this.Name = name;
            this.Image = newImage;
            this.Filepath = filepath;
        }

        #endregion Constructors

        #region Methods

        internal GenericImage Clone() {
            return this;
        }

        #endregion Methods

        #region Static methods

        // [TEST][TODO]
        internal static GenericImage CreateInstance(int id, string name, String filepath) {
            try {
                if (GenericImage.genericImages.ContainsKey(id) || !Hub.IsValidId(id)) {
                    // [TODO] duplicate id error msg
                    return null;
                }
                // [TEST][TODO]
                Image newImage = Image.FromFile(Storage.GetInstance().GetAbsolutePath(filepath));

                // [SC] register the id with the Hub
                Hub.RegisterId(id);
                // [SC] create new instance
                GenericImage genericImage = new GenericImage(id, name, newImage, filepath);
                // [SC] register the new instance
                GenericImage.genericImages.Add(id, genericImage);

                return genericImage;
            }
            catch (Exception ex) {
                Hub.DeregisterId(id);
                // [TODO] error msg
                return null;
            }
        }

        internal static GenericImage GetInstance(int id) {
            if (HasInstance(id)) {
                return GenericImage.genericImages[id];
            }
            else {
                // [TODO] error msg
                return null;
            }
        }

        internal static bool HasInstance(int id) {
            return GenericImage.genericImages.ContainsKey(id);
        }

        internal static bool RemoveInstance(int id) {
            // [TODO] before remove the image need to make sure it is not used anywhere
            throw new NotImplementedException();
        }

        internal static int GetInstanceCount() {
            return GenericImage.genericImages.Count;
        }

        internal static List<GenericImage> GetAllInstances() {
            return GenericImage.genericImages.Values.ToList<GenericImage>();
        }

        internal static GenericImage GetPlaceholder() {
            return GetInstance((int)Hub.Reserved.PLACEHOLDER_ID);
        }

        internal static GenericImage GetSpritePlaceholder() {
            return GenericImage.GetPlaceholder();
        }

        internal static GenericImage GetPortraitPlaceholder() {
            return GetInstance((int)Hub.Reserved.PORTRAIT_PLACEHOLDER_ID);
        }

        #endregion Static methods
    }
}
