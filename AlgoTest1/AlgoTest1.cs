using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExer {
    internal class GraphAlgoTest {
        public static void Test1() {
            int n = 7;
            int[][] edges = new int[][] {
                new int[] {0,1},
                new int[] {0,2},
                new int[] {0,7},
                new int[] {2, 3},
                new int[] {1, 4},
                new int[] {2, 4},
                new int[] {3, 5},
                new int[] {3, 6},
                new int[] {6, 7}
            };
            GraphUndirUnweight club = new GraphUndirUnweight();
            AddData(club, n, edges);
            int target = 5;

            for (target = 0; target < n + 2; target++) {
                //  Vertex vertex = GraphUndirUnweightAlgo.BreadthFirstSearchKey(club, target);
                Vertex vertex = GraphUndirUnweightAlgo.DepthFirstSearchKey(club, target);
                if (vertex == null)
                    Console.WriteLine($"Target {target} not found.");
                else
                    Console.WriteLine("{0}: Key = {1}, ", (vertex), target);
            }
        }

        static void AddData(GraphUndirUnweight graph, int numVertex, int[][] edges) {
            // add data to Graph
            for (int i = 0; i <= numVertex; i++) // numVertex -> number of vertex
                graph.AddVertex(i);

            int len = edges.GetLength(0);
            for (int i = 0; i < len; i++) {
                int x = edges[i][0];
                int y = edges[i][1];
                graph.AddEdgeAList(graph.GetVertex(x), graph.GetVertex(y));
            }
            graph.PrintEdgesAList();
        } // done

    }

}
