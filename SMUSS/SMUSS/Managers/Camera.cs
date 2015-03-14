using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SMUSS
{
    public class Camera
    {
        #region Fields & Members

        private float zoom;
        private Matrix transform;
        private Matrix inverseTransform;
        private Vector2 position;
        private Viewport viewport;
        private Vector2 inertiaOffset;
        private bool isAnimating;

        public float Zoom { get { return zoom; } set { zoom = value; } }
        public Matrix Transform { get { return transform; } set { transform = value; } }
        public Matrix InverseTransform { get { return inverseTransform; } }
        public Vector2 Position { get { return position; } set { position = value; } }

        #endregion

        #region Constructor

        public Camera(Viewport windowViewport)
        {
            zoom = 1.0f;
            inertiaOffset = new Vector2(35, 25);
            position = Vector2.Zero;
            viewport = windowViewport;
            transform = Matrix.CreateRotationZ(0.0f) *
                        Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                        Matrix.CreateTranslation(position.X, position.Y, 0);
            isAnimating = false;
        }

        #endregion

        #region Update

        public void Update()
        {
            zoom = MathHelper.Clamp(zoom, 0.0f, 10.0f);

            // Camera position in which the player is in the center;
            Vector2 projectedPosition = new Vector2();
            projectedPosition.X = (viewport.Width / 2) - Player.Instance.Position.X;
            projectedPosition.Y = (viewport.Height / 2) - Player.Instance.Position.Y;


            // Is the camera's position the same as the projected one?
            if (position != projectedPosition)
            {
                // Does the projected one go beyond the inertia offset?
                if (Math.Abs(position.X - projectedPosition.X) > inertiaOffset.X ||
                   (Math.Abs(position.Y - projectedPosition.Y) > inertiaOffset.Y))
                {

                    // Which axis does it overflow?
                    if (Math.Abs(position.X - projectedPosition.X) > inertiaOffset.X)
                    {
                        isAnimating = true;

                        if (position.X < projectedPosition.X)
                            position.X = projectedPosition.X - inertiaOffset.X;

                        if (position.X > projectedPosition.X)
                            position.X = projectedPosition.X + inertiaOffset.X;
                    }

                    if (Math.Abs(position.Y - projectedPosition.Y) > inertiaOffset.Y)
                    {
                        isAnimating = true;
                        if (position.Y < projectedPosition.Y)
                            position.Y = projectedPosition.Y - inertiaOffset.Y;

                        if (position.Y > projectedPosition.Y)
                            position.Y = projectedPosition.Y + inertiaOffset.Y;
                    }
                }

                else
                {
                    /*
                    if (isAnimating)
                    {
                        if (position.X > projectedPosition.Y)
                            position.X -= Math.Abs(position.X - projectedPosition.X);
                        if (position.X < projectedPosition.X)
                            position.X += Math.Abs(position.X - projectedPosition.X);
                        if (position.Y > projectedPosition.Y)
                            position.Y -= Math.Abs(position.Y - projectedPosition.Y);
                        if (position.Y < projectedPosition.Y)
                            position.Y += Math.Abs(position.Y - projectedPosition.Y);
                    }
                     */
                }
            }

            else
            {
                isAnimating = false;
            }


            transform = Matrix.CreateRotationZ(0.0f) *
                        Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                        Matrix.CreateTranslation(position.X, position.Y, 0);
            inverseTransform = Matrix.Invert(transform);


        }

        #endregion

    }
}
