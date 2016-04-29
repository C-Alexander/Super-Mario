using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioGame.Engine
{
    /// <summary> 
    /// A screen, such as for example a menu, pause or playing screen, is drawn and updated automatically.
    /// </summary>
    public abstract class Screen
    {
        private GraphicsDevice graphicsDevice;

        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
            set { graphicsDevice = value; }
        }

        public string ScreenName
        {
            get { return screenName; }
            set { screenName = value; }
        }

        private string screenName;

        /// <summary>
        /// Constructor of the screen class
        /// </summary>
        /// <param name="name">A unique identifier of the screen used to activate or manipulate it.</param>
        /// <param name="graphicsDevice">The graphicsdevice passed to created screens</param>
        public Screen(string name, GraphicsDevice graphicsDevice)
        {
            screenName = name;
            this.graphicsDevice = graphicsDevice;
        }

        public virtual void OnScreenActivate()
        {
            
        }
        /// <summary>
        /// Use this method to load all required content, such as for example graphics or sounds.
        /// </summary>
        public virtual void LoadContent(ContentManager Content)
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.End))
            {
                if (ScreenManager.CurrentScreen.ScreenName != "MapEditor") ScreenManager.SetScreen("MapEditor");
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}