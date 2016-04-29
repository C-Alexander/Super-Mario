using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Engine
{
    public class Map
    {
        private int difficulty;
        private int mapID;
        private int locationModifier;
        private int firstObjectXPosition;
        private int lastXPosition;

        List<SpriteGameObject> spriteGameObjects;
        List<ObstacleObject> jsonObstacleObjects;
        List<PointObject> jsonPointObjects;  

        public Map()
        {
            spriteGameObjects = new List<SpriteGameObject>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (SpriteGameObject o in spriteGameObjects)
            {
                o.Update(gameTime);
            }

        }

        public void initMap(int xPosition)
        {
            foreach (SpriteGameObject o in spriteGameObjects)
            {
                o.Position = o.StartPosition;
                o.Position += new Vector2(xPosition, 0);
            }
            spriteGameObjects.Sort((x, y) => x.Position.X.CompareTo(y.Position.X));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (SpriteGameObject o in spriteGameObjects)
            {
                o.Draw(spriteBatch);
            }
        }

        public int Difficulty
        {
            get { return difficulty; }
            set { difficulty = value; }
        }

        public int MapId
        {
            get { return mapID; }
            set { mapID = value; }
        }

        public int GetFirstObjectXPosition()
        {
            SortSprites();
                return firstObjectXPosition = Convert.ToInt32(spriteGameObjects[0].Position.X);
        }

        private void SortSprites()
        {
            spriteGameObjects.Sort((x, y) => x.Position.X.CompareTo(y.Position.X));
        }

        public int GetLastXPosition()
        {
            SortSprites();
                return lastXPosition = Convert.ToInt32(spriteGameObjects[spriteGameObjects.Count-1].Position.X + spriteGameObjects[spriteGameObjects.Count-1].Texture.Width);
        }

        public List<SpriteGameObject> SpriteGameObjects
        {
            get { return spriteGameObjects; }
            set { spriteGameObjects = value; }
        }

        public void TexturesToStrings()
        {
            jsonObstacleObjects = new List<ObstacleObject>();
            jsonPointObjects =new List<PointObject>();
            foreach (SpriteGameObject o in spriteGameObjects)
            {
                o.TextureName = o.Texture.Name;
                o.Texture = null;
            }
            while (0 < SpriteGameObjects.Count)
            {
                if (spriteGameObjects[0].GetType() == typeof (ObstacleObject))
                {
                    jsonObstacleObjects.Add((ObstacleObject)spriteGameObjects[0]);
                }
                if (spriteGameObjects[0].GetType() == typeof(PointObject)) {
                    jsonPointObjects.Add((PointObject)spriteGameObjects[0]);
                }
                spriteGameObjects.RemoveAt(0);
            }
        }

        public int LocationModifier {
            get { return locationModifier; }
            set { locationModifier = value; }
        }

        public List<ObstacleObject> JsonObstacleObjects {
            get { return jsonObstacleObjects; }
            set { jsonObstacleObjects = value; }
        }

        public List<PointObject> JsonPointObjects {
            get { return jsonPointObjects; }
            set { jsonPointObjects = value; }
        }

        public void StringsToTextures()
        {
            while (0 < jsonObstacleObjects.Count) {
                spriteGameObjects.Add(jsonObstacleObjects[0]);
                jsonObstacleObjects.RemoveAt(0);
            }
            while (0 < jsonPointObjects.Count) {
                spriteGameObjects.Add(jsonPointObjects[0]);
                jsonPointObjects.RemoveAt(0);
            }
            SortSprites();
            foreach (SpriteGameObject o in spriteGameObjects)
            {
                foreach (NamedTexture tex in GameSettings.NamedTextures)
                {
                    if (o.TextureName == tex.textureName)
                    {
                        o.Texture = tex.texture2D;
                    }
                }
            }
        }
    }
}
