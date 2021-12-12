using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day12
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var graph = ParseInput(input);
            Part1(graph);
            Part2(graph);
        }

        private static void Part1(Graph graph)
        {
            bool[] visited = new bool[graph.Vertices.Length];
            visited[graph.Start] = true;
            Console.WriteLine(FindRoutes(graph, graph.Start, graph.End, graph.Start, visited));
        }

        private static void Part2(Graph graph)
        {
            bool[] visited = new bool[graph.Vertices.Length];
            visited[graph.Start] = true;
            Console.WriteLine(FindRoutes2(graph, graph.Start, graph.End, graph.Start, visited, true));
        }

        private static int FindRoutes(Graph g, int start, int end, int current, bool[] visited)
        {
            if(current == end) return 1;
            int sum = 0;
            for(int to = 0; to < g.Vertices.Length; to++)
            {
                if(g.Edges[current, to] && (!visited[to] || g.IsBigVertex(to)))
                {
                    visited[to] = true;
                    sum += FindRoutes(g, start, end, to, visited);
                    visited[to] = false;
                }
            }
            return sum;
        }

        private static int FindRoutes2(Graph g, int start, int end, int current, bool[] visited, bool doubleHit)
        {
            if(current == end) 
                return 1;
            int sum = 0;
            for(int to = 0; to < g.Vertices.Length; to++)
            {
                if(g.Edges[current, to] && to != start)
                {
                    if(g.IsBigVertex(to))
                    {
                        sum += FindRoutes2(g, start, end, to, visited, doubleHit);
                    }
                    else
                    {
                        if(!visited[to])
                        {
                            visited[to] = true;
                            sum += FindRoutes2(g, start, end, to, visited, doubleHit);
                            visited[to] = false;
                        }
                        else if(doubleHit)
                        {
                            doubleHit = false;
                            sum += FindRoutes2(g, start, end, to, visited, doubleHit);
                            doubleHit = true;
                        }
                    }
                }
            }
            return sum;
        }

        private static Graph ParseInput(string[] lines)
        {
            var v = new List<string>();
            foreach(var l in lines)
            {
                var s = l.Split('-');
                if(!v.Contains(s[0]))
                    v.Add(s[0]);
                if(!v.Contains(s[1]))
                    v.Add(s[1]);
            }
            bool[,] edges = new bool[v.Count, v.Count];
            foreach(var l in lines)
            {
                var s = l.Split('-');
                var from = v.IndexOf(s[0]);
                var to = v.IndexOf(s[1]);
                edges[from, to] = edges[to, from] = true;
            }
            return new Graph()
            {
                Vertices = v.ToArray(),
                Edges = edges,
                Start = v.IndexOf("start"),
                End = v.IndexOf("end")
            };
        }

        private class Graph
        {
            public string[] Vertices;
            public bool[,] Edges;
            public int Start;
            public int End;

            internal bool IsBigVertex(int index) => char.IsUpper(Vertices[index][0]);
        }
    }
}