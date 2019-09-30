using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanMonoGame.GameSystem.UI
{
    class Label : UIElement
    {
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }

        public SpriteFont Font { get; set; }

        public Label(SpriteFont font, string text)
        {
            Font = font;
            Text = text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Color);
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
