using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioGame.Engine
{
    class MapEditor : Screen
    {
        private Texture2D background;
        private List<Texture2D> objects;
        private List<SpriteGameObject> spriteGameObjects;
        private int currentObject;
        private int scrollWheelValue;
        private bool plusDown;
        private bool minusDown;
        private bool mouseDown;
        private Random rand;
        private Map map;
        public MapEditor(GraphicsDevice graphicsDevice, string name = "MapEditor") : base(name, graphicsDevice)
        {
            scrollWheelValue = Mouse.GetState().ScrollWheelValue;
            currentObject = 0;
            rand = new Random();
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            background = Content.Load<Texture2D>("welsh_corgi_dog_glare_face_beautiful_72291_1920x1080");
            init();
        }

        public void init()
        {
            spriteGameObjects = new List<SpriteGameObject>();
            objects = new List<Texture2D>();
            foreach (NamedTexture o in GameSettings.NamedTextures) {
                objects.Add(o.texture2D);
            }
            map = new Map();
            map.Difficulty = 0;
            map.MapId = rand.Next(100000);
            plusDown = false;
            minusDown = false;
            mouseDown = false;
    }

        private void nextObject()
        {
            if (currentObject + 1 < objects.Count)
            {
                currentObject++;
            }
        }

        private void previousObject()
        {
            if (currentObject -1 > 0)
            {
                currentObject--;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            checkControls();
            Console.WriteLine(objects.Count);
        }

        private void checkControls()
        {
            if (Mouse.GetState().ScrollWheelValue > scrollWheelValue) {
                scrollWheelValue = Mouse.GetState().ScrollWheelValue;
                nextObject();
            }
            if (Mouse.GetState().ScrollWheelValue < scrollWheelValue) {
                scrollWheelValue = Mouse.GetState().ScrollWheelValue;
                previousObject();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus) && !plusDown) {
                plusDown = true;
                nextObject();
            }
            if (!Keyboard.GetState().IsKeyDown(Keys.OemPlus)) plusDown = false;
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus) && !minusDown) {
                minusDown = true;
                previousObject();
            }
            if (!Keyboard.GetState().IsKeyDown(Keys.OemMinus)) minusDown = false;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !mouseDown)
            {
                mouseDown = true;
                createObject();
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released) mouseDown = false;
            if (Mouse.GetState().RightButton == ButtonState.Pressed) {
                deleteObject();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && spriteGameObjects.Count > 0) saveMap();
        }

        private void saveMap()
        {
            map.SpriteGameObjects = spriteGameObjects;
            map.TexturesToStrings();
            JSONManager.saveMap(map);
            init();
        }

        private void deleteObject()
        {
            foreach (SpriteGameObject o in spriteGameObjects)
            {
                if (Mouse.GetState().X > o.Position.X &&
                    o.Position.X + o.Texture.Width > Mouse.GetState().X &&
                    Mouse.GetState().Y > o.Position.Y &&
                    o.Position.Y + o.Texture.Height > Mouse.GetState().Y)
                {
                    spriteGameObjects.Remove(o);
                    return;
                }
            }
        }

        private void createObject()
        {
            if (objects[currentObject].Name == "GameObjects/punt_design")
            {
                spriteGameObjects.Add(new PointObject(objects[currentObject], Mouse.GetState().Position.ToVector2(),
                    new Vector2(0, 0)));
            }
            else
            {
                spriteGameObjects.Add(new ObstacleObject(objects[currentObject], Mouse.GetState().Position.ToVector2(),
                    new Vector2(0, 0)));
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0, 0));
            spriteBatch.Draw(objects[currentObject], Mouse.GetState().Position.ToVector2());
            foreach (SpriteGameObject o in spriteGameObjects)
            {
                o.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
