using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graphs;


namespace Cat
{
    public static class Globals
    {
        public static int hexes = 10;
        public static Vector2 catPosition = new Vector2(hexes / 2, hexes / 2);
        public static Graph gameBoard = new Graph();
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        
        SpriteFont font;
        Texture2D hex;
        Texture2D cat;

        Scene scene = new Scene();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }
        protected override void Initialize()
        {
            IsMouseVisible = true;

            graphics.PreferredBackBufferHeight = 40 + Globals.hexes * 40;
            graphics.PreferredBackBufferWidth = 50 + Globals.hexes * 50;
            graphics.ApplyChanges();




            base.Initialize();

        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            hex = Content.Load<Texture2D>("hex");
            cat = Content.Load<Texture2D>("cat");
            font = Content.Load<SpriteFont>("fonts/MarkerFelt-16");
        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();

            foreach (var item in Globals.gameBoard.vertices.Values)
            {
                Vector2 drawnPos = new Vector2(
                    item.Coordinates.X * 50 + (item.Coordinates.Y % 2 == 0 ? 15 : 40),
                    item.Coordinates.Y * 40 + 20);


                spriteBatch.Draw(hex, drawnPos, Color.Green);


                spriteBatch.DrawString(font, item.edges.Count.ToString(), drawnPos, Color.Black);

            }

            spriteBatch.Draw(cat, new Vector2(
                Globals.catPosition.X * 50 + (Globals.catPosition.Y % 2 == 0 ? 30 : 45),  
                Globals.catPosition.Y * 40 + 25),
                Color.White);





            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
