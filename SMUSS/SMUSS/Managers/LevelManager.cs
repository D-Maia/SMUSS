using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SMUSS
{
    static class LevelManager
    {
        public static List<Level> Levels { get; set; }
        public static Level ActiveLevel { get; private set; }


        public static void StartLevel(Level level)
        {
            Levels = new List<Level>();
            Levels.Add(level);
            ActiveLevel = level;
        }

        public static void LoadContent(ContentManager contentManager)
        {
            foreach (Entity entity in ActiveLevel.Entities)
                EntitySpriteManager.LoadEntityContent(entity, contentManager);
        }

        public static void Update()
        {
            TilemapManager.Update();
            ActiveLevel.Update();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            TilemapManager.Draw(spriteBatch);
            ActiveLevel.Draw(spriteBatch);
        }
    }
}
