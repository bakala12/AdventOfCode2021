using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day5
{
    public class Program
    {
        public static void Main()
        {
            var lines = ParseInput();
            Part1(lines);
            Part2(lines);
        }

        private static Line[] ParseInput()
        {
            return File.ReadAllLines("input.txt").Select(l =>
            {
                var p = l.Split(" -> ");
                var s = p[0].Split(',').Select(int.Parse).ToArray();
                var e = p[1].Split(',').Select(int.Parse).ToArray();
                return new Line()
                {
                    Start = (s[0], s[1]),
                    End = (e[0], e[1])
                };
            }).ToArray();
        }

        private static void Part1(Line[] lines)
        {
            var pointsOverlap = new Dictionary<(int, int), int>();
            foreach(var l in lines)
            {
                if(l.Start.Item1 == l.End.Item1)
                {
                    var min = Math.Min(l.Start.Item2, l.End.Item2);
                    var max = Math.Max(l.Start.Item2, l.End.Item2);
                    for(int y = min; y <= max; y++)
                    {
                        if(!pointsOverlap.ContainsKey((l.Start.Item1, y)))
                            pointsOverlap.Add((l.Start.Item1, y), 1);
                        else
                            pointsOverlap[(l.Start.Item1, y)]++;
                    }
                }
                else if(l.Start.Item2 == l.End.Item2)
                {
                    var min = Math.Min(l.Start.Item1, l.End.Item1);
                    var max = Math.Max(l.Start.Item1, l.End.Item1);
                    for(int x = min; x <= max; x++)
                    {
                        if(!pointsOverlap.ContainsKey((x, l.Start.Item2)))
                            pointsOverlap.Add((x, l.Start.Item2), 1);
                        else
                            pointsOverlap[(x, l.Start.Item2)]++;
                    }
                }
            }
            Console.WriteLine(pointsOverlap.Values.Count(v => v >= 2));
        }

        private static void Part2(Line[] lines)
        {
            var pointsOverlap = new Dictionary<(int, int), int>();
            foreach(var l in lines)
            {
                if(l.Start.Item1 == l.End.Item1)
                {
                    var min = Math.Min(l.Start.Item2, l.End.Item2);
                    var max = Math.Max(l.Start.Item2, l.End.Item2);
                    for(int y = min; y <= max; y++)
                    {
                        if(!pointsOverlap.ContainsKey((l.Start.Item1, y)))
                            pointsOverlap.Add((l.Start.Item1, y), 1);
                        else
                            pointsOverlap[(l.Start.Item1, y)]++;
                    }
                }
                else if(l.Start.Item2 == l.End.Item2)
                {
                    var min = Math.Min(l.Start.Item1, l.End.Item1);
                    var max = Math.Max(l.Start.Item1, l.End.Item1);
                    for(int x = min; x <= max; x++)
                    {
                        if(!pointsOverlap.ContainsKey((x, l.Start.Item2)))
                            pointsOverlap.Add((x, l.Start.Item2), 1);
                        else
                            pointsOverlap[(x, l.Start.Item2)]++;
                    }
                }
                else if(Math.Abs(l.Start.Item1 - l.End.Item1) == Math.Abs(l.Start.Item2 - l.End.Item2))
                {
                    var vecX = l.Start.Item1 > l.End.Item1 ? -1 : 1;
                    var vecY = l.Start.Item2 > l.End.Item2 ? -1 : 1;
                    for(int i = 0; i <= Math.Abs(l.Start.Item1 - l.End.Item1); i++)
                    {
                        var p = (l.Start.Item1 + i*vecX, l.Start.Item2 + i*vecY);
                        if(!pointsOverlap.ContainsKey(p))
                            pointsOverlap.Add(p, 1);
                        else
                            pointsOverlap[p]++;
                    }
                }
            }
            Console.WriteLine(pointsOverlap.Values.Count(v => v >= 2));
        }
    }

    public struct Line
    {
        public (int, int) Start;
        public (int, int) End;
    }
}