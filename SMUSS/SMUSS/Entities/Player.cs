using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;

namespace SMUSS
{
    class Player : Entity
    {
        #region Member Variables

        private static Player instance;

        #endregion

        #region Initialization

        public static Player Instance
        {
            get
            {
                if (instance == null) instance = new Player();
                return instance;
            }
        }

        private Player()
        {
            Name = "player";
            Speed = 5;
            InverseMass = 0.4f;

            SpriteSize = new Vector2(68, 68);
            Size = new Vector2(68, 68);

            Position = (GameRoot.ScreenSize / 2) + new Vector2(15, 5);

            Orientation = SpriteAnimation.Facing.Down;

            AnimationsList = new List<SpriteAnimation.Animation>();
            AnimationsList.Add(SpriteAnimation.Animation.Still);
            AnimationsList.Add(SpriteAnimation.Animation.RunningRight);
            AnimationsList.Add(SpriteAnimation.Animation.RunningLeft);
            AnimationsList.Add(SpriteAnimation.Animation.RunningUp);
            AnimationsList.Add(SpriteAnimation.Animation.RunningDown);
            CurrentAnimation = SpriteAnimation.Animation.Still;
            
            isColliding = false;
        }

        #endregion

        #region Update Methods
        /// <summary>
        /// This region holds all update-related methods:
        ///     - void Update()
        ///     - void checkForTilemapCollisions(Vector2 velocity)
        /// </summary>
        /// 

        public override void Update()
        {
            // MOVEMENT
            Vector2 inputVelocity = Speed * Input.GetMovementDirection();

            HandleMovement(inputVelocity);
        }

        #endregion

        #region Movement Methods
        /// <summary>
        /// This region holds all non-active movement methods. By non-active we mean
        /// that action methods such as rolling or jumping are not included
        ///     - void HandleMovement(Vector2 inputVelocity)
        ///     - void StandStill()
        ///     - void Run(Vector2 inputVelocity)
        /// </summary>

        private void HandleMovement(Vector2 inputVelocity)
        {
            if (inputVelocity == Vector2.Zero)
                StandStill();

            else
            {
                CollisionManager.checkForPlayerToTilemapCollisions(inputVelocity);

                if (!isColliding)
                {
                    Run(inputVelocity);
                    updatePosition();
                }

                else
                {
                    if (CollisionManager.ResultingCollisionVector.X != 0 && CollisionManager.ResultingCollisionVector.Y != 0)
                        StandStill();

                    else if (CollisionManager.ResultingCollisionVector.X != 0 || CollisionManager.ResultingCollisionVector.Y != 0)
                    {
                        Run(inputVelocity);
                        updatePosition();
                    }
                }
            }

        }

        private void updatePosition()
        {
            Position.X += Velocity.X;
            Position.Y += Velocity.Y;

            if (isColliding)
            {
                if (CollisionManager.ResultingCollisionVector.X != 0)
                    Position.X += (float) 1.1 * CollisionManager.ResultingCollisionVector.X;

                if (CollisionManager.ResultingCollisionVector.Y != 0)
                    Position.Y += (float) 1.1 * CollisionManager.ResultingCollisionVector.Y;
            }
        }

        private void StandStill()
        {
            CurrentAnimation = SpriteAnimation.Animation.Still;
            IsAnimating = false;

            if (Velocity.LengthSquared() > 0.01)
            {
                Velocity.X /= Speed * InverseMass;
                Velocity.Y /= Speed * InverseMass;
            }
            else Velocity = Vector2.Zero;
        }

        private void Run(Vector2 inputVelocity)
        {

            // INPUT READ. BEGIN ANIMATION AND TURN ORIENTATION
            ++FramesIntoAnimation;
            IsAnimating = true;

            if (Math.Abs(inputVelocity.Y) > inputVelocity.X * inputVelocity.X)
            {
                if (inputVelocity.Y > 0)
                {
                    if (CurrentAnimation != SpriteAnimation.Animation.RunningDown)
                        FramesIntoAnimation = 0;
                    Orientation = SpriteAnimation.Facing.Down;
                    CurrentAnimation = SpriteAnimation.Animation.RunningDown;
                    IsAnimating = true;
                }
                else
                {
                    if (CurrentAnimation != SpriteAnimation.Animation.RunningUp)
                        FramesIntoAnimation = 0;
                    Orientation = SpriteAnimation.Facing.Up;
                    CurrentAnimation = SpriteAnimation.Animation.RunningUp;
                    IsAnimating = true;
                }
            }

            else
            {
                if (inputVelocity.X > 0)
                {
                    if (CurrentAnimation != SpriteAnimation.Animation.RunningRight)
                        FramesIntoAnimation = 0;
                    Orientation = SpriteAnimation.Facing.Right;
                    CurrentAnimation = SpriteAnimation.Animation.RunningRight;
                    IsAnimating = true;
                }
                if (inputVelocity.X < 0)
                {
                    Orientation = SpriteAnimation.Facing.Left;
                    if (CurrentAnimation != SpriteAnimation.Animation.RunningLeft)
                        FramesIntoAnimation = 0;

                    CurrentAnimation = SpriteAnimation.Animation.RunningLeft;
                    IsAnimating = true;
                }
            }



            //Physics Handling: Inertia on movement;


            Vector2 v, accel;

            accel = (InverseMass) * inputVelocity;

            if (accel.LengthSquared() < 0.001)
                accel = Vector2.Zero;

            if (Velocity.LengthSquared() < 0.0001)
                Velocity = Vector2.Zero;

            Math.Round(Velocity.X, 4);
            Math.Round(Velocity.Y, 4);
            v.X = (float)(Velocity.X + 0.5 * accel.X);
            v.Y = (float)(Velocity.Y + 0.9 * accel.Y);
            Velocity.X = v.X;
            Velocity.Y = v.Y;

            if (Velocity.LengthSquared() > (Speed * Speed))
            {
                Velocity.Normalize();
                Velocity *= Speed;
            }


        }

        #endregion

    }
}
