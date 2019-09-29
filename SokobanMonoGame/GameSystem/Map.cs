using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SokobanMonoGame.GameSystem
{
    public enum TileType
    {
        None,
        Wall,
        Ground,
        Target,
        Crate,
        Player
    }

    class Map : IDrawable
    {
        private Vector2 offset;
        public Vector2 Offset
        {
            get => offset; set
            {
                foreach (Sprite sprite in sprites)
                {
                    sprite.Position = sprite.Position - offset + value;
                }

                offset = value;
            }
        }

        private List<List<TileType>> mapList;
        private List<Sprite> sprites = new List<Sprite>();
        private readonly Texture2D texture2D;

        public Map(Texture2D texture2D)
        {
            this.texture2D = texture2D;
        }

        public void Load(List<List<TileType>> mapList)
        {
            this.mapList = mapList;
            sprites.Clear();

            for (int y = 0; y < mapList.Count; y++)
            {
                for (int x = 0; x < mapList[y].Count; x++)
                {
                    Sprite sprite = null;
                    switch (mapList[y][x])
                    {
                        case TileType.Wall:
                            sprite = new Sprite(new TextureRegion(texture2D, new Rectangle(128, 0, 64, 64)));
                            break;
                        case TileType.Ground:
                            sprite = new Sprite(new TextureRegion(texture2D, new Rectangle(0, 64, 64, 64)));
                            break;
                        case TileType.Target:
                            sprite = new Sprite(new TextureRegion(texture2D, new Rectangle(64, 64, 64, 64)));
                            break;
                        default:
                            break;
                    }

                    if (sprite != null)
                    {
                        sprite.Position = new Vector2(x * 64, y * 64) + offset;
                        sprites.Add(sprite);
                    }
                }
            }
        }

        public bool IsWall(Vector2 pos)
        {
            int x = (int)pos.X;
            int y = (int)pos.Y;
            return IsWall(x, y);
        }

        public bool IsWall(int x, int y)
        {
            TileType type = mapList[y][x];
            return type == TileType.Wall;
        }

        public bool IsTarget(Vector2 pos)
        {
            int x = (int)pos.X;
            int y = (int)pos.Y;
            return IsTarget(x, y);
        }

        public bool IsTarget(int x, int y)
        {
            TileType type = mapList[y][x];
            return type == TileType.Target;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var sprite in sprites)
            {
                sprite.Draw(spriteBatch);
            }
        }
    }
}
