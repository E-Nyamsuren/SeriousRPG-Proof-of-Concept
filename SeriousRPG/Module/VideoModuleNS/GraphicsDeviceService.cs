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
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace SeriousRPG.Module.VideoModuleNS 
{
    public class GraphicsDeviceService : IGraphicsDeviceService {
        private static readonly GraphicsDeviceService _instance = new GraphicsDeviceService();
        private static int _refCount;

        private GraphicsDevice _device;

        public GraphicsDevice GraphicsDevice {
            get { return _device; }
        }

        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset = (s, e) => { };
        public event EventHandler<EventArgs> DeviceResetting = (s, e) => { };

        protected GraphicsDeviceService() { }

        public static GraphicsDeviceService AddRef(IntPtr windowHandle, int width, int height) {
            if (Interlocked.Increment(ref _refCount) == 1)
                _instance.CreateDevice(windowHandle, width, height);

            return _instance;
        }

        public void Release() {
            Release(true);
        }

        protected void Release(bool disposing) {
            if (Interlocked.Decrement(ref _refCount) == 0) {
                if (disposing) {
                    if (DeviceDisposing != null)
                        DeviceDisposing(this, EventArgs.Empty);

                    _device.Dispose();
                }

                _device = null;
            }
        }

        protected void CreateDevice(IntPtr windowHandle, int width, int height) {
            GraphicsAdapter adapter = GraphicsAdapter.DefaultAdapter;
            GraphicsProfile profile = GraphicsProfile.Reach;
            PresentationParameters pp = new PresentationParameters() {
                DeviceWindowHandle = windowHandle,
                BackBufferWidth = Math.Max(width, 1),
                BackBufferHeight = Math.Max(height, 1),
            };

            _device = new GraphicsDevice(adapter, profile, pp);

            if (DeviceCreated != null)
                DeviceCreated(this, EventArgs.Empty);
        }

        public void ResetDevice(int width, int height) { }
    }
}
