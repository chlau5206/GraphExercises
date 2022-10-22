using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExer {
    internal class GraphBase {
    }

    public class Vertex : IEquatable<Vertex>, IComparable<Vertex> {
        public int Key { get; set; }

        // private int vexter { get; set; }

        public Vertex(int val = 0, int vexter = 0) {
            this.Key = val;
        }
        public override string ToString() {
            return $"{this.Key}";
        }
        public override bool Equals(object obj) {
            if (obj == null) return false;
            Vertex other = (Vertex)obj;
            return this.Key.Equals(other.Key);
        }
        public int CompareTo(Vertex other) {
            if (other == null)
                return 1;
            else
                return this.Key.CompareTo(other.Key);
        }
        public override int GetHashCode() {
            return this.Key.GetHashCode();
        }
        public bool Equals(Vertex other) {
            if (other == null) return false;
            return (this.Key.Equals(other.Key));
        }
    } // done.

    public struct EdgeType {
        public Vertex startPt;
        public Vertex endPt;
        public int weight; // unsigned integer; unweight = 0

        // public bool directed; // directed = true; undirected = false
    } // done.

    public abstract class GraphOps {
        internal const int UNWEIGHT = 0;
        internal Dictionary<int, Vertex> vertices { get; set; }
        internal Dictionary<Vertex, IList<EdgeType>> AdjList { get; set; }
        //public IList<IList<Vertex>> AdjMatrix { get; set; }
        //public int[][] AdjMatrix { get; set; }

        public GraphOps() {
            vertices = new Dictionary<int, Vertex>();
            AdjList = new Dictionary<Vertex, IList<EdgeType>>();
            //AdjMatrix = new List<IList<Vertex>>();
        } // done.

        public virtual bool VertexExists(int key) {
            return vertices.ContainsKey(key);
        }
        public virtual bool AddVertex(int key) {
            Vertex vertex = new Vertex(key);

            if (VertexExists(key))
                return false; // {vertex} is already existed.");
            else {
                vertices.Add(key, vertex);
                AdjList.Add(vertex, new List<EdgeType>()); // add new edge
#if (DEBUG)
                Console.WriteLine($"{vertex} added.");
#endif
                return true;
            }
        } // done
        public abstract void DelVertex(int key);
        // delete vertex and related Edges
        //public abstract void AddEdge(Vertex startFrom, Vertex endTo, int weight, bool directed);

        public virtual void AddEdgeToAdjList(Vertex startPt, Vertex endPt, int weight) {
            // Edge's dict had been created when create a new Vertex
            EdgeType edge = new EdgeType();
            edge.startPt = startPt;
            edge.endPt = endPt;
            edge.weight = weight;  // unweight Graph
            //  assume from startPt to endPt. No direction needed.
            // for bi-di, exec AddEdgeToAdjList(endPt, startPT, weight)

            // update edge list
            AdjList[startPt].Add(edge);

#if (DEBUG)
            Console.WriteLine($"Edge[{startPt}, {endPt}] added.");
#endif
        }
        public abstract void DelEdge(Vertex a, Vertex b); // direction is a -> b
        public virtual Vertex GetVertex(int key) {
            if (VertexExists(key))
                return vertices[key];
            else
                return null;
        }
        public abstract int GetVertexValue(Vertex key);
        public abstract void PrintAll();
        public abstract int GetCount();
        public virtual void PrintValues(IEnumerable myCollection) {
            foreach (Object obj in myCollection)
                Console.Write($"\t{obj}");
            Console.WriteLine();
        }
        public abstract void PrintEdgesAList();

        ///public abstract void AddData(object graph, int numVertex, int[][] edges);
        }
  
    public class GraphDirWeight : GraphOps {
        // Adjancey List approach
        public GraphDirWeight() {

            // Constructor ......

        }// under construction

        public override void DelVertex(int key) {
            // 1. delete Vertex
            // 2. DelEdge(a, b)
            // 3. DelEdge(b, a)
        }// under construction
        public void AddEdgeAList(Vertex startPt, Vertex endPt, int weight) {
            // for undirected/unweight/Adjacney List
            AddEdgeToAdjList(startPt, endPt, weight);
            // AddEdgeToAdjList(endPt, startPt, weight); // Undirect & weight
        } // in progress
        public override void DelEdge(Vertex a, Vertex b) { }

        public override int GetVertexValue(Vertex key) {
            return key.Key;
        } // done.
        public override void PrintAll() {
            Console.WriteLine(".. under contruction ..");
        }  // under contruction

        public override void PrintEdgesAList() {
            foreach (KeyValuePair<Vertex, IList<EdgeType>> edges in AdjList) {
                Console.WriteLine($"Vertex: {edges.Key}");
                foreach (EdgeType edge in edges.Value)
                    Console.WriteLine(
                        $"\tFrom: {edge.startPt} \tTo: {edge.endPt} \tWeight:{edge.weight}");
            }
        } // done

        public override int GetCount() {
            return 0;
        }
    }

    public class GraphDirWeightedAlgo {
        public GraphDirWeightedAlgo() {
        }

        public static void DijkstraShortestPath() {
            // find the shortest path by using Dijkstra's Algo
        }

        public static void BellmanFordShortestPath() {
            // find the shortest path by using Bellman-Ford's Algo
        }
        public static void GraphAlgoTest2() {
            int n = 6;
            int[][] edges = new int[][] {
                new int[] {1, 2, 2},
                new int[] {1, 3, 5},
                new int[] {2, 4, 6},
                new int[] {2, 5, 10},
                new int[] {3, 4, 9},
                new int[] {3, 5, 8},
                new int[] {4, 6, 4},
                new int[] {5, 6, 3}
            };
            GraphDirWeight club = new GraphDirWeight();
            AddData(club, n, edges);
            //int target = 5;

            ////for (target = 0; target < n + 2; target++) {
            ////  Vertex vertex = BreadthFirstSearchKey(club, target);
            //Vertex vertex = DepthFirstSearchKey(club, target);
            ////DepthFirstSearchKey
            //if (vertex == null)
            //    Console.WriteLine($"Target {target} not found.");
            //else
            //    Console.WriteLine("{0}: Key = {1}, ", (vertex), target);
            ////}
        }
        public static void AddData(GraphDirWeight graph, int numVertex, int[][] edges) {
            // add data to Graph
            GraphDirWeight theGraph = (GraphDirWeight)graph;
            for (int i = 1; i <= numVertex; i++) // numVertex -> number of vertex
                graph.AddVertex(i);

            int len = edges.GetLength(0);
            for (int i = 0; i < len; i++) {
                int begin = edges[i][0];
                int end = edges[i][1];
                int weight = edges[i][2];
                graph.AddEdgeAList(theGraph.GetVertex(begin), theGraph.GetVertex(end), weight);
            }
            graph.PrintEdgesAList();
        } // done
    }

    public class GraphUndirUnweight : GraphOps {
        // Adjancey List approach
        public GraphUndirUnweight() {

            // Constructor ......

        }// under construction

        public override void DelVertex(int key) {
            // 1. delete Vertex
            // 2. DelEdge(a, b)
            // 3. DelEdge(b, a)
        }// under construction
        public virtual void AddEdgeAList(Vertex startPt, Vertex endPt) {
            // for undirected/unweight/Adjacney List
            AddEdgeToAdjList(startPt, endPt, UNWEIGHT);
            AddEdgeToAdjList(endPt, startPt, UNWEIGHT);
        } // done.
        public override void DelEdge(Vertex a, Vertex b) { }

        public override int GetVertexValue(Vertex key) {
            return key.Key;
        } // done.
        public override void PrintAll() {
            Console.WriteLine();
        }  // under contruction

        public override void PrintEdgesAList() {
            foreach (KeyValuePair<Vertex, IList<EdgeType>> edges in AdjList) {
                Console.WriteLine($"Vertex: {edges.Key}");
                foreach (EdgeType edge in edges.Value)
                    Console.WriteLine(
                        $"\tFrom: {edge.startPt} To: {edge.endPt} ");  // Weight:{edge.weight} = 0 
            }
        } // done

        public override int GetCount() {
            return 0;
        }
    }

    public class GraphUndirUnweightAlgo {
        public GraphUndirUnweightAlgo() {
        }

        public static Vertex BreadthFirstSearchKey(GraphUndirUnweight graph, int target) {
            Queue<Vertex> path = new Queue<Vertex>();
            HashSet<Vertex> visted = new HashSet<Vertex>();
            Vertex vertex = graph.vertices.Values.ToArray()[0];

            // path.Enqueue(vertex);
            visted.Add(vertex);
            if (graph.GetVertexValue(vertex) == target)
                return vertex;
            else {
                return BFS(graph, target, path, vertex, visted);
            }
        } // done.

        protected static Vertex BFS(GraphUndirUnweight graph,
                            int target,
                            Queue<Vertex> path,
                            Vertex vertex,
                            HashSet<Vertex> visted) {
            do {
                if (graph.AdjList[vertex].Count != 0) {
                    foreach (var edge in graph.AdjList[vertex]) {
                        if (!visted.Contains(edge.endPt)) {
                            visted.Add(edge.endPt);
                            path.Enqueue(edge.endPt);
                            if (graph.GetVertexValue(edge.endPt) == target)
                                return edge.endPt;  // found it.
                        }
                    }
                }
                if (path.Count > 0)
                    return BFS(graph, target, path, path.Dequeue(), visted);
            } while (path.Count > 0);

            return null;  // if vertex not found.
        } // done.

        public static Vertex DepthFirstSearchKey(GraphUndirUnweight graph, int target) {
            HashSet<Vertex> visted = new HashSet<Vertex>();
            Vertex initVertex = graph.vertices.Values.ToArray()[0];

            return DFS(graph, target, initVertex, visted);
        }

        protected static Vertex DFS(GraphUndirUnweight graph,
                                    int target,
                                    Vertex vertex,
                                    HashSet<Vertex> visted) {

            // process vertex
            visted.Add(vertex);
            if (graph.GetVertexValue(vertex) == target)
                return vertex; // target found.
            else {
                // move to  next adj 
                if (graph.AdjList[vertex].Count >= 0) {
                    foreach (var edge in graph.AdjList[vertex]) { // act liked stack
                        if (!visted.Contains(edge.endPt))
                            return DFS(graph, target, edge.endPt, visted);
                    }
                }
            }
            return null;  // if vertex not found.
        }// status: Done

        static bool Checkdfs(int target,
                                Vertex vertex,
                                bool result,
                                HashSet<Vertex> visted,
                                GraphUndirUnweight graph) {

            // process vertex
            visted.Add(vertex);
            if (graph.GetVertexValue(vertex) == target)
                result = true; // target found.
            else {
                // move to  next adj 
                if (graph.AdjList[vertex].Count != 0) {
                    foreach (var edge in graph.AdjList[vertex]) { // act liked stack
                        if (!visted.Contains(edge.endPt))
                            result = Checkdfs(target, edge.endPt, result, visted, graph);
                        if (result) // found it.
                            break;
                    }
                }
            }
            return result;
        }// status: in progress

    }

}
