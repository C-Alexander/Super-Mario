using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Engine
{
    public class SpriteGameObject
    {
        private Texture2D texture;
        private Vector2 startPosition;
        private Vector2 position;
        private Vector2 velocity;
        private float rotation;
        public float rotationSpeed;
        private string textureName;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 StartPosition {
            get { return startPosition; }
            set { startPosition = value; }
        } 

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public SpriteGameObject(Texture2D texture, Vector2 position, Vector2 velocity, float rotationSpeed = 0)
        {
            this.texture = texture;
            this.position = position;
            startPosition = position;
            this.velocity = velocity;
            this.rotationSpeed = rotationSpeed;
        }

        public virtual void Update(GameTime gameTime)
        {
            position += (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds) + 
                new Vector2(GameSettings.SpeedPerSecond * (Convert.ToInt32(GameSettings.GameTime.TotalGameTime.TotalSeconds)), 0);
            if (rotationSpeed != 0) rotation += rotationSpeed/100;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (rotation != 0)
            {
                spriteBatch.Draw(texture, position - new Vector2(texture.Width/2, -texture.Height/2), null, null, new Vector2(texture.Width/2, texture.Height/2), rotation);
            }
            else
            {
                spriteBatch.Draw(texture, position);
            }
        }

        public string TextureName {
            get { return textureName; }
            set { textureName = value; }
        }
    }
}
