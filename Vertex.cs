using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Graphs
{
    public class Vertex
    {
        public Vector2 Coordinates { get; set; }
        public List<Vector2> edges;
        public int vertexID;
        private bool active = true;
        public Vector2 drawnPos;

        public Vertex(Vector2 coords, int id)
        {
            Coordinates = coords;
            edges = new List<Vector2>();
            vertexID = id;

            drawnPos = new Vector2(
                   Coordinates.X * 50 + (Coordinates.Y % 2 == 0 ? 0 : 25),
                   Coordinates.Y * 40);
        }

        public bool IsInside(Vector2 point)
        {
            Vector2 relative = new Vector2(point.X - drawnPos.X, point.Y - drawnPos.Y);

            if (relative.X > 48 || relative.X < 0 || relative.Y > 48 || relative.X < 0) return false;

            if (relative.X > 25) relative.X = 50 - relative.X;
            if (relative.Y > 25) relative.Y = 50 - relative.Y;

            if (relative.Y > 10) return true;

            if(relative.Y > -0.4 * relative.X + 10) return true;

            return false;

        }


        public void Link(Vertex vertexArgument)
        {
            if (!vertexArgument.edges.Contains(Coordinates))
            {
                edges.Add(vertexArgument.Coordinates);
                vertexArgument.edges.Add(Coordinates);
            }
        }
        public void UnLink(Vertex vertexArgument)
        {
            edges.Remove(vertexArgument.Coordinates);
            vertexArgument.edges.Remove(Coordinates);
        }
        public bool GetStatus()
        {
            return active;
        }
        public void TurnOff()
        {
            active = false;
        }

    }
}
