using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SokobanMonoGame.GameSystem
{
    class Sprite : IDrawable
    {
        public Vector2 Position { get; set; }
        public Color Color { get; set; } = Color.White;
        private TextureRegion textureRegion;

        public Sprite(TextureRegion textureRegion)
        {
            this.textureRegion = textureRegion;
        }

        public Sprite(Texture2D texture2D)
        {
            textureRegion = new TextureRegion(texture2D, texture2D.Bounds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureRegion.Texture2D, Position, textureRegion.Rect, Color);
        }
    }
}
