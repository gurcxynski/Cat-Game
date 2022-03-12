using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graphs;


namespace Cat
{
    public static class Globals
    {
        public static int hexes = 9;
        public static Graph gameBoard = new Graph();
        public static Cat cat = new Cat();
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D hex;
        Texture2D cat;
        readonly Scene scene = new Scene();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            hex = Content.Load<Texture2D>("hex");
            cat = Content.Load<Texture2D>("cat");
        }

        protected override void Update(GameTime gameTime)
        {
            scene.Update();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();

            foreach (var item in Globals.gameBoard.vertices.Values)
            {
                spriteBatch.Draw(hex, item.drawnPos, (item.GetStatus() ? Color.Green : Color.Red));
            }

            spriteBatch.Draw(cat, Globals.cat.drawnPos,Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
