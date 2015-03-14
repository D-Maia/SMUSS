using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System.IO;
using System.Text;
using System.Globalization;

namespace SMUSS
{
    public class Tilemap
    {
        public string Name;
        public int Size;

        private int tileSize = 32;
        private List<Tile> Tiles { get; set; }
        public List<TilemapCollisionObject> CollisionObjects { get; set; }
        public DeserializedJson RootObject { get; set; }

        public class DeserializedJson
        {
            public int height { get; set; }
            public List<Layer> layers { get; set; }
            public string orientation { get; set; }
            public string renderorder { get; set; }
            public int tileheight { get; set; }
            public List<Tileset> tilesets { get; set; }
            public int tilewidth { get; set; }
            public int version { get; set; }
            public int width { get; set; }
        }

        public Tilemap(string jsonString)
        {
            RootObject = JsonConvert.DeserializeObject<DeserializedJson>(jsonString);
            CollisionObjects = new List<TilemapCollisionObject>();
            Tiles = new List<Tile>();
        }

        public void MakeMap()
        {
            foreach (Layer layer in RootObject.layers)
            {
                if (layer.type == "tilelayer")
                    MakeTileLayer(layer);

                else if (layer.type == "objectgroup")
                    MakeObjectLayer(layer);
            }
        }

        public void MakeTileLayer(Layer layer)
        {
            int index = 0;

            for (int y = 0; y < layer.height; ++y)
            {
                for (int x = 0; x < layer.width; ++x)
                {
                    if (layer.data[index] != 0)
                    {
                        Tile tile = new Tile();
                        Tileset tileset = RootObject.tilesets.First();

                        Vector2 position = new Vector2(x * tileSize, y * tileSize);

                        Texture2D image = tileset.Asset;
                        Vector2 size = new Vector2(tileSize);

                        int targetTile = layer.data[index] - tileset.firstgid;
                        int a = targetTile % (tileset.imagewidth / tileSize);
                        int b = targetTile / (tileset.imagewidth / tileSize);
                        Rectangle tilesetSubsection = new Rectangle(a * tileSize, b * tileSize, tileSize, tileSize);

                        float depth = 1f - float.Parse(layer.properties.depth, CultureInfo.InvariantCulture.NumberFormat);

                        tile.Id = index;
                        tile.Image = image;
                        tile.Position = position;
                        tile.Size = size;
                        tile.TilesetSubsection = tilesetSubsection;
                        tile.Depth = depth;

                        Tiles.Add(tile);
                    }

                    index += 1;
                }
            }
        }

        public void MakeObjectLayer(Layer layer)
        {
            foreach (TilemapCollisionObject collisionObject in layer.objects)
            {
                CollisionObjects.Add(collisionObject);
                TilemapManager.CollisionObjects.Add(collisionObject);
            }
        }

        public void loadTileset(ContentManager contentManager, Tileset tileset)
        {
            string tilesetToLoad = "Tileset/";
            tilesetToLoad += tileset.name;

            tileset.Asset = contentManager.Load<Texture2D>(tilesetToLoad);
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile tile in Tiles)
                tile.Draw(spriteBatch);
        }
    }
}
