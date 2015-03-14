using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SMUSS
{
    static class TilemapManager
    {
        private static List<Tilemap> Tilemaps = new List<Tilemap>();
        public static List<TilemapCollisionObject> CollisionObjects = new List<TilemapCollisionObject>();

        public static void createChunk(string jsonFile)
        {
            // THIS METHOD IS CALLED BY THE LEVELMANAGER CLASS.
            // THE LEVEL MANAGER WILL DECIDE WHICH TILEMAP IS TO BE LOADED / DRAWN, AND
            // PASS THE PATH TO ITS CORRESPONDING JSON FILE TO THIS METHOD

            string pathToJsonFile = "C:\\Users\\Tico\\Documents\\Visual Studio 2010\\Projects\\SMUSS\\SMUSS\\SMUSS";
            pathToJsonFile += "\\Files\\TileMaps\\";
            pathToJsonFile += jsonFile;
            pathToJsonFile += ".txt";

            if (File.Exists(pathToJsonFile))
            {
                string jsonString = File.ReadAllText(pathToJsonFile);

                Tilemap tilemap = new Tilemap(jsonString);
                CreateTilemap(tilemap);
            }
        }

        public static void Update()
        {
        }

        public static void CreateTilemap(Tilemap tilemap)
        {
            foreach (Tileset tileset in tilemap.RootObject.tilesets)
                tilemap.loadTileset(GameRoot.ContentManager, tileset);
            
            tilemap.MakeMap();
            Tilemaps.Add(tilemap);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tilemap tilemap in Tilemaps)
            {
                tilemap.Draw(spriteBatch);
            }
        }

    }
}
