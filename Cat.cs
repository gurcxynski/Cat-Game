using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Graphs;

namespace Cat
{
    public class Cat
    {
        public Vector2 Position = new(Globals.hexes / 2);

        public void Jump()
        {
            Queue<Vertex> queue = new();
            Graph graph = Globals.gameBoard;
            queue.Enqueue(graph.vertices[new Vector2(-10, -10)]);
            List<Vector2> explored = new();
            List<Vertex> possible = new();
            while(queue.Count > 0)
            {
                Vertex v = queue.Dequeue();
                if (v.edges.Contains(Position))
                {
                    possible.Add(v);
                    Position = v.Coordinates;
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

        public Vector2 getDrawnPos()
        {
            return new (
                      Position.X * 50 + (Position.Y % 2 == 0 ? 0 : 25),
                      Position.Y * 40);
        }
    }
}
