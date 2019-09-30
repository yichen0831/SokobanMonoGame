using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanMonoGame.GameSystem.UI
{
    abstract class Button : UIElement
    {
        protected TextureRegion normalTextureRegion;
        protected TextureRegion pressedTextureRegion;

        public Button(TextureRegion normal, TextureRegion pressed)
        {
            normalTextureRegion = normal;
            pressedTextureRegion = pressed;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
