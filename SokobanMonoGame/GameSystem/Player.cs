using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        private readonly float movingDistance = 64f;

        public Player(Texture2D texture2D)
        {
            rightTextureRegion = new TextureRegion(texture2D, new Rectangle(0, 128, 64, 64));
            leftTextureRegion = new TextureRegion(texture2D, new Rectangle(64, 192, 64, 64));
            upTextureRegion = new TextureRegion(texture2D, new Rectangle(0, 192, 64, 64));
            downTextureRegion = new TextureRegion(texture2D, new Rectangle(64, 128, 64, 64));
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
                float alpha = (movingTime) / 0.32f;
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
