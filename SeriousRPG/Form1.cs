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

using System.Diagnostics;

using SeriousRPG.Model;
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.ImageObjectNS;
using SeriousRPG.Model.AnimationNS;
using SeriousRPG.Model.StateNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.ActorNS;

using SeriousRPG.Editor;
using SeriousRPG.FileIO;
using SeriousRPG.HubNS;


namespace SeriousRPG 
{
    public partial class TestApp : Form 
    {
        public TestApp() {
            InitializeComponent();

            // [SC] creating a Hub singleton
            Hub.GetInstance();

            // [SC] creating a Storage singleton
            Storage.GetInstance();
        }

        private void createNewGame_Click(object sender, EventArgs e) {
            // [SC] 1. Init canvas (map editor)
            MapEditorForm mapEditorForm = new MapEditorForm();

            // [SC] 2. Load resources and init game
            int gameId = 0;
            prepareGameAssets(gameId, mapEditorForm);

            // [SC] 3. Connect map editor to the game resources
            mapEditorForm.LoadGameResources();

            // [SC] 4. Show map editor
            mapEditorForm.Show();
        }

        private void prepareGameAssets(int gameId, ICanvas canvas) {
            int imageId = 0;

            GenericImage.CreateInstance(++imageId, "grass1", "resources/grass1.png");
            GenericImage.CreateInstance(++imageId, "water1", "resources/water1.png");

            GenericImage.CreateInstance(++imageId, "flower1_27x31", "resources/flower1_27x31.png");
            GenericImage.CreateInstance(++imageId, "flower1_29x28", "resources/flower1_29x28.png");
            GenericImage.CreateInstance(++imageId, "flower1_32x32", "resources/flower1_32x32.png");
            GenericImage.CreateInstance(++imageId, "plant1_28x24", "resources/plant1_28x24.png");
            GenericImage.CreateInstance(++imageId, "stones_32x29", "resources/stones_32x29.png");
            GenericImage.CreateInstance(++imageId, "tree1_32x64", "resources/tree1_32x64.png");
            GenericImage.CreateInstance(++imageId, "tree2_32x64", "resources/tree2_32x64.png");
            GenericImage.CreateInstance(++imageId, "tree1_64x64", "resources/tree1_64x64.png");
            GenericImage.CreateInstance(++imageId, "tree2_64x64", "resources/tree2_64x64.png");

            imageId = 100;
            GenericImage.CreateInstance(++imageId, "Char1_down1", "resources/Char1_down1.png");
            GenericImage.CreateInstance(++imageId, "Char1_down2", "resources/Char1_down2.png");
            GenericImage.CreateInstance(++imageId, "Char1_down3", "resources/Char1_down3.png");

            GenericImage.CreateInstance(++imageId, "Char1_left1", "resources/Char1_left1.png");
            GenericImage.CreateInstance(++imageId, "Char1_left2", "resources/Char1_left2.png");
            GenericImage.CreateInstance(++imageId, "Char1_left3", "resources/Char1_left3.png");

            GenericImage.CreateInstance(++imageId, "Char1_right1", "resources/Char1_right1.png");
            GenericImage.CreateInstance(++imageId, "Char1_right2", "resources/Char1_right2.png");
            GenericImage.CreateInstance(++imageId, "Char1_right3", "resources/Char1_right3.png");

            GenericImage.CreateInstance(++imageId, "Char1_up1", "resources/Char1_up1.png");
            GenericImage.CreateInstance(++imageId, "Char1_up2", "resources/Char1_up2.png");
            GenericImage.CreateInstance(++imageId, "Char1_up3", "resources/Char1_up3.png");

            GenericImage.CreateInstance(++imageId, "player_portrait", "resources/player_portrait.png");

            imageId = 120;
            GenericImage.CreateInstance(++imageId, "npc1_down1", "resources/npc1_down1.png");
            GenericImage.CreateInstance(++imageId, "npc1_down2", "resources/npc1_down2.png");
            GenericImage.CreateInstance(++imageId, "npc1_down3", "resources/npc1_down3.png");

            GenericImage.CreateInstance(++imageId, "npc1_left1", "resources/npc1_left1.png");
            GenericImage.CreateInstance(++imageId, "npc1_left2", "resources/npc1_left2.png");
            GenericImage.CreateInstance(++imageId, "npc1_left3", "resources/npc1_left3.png");

            GenericImage.CreateInstance(++imageId, "npc1_right1", "resources/npc1_right1.png");
            GenericImage.CreateInstance(++imageId, "npc1_right2", "resources/npc1_right2.png");
            GenericImage.CreateInstance(++imageId, "npc1_right3", "resources/npc1_right3.png");

            GenericImage.CreateInstance(++imageId, "npc1_up1", "resources/npc1_up1.png");
            GenericImage.CreateInstance(++imageId, "npc1_up2", "resources/npc1_up2.png");
            GenericImage.CreateInstance(++imageId, "npc1_up3", "resources/npc1_up3.png");

            GenericImage.CreateInstance(++imageId, "npc1_portrait", "resources/npc1_portrait.png");

            imageId = 140;
            GenericImage.CreateInstance(++imageId, "npc2_down1", "resources/npc2_down1.png");
            GenericImage.CreateInstance(++imageId, "npc2_down2", "resources/npc2_down2.png");
            GenericImage.CreateInstance(++imageId, "npc2_down3", "resources/npc2_down3.png");

            GenericImage.CreateInstance(++imageId, "npc2_left1", "resources/npc2_left1.png");
            GenericImage.CreateInstance(++imageId, "npc2_left2", "resources/npc2_left2.png");
            GenericImage.CreateInstance(++imageId, "npc2_left3", "resources/npc2_left3.png");

            GenericImage.CreateInstance(++imageId, "npc2_right1", "resources/npc2_right1.png");
            GenericImage.CreateInstance(++imageId, "npc2_right2", "resources/npc2_right2.png");
            GenericImage.CreateInstance(++imageId, "npc2_right3", "resources/npc2_right3.png");

            GenericImage.CreateInstance(++imageId, "npc2_up1", "resources/npc2_up1.png");
            GenericImage.CreateInstance(++imageId, "npc2_up2", "resources/npc2_up2.png");
            GenericImage.CreateInstance(++imageId, "npc2_up3", "resources/npc2_up3.png");

            GenericImage.CreateInstance(++imageId, "npc2_portrait", "resources/npc2_portrait.png");

            imageId = 160;
            GenericImage.CreateInstance(++imageId, "npc3_down1", "resources/npc3_down1.png");
            GenericImage.CreateInstance(++imageId, "npc3_down2", "resources/npc3_down2.png");
            GenericImage.CreateInstance(++imageId, "npc3_down3", "resources/npc3_down3.png");

            GenericImage.CreateInstance(++imageId, "npc3_left1", "resources/npc3_left1.png");
            GenericImage.CreateInstance(++imageId, "npc3_left2", "resources/npc3_left2.png");
            GenericImage.CreateInstance(++imageId, "npc3_left3", "resources/npc3_left3.png");

            GenericImage.CreateInstance(++imageId, "npc3_right1", "resources/npc3_right1.png");
            GenericImage.CreateInstance(++imageId, "npc3_right2", "resources/npc3_right2.png");
            GenericImage.CreateInstance(++imageId, "npc3_right3", "resources/npc3_right3.png");

            GenericImage.CreateInstance(++imageId, "npc3_up1", "resources/npc3_up1.png");
            GenericImage.CreateInstance(++imageId, "npc3_up2", "resources/npc3_up2.png");
            GenericImage.CreateInstance(++imageId, "npc3_up3", "resources/npc3_up3.png");

            GenericImage.CreateInstance(++imageId, "npc23_portrait", "resources/npc3_portrait.png");

            imageId = 180;
            GenericImage.CreateInstance(++imageId, "portrait1", "resources/portrait1.png");

            List<GenericImage> list = GenericImage.GetAllInstances();
            foreach (GenericImage image in list) {
                Debug.WriteLine(String.Format("Image {0}", image.Id));
            }

            ////////////////////////////////////////////////////

            // [SC] creating stub animations for player movements
            imageId = 100;
            int saId = 1000;
            // [SC] creating a stub animation instance first; note, stub animation itself is agnostic to its purpose
            StubAnimation temp = StubAnimation.CreateInstance(++saId, "Char1_down");
            // [SC] adding relevant images to the animation
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            temp = StubAnimation.CreateInstance(++saId, "Char1_left");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            temp = StubAnimation.CreateInstance(++saId, "Char1_right");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            temp = StubAnimation.CreateInstance(++saId, "Char1_up");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            // [SC] add preset
            imageId = 100;
            saId = 1000;
            ActorPreset preset = new ActorPreset();
            preset.Name = "Player preset";
            preset.Id = imageId;
            preset.DefaultSpriteId = imageId + 1;
            preset.PortraitId = imageId + 13;
            preset.DownSaId = ++saId;
            preset.LeftSaId = ++saId;
            preset.RightSaId = ++saId;
            preset.UpSaId = ++saId;
            ActorPreset.AddPreset(preset);

            // [SC] creating stub animations for NPC1
            imageId = 120;
            saId = 1020;
            temp = StubAnimation.CreateInstance(++saId, "npc1_down");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            temp = StubAnimation.CreateInstance(++saId, "npc1_left");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            temp = StubAnimation.CreateInstance(++saId, "npc1_right");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            temp = StubAnimation.CreateInstance(++saId, "npc1_up");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            // [SC] add presets
            imageId = 120;
            saId = 1020;
            preset = new ActorPreset();
            preset.Name = "Old man";
            preset.Id = imageId;
            preset.DefaultSpriteId = imageId + 1;
            preset.PortraitId = imageId + 13;
            preset.DownSaId = ++saId;
            preset.LeftSaId = ++saId;
            preset.RightSaId = ++saId;
            preset.UpSaId = ++saId;
            ActorPreset.AddPreset(preset);

            // [SC] creating stub animations for NPC2
            imageId = 140;
            saId = 1040;
            temp = StubAnimation.CreateInstance(++saId, "npc2_down");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            temp = StubAnimation.CreateInstance(++saId, "npc2_left");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            temp = StubAnimation.CreateInstance(++saId, "npc2_right");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            temp = StubAnimation.CreateInstance(++saId, "npc2_up");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            // [SC] add preset
            imageId = 140;
            saId = 1040;
            preset = new ActorPreset();
            preset.Name = "Old man";
            preset.Id = imageId;
            preset.DefaultSpriteId = imageId + 1;
            preset.PortraitId = imageId + 13;
            preset.DownSaId = ++saId;
            preset.LeftSaId = ++saId;
            preset.RightSaId = ++saId;
            preset.UpSaId = ++saId;
            ActorPreset.AddPreset(preset);

            // [SC] creating stub animations for NPC2
            imageId = 160;
            saId = 1060;
            temp = StubAnimation.CreateInstance(++saId, "npc3_down");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            temp = StubAnimation.CreateInstance(++saId, "npc3_left");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            temp = StubAnimation.CreateInstance(++saId, "npc3_right");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            temp = StubAnimation.CreateInstance(++saId, "npc3_up");
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(++imageId));
            temp.AddSprite(GenericImage.GetInstance(imageId - 1));

            // [SC] add preset
            imageId = 160;
            saId = 1060;
            preset = new ActorPreset();
            preset.Name = "Very old man";
            preset.Id = imageId;
            preset.DefaultSpriteId = imageId + 1;
            preset.PortraitId = imageId + 13;
            preset.DownSaId = ++saId;
            preset.LeftSaId = ++saId;
            preset.RightSaId = ++saId;
            preset.UpSaId = ++saId;
            ActorPreset.AddPreset(preset);

            ////////////////////////////////////////////////////

            // [SC] creating a new game instance
            Game game = Game.CreateInstance(gameId, "Test game", canvas);

            // [SC] retrieving player's instance
            Player player = game.GetPlayer() as Player;
            // [SC] setting player's default sprite
            player.DefaultSprite = GenericImage.GetInstance(101);
            // [SC] setting player's portrait
            player.Portrait = GenericImage.GetInstance(113);

            saId = 1000;
            // [SC] setting player's animation for down state
            State tempState = player.GetState((int)Hub.Reserved.STATE_DOWN);
            tempState.SetAnimation(++saId);

            // [SC] setting player's animation for left state
            tempState = player.GetState((int)Hub.Reserved.STATE_LEFT);
            tempState.SetAnimation(++saId);

            // [SC] setting player's animation for right state
            tempState = player.GetState((int)Hub.Reserved.STATE_RIGHT);
            tempState.SetAnimation(++saId);

            // [SC] setting player's animation for up state
            tempState = player.GetState((int)Hub.Reserved.STATE_UP);
            tempState.SetAnimation(++saId);

            ////////////////////////////////////////////////////
        }
    }
}
