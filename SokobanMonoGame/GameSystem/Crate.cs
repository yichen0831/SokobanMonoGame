using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SokobanMonoGame.GameSystem
{
    class Crate : IDrawable, IActable
    {
        public Vector2 Position { get; set; }
        private readonly TextureRegion normalTextureRegion;
        private readonly TextureRegion activatedTextureRegion;
        public bool Activated { get; set; }

        private bool isMoving;
        private Vector2 target;
        private float movingTime;
        private readonly float movingDistance = GameParameters.TileSize;
        private readonly float movingInterval = GameParameters.MoveInterval;

        public Crate(Texture2D texture2D)
        {
            normalTextureRegion = new TextureRegion(texture2D, new Rectangle(0, 0, GameParameters.TileSize, GameParameters.TileSize));
            activatedTextureRegion = new TextureRegion(texture2D, new Rectangle(GameParameters.TileSize, 0, GameParameters.TileSize, GameParameters.TileSize));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TextureRegion textureRegion;
            if (Activated)
            {
                textureRegion = activatedTextureRegion;
            }
            else
            {
                textureRegion = normalTextureRegion;
            }

            spriteBatch.Draw(textureRegion.Texture2D, Position, textureRegion.Rect, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (isMoving)
            {
                movingTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                float alpha = (movingTime) / movingInterval;
                Position = Vector2.Lerp(Position, target, alpha);

                if (alpha >= 1f)
                {
                    isMoving = false;
                }
            }
        }

        public void MoveRight()
        {
            if (isMoving)
            {
                return;
            }

            isMoving = true;
            target = Position + new Vector2(1, 0) * movingDistance;
            movingTime = 0f;
        }

        public void MoveLeft()
        {
            if (isMoving)
            {
                return;
            }

            isMoving = true;
            target = Position + new Vector2(-1, 0) * movingDistance;
            movingTime = 0f;
        }

        public void MoveUp()
        {
            if (isMoving)
            {
                return;
            }

            isMoving = true;
            target = Position + new Vector2(0, -1) * movingDistance;
            movingTime = 0f;
        }

        public void MoveDown()
        {
            if (isMoving)
            {
                return;
            }

            isMoving = true;
            target = Position + new Vector2(0, 1) * movingDistance;
            movingTime = 0f;
        }
    }
}
