﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanMonoGame.GameSystem
{
    class MainGame
    {
        enum State
        {
            GameInProcess,
            GameCompleted,
            ChangingLevel
        }

        private readonly ContentManager content;
        private readonly SpriteBatch spriteBatch;
        private Texture2D sokobanTexture2D;
        private Map map;
        private Player player;
        private List<Crate> crates;
        private readonly string[] levels = new string[]
        {
            "map1.txt",
            "map2.txt",
            "map3.txt",
            "map4.txt",
            "map5.txt",
            "map6.txt",
            "map7.txt",
            "map8.txt",
            "map9.txt",
            "map10.txt",
        };

        private State state;
        private int currentLevel = 0;
        private bool isLevelCompleted;
        private readonly float changeLevelInterval = 1f;
        private float changeLevelCountDown;

        public MainGame(ContentManager content, SpriteBatch spriteBatch)
        {
            this.content = content;
            this.spriteBatch = spriteBatch;
        }

        public void Init()
        {
            sokobanTexture2D = content.Load<Texture2D>("sokoban");

            Load();
        }

        private void Load()
        {
            isLevelCompleted = false;
            changeLevelCountDown = changeLevelInterval;

            MapLoader mapLoader = new MapLoader();
            string filename = Path.Combine(content.RootDirectory, levels[currentLevel]);
            mapLoader.Load(filename);

            map = new Map(sokobanTexture2D);
            map.Load(mapLoader.Tiles);
            map.Offset = new Vector2(64f, 64f);

            player = new Player(sokobanTexture2D)
            {
                Position = new Vector2(64f * mapLoader.Player.Item1, 64f * mapLoader.Player.Item2) + map.Offset
            };

            crates = new List<Crate>();
            foreach (var item in mapLoader.Crates)
            {
                Crate crate = new Crate(sokobanTexture2D)
                {
                    Position = new Vector2(64f * item.Item1, 64f * item.Item2) + map.Offset
                };
                crates.Add(crate);
            }
        }

        public void Update(GameTime gameTime)
        {
            switch (state)
            {
                case State.GameInProcess:
                    GameInProcess(gameTime);
                    break;
                case State.GameCompleted:
                    GameCompleted(gameTime);
                    break;
                case State.ChangingLevel:
                    ChangeLevel(gameTime);
                    break;
                default:
                    break;
            }
        }

        private void GameInProcess(GameTime gameTime)
        {
            bool shouldCheckComplete = false;

            KeyboardState keyboardState = Keyboard.GetState();

            if (!isLevelCompleted)
            {


                if (keyboardState.IsKeyDown(Keys.R))
                {
                    // Reset
                    Load();
                }

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    var pos = ConvertPosition(player.Position);
                    var move = new Vector2(-1, 0);
                    if (!player.IsMoving && CheckMovable(pos, move))
                    {
                        player.FaceLeft();
                        player.MoveLeft();
                        var cratePos = pos + move;
                        Crate crate = GetCrate(cratePos);
                        if (crate != null)
                        {
                            crate.MoveLeft();
                            bool isTarget = map.IsTarget(cratePos + move);
                            crate.Activated = isTarget;
                            shouldCheckComplete = true;
                        }
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    var pos = ConvertPosition(player.Position);
                    var move = new Vector2(1, 0);
                    if (!player.IsMoving && CheckMovable(pos, move))
                    {
                        player.FaceRight();
                        player.MoveRight();
                        Vector2 cratePos = pos + move;
                        Crate crate = GetCrate(cratePos);
                        if (crate != null)
                        {
                            crate.MoveRight();
                            bool isTarget = map.IsTarget(cratePos + move);
                            crate.Activated = isTarget;
                            shouldCheckComplete = true;
                        }
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    var pos = ConvertPosition(player.Position);
                    var move = new Vector2(0, -1);
                    if (!player.IsMoving && CheckMovable(pos, move))
                    {
                        player.FaceUp();
                        player.MoveUp();
                        var cratePos = pos + move;
                        Crate crate = GetCrate(cratePos);
                        if (crate != null)
                        {
                            crate.MoveUp();
                            bool isTarget = map.IsTarget(cratePos + move);
                            crate.Activated = isTarget;
                            shouldCheckComplete = true;
                        }
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    var pos = ConvertPosition(player.Position);
                    var move = new Vector2(0, 1);
                    if (!player.IsMoving && CheckMovable(pos, move))
                    {
                        player.FaceDown();
                        player.MoveDown();
                        var cratePos = pos + move;
                        Crate crate = GetCrate(cratePos);
                        if (crate != null)
                        {
                            crate.MoveDown();
                            bool isTarget = map.IsTarget(cratePos + move);
                            crate.Activated = isTarget;
                            shouldCheckComplete = true;
                        }
                    }
                }
            }
            else
            {
                if (changeLevelCountDown > 0f)
                {
                    changeLevelCountDown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    state = State.GameCompleted;
                }
            }

            if (shouldCheckComplete)
            {
                isLevelCompleted = CheckLevelCompleted();
            }

            foreach (var crate in crates)
            {
                crate.Update(gameTime);
            }

            player.Update(gameTime);
        }

        private void GameCompleted(GameTime gameTime)
        {
            state = State.ChangingLevel;
        }

        private void ChangeLevel(GameTime gameTime)
        {
            currentLevel = (currentLevel + 1) % levels.Length;
            Load();
            state = State.GameInProcess;
        }

        public void Draw()
        {
            spriteBatch.Begin();
            map.Draw(spriteBatch);
            foreach (var crate in crates)
            {
                crate.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            spriteBatch.End();
        }

        private bool CheckMovable(Vector2 pos, Vector2 move)
        {
            Vector2 next = pos + move;
            if (map.IsWall(next))
            {
                return false;
            }

            if (IsCrate(next))
            {
                Vector2 crateNext = next + move;
                if (IsCrate(crateNext) || map.IsWall(crateNext))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsCrate(Vector2 pos)
        {
            int x = (int)pos.X;
            int y = (int)pos.Y;
            return IsCrate(x, y);
        }

        private bool IsCrate(int x, int y)
        {
            foreach (var crate in crates)
            {
                Vector2 pos = crate.Position - map.Offset;
                int crateX = (int)(pos.X / 64f);
                int crateY = (int)(pos.Y / 64f);
                if (x == crateX && y == crateY)
                {
                    return true;
                }
            }

            return false;
        }

        private Crate GetCrate(Vector2 cratePos)
        {
            foreach (var crate in crates)
            {
                var convertPos = ConvertPosition(crate.Position);
                if (convertPos == cratePos)
                {
                    return crate;
                }
            }

            return null;
        }

        private Vector2 ConvertPosition(Vector2 screenPos)
        {
            int x = (int)((screenPos.X - map.Offset.X) / 64f);
            int y = (int)((screenPos.Y - map.Offset.Y) / 64f);
            return new Vector2(x, y);
        }

        private bool CheckLevelCompleted()
        {
            return crates.All((crate) => crate.Activated);
        }
    }
}
