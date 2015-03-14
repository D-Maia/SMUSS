using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SMUSS
{
    public class TestLevel : Level
    {
        public TestLevel()
        {
            Tilemaps = new List<Tilemap>();
            Entities = new List<Entity>();

            Entities.Add(Player.Instance);
            Entities.Add(new BuenoTheBear());

            TilemapManager.createChunk("Grassy");

            foreach (Entity entity in Entities)
                EntityManager.Add(entity);
        }
    }
}
