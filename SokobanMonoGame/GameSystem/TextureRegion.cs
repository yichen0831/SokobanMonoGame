using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanMonoGame.GameSystem
{
    class TextureRegion
    {
        public Rectangle Rect { get; set; }
        public Texture2D Texture2D { get; private set; }

        public TextureRegion(Texture2D texture2D, Rectangle rect)
        {
            Texture2D = texture2D;
            Rect = rect;
        }
    }
}
