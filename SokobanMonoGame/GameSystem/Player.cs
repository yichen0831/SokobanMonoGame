using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SokobanMonoGame.GameSystem
{
    enum Facing
    {
        Right,
        Left,
        Up,
        Down
    }

    class Player : IDrawable, IActable
    {
        public Vector2 Position { get; set; }
        private readonly TextureRegion rightTextureRegion;
        private readonly TextureRegion leftTextureRegion;
        private readonly TextureRegion upTextureRegion;
        private readonly TextureRegion downTextureRegion;
        private Facing facing;

        public bool IsMoving { get; private set; }
        private Vector2 target;
        private float movingTime;
        private readonly float movingDistance = GameParameters.TileSize;
        private readonly float movingInterval = GameParameters.MoveInterval;

        public Player(Texture2D texture2D)
        {
            rightTextureRegion = new TextureRegion(texture2D, new Rectangle(0, GameParameters.TileSize * 2, GameParameters.TileSize, GameParameters.TileSize));
            leftTextureRegion = new TextureRegion(texture2D, new Rectangle(GameParameters.TileSize, GameParameters.TileSize * 3, GameParameters.TileSize, GameParameters.TileSize));
            upTextureRegion = new TextureRegion(texture2D, new Rectangle(0, GameParameters.TileSize * 3, GameParameters.TileSize, GameParameters.TileSize));
            downTextureRegion = new TextureRegion(texture2D, new Rectangle(GameParameters.TileSize, GameParameters.TileSize * 2, GameParameters.TileSize, GameParameters.TileSize));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TextureRegion textureRegion;
            switch (facing)
            {
                case Facing.Right:
                    textureRegion = rightTextureRegion;
                    break;
                case Facing.Left:
                    textureRegion = leftTextureRegion;
                    break;
                case Facing.Up:
                    textureRegion = upTextureRegion;
                    break;
                case Facing.Down:
                    textureRegion = downTextureRegion;
                    break;
                default:
                    textureRegion = rightTextureRegion;
                    break;
            }

            spriteBatch.Draw(textureRegion.Texture2D, Position, textureRegion.Rect, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (IsMoving)
            {
                movingTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                float alpha = (movingTime) / movingInterval;
                Position = Vector2.Lerp(Position, target, alpha);

                if (alpha >= 1f)
                {
                    IsMoving = false;
                }
            }
        }

        public void MoveRight()
        {
            IsMoving = true;
            target = Position + new Vector2(1, 0) * movingDistance;
            movingTime = 0f;
        }

        public void MoveLeft()
        {
            IsMoving = true;
            target = Position + new Vector2(-1, 0) * movingDistance;
            movingTime = 0f;
        }

        public void MoveUp()
        {
            IsMoving = true;
            target = Position + new Vector2(0, -1) * movingDistance;
            movingTime = 0f;
        }

        public void MoveDown()
        {
            IsMoving = true;
            target = Position + new Vector2(0, 1) * movingDistance;
            movingTime = 0f;
        }

        public void FaceLeft()
        {
            facing = Facing.Left;
        }

        public void FaceRight()
        {
            facing = Facing.Right;
        }

        public void FaceUp()
        {
            facing = Facing.Up;
        }

        public void FaceDown()
        {
            facing = Facing.Down;
        }
    }
}
