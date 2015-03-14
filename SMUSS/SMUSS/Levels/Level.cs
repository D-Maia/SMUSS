using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SMUSS
{
    public abstract class Level
    {
        public List<Tilemap> Tilemaps { get; set; }
        public List<Entity> Entities { get; set; }

        public int Id;

        public Level()
        {
            Tilemaps = new List<Tilemap>();
            Entities = new List<Entity>();
        }

        public virtual void Update()
        {
            EntityManager.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            EntityManager.Draw(spriteBatch);
        }
    }   
}
