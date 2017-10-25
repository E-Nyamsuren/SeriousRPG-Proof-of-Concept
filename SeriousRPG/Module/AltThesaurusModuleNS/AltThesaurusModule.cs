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
using System.Xml.Linq;
using System.Drawing; // [SC] for Point

using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.ModuleNS;
using SeriousRPG.Model.ActorNS;
using SeriousRPG.Model.AnimationNS;
using SeriousRPG.Model.StateNS;
using SeriousRPG.Model.MapNS;
using SeriousRPG.Model.EventNS;
using SeriousRPG.HubNS;
using SeriousRPG.ControlIO;
using SeriousRPG.Misc;

using System.Diagnostics;

namespace SeriousRPG.Module.AltThesaurusModuleNS 
{
    class AltThesaurusModule : IModule 
    {
        #region Const and static fields

        private const int SCENE_WIDTH = 768;
        private const int SCENE_HEIGHT = 480;

        private const int SCENARIO_FILE_ID = 94001;
        private const int BG_IMAGE_ID = 95001;

        private const string moduleRootPath = "Module/AltThesaurusModule/res/";
        private static string moduleConfigFilepath = Path.Combine(moduleRootPath, "cfg.xml");

        private static int[] ENEMY_ID_LIST = { 95111, 95121, 95131, 95141, 95151, 95161 };
        private static int[] BULLET_ID_LIST = { 95301, 95311 };
        private static int[] MAIN_TREE_ID_LIST = { 95211, 95221, 95231 };
        private static PointSR[] ENEMY_POSITIONS = new PointSR[] { 
            new PointSR(93, 229),
            new PointSR(203, 142),
            new PointSR(360, 105),
            new PointSR(514, 142),
            new PointSR(626, 229)
        };
        private static PointSR[] ENEMY_TEXT_POSITION = new PointSR[] {
            new PointSR(25, 178),
            new PointSR(133, 89),
            new PointSR(288, 49),
            new PointSR(447, 89),
            new PointSR(555, 177)
        };
        private static PointSR MAIN_TREE_POSITION = new PointSR(287, 365);

        private const int DEFAULT_MTREE_HEALTH = 100;
        private const int DEFAULT_HEALTH_DECREMENT = 10;
        private const int DEFAULT_HEALTH_INCREMENT = 10;

        private const int PLAYER_SCORE_INCREMENT = 100;
        private const int PLAYER_SCORE_INCR_SHOW_DURATION = 1500;

        private static PointSR PLAYER_POSITION = new PointSR(360, 455);
        private static PointSR PLAYER_TEXT_POSITION = new PointSR(287, 396);

        private static PointSR PLAYER_SCORE_POSITION = new PointSR(553, AltThesaurusModule.SCENE_HEIGHT - 30);
        private static PointSR PLAYER_SCORE_INCR_POSITION = new PointSR((SCENE_WIDTH - 100) / 2, 188);

        private const int DEFAULT_SHOOTING_DELAY = 5000;
        
        #endregion Const and static fields

        #region Fields

        private List<ICollidableObject> cObjects;

        private SizeSR prevSize;

        private List<Concept> concepts = new List<Concept>();
        private List<Concept> distractors = new List<Concept>();
        private List<Item> items = new List<Item>();
        private int currItemIndex = -1;
        // [SC] measures a duration of one turn
        private int turnStartTime = 0;
        // [SC] stores experiment data
        private ExperimentData experData;

        private GenericImage bgImage = null;

        private List<StubAnimation> enemyStubAnimes = new List<StubAnimation>();
        private List<StubAnimation> bulletStubAnimes = new List<StubAnimation>();

        private List<CustomEnemy> enemies = new List<CustomEnemy>();

        private List<EnemyBullet> eBullets = new List<EnemyBullet>();
        private List<PlayerBullet> pBullets = new List<PlayerBullet>();

        private Actor mainTree;
        private List<int> mainTreeStateId = new List<int>();

        private PointSR playerWordPos = new PointSR();
        
        private int prevConceptIndex = -1;
        private string concept = null;
        private string playerInputStr;

        private int mTreeCurrentHealth = AltThesaurusModule.DEFAULT_MTREE_HEALTH;
        private int healthDecrement = AltThesaurusModule.DEFAULT_HEALTH_DECREMENT;
        private int healthIncrement = AltThesaurusModule.DEFAULT_HEALTH_INCREMENT;

        private int playerScoreIncrement = -1;
        private int playerScoreIncrShowDuration = AltThesaurusModule.PLAYER_SCORE_INCR_SHOW_DURATION;
        private int playerScoreIncrShowStart = -1;
        private int playerScore = 0;

        private int shootingDelay = AltThesaurusModule.DEFAULT_SHOOTING_DELAY;
        private int lastShotTime = -1;

        private bool canInputFlag = false; // [SC] if false then the player's text entries are not processed

        #endregion Fields

        #region Properties

        public Hub Hub {
            get;
            set;
        }

        public bool ToClear {
            get;
            set;
        }

        public bool WasCleared {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        public void Init(Hub hub, params string[] paramArray) {
            this.WasCleared = false;
            this.cObjects = new List<ICollidableObject>();

            this.Hub = hub;
            // 938, 576 [SC] size of the TestForm

            if (paramArray == null || paramArray.Length != 1) {
                // [TODO] critical error; invalid number of params, expected 1 param for thesaurus id
            }

            // [SC] loading a config data
            XDocument configXdoc;
            IEnumerable<XElement> resourceElems = null;
            try {
                string xmlStr = this.Hub.Storage.Load(moduleConfigFilepath);
                configXdoc = XDocument.Parse(xmlStr);
                resourceElems = configXdoc.Root.Elements("local-resource");
            }
            catch (Exception ex) {
                // [TODO] critical error
                Debug.WriteLine("Error reading config xml.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            // [SC] load xml with scenarios data
            try {
                string scenariosFilename = (from el in resourceElems
                                            where (string)el.Attribute("rid") == ""+AltThesaurusModule.SCENARIO_FILE_ID
                                            select el).SingleOrDefault().Value;
                
                string xmlStr = this.Hub.Storage.Load(Path.Combine(moduleRootPath, scenariosFilename));
                XDocument scenariosXdoc = XDocument.Parse(xmlStr);

                // [TODO] verify that the scenarioXdoc has a valid structure

                XElement thesaurus = (from el in scenariosXdoc.Root.Elements("Thesaurus")
                                          where (string)el.Attribute("tid") == paramArray[0]
                                          select el).SingleOrDefault();

                foreach (XElement xConcept in thesaurus.Elements("Concept")) {
                    this.concepts.Add(new Concept {
                        Id = xConcept.Attribute("cid").Value,
                        Text = xConcept.Element("Text").Value
                    });
                }

                if (thesaurus.Element("Type").Value == "experiment") {
                    foreach (XElement xItem in thesaurus.Elements("Item")) {
                        Item item = new Item();
                        item.Id = Int32.Parse(xItem.Attribute("iid").Value);
                        
                        foreach (XElement xConcept in xItem.Elements("Concept")) {
                            Concept concept = this.concepts.Find(p => p.Id.Equals(xConcept.Attribute("cid").Value));
                            item.concepts.Add(concept);
                            if (xConcept.Attribute("target").Value == "1") {
                                item.TargetConcept = concept;
                            }
                        }

                        if (xItem.Element("Shuffle").Value == "1") {
                            item.Shuffle = true;
                            item.concepts.Shuffle();
                        }

                        this.items.Add(item);
                    }

                    bool shuffleItems = false;
                    if (thesaurus.Element("Shuffle").Value == "1") {
                        shuffleItems = true;
                        this.items.Shuffle();
                    }

                    this.experData = new ExperimentData(paramArray[0], this.Hub.RunableGame.GetPlayer().Id, shuffleItems);
                }
                else {
                    foreach (XElement xDistractor in thesaurus.Elements("Distractor")) {
                        this.distractors.Add(new Concept {
                            Id = xDistractor.Attribute("cid").Value,
                            Text = xDistractor.Value
                        });
                    }
                }
            }
            catch (Exception ex) { 
                // [TODO] critical error
                Debug.WriteLine("Error reading scenario xml.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            // [SC] load all image files
            try {
                IEnumerable<XElement> imageElems = from el in resourceElems where (string) el.Attribute("type") == "image" select el;
                foreach (XElement imageElem in imageElems) {
                    int imageId = Int32.Parse(imageElem.Attribute("rid").Value);
                    string imageDescription = imageElem.Attribute("description").Value;
                    string imageFilename = imageElem.Value;

                    if (!GenericImage.HasInstance(imageId)) {
                        GenericImage.CreateInstance(imageId, imageDescription, Path.Combine(moduleRootPath, imageFilename));
                    }
                }
            }
            catch (Exception ex) {
                // [TODO] NON-critical error
                Debug.WriteLine("Error reading images.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            createAssetInstances();

            this.bgImage = GenericImage.GetInstance(AltThesaurusModule.BG_IMAGE_ID);
            
            Player player = this.Hub.RunableGame.GetPlayer() as Player; // [TODO] casting to Player
            // [SC] storing player's world coordinate to be restored at end of module execution
            this.playerWordPos.X = player.X;
            this.playerWordPos.Y = player.Y;
            // [SC] set player's position in the module
            player.X = AltThesaurusModule.PLAYER_POSITION.X;
            player.Y = AltThesaurusModule.PLAYER_POSITION.Y;
            // [SC] set player's current state to facing up
            player.SetCurrentState((int)Hub.Reserved.STATE_UP);

            // [SC] resizing the canvas
            ICanvas canvas = this.Hub.RunableGame.Canvas;
            this.prevSize = canvas.SizeSR;
            canvas.SizeSR = new SizeSR(AltThesaurusModule.SCENE_WIDTH, AltThesaurusModule.SCENE_HEIGHT);

            initNewRound();
        }

        private void createAssetInstances() {
            // [SC] create enemy stub animations
            this.enemyStubAnimes.Clear();
            foreach (int enemyId in AltThesaurusModule.ENEMY_ID_LIST) {
                // [SC] creating a stub animation with an autoid
                StubAnimation enemySA = StubAnimation.CreateInstance(enemyId + "_animation");
                // [SC] adding relevant images to the animation
                int counter = enemyId;
                enemySA.AddSprite(GenericImage.GetInstance(counter));
                enemySA.AddSprite(GenericImage.GetInstance(++counter));
                enemySA.AddSprite(GenericImage.GetInstance(1+counter));
                enemySA.AddSprite(GenericImage.GetInstance(counter));
                // [SC] setting millisecond delays between sprites
                enemySA.SpriteDelay = 300;

                this.enemyStubAnimes.Add(enemySA);
            }

            // [SC] create bullet stub animations
            bulletStubAnimes.Clear();
            foreach (int bulletId in AltThesaurusModule.BULLET_ID_LIST) {
                StubAnimation bulletSA = StubAnimation.CreateInstance(bulletId + "_animation");
                int counter = bulletId;
                bulletSA.AddSprite(GenericImage.GetInstance(counter));
                bulletSA.AddSprite(GenericImage.GetInstance(++counter));
                bulletSA.AddSprite(GenericImage.GetInstance(++counter));
                bulletSA.AddSprite(GenericImage.GetInstance(++counter));
                bulletSA.SpriteDelay = 50;

                bulletStubAnimes.Add(bulletSA);
            }

            // [SC] creating the main tree actor with animations
            this.mainTree = new Actor("main tree", null);
            // [SC] creating states and assigning animations
            foreach (int id in AltThesaurusModule.MAIN_TREE_ID_LIST) {
                // [SC] creating a stub animation with an autoid
                StubAnimation treeStubAnime = StubAnimation.CreateInstance(id + "_animation");
                int counter = id;
                treeStubAnime.AddSprite(GenericImage.GetInstance(counter));
                treeStubAnime.AddSprite(GenericImage.GetInstance(++counter));
                treeStubAnime.AddSprite(GenericImage.GetInstance(++counter));
                treeStubAnime.AddSprite(GenericImage.GetInstance(++counter));
                treeStubAnime.SpriteDelay = 500;

                // [SC] creating and adding states
                StubState stubState = StubState.CreateInstance("placeholder"); // [SC] creating a stub state
                State state = State.CreateInstance(stubState); // [SC] creating a state
                state.SetAnimation(treeStubAnime.Id); // [SC] adding animation to the state
                this.mainTree.AddState(state); // [SC] adding state to the actor

                this.mainTreeStateId.Add(state.Id);
            }
            this.mainTree.SetCurrentState(this.mainTreeStateId[0]);
            this.mainTree.X = AltThesaurusModule.MAIN_TREE_POSITION.X;
            this.mainTree.Y = AltThesaurusModule.MAIN_TREE_POSITION.Y;
        }

        private void initNewRound() {
            this.lastShotTime = -1;

            this.playerInputStr = "";

            // [SC] clear the list of collidable objects
            this.cObjects.Clear();
            // [SC] add the main tree as collidable object
            this.cObjects.Add(this.mainTree);

            this.eBullets.Clear();
            this.pBullets.Clear();

            if (this.items.Count > 0) {
                createExperimentEnemies();
            }
            else {
                createExerciseEnemies();
            }
            
            this.canInputFlag = true;
        }

        private void createExperimentEnemies() {
            // [SC] measure previous turn duration
            if (this.currItemIndex >= 0) {
                Item prevItem = this.items[this.currItemIndex];
                this.experData.AddTurnData(prevItem.Id, prevItem.TargetConcept.Text, prevItem.Shuffle
                    , this.turnStartTime, Environment.TickCount);
            }

            // [SC] if true then no more items are available
            if (++this.currItemIndex >= this.items.Count) {
                // [TODO]
                Debug.WriteLine(String.Format("Data for player {0} for dataset {1}:", this.experData.PlayerId, this.experData.DatasetId));
                Debug.WriteLine(String.Format("\tItemId\tText\tShuffled\tStartTime\tEndTime\tDuration"));
                foreach (TurnData data in this.experData.turnData) {
                    Debug.WriteLine(String.Format("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}"
                        , data.ItemId, data.TargetText, data.Shuffled, data.TurnStartTime
                        , data.TurnEndTime, data.TurnEndTime - data.TurnStartTime));
                }

                this.ToClear = true;
                return;
            }

            this.enemies.Clear();

            Item item = this.items[this.currItemIndex];
            this.concept = item.TargetConcept.Text;

            // [SC] randomly assign animations to enemies
            List<int> animeIds = new List<int>();
            int currEnemyCount = 0;
            while (currEnemyCount < item.concepts.Count) {
                animeIds.Add(AuxiliaryMethods.rnd.Next(0, AltThesaurusModule.ENEMY_ID_LIST.Length - 1));
                ++currEnemyCount;
            }

            // [SC] creating enemy actor objects
            for (int index = 0; index < item.concepts.Count; index++) {
                string text = item.concepts[index].Text;

                CustomEnemy enemy = new CustomEnemy(text, null, text); // [TODO] Game class intance is passed as null
                State idleState = enemy.GetState((int)Hub.Reserved.STATE_IDLE); // [SC] idle state is created by default by the Actor constructor
                idleState.SetAnimation(this.enemyStubAnimes[animeIds[index]].Id);
                enemy.DefaultSprite = idleState.Image;

                this.enemies.Add(enemy);
                this.cObjects.Add(enemy);
            }

            // [SC] assing drawing coordinates to enemies
            for (int index = 0; index < item.concepts.Count; index++) {
                this.enemies[index].X = AltThesaurusModule.ENEMY_POSITIONS[index].X;
                this.enemies[index].Y = AltThesaurusModule.ENEMY_POSITIONS[index].Y;

                this.enemies[index].TextX = AltThesaurusModule.ENEMY_TEXT_POSITION[index].X;
                this.enemies[index].TextY = AltThesaurusModule.ENEMY_TEXT_POSITION[index].Y;
            }

            this.turnStartTime = Environment.TickCount;
        }

        private void createExerciseEnemies() {
            this.enemies.Clear();

            // [SC] randonly choose index for a concept
            int newConceptIndex;
            do {
                newConceptIndex = AuxiliaryMethods.rnd.Next(0, this.concepts.Count - 1);
            } while (newConceptIndex == this.prevConceptIndex);
            this.prevConceptIndex = newConceptIndex;
            
            // [SC] randomly choose four indices for distractors
            List<int> distIndices = new List<int>();
            int currEnemyCount = 0;
            while (currEnemyCount < AltThesaurusModule.ENEMY_POSITIONS.Length - 1) {
                int newDistIndex;
                do {
                    newDistIndex = AuxiliaryMethods.rnd.Next(0, this.distractors.Count - 1);
                } while (distIndices.Contains(newDistIndex));
                distIndices.Add(newDistIndex);
                ++currEnemyCount;
            }

            // [SC] randomly assign animations to enemies
            List<int> animeIds = new List<int>();
            currEnemyCount = 0;
            while (currEnemyCount < AltThesaurusModule.ENEMY_POSITIONS.Length) {
                animeIds.Add(AuxiliaryMethods.rnd.Next(0, AltThesaurusModule.ENEMY_ID_LIST.Length - 1));
                ++currEnemyCount;
            }

            // [SC] creating enemy actor objects
            currEnemyCount = 0;
            while (currEnemyCount < AltThesaurusModule.ENEMY_POSITIONS.Length) {
                string text;

                if (currEnemyCount == 0) {
                    text = this.concepts[newConceptIndex].Text;
                    this.concept = text;
                }
                else {
                    text = this.distractors[distIndices[currEnemyCount - 1]].Text;
                }

                CustomEnemy enemy = new CustomEnemy(text, null, text); // [TODO] Game class intance is passed as null
                State idleState = enemy.GetState((int)Hub.Reserved.STATE_IDLE); // [SC] idle state is created by default by the Actor constructor
                idleState.SetAnimation(this.enemyStubAnimes[animeIds[currEnemyCount]].Id);
                enemy.DefaultSprite = idleState.Image;
        
                this.enemies.Add(enemy);
                this.cObjects.Add(enemy);
                ++currEnemyCount;
            }

            // [SC] assing drawing coordinates to enemies
            this.enemies.Shuffle();
            for (int index = 0; index < this.enemies.Count; index++) {
                this.enemies[index].X = AltThesaurusModule.ENEMY_POSITIONS[index].X;
                this.enemies[index].Y = AltThesaurusModule.ENEMY_POSITIONS[index].Y;

                this.enemies[index].TextX = AltThesaurusModule.ENEMY_TEXT_POSITION[index].X;
                this.enemies[index].TextY = AltThesaurusModule.ENEMY_TEXT_POSITION[index].Y;
            }
        }

        private void VerifyInput() {
            foreach (CustomEnemy enemy in this.enemies) {
                if (this.playerInputStr.Equals(enemy.TargetText, StringComparison.InvariantCultureIgnoreCase)) {
                    this.canInputFlag = false;

                    Player player = this.Hub.RunableGame.GetPlayer() as Player;

                    // [SC] create bullet actor
                    PlayerBullet pBullet = new PlayerBullet("bullet", null);
                    // [SC] set bullet animation
                    State idleState = pBullet.GetState((int)Hub.Reserved.STATE_IDLE); // [SC] idle state is created by default by the Actor constructor
                    idleState.SetAnimation(this.bulletStubAnimes[1].Id);
                    pBullet.DefaultSprite = idleState.Image;
                    // [SC] set bullet spawn coordinates
                    pBullet.X = player.X;
                    pBullet.Y = player.Y - (player.Height + 1);
                    // [SC] set player as destination for the bullet
                    pBullet.DestActor = enemy;
                    // [SC] set to ignore collision with the main tree
                    pBullet.IgnoreCollisionWith(this.mainTree.Id);

                    this.pBullets.Add(pBullet);

                    break;
                }
            }
        }

        private void decreaseMTreeHealth() {
            this.mTreeCurrentHealth -= this.healthDecrement;

            if (this.mTreeCurrentHealth < 0) {
                this.mTreeCurrentHealth = 0;
            }

            setMainTreeState();
        }

        private void increaseMTreeHealth() {
            this.mTreeCurrentHealth += this.healthIncrement;

            if (this.mTreeCurrentHealth > AltThesaurusModule.DEFAULT_MTREE_HEALTH) {
                this.mTreeCurrentHealth = AltThesaurusModule.DEFAULT_MTREE_HEALTH;
            }

            setMainTreeState();
        }

        private void setMainTreeState() {
            float stateHealthIncr = AltThesaurusModule.DEFAULT_MTREE_HEALTH / this.mainTreeStateId.Count;
            for (int increment = 1; increment <= this.mainTreeStateId.Count; increment++) {
                float maxHealthLimit = AltThesaurusModule.DEFAULT_MTREE_HEALTH - stateHealthIncr * (increment - 1);
                float minHealthLimit = AltThesaurusModule.DEFAULT_MTREE_HEALTH - stateHealthIncr * increment;
                if (increment == this.mainTreeStateId.Count) {
                    minHealthLimit = 0;
                }
                if (minHealthLimit < this.mTreeCurrentHealth && this.mTreeCurrentHealth <= maxHealthLimit) { // [SC] make ranges, inclusive and exlusives are correct
                    if (this.mainTree.GetCurrentStateId() != this.mainTreeStateId[increment - 1]) {
                        this.mainTree.SetCurrentState(this.mainTreeStateId[increment - 1]);
                    }
                }
            }
        }

        public void Iterate() {
            if (this.ToClear && !this.WasCleared) {
                this.Clear();
                return;
            }
            else if (this.ToClear || this.WasCleared) {
                return;
            }

            int tickCount = Environment.TickCount;

            HandleEvents(tickCount);

            Animate(tickCount);

            Draw(tickCount);
        }

        public void HandleEvents(int tickCount) {
            Player player = this.Hub.RunableGame.GetPlayer() as Player;

            // [TODO][SC] need a faster and easier system for checking for relevant events
            // [SC] check for collisions
            List<IEvent> collisionEvents = EventRegistry.GetEventsByType(typeof(CollisionEvent));
            for(int colEventIndex = collisionEvents.Count - 1; colEventIndex >= 0; colEventIndex--) {
                CollisionEvent colEvent = collisionEvents[colEventIndex] as CollisionEvent;

                if (colEvent.HasActor(this.mainTree.Id)) {
                    for (int eBulletIndex = this.eBullets.Count - 1; eBulletIndex >= 0; eBulletIndex--) {
                        EnemyBullet eBullet = this.eBullets[eBulletIndex];

                        if (colEvent.HasActor(eBullet.Id)) {
                            // [SC] decrease player health
                            decreaseMTreeHealth();

                            // [SC] remove bullet
                            this.eBullets.RemoveAt(eBulletIndex);

                            // [SC] remove the event from the event registry
                            EventRegistry.RemoveEvent(colEvent.Id);

                            break;
                        }
                    }
                }
                else {
                    for (int pBulletIndex = this.pBullets.Count - 1; pBulletIndex >= 0; pBulletIndex--) {
                        PlayerBullet pBullet = this.pBullets[pBulletIndex];

                        if (colEvent.HasActor(pBullet.Id)) {
                            for(int enemyIndex = this.enemies.Count - 1; enemyIndex >= 0; enemyIndex --) {
                                CustomEnemy enemy = this.enemies[enemyIndex];

                                if (colEvent.HasActor(enemy.Id)) {
                                    // [SC] remove the event from the event registry
                                    EventRegistry.RemoveEvent(colEvent.Id);

                                    if (this.playerInputStr.Equals(this.concept, StringComparison.InvariantCultureIgnoreCase)) {
                                        // [SC] correct answer
                                        increaseMTreeHealth();

                                        this.playerScoreIncrement = AltThesaurusModule.PLAYER_SCORE_INCREMENT * this.enemies.Count;

                                        initNewRound();
                                    }
                                    else {
                                        // [SC] incorrect answer

                                        // [SC] remove bullet
                                        this.pBullets.RemoveAt(pBulletIndex);
                                        // [SC] remove enemy
                                        this.enemies.RemoveAt(enemyIndex);

                                        this.playerScoreIncrement = AltThesaurusModule.PLAYER_SCORE_INCREMENT;

                                        this.playerInputStr = "";

                                        this.canInputFlag = true;
                                    }

                                    this.playerScoreIncrShowStart = tickCount;

                                    this.playerScore += this.playerScoreIncrement;

                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (this.lastShotTime < 0) {
                this.lastShotTime = tickCount;
            } 
            else if (tickCount - this.lastShotTime >= this.shootingDelay && this.enemies.Count > 0) { // [SC] check if one of enemies should shoot
                CustomEnemy enemy = this.enemies[AuxiliaryMethods.rnd.Next(this.enemies.Count - 1)];

                // [SC] create bullet actor
                EnemyBullet eBullet = new EnemyBullet("bullet", null);
                // [SC] set bullet animation
                State idleState = eBullet.GetState((int)Hub.Reserved.STATE_IDLE); // [SC] idle state is created by default by the Actor constructor
                idleState.SetAnimation(this.bulletStubAnimes[0].Id);
                eBullet.DefaultSprite = idleState.Image;
                // [SC] set bullet spawn coordinates
                eBullet.X = enemy.X;
                eBullet.Y = enemy.Y + eBullet.Height + 1;
                // [SC] set the main tree as destination for the bullet
                //eBullet.DestActor = this.mainTree;
                eBullet.DestPoint = new PointSR(this.mainTree.X + this.mainTree.Width / 2, this.mainTree.Y - this.mainTree.Height / 2);

                this.eBullets.Add(eBullet);

                this.lastShotTime = tickCount;
            }

            // [SC] update state of enemy bullets
            foreach (EnemyBullet eBullet in this.eBullets) {
                eBullet.Move(5, false);
            }

            // [SC] update state of player bullets
            foreach (PlayerBullet pBullet in this.pBullets) {
                pBullet.Move(20, false); // [TODO]
            }
        }

        public void Animate(int tickCount) {
            Player player = (this.Hub.RunableGame.GetPlayer() as Player);

            // [SC] animate enemy bullets
            foreach (EnemyBullet eBullet in this.eBullets) {
                eBullet.Animate(tickCount);
            }

            // [SC] animate player bullets
            foreach (PlayerBullet pBullet in this.pBullets) {
                pBullet.Animate(tickCount);
            }

            // [SC] animate enemies
            foreach (CustomEnemy enemy in this.enemies) {
                enemy.Animate(tickCount); // [TODO] Environment.TickCount
            }

            // [SC] animate the main tree
            this.mainTree.Animate(tickCount);

            // [SC] animate player
            player.Animate(tickCount); // [TODO] casting to Player
        }

        public void Draw(int tickCount) {
            ICanvas canvas = this.Hub.RunableGame.Canvas;

            canvas.Clear();

            // [SC] draw background image
            canvas.DrawImage(this.bgImage, 0, this.bgImage.Height - 1, this.bgImage.Width, this.bgImage.Height);

            // [SC] draw enemies and overhead texts
            for(int index=0; index<this.enemies.Count; index++) {
                CustomEnemy enemy = this.enemies[index];
                canvas.DrawImage(enemy.Image, enemy.X, enemy.Y, enemy.Width, enemy.Height);
                if (enemy.TargetText.Equals(this.concept)) {
                    canvas.DrawText(enemy.TargetText, enemy.TextX, enemy.TextY, "Arial", 16, Color.Red); // [TODO] font properties
                } 
                else {
                    canvas.DrawText(enemy.TargetText, enemy.TextX, enemy.TextY, "Arial", 16, Color.Black); // [TODO] font properties
                }
            }

            // [SC] draw the main tree
            canvas.DrawImage(this.mainTree.Image, this.mainTree.X, this.mainTree.Y,
                this.mainTree.Width, this.mainTree.Height);

            // [SC] draw player
            Player player = this.Hub.RunableGame.GetPlayer() as Player; // [TODO] casting to Player
            canvas.DrawImage(player.Image, player.X, player.Y, player.Width, player.Height);

            // [SC] draw player input string
            canvas.DrawText(this.playerInputStr,
                AltThesaurusModule.PLAYER_TEXT_POSITION.X, AltThesaurusModule.PLAYER_TEXT_POSITION.Y,
                "Arial", 20, Color.Black);

            // [SC] draw enemy bullets
            foreach (EnemyBullet eBullet in this.eBullets) {
                canvas.DrawImage(eBullet.Image, eBullet.X, eBullet.Y, eBullet.Width, eBullet.Height);
            }

            // [SC] draw player bullets
            foreach (PlayerBullet pBullet in this.pBullets) {
                canvas.DrawImage(pBullet.Image, pBullet.X, pBullet.Y, pBullet.Width, pBullet.Height);
            }

            // [SC] draw player health
            canvas.DrawRectangle(10, AltThesaurusModule.SCENE_HEIGHT - 30, AltThesaurusModule.DEFAULT_MTREE_HEALTH * 2, 20, Color.Red, 2);
            canvas.DrawRectangle(10, AltThesaurusModule.SCENE_HEIGHT - 30, this.mTreeCurrentHealth * 2, 20, Brushes.Red);

            // [SC] draw player score
            canvas.DrawText(String.Format("Your score: {0}", this.playerScore),
                AltThesaurusModule.PLAYER_SCORE_POSITION.X, AltThesaurusModule.PLAYER_SCORE_POSITION.Y,
                "Arial", 15, Color.Black);

            // [SC] draw player score earnings
            if (this.playerScoreIncrement > 0) {
                if (tickCount - this.playerScoreIncrShowStart < this.playerScoreIncrShowDuration) {
                    canvas.DrawText(String.Format("+{0}", this.playerScoreIncrement),
                        AltThesaurusModule.PLAYER_SCORE_INCR_POSITION.X, AltThesaurusModule.PLAYER_SCORE_INCR_POSITION.Y,
                        "Arial", 30, Color.Green);
                }
                else {
                    this.playerScoreIncrement = -1;
                }
            }
            
            canvas.DrawBufferToCanvas();
        }

        /// <summary>
        /// Call this method before at the end of the module use.
        /// </summary>
        public void Clear() {
            if (this.WasCleared || !this.ToClear) {
                return;
            }

            // [SC][TODO] Unload all images?

            // [SC] reset player's coordinates to its original coordinates
            IPlayer player = this.Hub.RunableGame.GetPlayer();
            player.X = this.playerWordPos.X;
            player.Y = this.playerWordPos.Y;

            // [SC] restore the original size of the canvas
            ICanvas canvas = this.Hub.RunableGame.Canvas;
            canvas.SizeSR = this.prevSize;

            // [SC] prevents the game from transferring control loop to the module
            this.Hub.RunableGame.ActiveModule = null;

            // [SC] indicate that the module was cleared
            this.WasCleared = true;
        }

        public void KeyDown(Keys key, bool upperCase) {
            int tickCount = Environment.TickCount;

            if (this.canInputFlag) {
                if (key == Keys.Back || key == Keys.Delete) {
                    this.playerInputStr = "";
                    Draw(tickCount);
                }
                else if (key == Keys.Escape) {
                    this.ToClear = true;
                }
                else {
                    this.playerInputStr += KeyboardQwertyEnglish.GetString(key, upperCase);
                    Draw(tickCount);
                    VerifyInput();
                }
            }
        }

        public void KeyUp(Keys key, bool upperCase) { }

        public IEnumerable<ICollidableObject> GetCollidableObjects() {
            return this.cObjects;
        }

        public IEnumerable<IClickable> GetClickableObjects() {
            return null;
        }

        #endregion Methods
    }
}
