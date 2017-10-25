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

using System.Drawing; // [SC] for Color class

using SeriousRPG.Model.ModuleNS;
using SeriousRPG.Model.MapNS; // ICollidableObject
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.ActorNS;
using SeriousRPG.Model.AnimationNS;
using SeriousRPG.Model.StateNS;
using SeriousRPG.Model.EventNS;
using SeriousRPG.HubNS;
using SeriousRPG.Misc;
using SeriousRPG.ControlIO;

using System.Diagnostics;

namespace SeriousRPG.Module.CardMatchingModuleNS 
{
    class CardMatchingModule : IModule
    {
        #region Consts and statics

        private const int SCENE_WIDTH = 768;
        private const int SCENE_HEIGHT = 640;

        private const int SCENARIO_FILE_ID = 53001;
        private const int BG_IMAGE_ID = 54001;

        private const string moduleRootPath = "Module/CardMatchingModule/res/";
        private static string moduleConfigFilepath = Path.Combine(moduleRootPath, "cfg.xml");

        #endregion Consts and statics

        #region Fields

        private List<ICollidableObject> cObjects;
        private List<IClickable> clickableObjects;

        private GenericImage bgImage = null;

        private SizeSR prevSize; // [SC] canvas size set for the world map

        #endregion Fields

        #region Player related vars and consts

        private const int PLAYER_SCORE_INCREMENT = 100;
        private static PointSR PLAYER_POSITION = new PointSR(80, 72);
        private static PointSR PLAYER_SCORE_POSITION = new PointSR(576, 584);

        private int playerScoreIncrement = CardMatchingModule.PLAYER_SCORE_INCREMENT;
        private int playerScore = 0;
        
        private PointSR playerWordPos = new PointSR(); // [SC] player's position in the world map
        
        #endregion Player related vars and consts

        #region Enemy related vars and consts

        private const int ENEMY_ID = 54201;
        private static PointSR ENEMY_START_POSITION = new PointSR(640, 72);
        private DynamicActor enemy;

        #endregion Enemey related vars and consts

        #region Firepunch related vars and consts

        private const int FIRE_MOVING_ID = 54401;
        private const int FIRE_DISSOLVING_ID = 54411;
        private static PointSR FIRE_START_POSITION = new PointSR(80, 64);
        private List<StubAnimation> fireStubAnimes = new List<StubAnimation>();
        private List<DynamicActor> firepunches = new List<DynamicActor>();

        private int knockbackDistance = 100;

        #endregion Firepunch related vars and consts

        #region Scenario related vars and consts

        private static SizeSR SCORE_RECT = new SizeSR(400, 200);

        private const int MIN_PAIRS = 2;
        private const int MAX_PAIRS = 6;

        private const int ROW_COUNT = 4;
        private const int COL_COUNT = 3;

        private int playablePairs = CardMatchingModule.MAX_PAIRS; // [SC] the number of card pairs to show
        private int targetPairs = 1;    // [SC] a target number of card pairs a player should match

        private Dictionary<string, Concept> allConcepts = new Dictionary<string, Concept>();

        // [SC] a list of all available pairs in a exercise
        private List<Pair> allPairs = new List<Pair>();
        // [SC] indices of pairs that are used in the current turn
        private List<Pair> turnPairs = new List<Pair>();

        // [SC] list of all items to be used in the experiment
        List<Item> items = new List<Item>();
        private int currItemIndex = -1;

        // [SC] measures a duration of one turn
        private int turnStartTime = 0;
        // [SC] stores experiment data
        private ExperimentData experData;

        private bool drawScoreFlag = false;

        #endregion Scenario related vars and consts

        #region Card related vars and consts

        private const int START_CARD_ID = 54011;
        private const int END_CARD_ID = 54017;
        
        private const int START_X = 64;
        private const int START_Y = 192;
        private const int CARD_WIDTH = 192;
        private const int CARD_HEIGHT = 96;
        private const int HORIZONTAL_MARGIN = 32;
        private const int VERTICAL_MARGIN = 16;

        private List<StubState> cardStubStates = new List<StubState>();
        private List<Cell> cells = new List<Cell>();

        // [SC] the list of two cards that are open at the same time
        private List<Card> openCards = new List<Card>();

        #endregion Card related vars and consts

        #region Instruction related fields and consts

        private const int PREV_ID = 54301;
        private const int NEXT_ID = 54311;

        private static PointSR PREV_POSITION = new PointSR(16, 608);
        private static PointSR NEXT_POSITION = new PointSR(528, 608);
        
        private static PointSR INSTR_START_POSITION = new PointSR(56, 552);

        // [SC] contains next and prev buttons
        List<Actor> instrBtns = new List<Actor>();

        // [SC] the index of the currently shown index (value between 0 and 5)
        private int drawnInstructionIndex = 0;

        #endregion Instruction related fields and consts

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
            this.clickableObjects = new List<IClickable>();

            this.Hub = hub;
            // 938, 576 [SC] size of the TestForm

            // [SC] [0] number of target pairs, [1] the number of pairs; [2] target set id
            if (paramArray == null || paramArray.Length != 3) {
                // [TODO] critical error; invalid number of params, expected 2 params for thesaurus and distractors
            }

            if (!Int32.TryParse(paramArray[0], out this.targetPairs)) {
                // [TODO]
            }

            if (!Int32.TryParse(paramArray[1], out this.playablePairs)) {
                // [TODO]
            }

            if (this.playablePairs < CardMatchingModule.MIN_PAIRS) {
                this.playablePairs = CardMatchingModule.MIN_PAIRS;
                // [TODO]
            }
            else if (this.playablePairs > CardMatchingModule.MAX_PAIRS) {
                this.playablePairs = CardMatchingModule.MAX_PAIRS;
                // [TODO]
            }

            if (this.playablePairs < this.targetPairs) {
                this.targetPairs = this.playablePairs;
                // [TODO]
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
            XDocument scenariosXdoc = null;
            try {
                string scenariosFilename = (from el in resourceElems
                                            where (string)el.Attribute("rid") == "" + CardMatchingModule.SCENARIO_FILE_ID
                                            select el).SingleOrDefault().Value;

                string xmlStr = this.Hub.Storage.Load(Path.Combine(moduleRootPath, scenariosFilename));
                scenariosXdoc = XDocument.Parse(xmlStr);

                // [TODO] verify that the scenarioXdoc has a valid structure
            }
            catch (Exception ex) {
                // [TODO] critical error
                Debug.WriteLine("Error reading scenario xml.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            // [SC] load all image files
            try {
                IEnumerable<XElement> imageElems = from el in resourceElems where (string)el.Attribute("type") == "image" select el;
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
            
            // [SC] load concepts and pairs
            try {
                XElement targetSet = (from el in scenariosXdoc.Root.Elements("Targets")
                    where (string)el.Attribute("setid") == paramArray[2]
                    select el).SingleOrDefault();

                // [SC] parse all concepts
                foreach (XElement xConcept in targetSet.Elements("Concept")) {
                    Concept concept = new Concept();
                    concept.Id = xConcept.Attribute("cid").Value;
                    concept.Text = xConcept.Element("Text").Value;
                    if (xConcept.Element("Image") != null) {
                        int imageId;
                        if (Int32.TryParse(xConcept.Element("Image").Attribute("rid").Value, out imageId)) {
                            concept.Image = GenericImage.GetInstance(imageId);
                        }
                    }
                    this.allConcepts.Add(concept.Id, concept);
                }

                if (targetSet.Element("Type").Value == "exercise") { // [SC] playing in the exercise mode
                    // [SC] parse all pairs
                    foreach (XElement xPair in targetSet.Elements("Pair")) {
                        Pair pair = new Pair();
                        pair.Id = xPair.Attribute("pid").Value;
                        pair.Instruction = xPair.Element("Instruction").Value;
                        foreach (XElement xConcept in xPair.Elements("Concept")) {
                            pair.concepts.Add(this.allConcepts[xConcept.Attribute("cid").Value]);
                        }
                        this.allPairs.Add(pair);
                    }

                    if (this.allPairs.Count < this.playablePairs) {
                        this.playablePairs = this.allPairs.Count;
                    }
                }
                else { // [SC] playing in the experiment mode
                    foreach (XElement xItem in targetSet.Elements("Item")) {
                        Item item = new Item();
                        item.Id = Int32.Parse(xItem.Attribute("iid").Value);

                        // [SC] parse all pairs
                        foreach (XElement xPair in xItem.Elements("Pair")) {
                            Pair pair = new Pair();
                            pair.Id = xPair.Attribute("pid").Value;
                            pair.IsFound = false;
                            if (xPair.Attribute("target").Value == "1") {
                                pair.IsTarget = true;
                            }
                            else {
                                pair.IsTarget = false;
                            }
                            pair.Instruction = xPair.Element("Instruction").Value;
                            foreach (XElement xConcept in xPair.Elements("Concept")) {
                                pair.concepts.Add(this.allConcepts[xConcept.Attribute("cid").Value]);
                            }
                            item.pairs.Add(pair);
                        }

                        this.items.Add(item);
                    }

                    bool shuffled;
                    if (targetSet.Element("Shuffle").Value == "1") {
                        this.items.Shuffle();
                        shuffled = true;
                    }
                    else {
                        this.items = this.items.OrderBy(p => p.Id).ToList();
                        shuffled = false;
                    }

                    this.experData = new ExperimentData(paramArray[2], this.Hub.RunableGame.GetPlayer().Id, shuffled);
                }
            }
            catch (Exception ex) {
                // [TODO] critical error
                Debug.WriteLine("Error reading concepts and pairs from xml.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            this.bgImage = GenericImage.GetInstance(CardMatchingModule.BG_IMAGE_ID);

            Player player = this.Hub.RunableGame.GetPlayer() as Player; // [TODO] casting to Player
            // [SC] storing player's world coordinate to be restored at end of module execution
            this.playerWordPos.X = player.X;
            this.playerWordPos.Y = player.Y;
            // [SC] set player's position in the module
            player.X = CardMatchingModule.PLAYER_POSITION.X;
            player.Y = CardMatchingModule.PLAYER_POSITION.Y;
            // [SC] set player's current state to facing up
            player.SetCurrentState((int)Hub.Reserved.STATE_RIGHT);
            this.cObjects.Add(player);
            
            // [SC] resizing the canvas
            ICanvas canvas = this.Hub.RunableGame.Canvas;
            this.prevSize = canvas.SizeSR;
            canvas.SizeSR = new SizeSR(CardMatchingModule.SCENE_WIDTH, CardMatchingModule.SCENE_HEIGHT);
            
            initNewRound();
        }

        private void createAssetInstances() {
            // [SC] create card stub animations
            this.cardStubStates.Clear();

            // [SC] adding card opening stub animations
            StubAnimation cardOpenSA = StubAnimation.CreateInstance("card_open_animation");
            for (int currId = CardMatchingModule.START_CARD_ID; currId <= CardMatchingModule.END_CARD_ID; currId++) {
                cardOpenSA.AddSprite(GenericImage.GetInstance(currId));
            }
            cardOpenSA.CanRepeat = false;
            cardOpenSA.SpriteDelay = 10;
            
            // [SC] adding card closing stub animations
            StubAnimation cardCloseSA = StubAnimation.CreateInstance("card_close_animation");
            for (int currId = CardMatchingModule.END_CARD_ID; currId >= CardMatchingModule.START_CARD_ID; currId--) {
                cardCloseSA.AddSprite(GenericImage.GetInstance(currId));
            }
            cardCloseSA.CanRepeat = false;
            cardCloseSA.SpriteDelay = 10;

            // [SC] creating card opening stub state
            StubState openStubState = StubState.CreateInstance("card_open_state"); // [SC] creating a state
            this.cardStubStates.Add(openStubState);

            // [SC] creating card closing stub state
            StubState closeStubState = StubState.CreateInstance("card_closed_state"); // [SC] creating a state
            this.cardStubStates.Add(closeStubState);

            // [SC] creating card actors
            for (int rowIndex = 0; rowIndex < CardMatchingModule.ROW_COUNT; rowIndex++) {
                for (int colIndex = 0; colIndex < CardMatchingModule.COL_COUNT; colIndex++) {
                    Cell cell = new Cell();
                    cell.RowIndex = rowIndex;
                    cell.ColIndex = colIndex;

                    Card card = new Card(rowIndex + "-" + colIndex, GenericImage.GetInstance(CardMatchingModule.START_CARD_ID));
                    
                    State openingState = State.CreateInstance(this.cardStubStates[0]);
                    openingState.SetAnimation(cardOpenSA.Id);
                    card.AddState(openingState);

                    State closingState = State.CreateInstance(this.cardStubStates[1]);
                    closingState.SetAnimation(cardCloseSA.Id);
                    card.AddState(closingState);

                    card.X = CardMatchingModule.START_X + colIndex * CardMatchingModule.CARD_WIDTH
                        + colIndex * CardMatchingModule.HORIZONTAL_MARGIN;
                    card.Y = CardMatchingModule.START_Y + rowIndex * CardMatchingModule.CARD_HEIGHT
                        + rowIndex * CardMatchingModule.VERTICAL_MARGIN;

                    card.MouseClick += cardMouseClicked;
                    card.MouseClick += cardMouseOver;

                    card.Cell = cell;
                    cell.Card = card;

                    this.cells.Add(cell);
                }
            }

            // [SC] creating enemy
            StubAnimation enemyStubAnime = StubAnimation.CreateInstance("enemy_animation");
            int counter = CardMatchingModule.ENEMY_ID;
            enemyStubAnime.AddSprite(GenericImage.GetInstance(counter));
            enemyStubAnime.AddSprite(GenericImage.GetInstance(++counter));
            enemyStubAnime.AddSprite(GenericImage.GetInstance(++counter));
            enemyStubAnime.SpriteDelay = 300;
            this.enemy = new DynamicActor("enemy", null);
            this.enemy.GetState((int)Hub.Reserved.STATE_LEFT).SetAnimation(enemyStubAnime.Id);
            this.enemy.SetCurrentState((int)Hub.Reserved.STATE_LEFT);
            this.enemy.DestActor = this.Hub.RunableGame.GetPlayer() as Player;
            this.enemy.X = CardMatchingModule.ENEMY_START_POSITION.X;
            this.enemy.Y = CardMatchingModule.ENEMY_START_POSITION.Y;
            this.cObjects.Add(this.enemy);

            // [SC] creating firepunch stub animation
            StubAnimation fireStubAnime = StubAnimation.CreateInstance("fire_moving_animation");
            counter = CardMatchingModule.FIRE_MOVING_ID;
            fireStubAnime.AddSprite(GenericImage.GetInstance(counter));
            fireStubAnime.AddSprite(GenericImage.GetInstance(++counter));
            fireStubAnime.SpriteDelay = 50;
            this.fireStubAnimes.Add(fireStubAnime);
            fireStubAnime = StubAnimation.CreateInstance("fire_dissolving_animation");
            counter = CardMatchingModule.FIRE_DISSOLVING_ID;
            fireStubAnime.AddSprite(GenericImage.GetInstance(counter));
            fireStubAnime.AddSprite(GenericImage.GetInstance(++counter));
            fireStubAnime.AddSprite(GenericImage.GetInstance(++counter));
            fireStubAnime.AddSprite(GenericImage.GetInstance(++counter));
            fireStubAnime.AddSprite(GenericImage.GetInstance(++counter));
            fireStubAnime.AddSprite(GenericImage.GetInstance(++counter));
            fireStubAnime.SpriteDelay = 50;
            this.fireStubAnimes.Add(fireStubAnime);

            // [SC] creating previous button actor
            Actor prevButton = new Actor("prev button", GenericImage.GetInstance(CardMatchingModule.PREV_ID));
            prevButton.X = CardMatchingModule.PREV_POSITION.X;
            prevButton.Y = CardMatchingModule.PREV_POSITION.Y;
            prevButton.MouseClick += instructionMouseClicked;
            this.instrBtns.Add(prevButton);

            // [SC] creating previous button actor
            Actor nextButton = new Actor("next button", GenericImage.GetInstance(CardMatchingModule.NEXT_ID));
            nextButton.X = CardMatchingModule.NEXT_POSITION.X;
            nextButton.Y = CardMatchingModule.NEXT_POSITION.Y;
            nextButton.MouseClick += instructionMouseClicked;
            this.instrBtns.Add(nextButton);
        }

        private void initNewRound() {
            if (this.allPairs.Count > 0) {
                addExercisePairs();
            }
            else {
                addExperimentItems();
            }
        }

        private void addExercisePairs() {
            // [SC] clear past turn data
            this.turnPairs.Clear();
            this.openCards.Clear();
            this.clickableObjects.Clear();

            List<Cell> temp = new List<Cell>();

            // [SC] resetting cards and adding them to clickable list; resetting cells
            for (int index = 0; index < this.cells.Count; index++) {
                Card card = this.cells[index].Card;
                card.SetCurrentState((int)Hub.Reserved.STATE_IDLE);
                card.GetState(this.cardStubStates[0].Id).ResetAnimation();
                card.GetState(this.cardStubStates[1].Id).ResetAnimation();
                card.IsClickable = true;
                
                this.cells[index].Concept = null;
                this.cells[index].Pair = null;

                if (index < this.playablePairs * 2) {
                    temp.Add(this.cells[index]);
                    this.clickableObjects.Add(card);
                }
            }

            // [SC] shuffle the cells
            temp.Shuffle();

            Color[] conceptTextColors = { Color.DarkBlue, Color.Black, Color.Purple, Color.Red, Color.Green, Color.DarkOrange };
            int colorIndex = 0;

            // [SC] randomly choose N number of indices for card pairs
            int currentCardPairCount = 0;
            List<int> pairIndices = new List<int>();
            while (currentCardPairCount < this.playablePairs) {
                int newIndex;
                do {
                    newIndex = AuxiliaryMethods.rnd.Next(0, this.allPairs.Count);
                } while (pairIndices.Contains(newIndex));
                pairIndices.Add(newIndex);

                Pair pair = this.allPairs[newIndex];
                this.turnPairs.Add(pair);

                pair.IsFound = false;
                pair.IsTarget = true;
                pair.ConceptTextColor = conceptTextColors[colorIndex++];

                temp[currentCardPairCount * 2].Concept = pair.concepts[0];
                temp[currentCardPairCount * 2 + 1].Concept = pair.concepts[1];

                temp[currentCardPairCount * 2].Pair = pair;
                temp[currentCardPairCount * 2 + 1].Pair = pair;

                currentCardPairCount++;
            }

            // [SC] adding instruction buttons to the clickable list
            foreach (Actor actor in this.instrBtns) {
                this.clickableObjects.Add(actor);
            }
        }

        private void addExperimentItems() {
            // [SC] measure previous turn duration
            if (this.currItemIndex >= 0) {
                int totalOpenPairs = 0;
                foreach (Pair pair in this.turnPairs) {
                    if (pair.IsFound) {
                        totalOpenPairs++;
                    }
                    else {
                        this.playerScore += this.playerScoreIncrement;
                    }
                }

                this.experData.AddTurnData(this.items[this.currItemIndex].Id, this.turnStartTime, Environment.TickCount, totalOpenPairs);
            }

            // [SC] if true then no more items are available
            if (++this.currItemIndex >= this.items.Count) {
                // [TODO]
                Debug.WriteLine(String.Format("Data for player {0} for dataset {1}:", this.experData.PlayerId, this.experData.DatasetId));
                Debug.WriteLine(String.Format("\tItemId\tStartTime\tEndTime\tDuration\tOpenPairs"));
                foreach (TurnData data in this.experData.turnData) {
                    Debug.WriteLine(String.Format("\t{0}\t{1}\t{2}\t{3}\t{4}"
                        , data.ItemId, data.TurnStartTime, data.TurnEndTime, data.TurnEndTime-data.TurnStartTime, data.TurnOpenPairs));
                }

                this.ToClear = true;
                return;
            }

            // [SC] clear past turn data
            this.openCards.Clear();
            this.clickableObjects.Clear();
            this.turnPairs.Clear();
            
            Item item = this.items[this.currItemIndex];

            List<Cell> temp = new List<Cell>();

            // [SC] resetting cards and adding them to clickable list; resetting cells
            for (int index = 0; index < this.cells.Count; index++) {
                Card card = this.cells[index].Card;
                card.SetCurrentState((int)Hub.Reserved.STATE_IDLE);
                card.GetState(this.cardStubStates[0].Id).ResetAnimation();
                card.GetState(this.cardStubStates[1].Id).ResetAnimation();
                card.IsClickable = true;

                this.cells[index].Concept = null;
                this.cells[index].Pair = null;

                if (index < item.pairs.Count * 2) {
                    temp.Add(this.cells[index]);
                    this.clickableObjects.Add(card);
                }
            }

            // [SC] shuffle the cells
            temp.Shuffle();

            Color[] conceptTextColors = { Color.DarkBlue, Color.Black, Color.Purple, Color.Red, Color.Green, Color.DarkOrange };
            int colorIndex = 0;

            for (int index = 0; index < item.pairs.Count; index++) {
                Pair pair = item.pairs[index];
                pair.ConceptTextColor = conceptTextColors[colorIndex++];

                temp[index * 2].Concept = pair.concepts[0];
                temp[index * 2 + 1].Concept = pair.concepts[1];

                temp[index * 2].Pair = pair;
                temp[index * 2 + 1].Pair = pair;
            }

            this.turnPairs = item.pairs.ToList();

            // [SC] adding instruction buttons to the clickable list
            foreach (Actor actor in this.instrBtns) {
                this.clickableObjects.Add(actor);
            }

            this.turnStartTime = Environment.TickCount;
        }

        private void createFirepunch() {
            DynamicActor firepunch = new DynamicActor("firepunch", null);
            firepunch.GetState((int)Hub.Reserved.STATE_RIGHT).SetAnimation(fireStubAnimes[0].Id);
            firepunch.GetState((int)Hub.Reserved.STATE_IDLE).SetAnimation(fireStubAnimes[1].Id);
            firepunch.SetCurrentState((int)Hub.Reserved.STATE_RIGHT);
            firepunch.DestActor = this.enemy;
            firepunch.X = CardMatchingModule.FIRE_START_POSITION.X;
            firepunch.Y = CardMatchingModule.FIRE_START_POSITION.Y;
            this.firepunches.Add(firepunch);
            // [SC] collision detection
            firepunch.IgnoreCollisionWith(this.Hub.RunableGame.GetPlayer().Id);
            foreach (Actor existingFires in this.firepunches) {
                firepunch.IgnoreCollisionWith(existingFires.Id);
            }
            this.cObjects.Add(firepunch);
        }

        private void compareOpenCards() {
            if (this.openCards.Count == 2 && this.openCards[0].AnimationEnded() && this.openCards[1].AnimationEnded()) {
                Concept conceptOne = this.openCards[0].Cell.Concept;
                Concept conceptTwo = this.openCards[1].Cell.Concept;

                bool matchingFlag = false;
                if (conceptOne != conceptTwo) {
                    foreach (Pair pair in this.turnPairs) {
                        // [SC] matching concepts were found
                        if (!pair.IsFound && pair.concepts.Contains(conceptOne) && pair.concepts.Contains(conceptTwo)) {
                            // [SC] firepunch the enemy
                            createFirepunch();

                            this.openCards[0].IsClickable = false;
                            this.openCards[1].IsClickable = false;

                            pair.IsFound = true;

                            this.playerScore += this.playerScoreIncrement;

                            matchingFlag = true;

                            break;
                        }
                    }
                }

                if (matchingFlag) {
                    // [SC] verify if all target pairs were found
                    bool startNewTurn = true;
                    foreach (Pair pair in this.turnPairs) {
                        if (pair.IsTarget && !pair.IsFound) {
                            startNewTurn = false;
                            break;
                        }
                    }
                    if (startNewTurn) {
                        // [SC] start a new round
                        initNewRound();
                    }
                }
                else {
                    // [SC] close the mismatching cards
                    foreach (Card openCard in this.openCards) {
                        openCard.GetState(openCard.GetCurrentStateId()).ResetAnimation();
                        openCard.SetCurrentState(this.cardStubStates[1].Id);
                    }
                }

                this.openCards.Clear();
            }
        }

        private void cardMouseClicked(object sender, SRMouseEventArgs e) {
            if (this.drawScoreFlag) {
                return;
            }

            Card card = sender as Card;
            
            if (card.GetCurrentStateId() != this.cardStubStates[0].Id) { // [SC] the card is not in the opening state
                if (this.openCards.Count < 2) {
                    // [SC] set the card to opening state
                    card.GetState(card.GetCurrentStateId()).ResetAnimation();
                    card.SetCurrentState(this.cardStubStates[0].Id);
                    this.openCards.Add(card);
                }
            }
            else {
                // [SC] set the card in the opening state to closing state
                card.GetState(card.GetCurrentStateId()).ResetAnimation();
                card.SetCurrentState(this.cardStubStates[1].Id);
                this.openCards.Remove(card);
            }
        }

        private void cardMouseOver(object sender, SRMouseEventArgs e) {
            // [TODO]
        }

        private void instructionMouseClicked(object sender, SRMouseEventArgs e) {
            if (this.drawScoreFlag) {
                return;
            }

            if (sender == this.instrBtns[0]) {
                if (--this.drawnInstructionIndex < 0) {
                    this.drawnInstructionIndex = this.turnPairs.Count - 1;
                }
            }
            else {
                if (++this.drawnInstructionIndex >= this.turnPairs.Count) {
                    this.drawnInstructionIndex = 0;
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
            if (this.drawScoreFlag) {
                return;
            }

            compareOpenCards();

            // [SC] remove firepunches that ended idle state animation
            for (int fireIndex = this.firepunches.Count - 1; fireIndex >= 0; fireIndex--) {
                DynamicActor firepunch = this.firepunches[fireIndex];
                if (firepunch.GetCurrentStateId() == (int)Hub.Reserved.STATE_IDLE) {
                    if (firepunch.AnimationEnded()) {
                        this.firepunches.Remove(firepunch);
                    }
                }
            }

            // [SC] check for collisions
            List<IEvent> collisionEvents = EventRegistry.GetEventsByType(typeof(CollisionEvent));
            for (int colEventIndex = collisionEvents.Count - 1; colEventIndex >= 0; colEventIndex--) {
                CollisionEvent colEvent = collisionEvents[colEventIndex] as CollisionEvent;

                if (colEvent.HasActors(this.Hub.RunableGame.GetPlayer().Id, this.enemy.Id)) {
                    // [SC] remove the event from the event registry
                    EventRegistry.RemoveEvent(colEvent.Id);

                    // [TODO]
                    if (this.allPairs.Count > 0) {
                        this.drawScoreFlag = true;
                        return; // [TODO]
                    }
                } else {
                    for (int fireIndex = this.firepunches.Count - 1; fireIndex >= 0; fireIndex--) {
                        DynamicActor firepunch = this.firepunches[fireIndex];
                        if (colEvent.HasActor(firepunch.Id)) {
                            // [SC] remove the event from the event registry
                            EventRegistry.RemoveEvent(colEvent.Id);

                            // [SC] set firepunch to the idle state
                            firepunch.SetCurrentState((int)Hub.Reserved.STATE_IDLE);
                            this.cObjects.Remove(firepunch);

                            // [SC] knockback the enemy
                            this.enemy.X += this.knockbackDistance;
                            if (this.enemy.X > CardMatchingModule.ENEMY_START_POSITION.X) {
                                this.enemy.X = CardMatchingModule.ENEMY_START_POSITION.X;
                            }
                        }
                    }
                }
            }

            // [SC] move firepunches
            foreach (DynamicActor firepunch in this.firepunches) {
                if (firepunch.GetCurrentStateId() == (int)Hub.Reserved.STATE_RIGHT) {
                    firepunch.Move(20, false);
                }
            }
            
            this.enemy.Move(0.5f, false);
        }

        public void Animate(int tickCount) {
            Player player = (this.Hub.RunableGame.GetPlayer() as Player);

            // [SC] animate player
            player.Animate(tickCount); // [TODO] casting to Player

            // [SC] animate enemy
            this.enemy.Animate(tickCount);

            // [SC] animate firepunches
            foreach (Actor firepunch in this.firepunches) {
                firepunch.Animate(tickCount);
            }

            // [SC] animate cards
            foreach (Cell cell in this.cells) {
                if (cell.Concept == null) {
                    break;
                }
                cell.Card.Animate(tickCount);
            }
        }

        public void Draw(int tickCount) {
            ICanvas canvas = this.Hub.RunableGame.Canvas;

            canvas.Clear();

            // [SC] draw background image
            canvas.DrawImage(this.bgImage, 0, this.bgImage.Height - 1, this.bgImage.Width, this.bgImage.Height);

            // [SC] draw player
            Player player = this.Hub.RunableGame.GetPlayer() as Player; // [TODO] casting to Player
            canvas.DrawImage(player.Image, player.X, player.Y, player.Width, player.Height);

            // [SC] draw enemy
            canvas.DrawImage(this.enemy.Image, this.enemy.X, this.enemy.Y, this.enemy.Width, this.enemy.Height);

            // [SC] draw firepunches
            foreach (Actor firepunch in this.firepunches) {
                canvas.DrawImage(firepunch.Image, firepunch.X, firepunch.Y, firepunch.Width, firepunch.Height);
            }

            string textFont = "Arial";
            float fontSize = 20;

            foreach (Cell cell in this.cells) {
                if (cell.Concept == null) {
                    break;
                }

                if (cell.Concept.Image == null) {
                    // [SC] draw card hidden text
                    SizeSR textSize = canvas.GetTextSize(cell.Concept.Text, textFont, fontSize);
                    float textX = cell.Card.X + (cell.Card.Width - textSize.Width) / 2;
                    float textY = cell.Card.Y - (cell.Card.Height - textSize.Height) / 2;
                    canvas.DrawText(cell.Concept.Text, textX, textY, textFont, fontSize, cell.Pair.ConceptTextColor);
                }
                else {
                    // [SC] draw card hidden image
                    float imageX = cell.Card.X + (cell.Card.Width - cell.Concept.Image.Width) / 2;
                    float imageY = cell.Card.Y - (cell.Card.Height - cell.Concept.Image.Height) / 2;
                    canvas.DrawImage(cell.Concept.Image, imageX, imageY, cell.Concept.Image.Width, cell.Concept.Image.Height);
                }

                // [SC] draw card background
                canvas.DrawImage(cell.Card.Image, cell.Card.X, cell.Card.Y, cell.Card.Width, cell.Card.Height);
            }

            // [SC] draw instruction
            fontSize = 13;
            Pair pair = this.turnPairs[this.drawnInstructionIndex];
            SizeSR instrSize = canvas.GetTextSize(pair.Instruction, textFont, fontSize);
            float instrX = CardMatchingModule.INSTR_START_POSITION.X;
            float instrY = CardMatchingModule.INSTR_START_POSITION.Y + instrSize.Height - 1;
            canvas.DrawText(pair.Instruction, instrX, instrY, textFont, fontSize, pair.ConceptTextColor);

            // [SC] draw instruction buttons
            foreach (Actor button in this.instrBtns) {
                canvas.DrawImage(button.Image, button.X, button.Y, button.Width, button.Height);
            }

            // [SC] draw player score
            canvas.DrawText(String.Format("Your score: {0}", this.playerScore),
                CardMatchingModule.PLAYER_SCORE_POSITION.X, CardMatchingModule.PLAYER_SCORE_POSITION.Y,
                textFont, fontSize, Color.Black);

            if (this.drawScoreFlag) {
                float x = (CardMatchingModule.SCENE_WIDTH - CardMatchingModule.SCORE_RECT.Width)/2;
                float y = CardMatchingModule.SCENE_HEIGHT - (CardMatchingModule.SCENE_HEIGHT - CardMatchingModule.SCORE_RECT.Height) / 2;
                canvas.DrawRectangle(x, y, CardMatchingModule.SCORE_RECT.Width, CardMatchingModule.SCORE_RECT.Height, Brushes.Gray);
                
                string msg = String.Format("Game Over.\n\rYour final score is {0}.", this.playerScore);
                SizeSR msgSize = canvas.GetTextSize(msg, textFont, fontSize);
                float msgX = x + (CardMatchingModule.SCORE_RECT.Width - msgSize.Width)/2;
                float msgY = y - (CardMatchingModule.SCORE_RECT.Height - msgSize.Height)/2;
                canvas.DrawText(msg, msgX, msgY, textFont, fontSize, Color.Black);
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
            if (key == Keys.Escape || key == Keys.Delete) {
                this.ToClear = true;
            }
        }

        public void KeyUp(Keys key, bool upperCase) { }

        public IEnumerable<ICollidableObject> GetCollidableObjects() {
            return this.cObjects;
        }

        public IEnumerable<IClickable> GetClickableObjects() {
            return this.clickableObjects;
        }

        #endregion Methods
    }
}
