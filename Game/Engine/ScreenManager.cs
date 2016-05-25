using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Engine
{
    /// <summary> 
    /// The screen manager used to change, update and draw screens along with loading their content.
    /// </summary>
    public static class ScreenManager
    {
        private static List<Screen> screenList;
        private static Screen currentScreen;
        private static bool firstUnactivated = true;

        public static Screen CurrentScreen
        {
            get { return currentScreen; }
        }

        static ScreenManager()
        {
            screenList = new List<Screen>();
        }

        /// <summary>
        /// Adds a new Screen to the Screen Manager. The first screen added is the default (first) screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public static void AddScreen(Screen screen)
        {
            screenList.Add(screen);
            if (screenList.Count == 1)
            {
                currentScreen = screen;
            }
        }
        /// <summary>
        /// Loads all the content in screens inside of the screenmanager. 
        /// </summary>
        public static void LoadContent(ContentManager contentManager)
        {
            foreach (Screen screen in screenList)
            {
                screen.LoadContent(contentManager);
            }
        }

        public static void SetScreen(string screenName)
        {
            foreach (Screen s in screenList)
            {
                if (s.ScreenName.Equals(screenName))
                {
                    currentScreen = s;
                    s.OnScreenActivate();
                }
            }
        }

        public static void UnloadContent()
        {
            foreach (Screen screen in screenList)
            {
                screen.UnloadContent();
            }
        }

        public static void Update(GameTime gameTime)
        {
            if (!GameSettings.Debug && firstUnactivated) { currentScreen.OnScreenActivate();
                firstUnactivated = false;
            }
            currentScreen.Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            currentScreen.Draw(spriteBatch, gameTime);
        }
    }
}