using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day7
{
    public class Program
    {
        public static void Main()
        {
            var nums = File.ReadAllText("input.txt").Split(',').Select(int.Parse).ToArray();
            Part1(nums);
            Part2(nums);
        }

        private static void Part1(int[] numbers)
        {
            int min = int.MaxValue;
            for(int x = numbers.Min(); x <= numbers.Max(); x++)
            {
                var y = numbers.Sum(xi => Math.Abs(x - xi));
                if(y < min)
                    min = y;
            }
            Console.WriteLine(min);
        }

        private static void Part2(int[] numbers)
        {
            int min = int.MaxValue;
            for(int x = numbers.Min(); x <= numbers.Max(); x++)
            {
                var y = numbers.Sum(xi => (Math.Abs(x - xi)+1)*(Math.Abs(x-xi)) / 2);
                if(y < min)
                    min = y;
            }
            Console.WriteLine(min);
        }
    }
}