using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day13
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var (dots, folds) = ParseInput(input);
            Part1(dots, folds);
            Part2(dots, folds);
        }

        private static (List<(int,int)>, List<(bool, int)>) ParseInput(string[] lines)
        {
            var dots = new List<(int, int)>();
            var folds = new List<(bool, int)>();
            bool foldStart = false;
            foreach(var l in lines)
            {
                if(l == string.Empty)
                    foldStart = true;
                else if(!foldStart)
                {
                    var s = l.Split(',');
                    dots.Add((int.Parse(s[0]), int.Parse(s[1])));
                }
                else if(foldStart && l.StartsWith("fold"))
                {
                    folds.Add((l[11] == 'x', int.Parse(l.Split('=')[1])));
                }
            }
            return (dots, folds);
        }

        private static void Part1(List<(int,int)> dots, List<(bool, int)> folds)
        {
            Console.WriteLine(Fold(dots, folds[0]).Count); 
        }

        private static void Part2(List<(int,int)> dots, List<(bool, int)> folds)
        {
            foreach(var fold in folds)
                dots = Fold(dots, fold);
            int maxX = dots.Select(p => p.Item1).Max();
            int maxY = dots.Select(p => p.Item2).Max();
            var array = new bool[maxY+1, maxX+1];
            foreach(var (x,y) in dots)
            {
                array[y,x] = true;
            }
            for(int i = 0; i <= maxY; i++)
            {
                for(int j = 0; j <= maxX; j++)
                {
                    Console.Write(array[i,j] ? '#' : '.');
                }
                Console.WriteLine();
            }
        }

        private static List<(int, int)> Fold(List<(int, int)> dots, (bool, int) fold)
        {
            var newDots = new List<(int,int)>();
            var (h, l) = fold;
            foreach(var (x,y) in dots)
            {
                if(h)
                {
                    if(x < l)
                        newDots.Add((x,y));
                    if(x > l && 2*l-x >= 0)
                        newDots.Add((2*l-x, y));
                }
                else
                {
                    if(y < l)
                        newDots.Add((x,y));
                    if(y > l && 2*l - y >= 0)
                        newDots.Add((x, 2*l - y));
                }
            } 
            return newDots.Distinct().ToList();
        }
    }
}
