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

namespace SeriousRPG.ControlIO 
{
    public enum Keys 
    {
        A,
        Alt,
        B,
        Back, // [SC] BACKSPACE key
        C,
        CapsLoc, // [SC] CAPS LOCK key
        ControlKey, // [SC] CTRL key
        D,
        D0,
        D1,
        D2,
        D3,
        D4,
        D5,
        D6,
        D7,
        D8,
        D9,
        Delete, // [SC] DELETE key
        Down,
        E,
        Enter, // [SC] ENTER key
        Escape, // [SC] ESC key
        F,
        F1,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8,
        F9,
        F10,
        F11,
        F12,
        G,
        H,
        I,
        Insert, // [SC] INSERT key
        J,
        K,
        L,
        LControlKey, // [SC] left CTRL key
        Left,
        LMenu, // [SC] left ALT key
        LShiftKey, // [SC] left SHIFT key
        M,
        Menu, // [SC] ALT key
        N,
        O,
        OemBackslash, // [SC] The OEM angle bracket or backslash key
        OemClear, // [SC] The CLEAR key
        OemCloseBrackets, // [SC] The OEM close bracket key
        Oemcomma, // [SC] The OEM comma key
        OemMinus, // [SC] The OEM minus key
        OemOpenBrackets, // [SC] The OEM open bracket key
        OemPeriod, // [SC] The OEM period key
        OemPipe, // [SC] The OEM pipe key
        Oemplus, // [SC] The OEM plus key
        OemQuestion, // [SC] The OEM question mark key
        OemQuotes, // [SC] The OEM singled/double quote key
        OemSemicolon, // [SC] The OEM Semicolon key
        Oemtilde, // [SC] The OEM tilde key
        P,
        Q,
        R,
        RControlKey, // [SC] right CTRL key
        Right,
        RMenu, // [SC] right ALT key
        RShiftKey, // [SC] right SHIFT key
        S,
        ShiftKey, // [SC] SHIFT key
        Space, // [SC] SPACEBAR key
        T,
        Tab, // [SC] TAB key
        U,
        Up,
        V,
        W,
        X,
        Y,
        Z
    };

    public static class KeyboardQwertyEnglish
    {
        public static string GetString(Keys key, bool upperCase) {
            switch (key) {
                case Keys.A: return upperCase ? "A" : "a";
                case Keys.B: return upperCase ? "B" : "b";
                case Keys.C: return upperCase ? "C" : "c";
                case Keys.D: return upperCase ? "D" : "d";
                case Keys.D0: return upperCase ? ")" : "0";
                case Keys.D1: return upperCase ? "!" : "1";
                case Keys.D2: return upperCase ? "@" : "2";
                case Keys.D3: return upperCase ? "#" : "3";
                case Keys.D4: return upperCase ? "$" : "4";
                case Keys.D5: return upperCase ? "%" : "5";
                case Keys.D6: return upperCase ? "^" : "6";
                case Keys.D7: return upperCase ? "&" : "7";
                case Keys.D8: return upperCase ? "*" : "8";
                case Keys.D9: return upperCase ? "(" : "9";
                case Keys.E: return upperCase ? "E" : "e";
                case Keys.Enter: return Environment.NewLine;
                case Keys.F: return upperCase ? "F" : "f";
                case Keys.G: return upperCase ? "G" : "g";
                case Keys.H: return upperCase ? "H" : "h";
                case Keys.I: return upperCase ? "I" : "i";
                case Keys.J: return upperCase ? "J" : "j";
                case Keys.K: return upperCase ? "K" : "k";
                case Keys.L: return upperCase ? "L" : "l";
                case Keys.M: return upperCase ? "M" : "m";
                case Keys.N: return upperCase ? "N" : "n";
                case Keys.O: return upperCase ? "O" : "o";
                case Keys.OemBackslash: return upperCase ? "|" : "\\";
                case Keys.OemCloseBrackets: return upperCase ? "}" : "]";
                case Keys.Oemcomma: return upperCase ? "<" : ",";
                case Keys.OemMinus: return upperCase ? "_" : "-";
                case Keys.OemOpenBrackets: return upperCase ? "{" : "[";
                case Keys.OemPeriod: return upperCase ? ">" : ".";
                case Keys.Oemplus: return upperCase ? "+" : "=";
                case Keys.OemQuestion: return upperCase ? "?" : "/";
                case Keys.OemQuotes: return upperCase ? "\"" : "'";
                case Keys.OemSemicolon: return upperCase ? ":" : ";";
                case Keys.Oemtilde: return upperCase ? "~" : "`";
                case Keys.P: return upperCase ? "P" : "p";
                case Keys.Q: return upperCase ? "Q" : "q";
                case Keys.R: return upperCase ? "R" : "r";
                case Keys.S: return upperCase ? "S" : "s";
                case Keys.Space: return " ";
                case Keys.T: return upperCase ? "T" : "t";
                case Keys.Tab: return "\t";
                case Keys.U: return upperCase ? "U" : "u";
                case Keys.V: return upperCase ? "V" : "v";
                case Keys.W: return upperCase ? "W" : "w";
                case Keys.X: return upperCase ? "X" : "x";
                case Keys.Y: return upperCase ? "Y" : "y";
                case Keys.Z: return upperCase ? "Z" : "z";
                default: return ""; // [TODO]
            }
        }
    }
}
