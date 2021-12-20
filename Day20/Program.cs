using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day20
{
    public class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            var (pattern, image) = Parse(lines);
            Part1(pattern, image);
            Part2(pattern, image);
        }

        private static (string, bool[][]) Parse(string[] lines)
        {
            var image = new bool[lines.Length-2][];
            for(int i = 2; i < lines.Length; i++)
                image[i-2] = lines[i].Select(c => c == '#').ToArray();
            return (lines[0], image);
        }

        private static void Part1(string pattern, bool[][] image)
        {
            var img = Transform(image, pattern, 2);
            Console.WriteLine(CountBits(img));
        }

        private static void Part2(string pattern, bool[][] image)
        {
            var img = Transform(image, pattern, 50);
            Console.WriteLine(CountBits(img));
        }

        private static bool[][] Transform(bool[][] image, string pattern, int steps)
        {
            bool outsideBit = false;
            int step = 0;
            while(step++ < steps)
            {
                image = ProcessImage(pattern, image, image.Length, image[0].Length, outsideBit);
                outsideBit = pattern[outsideBit ? 511 : 0] == '#';
            }
            return image;
        }

        private static bool[][] ProcessImage(string pattern, bool[][] image, int rows, int columns, bool outsideBit)
        {
            bool[][] result = new bool[rows+2][];
            for(int i = 0; i < rows+2; i++)
                result[i] = new bool[columns+2]; 
            for(int i = -1; i < rows+1; i++)
            {
                for(int j = -1; j < columns+1; j++)
                {
                    result[i+1][j+1] = SetBit(pattern, image, i, j, outsideBit);
                }
            }
            return result;
        }  

        private static bool SetBit(string pattern, bool[][] image, int i, int j, bool outsideBit)
        {
            int ind = 0;
            for(int ii = i-1; ii <= i+1; ii++)
            {
                for(int jj = j-1; jj <= j+1; jj++)
                {
                    ind *= 2;
                    if(ii < 0 || ii >= image.Length || jj < 0 || jj >= image[ii].Length)
                        ind += outsideBit ? 1 : 0;
                    else
                        ind += !image[ii][jj] ? 0 : 1;
                }
            }
            return pattern[ind] == '#';
        }

        private static long CountBits(bool[][] image)
        {
            int c = 0;
            for(int i = 0; i < image.Length; i++)
            {   
                for(int j = 0; j < image[i].Length; j++)
                {   
                    if(image[i][j])
                        c++;
                }
            }
            return c;
        }
    }
}