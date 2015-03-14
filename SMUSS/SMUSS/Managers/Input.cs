using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace SMUSS
{
    static class Input
    {
        private static KeyboardState keyboardState, lastKeyboardState;
        private static GamePadState gamepadState, lastGamepadState;

        public static void Update()
        {
            lastKeyboardState = keyboardState;
            lastGamepadState = gamepadState;

            keyboardState = Keyboard.GetState();
            gamepadState = GamePad.GetState(PlayerIndex.One);

        }

        public static Vector2 GetMovementDirection()
        {

            //Vector2 direction = gamepadState.ThumbSticks.Left;
            Vector2 direction = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Left))
                direction.X -= 1;
            if (keyboardState.IsKeyDown(Keys.Right))
                direction.X += 1;
            if (keyboardState.IsKeyDown(Keys.Up))
                direction.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.Down))
                direction.Y += 1;

            if (direction.LengthSquared() > 1)
                direction.Normalize();

            return direction;

        }
    }
}
