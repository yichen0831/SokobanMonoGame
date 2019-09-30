using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanMonoGame.GameSystem.UI
{
    class SimpleUI
    {
        private readonly List<UIElement> elements = new List<UIElement>();
        private readonly SpriteBatch spriteBatch;

        public SimpleUI(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Add(UIElement element)
        {
            elements.Add(element);
        }

        public void Remove(UIElement element)
        {
            elements.Remove(element);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var element in elements)
            {
                element.Update(gameTime);
            }
        }

        public void Draw()
        {
            spriteBatch.Begin();
            foreach (var element in elements)
            {
                element.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

    }
}
