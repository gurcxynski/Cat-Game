using Microsoft.Xna.Framework;
using MonoGame.EasyInput;
using System;
using Graphs;

namespace Cat
{
    internal class Scene
    {
        private readonly EasyMouse mouse = new EasyMouse();

        public Scene()
        {
            Initialize();
        }
        public void Initialize()
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

            Globals.gameBoard.Add(new Vector2(-10, -10));

            foreach (var item in Globals.gameBoard)
            {
                if (item.Coordinates.X == 0 || item.Coordinates.Y == 0 || item.Coordinates.X == Globals.hexes - 1 || item.Coordinates.Y == Globals.hexes - 1)
                {
                    Globals.gameBoard.Link(item.Coordinates, new Vector2(-10, -10));
                }
            }

            Random rand = new Random();

            for (int i = 0; i < rand.Next(3, 10); i++)
            {
                Globals.gameBoard.Deactivate(Globals.gameBoard.vertices[new Vector2(rand.Next(0, Globals.hexes), rand.Next(0, Globals.hexes))]);

            }
        }
        public void Update()
        {
            mouse.Update();
            if (mouse.ReleasedThisFrame(MouseButtons.Left))
            {
                foreach (var item in Globals.gameBoard.vertices.Values)
                {
                    if (item.isAvailable() && item.IsInside(new Vector2(mouse.Position.X, mouse.Position.Y)))
                    {
                        Globals.gameBoard.Deactivate(item);
                        Globals.cat.Jump();
                        break;
                    }
                }
            }
            if (Globals.cat.Position == new Vector2(-10, -10))
            {
                Globals.gameBoard = new Graph();
                Initialize();
                Globals.cat = new Cat();
            }
        }
    }
}
