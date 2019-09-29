using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanMonoGame.GameSystem
{
    class MapLoader
    {
        public Tuple<int, int> Player;
        public List<Tuple<int, int>> Crates { get; set; }
        public List<List<TileType>> Tiles { get; set; }

        public void Load(string filename)
        {
            string[] data = File.ReadAllLines(filename);

            Tiles = new List<List<TileType>>();
            Crates = new List<Tuple<int, int>>();
            int y = 0;
            foreach (var line in data)
            {
                Tiles.Add(new List<TileType>());
                int x = 0;
                foreach (var item in line)
                {
                    switch (item)
                    {
                        case '#':
                            // Wall
                            Tiles[y].Add(TileType.Wall);
                            break;
                        case ' ':
                            // Ground
                            Tiles[y].Add(TileType.Ground);
                            break;
                        case '.':
                            // Target
                            Tiles[y].Add(TileType.Target);
                            break;
                        case '$':
                            // Crate
                            Tiles[y].Add(TileType.Ground);
                            Crates.Add(new Tuple<int, int>(x, y));
                            break;
                        case '@':
                            // Player
                            Tiles[y].Add(TileType.Ground);
                            Player = new Tuple<int, int>(x, y);
                            break;
                        case '+':
                            // Player on target
                            Tiles[y].Add(TileType.Target);
                            Player = new Tuple<int, int>(x, y);
                            break;
                        case '`':
                        default:
                            Tiles[y].Add(TileType.None);
                            break;
                    }
                    x++;
                }
                y++;
            }
        }
    }
}
