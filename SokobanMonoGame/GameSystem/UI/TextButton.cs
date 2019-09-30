using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SokobanMonoGame.GameSystem.UI
{
    class TextButton : Button
    {
        public event Action ClickedEvent;

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                CalculateTextOffset();
            }
        }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                CalculateButtonArea();
            }
        }

        public TextureRegion current;
        public SpriteFont Font { get; set; }
        private Vector2 textOffset;
        private MouseState prevState;
        private Rectangle buttonArea;

        private int width;
        private int height;

        public TextButton(TextureRegion normal, TextureRegion pressed, SpriteFont font, string text) : base(normal, pressed)
        {
            Font = font;
            Text = text;
            current = normal;
            width = normal.Rect.Width;
            height = normal.Rect.Height;
            CalculateButtonArea();
        }

        private void CalculateTextOffset()
        {
            Vector2 textSize = Font.MeasureString(Text);
            textOffset = new Vector2((normalTextureRegion.Rect.Width - textSize.X) / 2f, (normalTextureRegion.Rect.Height - textSize.Y) / 2f);
        }

        private void CalculateButtonArea()
        {
            buttonArea = new Rectangle((int)Position.X, (int)Position.Y, width, height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(current.Texture2D, Position, current.Rect, Color.White);
            spriteBatch.DrawString(Font, Text, Position + textOffset, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (buttonArea.Contains(mouseState.Position))
                {
                    current = pressedTextureRegion;
                }
            }
            else
            {
                if (prevState.LeftButton == ButtonState.Pressed && buttonArea.Contains(mouseState.Position))
                {
                    ClickedEvent?.Invoke();
                }
                current = normalTextureRegion;
            }

            prevState = mouseState;
        }
    }
}
