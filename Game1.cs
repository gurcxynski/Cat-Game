using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Graphs;

namespace Cat
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Graph board = new Graph(); 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }
        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board.Add(new Vector2(i, j));
                }
            }
            foreach (var item in board)
            {
                board.Link(item.vertexID, item.vertexID + 1);
                board.Link(item.vertexID, item.vertexID + 9);
            }
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            

            base.Draw(gameTime);
        }

    }
}
