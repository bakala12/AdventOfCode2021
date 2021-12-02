using System;
using System.IO;
using System.Linq;

namespace Day1
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt").Select(int.Parse).ToArray();
            Part1(input);
            Part2(input);
        }

        private static void Part1(int[] input)
        {
            int s = 0;
            for(int i = 1; i<input.Length; i++)
                if(input[i] > input[i-1])
                    s++;
            Console.WriteLine(s);
        }

        private static void Part2(int[] input)
        {
            int s = 0;
            for(int i = 3; i<input.Length; i++)
                if(input[i] > input[i-3])
                    s++;
            Console.WriteLine(s);
        }
    }
}