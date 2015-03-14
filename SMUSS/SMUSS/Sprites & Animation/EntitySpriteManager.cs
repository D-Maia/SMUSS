using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SMUSS
{
    static class EntitySpriteManager
    {
        // Entities
        
        public static void LoadEntityContent(Entity entity, ContentManager content)
        {
            entity.Sprites = new Dictionary<SpriteAnimation.Animation, Texture2D>();
            string path  = "Entity/" + entity.Name + "/";

            if (entity.AnimationsList.Contains(SpriteAnimation.Animation.Still))
            {
                Texture2D Still = content.Load<Texture2D>(path + "still");
                entity.Sprites.Add(SpriteAnimation.Animation.Still, Still);
            }

            if (entity.AnimationsList.Contains(SpriteAnimation.Animation.RunningRight))
            {
                Texture2D RunningRight = content.Load<Texture2D>(path + "running-right");
                entity.Sprites.Add(SpriteAnimation.Animation.RunningRight, RunningRight);
            }

            if (entity.AnimationsList.Contains(SpriteAnimation.Animation.RunningLeft))
            {
                Texture2D RunningLeft = content.Load<Texture2D>(path + "running-left");
                entity.Sprites.Add(SpriteAnimation.Animation.RunningLeft, RunningLeft);
            }

            if (entity.AnimationsList.Contains(SpriteAnimation.Animation.RunningUp))
            {
                Texture2D RunningUp = content.Load<Texture2D>(path + "running-up");
                entity.Sprites.Add(SpriteAnimation.Animation.RunningUp, RunningUp);
            }

            if (entity.AnimationsList.Contains(SpriteAnimation.Animation.RunningDown))
            {
                Texture2D RunningDown = content.Load<Texture2D>(path + "running-down");
                entity.Sprites.Add(SpriteAnimation.Animation.RunningDown, RunningDown);
            }
        }

        public static Rectangle getCharacterOrientationSpriteOffset(Entity entity, SpriteAnimation.Facing orientation)
        {
            Rectangle orientedSpriteOffset = new Rectangle();

            if (orientation == SpriteAnimation.Facing.Down)
                orientedSpriteOffset = new Rectangle(0, 0, (int)entity.SpriteSize.X, (int)entity.SpriteSize.Y);
            else if (orientation == SpriteAnimation.Facing.Left)
                orientedSpriteOffset = new Rectangle((int)entity.SpriteSize.X, 0, (int)entity.SpriteSize.X, (int)entity.SpriteSize.Y);
            else if (orientation == SpriteAnimation.Facing.Up)
                orientedSpriteOffset = new Rectangle(2 * (int)entity.SpriteSize.X, 0, (int)entity.SpriteSize.X, (int)entity.SpriteSize.Y);
            else if (orientation == SpriteAnimation.Facing.Right)
                orientedSpriteOffset = new Rectangle(3 * (int)entity.SpriteSize.X, 0, (int)entity.SpriteSize.X, (int)entity.SpriteSize.Y);
            
            return orientedSpriteOffset;
        }

        public static Rectangle GetEntitySprite(Entity entity, int frames)
        {
            Rectangle offset = new Rectangle();
            if (entity.IsAnimating == false)
            {
                offset = EntitySpriteManager.getCharacterOrientationSpriteOffset(entity, entity.Orientation);
            }

            else
            {
                Texture2D image = entity.Sprites[entity.CurrentAnimation];
                int width = image.Width,
                    height = image.Height;

                frames = (int)Math.Floor((double)frames/8);
                offset = new Rectangle((frames%(width/height))*(int)entity.SpriteSize.X, 0, 
                                       (int)entity.SpriteSize.X, (int)entity.SpriteSize.Y);
            }
 
            return offset;
        }
    }
}
