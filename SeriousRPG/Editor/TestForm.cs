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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading; // [SC] Thread

using System.Runtime.InteropServices; // [SC] for DllImport

using SeriousRPG.Misc;
using SeriousRPG.Model;
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.ImageObjectNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.MapNS;
using SeriousRPG.Model.ActorNS;
using SeriousRPG.Model.ModuleNS;

using System.Diagnostics; // [TODO]

namespace SeriousRPG.Editor 
{
    public partial class TestForm : Form, ICanvas
    {
        Color canvasBgColor = Color.AliceBlue;

        Bitmap buffer;
        Graphics bufferGraphics;

        internal TestForm() {
            InitializeComponent();
            
            // [SC] set the canvase with custom background color
            this.BackColor = this.canvasBgColor;

            this.buffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height); // [TODO] what happens if the window is resized?
            this.bufferGraphics = Graphics.FromImage(buffer);

            Application.Idle += ApplicationIdleEventHandler;
        }

        #region Keyboard input handlers

        private void testForm_KeyDownEvent(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Left:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Left, false);
                    break;
                case Keys.Right:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Right, false);
                    break;
                case Keys.Down:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Down, false);
                    break;
                case Keys.Up:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Up, false);
                    break;
                case Keys.A:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.A, false);
                    break;
                case Keys.B:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.B, false);
                    break;
                case Keys.Back:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Back, false);
                    break;
                case Keys.C:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.C, false);
                    break;
                case Keys.D:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.D, false);
                    break;
                case Keys.Delete:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Delete, false);
                    break;
                case Keys.D0:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.D0, false);
                    break;
                case Keys.D1:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.D1, false);
                    break;
                case Keys.D2:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.D2, false);
                    break;
                case Keys.D3:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.D3, false);
                    break;
                case Keys.D4:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.D4, false);
                    break;
                case Keys.D5:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.D5, false);
                    break;
                case Keys.D6:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.D6, false);
                    break;
                case Keys.D7:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.D7, false);
                    break;
                case Keys.D8:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.D8, false);
                    break;
                case Keys.D9:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.D9, false);
                    break;
                case Keys.E:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.E, false);
                    break;
                case Keys.Escape:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Escape, false);
                    break;
                case Keys.F:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.F, false);
                    break;
                case Keys.G:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.G, false);
                    break;
                case Keys.H:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.H, false);
                    break;
                case Keys.I:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.I, false);
                    break;
                case Keys.J:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.J, false);
                    break;
                case Keys.K:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.K, false);
                    break;
                case Keys.L:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.L, false);
                    break;
                case Keys.M:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.M, false);
                    break;
                case Keys.N:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.N, false);
                    break;
                case Keys.O:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.O, false);
                    break;
                case Keys.OemBackslash:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.OemBackslash, false);
                    break;
                case Keys.OemCloseBrackets:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.OemCloseBrackets, false);
                    break;
                case Keys.Oemcomma:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Oemcomma, false);
                    break;
                case Keys.OemMinus:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.OemMinus, false);
                    break;
                case Keys.OemOpenBrackets:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.OemOpenBrackets, false);
                    break;
                case Keys.OemPeriod:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.OemPeriod, false);
                    break;
                case Keys.Oemplus:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Oemplus, false);
                    break;
                case Keys.OemQuestion:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.OemQuestion, false);
                    break;
                case Keys.OemQuotes:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.OemQuotes, false);
                    break;
                case Keys.OemSemicolon:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.OemSemicolon, false);
                    break;
                case Keys.Oemtilde:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Oemtilde, false);
                    break;
                case Keys.P:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.P, false);
                    break;
                case Keys.Q:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Q, false);
                    break;
                case Keys.R:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.R, false);
                    break;
                case Keys.S:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.S, false);
                    break;
                case Keys.Space:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Space, false);
                    break;
                case Keys.T:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.T, false);
                    break;
                case Keys.Tab:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Tab, false);
                    break;
                case Keys.U:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.U, false);
                    break;
                case Keys.V:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.V, false);
                    break;
                case Keys.W:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.W, false);
                    break;
                case Keys.X:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.X, false);
                    break;
                case Keys.Y:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Y, false);
                    break;
                case Keys.Z:
                    RunableGame.GetInstance().KeyDown(SeriousRPG.ControlIO.Keys.Z, false);
                    break;
                default:
                    break;
            }

            e.Handled = true;
        }

        private void TestForm_KeyUp(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Left:
                    RunableGame.GetInstance().KeyUp(SeriousRPG.ControlIO.Keys.Left, false);
                    break;
                case Keys.Right:
                    RunableGame.GetInstance().KeyUp(SeriousRPG.ControlIO.Keys.Right, false);
                    break;
                case Keys.Down:
                    RunableGame.GetInstance().KeyUp(SeriousRPG.ControlIO.Keys.Down, false);
                    break;
                case Keys.Up:
                    RunableGame.GetInstance().KeyUp(SeriousRPG.ControlIO.Keys.Up, false);
                    break;
                default:
                    break;
            }

            e.Handled = true;
        }

        #endregion Keyboard input handlers

        #region Mouse input handlers

        private void TestForm_MouseClick(object sender, MouseEventArgs e) {
            RunableGame.GetInstance().MouseClick(TranslateMouseButton(e.Button), e.X, e.Y);
        }

        private void TestForm_MouseDoubleClick(object sender, MouseEventArgs e) {
            RunableGame.GetInstance().MouseDoubleClick(TranslateMouseButton(e.Button), e.X, e.Y);
        }

        public SeriousRPG.ControlIO.SRMouseButton TranslateMouseButton(MouseButtons button) {
            switch (button) {
                case MouseButtons.Left: return SeriousRPG.ControlIO.SRMouseButton.Left;
                case MouseButtons.Right: return SeriousRPG.ControlIO.SRMouseButton.Right;
                case MouseButtons.Middle: return SeriousRPG.ControlIO.SRMouseButton.Middle;
                case MouseButtons.None: return SeriousRPG.ControlIO.SRMouseButton.None;
                default: return SeriousRPG.ControlIO.SRMouseButton.None;
            }
        }

        #endregion Mouse input handlers

        #region Methods for querying state of the message queue

        private void TestForm_FormClosing(object sender, FormClosingEventArgs e) {
            Application.Idle -= ApplicationIdleEventHandler;
        }

        void ApplicationIdleEventHandler(object sender, EventArgs e) {
            while (IsApplicationIdle()) {
                RunableGame.GetInstance().Iterate();

                // [SC] sleep for a specified
                Thread.Sleep(15);
            }
        }

        bool IsApplicationIdle() {
            NativeMessage result;
            return PeekMessage(out result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
        }

        [DllImport("user32.dll")]
        public static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);

        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage {
            public IntPtr Handle;
            public uint Message;
            public IntPtr WParameter;
            public IntPtr LParameter;
            public uint Time;
            public Point Location;
        }
        
        #endregion Methods for querying state of the message queue

        #region ICanvas methods

        private Panel modulePanel;
        public IUiComponentModule UiModule {
            get {
                return this.modulePanel as IUiComponentModule;
            }
            set {
                if (this.modulePanel != null && value != null) { 
                    // [TODO] error msg
                }
                else if (value == null) {
                    if (this.modulePanel != null) { // [SC] removing the modules
                        this.Controls.Remove(this.modulePanel);
                        this.modulePanel = null;
                        this.Focus();
                    }
                } 
                else {
                    Panel temp = value as Panel;
                    if (temp != null) {
                        Clear();
                        this.modulePanel = temp;
                        this.modulePanel.Location = new Point(0, 0);
                        this.Controls.Add(this.modulePanel);
                    }
                }
            }
        }

        public SizeSR SizeSR {
            get {
                return new SizeSR(this.ClientSize.Width, this.ClientSize.Height);
            }
            set {
                if (value != null) {
                    this.ClientSize = new Size(value.Width, value.Height);

                    this.bufferGraphics.Dispose();
                    this.buffer.Dispose();

                    this.buffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height); // [TODO] what happens if the window is resized?
                    this.bufferGraphics = Graphics.FromImage(this.buffer);
                }
            }
        }

        public void DrawImage(GenericImage image, float x, float y, int width, int height) {
            if (this.UiModule == null) {
                this.bufferGraphics.DrawImage(image.Image, x, y - (height - 1), width, height);
            }
        }

        public void DrawRectangle(float x, float y, int width, int height, Brush brush) {
            if (this.UiModule == null) {
                this.bufferGraphics.FillRectangle(brush, x, y - (height - 1), width, height);
            }
        }

        public void DrawRectangle(float x, float y, int width, int height, Color borderColor, int borderWidth) {
            if (this.UiModule == null) {
                this.bufferGraphics.DrawRectangle(new Pen(borderColor, borderWidth), x, y - (height - 1), width, height);
            }
        }

        public void DrawText(string text, float x, float y, string font, float fontSize, Color color) {
            if (this.UiModule == null) {
                Font drawFont = new Font(font, fontSize);
                SolidBrush drawBrush = new SolidBrush(color);
                StringFormat drawFormat = new StringFormat();
                SizeF textSize = this.bufferGraphics.MeasureString(text, drawFont);
                this.bufferGraphics.DrawString(text, drawFont, drawBrush, x, y - (textSize.Height - 1), drawFormat);
                drawFont.Dispose();
                drawBrush.Dispose();
            }
        }

        public SizeSR GetTextSize(string text, string font, float fontSize) {
            Font drawFont = new Font(font, fontSize); 
            SizeF textSize = this.bufferGraphics.MeasureString(text, drawFont);
            return new SizeSR((int)textSize.Width, (int)textSize.Height);
        }

        public void Clear() {
            if (this.UiModule == null) {
                this.bufferGraphics.Clear(this.canvasBgColor);
            }
        }

        public void DrawBufferToCanvas() {
            if (this.UiModule == null) {
                Graphics g = this.CreateGraphics();
                g.DrawImage(buffer, 0, 0);
                g.Dispose();
            }
        }

        #endregion ICanvas methods
    }
}
