using System;
using System.IO;
using System.Linq;

namespace Day3
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            int[] s = new int[input[0].Length];
            for(int i = 0; i < input[0].Length; i++)
            {
                for(int j = 0; j < input.Length; j++)
                {
                    s[i] += input[j][i] == '1' ? 1 : 0;
                }
            }
            int gamma = 0;
            int epsilon = 0;
            for(int k = 0; k < s.Length; k++)
            {
                if(s[k] > input.Length - s[k])
                    gamma += 1;
                else
                    epsilon += 1;
                gamma *= 2;
                epsilon *= 2;
            }
            Console.WriteLine(gamma * epsilon / 4);
        }

        private static void Part2(string[] input)
        {
            bool[] unselectedMost = new bool[input.Length];
            bool[] unselectedLess = new bool[input.Length];
            int pos = 0;
            bool endMost = false, endLess = false;
            int indMost = 0, indLess = 0;
            do
            {
                if(!endMost)
                {
                    int mostBit = FindCommonBit(input, unselectedMost, pos, true);
                    (endMost, indMost) = Filter(input, unselectedMost, pos, mostBit);
                }
                if(!endLess)
                {
                    int lessBit = FindCommonBit(input, unselectedLess, pos, false);
                    (endLess, indLess) = Filter(input, unselectedLess, pos, lessBit);
                }
                pos++;
            }
            while(pos < input.Length && (!endLess || !endMost));
            int oxygen = ConvertToDec(input[indMost]);
            int co2 = ConvertToDec(input[indLess]);
            Console.WriteLine(oxygen * co2);
        }

        private static int FindCommonBit(string[] input, bool[] unselected, int pos, bool most)
        {
            var rem = input.Where((s, i) => !unselected[i]).ToArray();
            var s = rem.Select(s => s[pos] == '1' ? 1 : 0).Sum();
            return most ? (s >= rem.Length - s ? 1 : 0) : (s < rem.Length - s ? 1 : 0);
        }

        private static (bool, int) Filter(string[] input, bool[] unselected, int pos, int bit)
        {
            int ind = -1;
            int c = 0;
            for(int i = 0; i < input.Length; i++)
            {
                if(!unselected[i])
                {
                    if((input[i][pos] == '1' ? 1 : 0) != bit)
                        unselected[i] = true;
                    else
                    {
                        ind = i;
                        c++;       
                    }

                }
            }
            return (c == 1, ind);
        }

        private static int ConvertToDec(string input)
        {
            int s = 0;
            for(int i = 0; i < input.Length; i++)
            {
                s += (input[i] == '1' ? 1 : 0);
                s *= 2;
            }
            return s/2;
        }
    }
}