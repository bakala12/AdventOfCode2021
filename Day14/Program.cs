using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day14
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var (temp, formulas) = ParseInput(input);
            Part1(temp.ToCharArray(), formulas);
            Part2(temp.ToCharArray(), formulas);
        }

        private static (string, Dictionary<(char, char), char>) ParseInput(string[] lines)
        {
            var temp = lines[0];
            var formulas = new Dictionary<(char, char), char>();
            for(int i = 2; i < lines.Length; i++)
            {
                var s = lines[i].Split(" -> ");
                formulas.Add((s[0][0], s[0][1]), s[1][0]);
            }
            return (temp, formulas);
        }

        private static void Part1(char[] temp, Dictionary<(char, char), char> formulas)
        {
            Console.WriteLine(Calculate(temp, formulas, 10));
        }

        private static void Part2(char[] temp, Dictionary<(char, char), char> formulas)
        {
            Console.WriteLine(Calculate(temp, formulas, 40));
        }

        private static long Calculate(char[] temp, Dictionary<(char, char), char> formulas, int steps)
        {
            var map = new Dictionary<char, int>();
            int ind = 0;            
            foreach(var f in formulas)
            {
                if(!map.ContainsKey(f.Key.Item1))
                    map.Add(f.Key.Item1, ind++);
                if(!map.ContainsKey(f.Key.Item2))
                    map.Add(f.Key.Item2, ind++);
            }
            var tab = new long[steps+1, map.Count, map.Count, map.Count];
            foreach(var ((A,B),C) in formulas)
            {
                var ai = map[A];
                var bi = map[B];
                tab[0,ai,bi,ai]++; 
                tab[0,ai,bi,bi]++;
            }
            for(int d = 1; d <= steps; d++)
            {
                foreach(var ((a,b),c) in formulas)
                {
                    var ai = map[a];
                    var bi = map[b];
                    var ci = map[c];
                    for(int x = 0; x < map.Count; x++)
                        tab[d, ai, bi, x] = tab[d-1, ai, ci, x] + tab[d-1, ci, bi, x] + (x == ci ? -1 : 0);
                }
            }
            long[] counts = new long[map.Count];
            for(int i = 1; i < temp.Length; i++)
            {
                var ai = map[temp[i-1]];
                var bi = map[temp[i]];
                for(int x = 0; x < map.Count; x++)
                    counts[x] += tab[steps, ai, bi, x];
                counts[bi]--;
            }
            counts[map[temp.Last()]]++;
            return counts.Max() - counts.Min();
        }
    }
}