using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SMUSS
{
    public static class CollisionManager
    {
        public static Vector2 ResultingCollisionVector;

        #region Player to Tilemap Collision Methods

        public static void checkForPlayerToTilemapCollisions(Vector2 velocity)
        {

            Vector2 expectedPosition = Player.Instance.Position + velocity;

            ResultingCollisionVector = Vector2.Zero;

            Rectangle playerRectangle = new Rectangle((int)expectedPosition.X, (int)expectedPosition.Y, (int)Player.Instance.Size.X, (int)Player.Instance.Size.Y);
            Rectangle collisionObjectRectangle, intersectionRectangle;

            foreach (TilemapCollisionObject collisionObject in TilemapManager.CollisionObjects)
            {
                collisionObjectRectangle = new Rectangle((int)collisionObject.x, (int)collisionObject.y,
                                                         (int)collisionObject.width, (int)collisionObject.height);

                Vector2 collisionResult = CollisionManager.HandleBoxToBoxCollision(collisionObjectRectangle, playerRectangle);

                if (collisionResult != Vector2.Zero)
                {
                    if (collisionResult.X != 0)
                    {
                        if (collisionResult.X > 0)
                            ResultingCollisionVector.X = Math.Max(ResultingCollisionVector.X, collisionResult.X);
                        else
                            ResultingCollisionVector.X = Math.Min(ResultingCollisionVector.X, collisionResult.X);
                    }
                    else
                    {
                        if (collisionResult.Y > 0)
                            ResultingCollisionVector.Y = Math.Max(ResultingCollisionVector.Y, collisionResult.Y);
                        else
                            ResultingCollisionVector.Y = Math.Min(ResultingCollisionVector.Y, collisionResult.Y);
                    }
                }

                else
                    intersectionRectangle = Rectangle.Empty;
            }

            if (ResultingCollisionVector == Vector2.Zero)
                Player.Instance.isColliding = false;
            
            else
                Player.Instance.isColliding = true;
            
        }

        #endregion

        #region Shape Collision Methods

        public static Vector2 HandleBoxToBoxCollision(Rectangle stillBox, Rectangle moverBox)
        {
            Vector2 ResultingCollisionVector;

            bool isCollidingOnX = false, 
                 isCollidingOnY = false;

            Vector2 projectedCenterMassDistance, halfwidthSum;
            Vector2 CollisionMagnitude = Vector2.Zero;

            if (moverBox.Center.X >= stillBox.Center.X)
            {
                // MOVING BOX IS TO THE RIGHT
                // CHECK X-AXIS
                projectedCenterMassDistance.X = moverBox.Center.X - stillBox.Center.X;
                halfwidthSum.X = stillBox.Width / 2 + moverBox.Width / 2;

                if (projectedCenterMassDistance.X <= halfwidthSum.X)
                {
                    isCollidingOnX = true;
                    CollisionMagnitude.X = halfwidthSum.X - projectedCenterMassDistance.X;
                }

                if (isCollidingOnX)
                {
                    // CHECK Y-AXIS COLLISION IF COLLIDES ON X (SEPARATING AXES THEOREM) 
                    if (moverBox.Center.Y <= stillBox.Center.Y)
                    {
                        // Moving Box is RIGHT ABOVE
                        projectedCenterMassDistance.Y = stillBox.Center.Y - moverBox.Center.Y;
                        halfwidthSum.Y = stillBox.Height / 2 + moverBox.Height / 2;

                        if (projectedCenterMassDistance.Y <= halfwidthSum.Y)
                        {
                            isCollidingOnY = true;
                            CollisionMagnitude.Y = -(halfwidthSum.Y - projectedCenterMassDistance.Y);
                        }
                    }
                    else
                    {
                        // Moving Box is RIGHT BELOW
                        projectedCenterMassDistance.Y = moverBox.Center.Y - stillBox.Center.Y;
                        halfwidthSum.Y = stillBox.Height / 2 + moverBox.Height / 2;

                        if (projectedCenterMassDistance.Y <= halfwidthSum.Y)
                        {
                            isCollidingOnY = true;
                            CollisionMagnitude.Y = halfwidthSum.Y - projectedCenterMassDistance.Y;
                        }

                    }
                }
                
            }
            else
            {
                // MOVING BOX IS TO THE LEFT
                // CHECK X-AXIS 

                projectedCenterMassDistance.X = stillBox.Center.X - moverBox.Center.X;
                halfwidthSum.X = stillBox.Width / 2 + moverBox.Width / 2;

                if (projectedCenterMassDistance.X <= halfwidthSum.X)
                {
                    isCollidingOnX = true;
                    CollisionMagnitude.X = -(halfwidthSum.X - projectedCenterMassDistance.X);
                }

                if (isCollidingOnX)
                {
                    if (moverBox.Center.Y <= stillBox.Center.Y)
                    {
                        // Moving Box is LEFT ABOVE
                        projectedCenterMassDistance.Y = stillBox.Center.Y - moverBox.Center.Y;
                        halfwidthSum.Y = stillBox.Height / 2 + moverBox.Height / 2;

                        if (projectedCenterMassDistance.Y <= halfwidthSum.Y)
                        {
                            isCollidingOnY = true;
                            CollisionMagnitude.Y = - (halfwidthSum.Y - projectedCenterMassDistance.Y);
                        }
                    }
                    else
                    {
                        // Moving Box is LEFT BELOW
                        projectedCenterMassDistance.Y = moverBox.Center.Y - stillBox.Center.Y;
                        halfwidthSum.Y = stillBox.Height / 2 + moverBox.Height / 2;

                        if (projectedCenterMassDistance.Y <= halfwidthSum.Y)
                        {
                            isCollidingOnY = true;
                            CollisionMagnitude.Y = halfwidthSum.Y - projectedCenterMassDistance.Y;
                        }
                    }
                }
            }

            if (isCollidingOnX && isCollidingOnY)
            {
                if (Math.Abs(CollisionMagnitude.X) >= Math.Abs(CollisionMagnitude.Y))
                    ResultingCollisionVector = new Vector2(0, CollisionMagnitude.Y);
                else
                    ResultingCollisionVector = new Vector2(CollisionMagnitude.X, 0);

                return ResultingCollisionVector;
            }

            else
                return Vector2.Zero;

        }

        public static Vector2 HandleTriangleToBoxCollision(GeometryUtils.Shape.Triangle triangle, Rectangle box)
        {
            return Vector2.Zero;
        }

        #endregion
    }
}
