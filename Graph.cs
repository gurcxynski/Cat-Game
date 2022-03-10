using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;

namespace Graphs
{
    public class Vertex
    {
        public Vector2 Coordinates { get; set; }
        public readonly int vertexID;
        public List<int> edges;
        public Vertex(Vector2 coords, int ID)
        {
            Coordinates = coords;
            vertexID = ID;
            edges = new List<int>();
        }
        public void Link(Vertex vertexArgument)
        {
            if (!vertexArgument.edges.Contains(vertexID))
            {
                edges.Add(vertexArgument.vertexID);
                vertexArgument.edges.Add(vertexID);
            }
        }
        public void UnLink(Vertex vertexArgument)
        {
            edges.Remove(vertexArgument.vertexID);
            vertexArgument.edges.Remove(vertexID);
        }
    }
    public class Graph: IEnumerable<Vertex>
    {
        public Dictionary<int, Vertex> vertices;
        private int NextID { get; set; }

        public Graph()
        {
            vertices = new Dictionary<int, Vertex>();
            NextID = 0;
        }

        public bool Link(int firstEdge, int secondEdge)
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
        public bool Unlink(int a, int b)
        {
            if (vertices.ContainsKey(a) && vertices.ContainsKey(b))
            {
                Vertex from = vertices[a];
                Vertex to = vertices[b];
                from.UnLink(to);
                return true;
            }
            return false;
        }
        public int Add(Vector2 value)
        {
            vertices[NextID] = new Vertex(value, NextID);
            int temp = NextID;
            NextID++;
            return temp;
        } 
        public void RemoveAtIndex(int removedIndex)
        {
            if (vertices.ContainsKey(removedIndex))
            {
                vertices.Remove(removedIndex);
                if (removedIndex != vertices.Count - 1)
                {
                    vertices.Add(removedIndex, vertices[removedIndex + 1]);
                    for (int i = removedIndex + 1; i < vertices.Count - 1; i++)
                    {
                        vertices[i] = vertices[i + 1];
                    }
                    vertices.Remove(vertices.Count - 1);
                }
                NextID--;
            }
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
                currentVertex = currentGraph.vertices[0];
                return true;
            }
            else if (currentVertex.vertexID < currentGraph.vertices.Count - 1) 
            {
                currentVertex = currentGraph.vertices[currentVertex.vertexID + 1];
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
