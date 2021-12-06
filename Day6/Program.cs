using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day6
{
    public class Program
    {
        public static void Main()
        {
            var nums = File.ReadAllText("input.txt").Split(',').Select(int.Parse).ToArray();
            Part1(nums);
            Part2(nums);
        }

        private static void Part1(int[] num)
        {
            int[] numbers = new int[9];
            foreach(var n in num)
                numbers[n]++;
            for(int i = 1; i <= 80; i++)
            {
                var newFishes = numbers[0];
                for(int j = 1; j <= 8; j++)
                    numbers[j-1] = numbers[j];
                numbers[8] = newFishes;
                numbers[6] += newFishes; 
            }
            Console.WriteLine(numbers.Sum());
        }

        private static void Part2(int[] num)
        {
            long[] numbers = new long[9];
            foreach(var n in num)
                numbers[n]++;
            for(int i = 1; i <= 256; i++)
            {
                var newFishes = numbers[0];
                for(int j = 1; j <= 8; j++)
                    numbers[j-1] = numbers[j];
                numbers[8] = newFishes;
                numbers[6] += newFishes; 
            }
            Console.WriteLine(numbers.Sum());
        }
    }
}