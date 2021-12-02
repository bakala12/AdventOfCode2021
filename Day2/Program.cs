using System;
using System.IO;
using System.Linq;

namespace Day2
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt").ToArray();
            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            int h = 0;
            int d = 0;
            foreach(var line in input)
            {
                var s = line.Split(' ');
                int m = int.Parse(s[1]);
                switch(s[0])
                {
                    case "forward":
                        h += m;
                        break;
                    case "down":
                        d += m;
                        break;
                    case "up":
                        d -= m;
                        break;
                }
            }
            Console.WriteLine(h * d);
        }

        private static void Part2(string[] input)
        {
            int h = 0;
            int d = 0;
            int a = 0;
            foreach(var line in input)
            {
                var s = line.Split(' ');
                int m = int.Parse(s[1]);
                switch(s[0])
                {
                    case "forward":
                        h += m;
                        d += a * m;
                        break;
                    case "down":
                        a += m;
                        break;
                    case "up":
                        a -= m;
                        break;
                }
            }
            Console.WriteLine(h * d);
        }
    }
}