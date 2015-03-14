using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna;
using Microsoft.Xna.Framework;

namespace SMUSS
{
    public class BuenoTheBear : npc
    {


        public BuenoTheBear()
        {
            Name = "Bueno, the bear";
            Speed = 0;

            SpriteSize = new Vector2(53, 120);
            Size = new Vector2(51, 123);
            Position = (GameRoot.ScreenSize / 2) + new Vector2(20, -10);

            Orientation = SpriteAnimation.Facing.Down;

            AnimationsList = new List<SpriteAnimation.Animation>();
            AnimationsList.Add(SpriteAnimation.Animation.Still);
            CurrentAnimation = SpriteAnimation.Animation.Still;

            isColliding = false;
        }

        public override void Update()
        {
            
        }
    }
}
