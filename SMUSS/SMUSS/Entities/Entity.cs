using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SMUSS
{
    public abstract class Entity
    {
        #region Members

        public string Name { get; protected set; }
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Size { get; protected set; }
        public Vector2 SpriteSize { get; protected set; }
        public float InverseMass { get; protected set; }
        public int Speed { get; protected set; }

        public SpriteAnimation.Facing Orientation { get; set; }
        public Dictionary<SpriteAnimation.Animation, Texture2D> Sprites { get; set; }
        public bool IsAnimating { get; set; }
        public List<SpriteAnimation.Animation> AnimationsList { get; protected set; }
        public SpriteAnimation.Animation CurrentAnimation { get; protected set; }
        public int FramesIntoAnimation { get; set; }

        public bool isColliding;
        public bool IsExpired;


        #endregion


        #region Update & Draw

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = EntitySpriteManager.GetEntitySprite(this, FramesIntoAnimation);
            Texture2D image = Sprites[CurrentAnimation];

            spriteBatch.Draw(image, Position, sourceRect, Color.White, 0, SpriteSize / 2f, 1f, 0, 0.2f);
        }

        #endregion

    }
}
