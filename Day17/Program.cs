using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day17
{
    public class Program
    {
        public static void Main()
        {
            var text = File.ReadAllText("input.txt");
            var range = Parse(text);
            Part1(range);
            Part2(range);
        }

        private static Range Parse(string text)
        {
            var s = text.Substring(15, text.Length-15).Split(", y=");
            var x = s[0].Split("..").Select(int.Parse).ToArray();
            var y = s[1].Split("..").Select(int.Parse).ToArray();
            return new Range(x[0], x[1], y[0], y[1]);
        }

        private static void Part1(in Range range)
        {
            //for n = 2*vy+1 y will be equal to 0 with velocity = -vy-1. To hit the area it needs to be vy = -range.MaxY-1
            Console.WriteLine((-range.MinY)*((-range.MinY)-1)/2);
        }

        private static void Part2(in Range range)
        {
            int c = 0;
            for(int vx = 1; vx <= range.MaxX; vx++)
            {
                for(int vy = range.MinY; vy <= -range.MinY; vy++)
                {
                    for(int n = 1; n <= 1000; n++)
                    {
                        var x = n <= vx ? (2*vx-n+1)*n/2 : (vx+1)*vx/2;
                        var y = (2*vy-n+1)*n/2;
                        if(x >= range.MinX && x <= range.MaxX && y >= range.MinY && y <= range.MaxY)
                        {
                            c++;
                            break;
                        }   
                    }
                }
            }
            Console.WriteLine(c);
        }
    }

    public struct Range
    {
        public int MinX;
        public int MaxX;
        public int MinY;
        public int MaxY;

        public Range(int minX, int maxX, int minY, int maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }
    }
}

