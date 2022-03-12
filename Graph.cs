using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;
using Cat; 

namespace Graphs
{
    public class Graph: IEnumerable<Vertex>
    {
        public Dictionary<Vector2, Vertex> vertices;
        private int NextID { get; set; }

        public Graph()
        {
            vertices = new Dictionary<Vector2, Vertex>();
            NextID = 0;
        }

        public bool Link(Vector2 firstEdge, Vector2 secondEdge)
        {
            if (vertices.ContainsKey(firstEdge) && vertices.ContainsKey(secondEdge))
            {
                Vertex from = vertices[firstEdge];
                Vertex to = vertices[secondEdge];
                from.Link(to);
                return true;
            }
            return false;
        }
        public bool Unlink(Vector2 firstEdge, Vector2 secondEdge)
        {
            if (vertices.ContainsKey(firstEdge) && vertices.ContainsKey(secondEdge))
            {
                Vertex from = vertices[firstEdge];
                Vertex to = vertices[secondEdge];
                from.UnLink(to);
                to.UnLink(from);
                return true;
            }
            return false;
        }
        public void Deactivate(Vertex edge)
        {
            if (edge.Coordinates == Globals.cat.Position) return;
            edge.edges.Clear();
            foreach (var item in vertices.Values)
            {
                item.UnLink(edge);
            }
            edge.TurnOff();
        }
        public void Add(Vector2 coords)
        {
            vertices[coords] = new Vertex(coords, NextID);
            NextID++;
        } 

        public Vertex FindID(int id)
        {
            foreach (var item in vertices.Values)
            {
                if (item.vertexID == id) return item;
            }
            return null;
        }

        public IEnumerator<Vertex> GetEnumerator()
        {
            return new GraphEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new GraphEnumerator(this);
        }
    }
    public class GraphEnumerator : IEnumerator<Vertex>
    {
        private Graph currentGraph;
        private Vertex currentVertex;
        public Vertex Current
        {
            get
            {
                if (currentVertex == null)
                {
                    return null;
                }
                else
                {
                    return currentVertex;
                }
            }
        }

        object IEnumerator.Current
        {
            get
            {
                if (currentVertex == null)
                {
                    return null;
                }
                else
                {
                    return currentVertex;
                }
            }
        }
        public GraphEnumerator(Graph graphArgument)
        {
            currentGraph = graphArgument;
            currentVertex = null;
        }

        public bool MoveNext()
        {
            if (currentVertex == null)
            {
                currentVertex = currentGraph.FindID(0);
                return true;
            }
            if (currentVertex.vertexID < currentGraph.vertices.Count - 1) 
            {
                currentVertex = currentGraph.FindID(currentVertex.vertexID + 1);
                return true;
            }
            return false;
        }

        public void Reset()
        {
            currentVertex = null;
        }

        public void Dispose()
        {

        }
    }
}
