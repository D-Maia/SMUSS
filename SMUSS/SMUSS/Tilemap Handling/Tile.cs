using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SMUSS
{
    public class Tile
    {
        public int Id { get; set; }
        public Texture2D Image { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Rectangle TilesetSubsection { get; set; }
        private float opacity;
        public float Depth { get; set; }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, TilesetSubsection, Color.White, 0, Size, 1f, 0, Depth);
        }
    }
}
