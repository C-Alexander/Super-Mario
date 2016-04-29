using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Engine
{
    public static class GameSettings
    {
        private static string spriteFolder;
        private static List<NamedTexture> namedTextures;
        private static ContentManager content;
        private static Random rand;
        private static GameTime gameTime;
        private static bool debug = false;

        public static bool Debug
        {
            get { return debug; }
        }

        public static GameTime GameTime
        {
            get { return gameTime; }
            set { gameTime = value;  }
        }

        static GameSettings()
        {
            spriteFolder = "GameObjects";
            NamedTextures = new List<NamedTexture>();
            rand = new Random();
        }
        public static string SpriteFolder {
            get { return spriteFolder; }
        }

        public static List<NamedTexture> NamedTextures {
            get { return namedTextures; }
            set { namedTextures = value; }
        }

        public static ContentManager Content {
            get { return content; }
            set { content = value; }
        }

        public static int getRand(int minValue, int maxValue)
        {
            return rand.Next(minValue, maxValue);
        }
    }

    public class NamedTexture
    {
        public string textureName;
        public Texture2D texture2D;
    }
}
