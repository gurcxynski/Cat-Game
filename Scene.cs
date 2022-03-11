using Microsoft.Xna.Framework;

namespace Cat
{
    internal class Scene
    {
        readonly Graphs.Graph board = Globals.gameBoard;
        public Scene()
        {
            for (int i = 0; i < Globals.hexes; i++)
            {
                for (int j = 0; j < Globals.hexes; j++)
                {
                    board.Add(new Vector2(i, j));
                }
            }

            foreach (var item in board)
            {
                board.Link(item.Coordinates, new Vector2(item.Coordinates.X + 1, item.Coordinates.Y));
                board.Link(item.Coordinates, new Vector2(item.Coordinates.X, item.Coordinates.Y + 1));

                if (item.Coordinates.Y % 2 == 0)
                {
                    board.Link(item.Coordinates, new Vector2(item.Coordinates.X - 1, item.Coordinates.Y + 1));
                }
                else
                {
                    board.Link(item.Coordinates, new Vector2(item.Coordinates.X + 1, item.Coordinates.Y + 1));
                }
            }
            Globals.gameBoard = board;
        }

        public void Update()
        {

        }
    }
}
