using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graphs;
using SharpDX.Win32;
using System.Collections.Generic;
using System;

namespace Cat
{
    public static class Globals
    {
        public const int hexes = 9;
        public static Graph gameBoard = new();
        public static Cat cat = new();
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Dictionary<string, Texture2D> textureList;
        readonly Scene scene = new();

        public Game1()
        {
            graphics = new(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            IsMouseVisible = true;

            graphics.PreferredBackBufferHeight = 10 + Globals.hexes * 40;
            graphics.PreferredBackBufferWidth = 25 + Globals.hexes * 50;
            graphics.ApplyChanges();

            base.Initialize();

        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureList = new Dictionary<string, Texture2D>
            {
                ["hex"] = Content.Load<Texture2D>("hex"),
                ["cat"] = Content.Load<Texture2D>("cat"),
            };

        }

        protected override void Update(GameTime gameTime)
        {
            scene.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();

            foreach (var item in Globals.gameBoard.vertices.Values)
            {
                spriteBatch.Draw(textureList["hex"], item.getDrawnPos(), (item.GetStatus() ? Color.Green : Color.Red));
            }

            spriteBatch.Draw(textureList["cat"], Globals.cat.getDrawnPos(), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
