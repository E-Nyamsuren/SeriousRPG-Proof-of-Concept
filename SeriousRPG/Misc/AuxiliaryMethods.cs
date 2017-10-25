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

namespace SeriousRPG.Misc
{
    public static class AuxiliaryMethods 
    {
        public static Random rnd = new Random();

        public static void Shuffle<T>(this IList<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    public class CellSR
    {
        private int rowIndex;
        private int colIndex;

        public int RowIndex {
            get { return this.rowIndex; }
            set {
                if (value >= 0) {
                    this.rowIndex = value;
                }
                else { 
                    // [TODO] throw error?
                }
            }
        }

        public int ColIndex {
            get { return this.colIndex; }
            set {
                if (value >= 0) {
                    this.colIndex = value;
                }
                else {
                    // [TODO] throw error?
                }
            }
        }

        public CellSR() { }

        public CellSR(int rowIndex, int colIndex) {
            this.RowIndex = rowIndex;
            this.ColIndex = colIndex;
        }
    }

    public class PointSR 
    {
        public float X {
            get;
            set;
        }

        public float Y {
            get;
            set;
        }

        public PointSR() {
        }

        public PointSR(float x, float y) {
            this.X = x;
            this.Y = y;
        }
    }

    public class VectorSR
    {
        public float X {
            get;
            set;
        }

        public float Y {
            get;
            set;
        }

        public VectorSR() {
        }

        public VectorSR(float x, float y) {
            this.X = x;
            this.Y = y;
        }

        public static float Length(PointSR point) {
            return VectorSR.Length(point.X, point.Y);
        }

        public static float Length(VectorSR vector) {
            return VectorSR.Length(vector.X, vector.Y);
        }

        public static float Length(float x, float y) {
            return (float)Math.Sqrt(x * x + y * y);
        }

        public static VectorSR Normalize(PointSR point) {
            return VectorSR.Normalize(point.X, point.Y);
        }

        public static VectorSR Normalize(VectorSR vector) {
            return VectorSR.Normalize(vector.X, vector.Y);
        }

        public static VectorSR Normalize(float x, float y) {
            return new VectorSR(x / Length(x, y), y / Length(x, y));
        }

        public static float EuclideanDistance(PointSR pointOne, PointSR pointTwo) {
            return VectorSR.EuclideanDistance(pointOne.X, pointOne.Y, pointTwo.X, pointTwo.Y);
        }

        public static float EuclideanDistance(VectorSR vectorOne, VectorSR vectorTwo) {
            return VectorSR.EuclideanDistance(vectorOne.X, vectorOne.Y, vectorTwo.X, vectorTwo.Y);
        }

        public static float EuclideanDistance(float xOne, float yOne, float xTwo, float yTwo) {
            return Length(xTwo - xOne, yTwo - yOne);
        }

        public static VectorSR Direction(PointSR pointOne, PointSR pontTwo) {
            return VectorSR.Direction(pointOne.X, pointOne.Y, pontTwo.X, pontTwo.Y);
        }

        public static VectorSR Direction(VectorSR vectorOne, VectorSR vectorTwo) {
            return VectorSR.Direction(vectorOne.X, vectorOne.Y, vectorTwo.X, vectorTwo.Y);
        }

        public static VectorSR Direction(float xOne, float yOne, float xTwo, float yTwo) {
            return VectorSR.Normalize(xTwo - xOne, yTwo - yOne);
        }
    }

    public class SizeSR
    {
        private int width = 0;
        private int height = 0;

        public int Width {
            get {
                return this.width;
            }
            set {
                if (value >= 0) {
                    this.width = value;
                }
                else { 
                    // [TODO] error msg
                }
            }
        }

        public int Height {
            get {
                return this.height;
            }
            set {
                if (value >= 0) {
                    this.height = value;
                }
                else {
                    // [TODO] error msg
                }
            }
        }

        public SizeSR() { }

        public SizeSR(int width, int height) {
            this.Width = width;
            this.Height = height;
        }
    }
}
