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

using System.IO;

using System.Diagnostics; // [TODO]

namespace SeriousRPG.FileIO 
{
    // [SC] a sealed class
    internal sealed class Storage : IStorage 
    {
        #region Constants and Statics

        private static volatile IStorage storage;           // [SC] ensure assignment before instance members are accessed
        private static object synchLock = new Object();     // [SC] used to lockon to prevent deadlocks

        #endregion Constants and Statics

        #region Constructors

        private Storage() {
            //AppDomain.CurrentDomain.BaseDirectory // [TODO]
        }

        #endregion Constructors

        #region File IO methods

        public string Load(string path) {
            try {
                string content = File.ReadAllText(GetAbsolutePath(path));
                return content;
            }
            catch (IOException ex) {
                // [TODO] print exception
                return null;
            }
        }

        public bool Save(string path, string content) {
            try {
                File.WriteAllText(GetAbsolutePath(path), content);
                return true;
            } catch (IOException ex) {
                // [TODO] print exception
                return false;
            }
        }

        public bool Delete(string path) {
            try {
                File.Delete(GetAbsolutePath(path));
                return true;
            }
            catch (IOException ex) {
                // [TODO] print exception
                return false;
            }
        } 

        public bool Exists(string path) {
            return File.Exists(GetAbsolutePath(path));
        }

        public string GetAbsolutePath(string path) {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }

        #endregion File IO methods

        #region Static methods

        public static IStorage CreateInstance() {
            if (Storage.storage == null) {
                lock (Storage.synchLock) {
                    if (Storage.storage == null) {
                        Storage.storage = new Storage();
                    }
                }
            }

            return Storage.storage;
        }

        public static IStorage GetInstance() {
            return Storage.CreateInstance();
        }

        public static void ClearInstance() {
            lock (Storage.storage) {
                Storage.storage = null;
            }
        }

        #endregion Static methods
    }
}
