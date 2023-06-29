using Microsoft.Xna.Framework;
using System;
using Graphs;
using MonoGame.Extended.Input.InputListeners;

namespace Cat
{
    internal class Scene
    {
        private readonly MouseListener _mouseListener = new();

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

            var rand = new Random();

            var hexes = Math.Pow(Globals.hexes, 2);

            for (int i = 0; i < rand.Next(10, 30) * 0.01 * hexes; i++)
            {
                Globals.gameBoard.Deactivate(Globals.gameBoard.vertices[new Vector2(rand.Next(Globals.hexes), rand.Next(Globals.hexes))]);

            }
        }
        public void Update(GameTime gameTime)
        {
            _mouseListener.Update(gameTime);
            _mouseListener.MouseClicked += (sender, args) =>
            {
                foreach (var item in Globals.gameBoard.vertices.Values)
                {
                    if (item.isAvailable() && item.IsInside(new Vector2(args.Position.X, args.Position.Y)))
                    {
                        Globals.gameBoard.Deactivate(item);
                        Globals.cat.Jump();
                        break;
                    }/*eeeeeeeeeeebac policje*/

                }
            };
            if (Globals.cat.Position == new Vector2(-10, -10))
            {
                Globals.gameBoard = new Graph();
                Initialize();
                Globals.cat = new Cat();
            }
        }
    }
}
