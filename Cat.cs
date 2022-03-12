using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Graphs;

namespace Cat
{
    public class Cat
    {
        public Vector2 Position = new Vector2(Globals.hexes / 2, Globals.hexes / 2);
        public Vector2 drawnPos;

        public Cat()
        {
            drawnPos = new Vector2(
                      Position.X * 50 + (Position.Y % 2 == 0 ? 0 : 25),
                      Position.Y * 40);
        }
        public void Jump()
        {
            Queue<Vertex> queue = new Queue<Vertex>();
            Graph graph = Globals.gameBoard;
            queue.Enqueue(graph.vertices[new Vector2(-10, -10)]);
            List<Vector2> explored = new List<Vector2>();
            while(queue.Count > 0)
            {
                Vertex v = queue.Dequeue();
                if (v.edges.Contains(Position))
                {
                    Position = v.Coordinates;
                    drawnPos = new Vector2(
                      Position.X * 50 + (Position.Y % 2 == 0 ? 0 : 25),
                      Position.Y * 40);
                    return;
                }
                foreach (var item in v.edges)
                {
                    if (!explored.Contains(item))
                    {
                        explored.Add(item);
                        queue.Enqueue(graph.vertices[item]);
                    }
                }
            }
        }
    }
}
