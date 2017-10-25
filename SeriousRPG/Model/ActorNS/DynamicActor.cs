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

using SeriousRPG.HubNS;
using SeriousRPG.Model.DrawingNS;
using SeriousRPG.Model.GameNS;
using SeriousRPG.Model.ImageObjectNS;
using SeriousRPG.Model.StateNS;
using SeriousRPG.Misc;

using System.Diagnostics; // [TODO]

namespace SeriousRPG.Model.ActorNS
{
    internal class DynamicActor : Actor, IDynamicActor
    {
        #region Constants

        internal const float DEFAULT_MAX_SPEED = 5;

        internal const float MIN_CURRENT_SPEED = 1;
        
        #endregion Constants

        #region Fields

        // [SC] actors max speed
        private float maxSpeed = DynamicActor.DEFAULT_MAX_SPEED;

        // [SC] actors current speed, increases until max speed is reached
        private float currSpeed = DynamicActor.MIN_CURRENT_SPEED;

        // [SC] destination point toward which the actor should be moving
        private PointSR destPoint = new PointSR { X = -1, Y = -1 };

        // [SC] destination actor toward which this actor should be moving
        private IActor destActor = null;

        #endregion Fields

        #region Properties

        public float MaxSpeed {
            get { return this.maxSpeed; }
            set {
                if (value >= 0) {
                    this.maxSpeed = value;
                } else {
                    // [TODO] error msg
                }
            }
        }

        public float CurrSpeed {
            get { return this.currSpeed; }
            set {
                if (value > this.MaxSpeed) {
                    this.currSpeed = this.MaxSpeed;
                }
                else if (value < DynamicActor.MIN_CURRENT_SPEED) {
                    this.currSpeed = DynamicActor.MIN_CURRENT_SPEED;
                }
                else {
                    this.currSpeed = value;
                }
            }
        }

        public PointSR DestPoint {
            get { return this.destPoint; }
            set {
                if (value != null) {
                    this.destPoint = value;
                    this.destActor = null;
                }
            }
        }

        public IActor DestActor {
            get { return this.destActor; }
            set {
                if (value != null) {
                    this.destActor = value;
                    this.destPoint = new PointSR { X = -1, Y = -1 };
                }
            }
        }

        #endregion Properties

        #region Constructors

        internal DynamicActor(string name, GenericImage defaultSprite)
            : base(name, defaultSprite) {

            Init();
        }

        internal DynamicActor(int id, string name, GenericImage defaultSprite, bool isClone)
            : base(id, name, defaultSprite, isClone) {
            
            Init();
        }

        private void Init() {
            // [SC] creating a Up state
            if (!AddState(State.CreateInstance(StubState.GetInstance((int)Hub.Reserved.STATE_UP)))) {
                // [TODO]
            }

            // [SC] creating a Down state
            if (!AddState(State.CreateInstance(StubState.GetInstance((int)Hub.Reserved.STATE_DOWN)))) {
                // [TODO]
            }

            // [SC] creating a Left state
            if (!AddState(State.CreateInstance(StubState.GetInstance((int)Hub.Reserved.STATE_LEFT)))) {
                // [TODO]
            }

            // [SC] creating a Right state
            if (!AddState(State.CreateInstance(StubState.GetInstance((int)Hub.Reserved.STATE_RIGHT)))) {
                // [TODO]
            }
        }

        #endregion Constructors

        #region Methods

        private void IncreaseCurrSpeed() {
            this.CurrSpeed += 2;
        }

        private void DecreaseCurrSpeed() {
            this.CurrSpeed -= 2;
        }

        private void ResetCurrentSpeed() {
            this.CurrSpeed = DynamicActor.MIN_CURRENT_SPEED;
        }

        public bool Move(float targetX, float targetY, float speed, bool updateState) {
            if (speed < 0) {
                // [TODO] warning msg
                return false;
            }

            if (targetX < 0 || targetY < 0) {
                // [TODO] invalid target coordinate msg
                return false;
            }

            // [SC] calculate the direction toward the target
            VectorSR direction = VectorSR.Direction(this.X, this.Y, targetX, targetY);

            float prevX = this.X;
            float prevY = this.Y;

            // [SC] provisionally, calculate new position of this actor
            this.X = speed * direction.X + this.X;
            this.Y = speed * direction.Y + this.Y;

            // [SC] collision detection
            if (!RunableGame.GetInstance().IsNoCollision(this)) {
                this.X = prevX;
                this.Y = prevY;
                
                this.ResetCurrentSpeed();

                return false;
            }

            if (updateState) {
                // [SC] identify state
                if (prevX < this.X) {
                    this.SetCurrentState((int)Hub.Reserved.STATE_RIGHT);
                }
                else if (prevX > this.X) {
                    this.SetCurrentState((int)Hub.Reserved.STATE_LEFT);
                }
                else if (prevY < this.Y) {
                    this.SetCurrentState((int)Hub.Reserved.STATE_DOWN);
                }
                else if (prevY > this.Y) {
                    this.SetCurrentState((int)Hub.Reserved.STATE_UP);
                }
            }

            this.IncreaseCurrSpeed();

            return true;
        }

        public bool Move(float speed, bool updateState) {
            float targetX = this.DestPoint.X;
            float targetY = this.DestPoint.Y;

            if (this.destActor != null) {
                targetX = this.DestActor.X;
                targetY = this.DestActor.Y;
            }

            return this.Move(targetX, targetY, speed, updateState);
        }

        public bool Move(bool updateState) {
            return this.Move(this.CurrSpeed, updateState);
        }

        public bool MoveUp(float speed, bool updateState) {
            if (speed < 0) {
                // [TODO] warning msg
                return false;
            }

            float prevY = this.Y;
            this.Y = this.Y - speed;

            // [SC] collision detection
            if (!RunableGame.GetInstance().IsNoCollision(this)) {
                this.Y = prevY;
                return false;
            }

            if (updateState) {
                this.SetCurrentState((int)Hub.Reserved.STATE_UP);
            }

            return true;
        }

        public bool MoveUp(bool updateState) {
            if (this.MoveUp(this.CurrSpeed, updateState)) {
                this.IncreaseCurrSpeed();
                return true;
            }
            else {
                this.ResetCurrentSpeed();
                return false;
            }
        }

        public bool MoveDown(float speed, bool updateState) {
            if (speed < 0) {
                // [TODO] warning msg
                return false;
            }

            float prevY = this.Y;
            this.Y = this.Y + speed;

            // [SC] collision detection
            if (!RunableGame.GetInstance().IsNoCollision(this)) {
                this.Y = prevY;
                return false;
            }

            if (updateState) {
                this.SetCurrentState((int)Hub.Reserved.STATE_DOWN);
            }

            return true;
        }

        public bool MoveDown(bool updateState) {
            if (this.MoveDown(this.CurrSpeed, updateState)) {
                this.IncreaseCurrSpeed();
                return true;
            }
            else {
                this.ResetCurrentSpeed();
                return false;
            }
        }

        public bool MoveLeft(float speed, bool updateState) {
            if (speed < 0) {
                // [TODO] warning msg
                return false;
            }

            float prevX = this.X;
            this.X = this.X - speed;

            // [SC] collision detection
            if (!RunableGame.GetInstance().IsNoCollision(this)) {
                this.X = prevX;
                return false;
            }

            if (updateState) {
                this.SetCurrentState((int)Hub.Reserved.STATE_LEFT);
            }

            return true;
        }

        public bool MoveLeft(bool updateState) {
            if (this.MoveLeft(this.CurrSpeed, updateState)) {
                this.IncreaseCurrSpeed();
                return true;
            }
            else {
                this.ResetCurrentSpeed();
                return false;
            }
        }

        public bool MoveRight(float speed, bool updateState) {
            if (speed < 0) {
                // [TODO] warning msg
                return false;
            }

            float prevX = this.X;
            this.X = this.X + speed;

            // [SC] collision detection
            if (!RunableGame.GetInstance().IsNoCollision(this)) {
                this.X = prevX;
                return false;
            }

            if (updateState) {
                this.SetCurrentState((int)Hub.Reserved.STATE_RIGHT);
            }

            return true;
        }

        public bool MoveRight(bool updateState) {
            if (this.MoveRight(this.CurrSpeed, updateState)) {
                this.IncreaseCurrSpeed();
                return true;
            }
            else {
                this.ResetCurrentSpeed();
                return false;
            }
        }

        public bool MoveUpLeft(float speedX, float speedY) {
            // [TODO] do not reuse up, left, right and down methods; implement as single transaction
            return false;
        }

        /// <summary>
        /// Called if the actor stopped moving.
        /// </summary>
        /// <returns></returns>
        public void SetIdle() {
            this.SetCurrentState((int)Hub.Reserved.STATE_IDLE);
            this.ResetCurrentSpeed();
        }

        #endregion Methods

        #region Misc methods

        public new virtual IActor Clone() {
            DynamicActor cloneActor = new DynamicActor(this.Id, this.Name, this.DefaultSprite.Clone(), true);

            cloneActor.Description = this.Description; // [TODO] copy string

            cloneActor.Portrait = this.Portrait.Clone();

            cloneActor.Health = this.Health;
            cloneActor.Experience = this.Experience;
            cloneActor.Level = this.Level;

            cloneActor.CanCollide = this.CanCollide;
            cloneActor.IsClickable = this.IsClickable;

            cloneActor.X = this.X;
            cloneActor.Y = this.Y;

            cloneActor.ClearStates();
            foreach (State state in this.states) {
                cloneActor.AddState(state.Clone());
            }

            if (HasCurrentState()) {
                cloneActor.SetCurrentState(GetCurrentStateId());
            }

            cloneActor.MaxSpeed = this.MaxSpeed;
            cloneActor.CurrSpeed = this.CurrSpeed;
            cloneActor.DestPoint.X = this.DestPoint.X;
            cloneActor.DestPoint.Y = this.DestPoint.Y;
            // clonePlayer.DestActor // [TODO]

            return cloneActor;
        }

        #endregion Misc methods
    }
}
