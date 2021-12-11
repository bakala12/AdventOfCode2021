using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day11
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt").Select(l => l.Select(c => c-'0').ToArray()).ToArray();
            Part1(input);
            var input2 = File.ReadAllLines("input.txt").Select(l => l.Select(c => c-'0').ToArray()).ToArray();
            Part2(input2);
        }

        private static void Part1(int[][] input)
        {
            long flashes = 0;
            for(int step = 0; step < 100; step++)
            {
                for(int i = 0; i < input.Length; i++)
                {
                    for(int j = 0; j < input[i].Length; j++)
                    {
                        input[i][j]++;
                    }
                }
                bool[,] flushed = new bool[input.Length, input.Length];
                for(int i = 0; i < input.Length; i++)
                {
                    for(int j = 0; j < input[i].Length; j++)
                    {
                        if(input[i][j] > 9)
                            flashes += Flash(input, i, j, flushed);
                    }
                }
            }
            Console.WriteLine(flashes);
        }

        private static void Part2(int[][] input)
        {
            int step = 0;
            for(step = 1; ; step++)
            {
                for(int i = 0; i < input.Length; i++)
                {
                    for(int j = 0; j < input[i].Length; j++)
                    {
                        input[i][j]++;
                    }
                }
                bool[,] flushed = new bool[input.Length, input.Length];
                int f = 0;
                for(int i = 0; i < input.Length; i++)
                {
                    for(int j = 0; j < input[i].Length; j++)
                    {
                        if(input[i][j] > 9)
                            f += Flash(input, i, j, flushed);
                    }
                }
                if(f == input.Length * input.Length)
                    break;
            }
            Console.WriteLine(step);
        }

        private static int Flash(int[][] input, int i, int j, bool[,] flushed)
        {
            if(i < 0 || i >= input.Length || j < 0 || j >= input[i].Length || flushed[i,j])
                return 0;
            if(input[i][j] <= 9)
                input[i][j]++;
            if(input[i][j] <= 9)
                return 0;
            input[i][j] = 0;
            flushed[i,j] = true;
            return 1 + Flash(input, i-1, j-1, flushed) + Flash(input, i-1, j, flushed) + Flash(input, i-1, j+1, flushed) 
                + Flash(input, i, j-1, flushed) + Flash(input, i, j+1, flushed) + Flash(input, i+1, j-1, flushed)
                + Flash(input, i+1, j, flushed) + Flash(input, i+1, j+1, flushed);
        }
    }
}