using Microsoft.Xna.Framework;
using MonoGame.EasyInput;
using System;

namespace Cat
{
    internal class Scene
    {
        private readonly EasyMouse mouse = new EasyMouse();

        public Scene()
        {


            for (int i = 0; i < Globals.hexes; i++)
            {
                for (int j = 0; j < Globals.hexes; j++)
                {
                    Globals.gameBoard.Add(new Vector2(i, j));
                }
            }

            foreach (var item in Globals.gameBoard)
            {
                Globals.gameBoard.Link(item.Coordinates, new Vector2(item.Coordinates.X + 1, item.Coordinates.Y));
                Globals.gameBoard.Link(item.Coordinates, new Vector2(item.Coordinates.X, item.Coordinates.Y + 1));

                if (item.Coordinates.Y % 2 == 0)
                {
                    Globals.gameBoard.Link(item.Coordinates, new Vector2(item.Coordinates.X - 1, item.Coordinates.Y + 1));
                }
                else
                {
                    Globals.gameBoard.Link(item.Coordinates, new Vector2(item.Coordinates.X + 1, item.Coordinates.Y + 1));
                }
            }

        }
        public void Update()
        {
            mouse.Update();
            if (mouse.ReleasedThisFrame(MouseButtons.Left))
            {
                foreach (var item in Globals.gameBoard.vertices.Values)
                {
                    if (item.IsInside(new Vector2(mouse.Position.X, mouse.Position.Y)))
                    {
                        Globals.gameBoard.UnlinkAll(item);
                        item.Deactivate();
                    }
                }
            }


        }
    }
}
