using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day9
{
    public class Program
    {
        public static void Main()
        {
            var nums = File.ReadAllLines("input.txt").Select(s => s.Select(c => c - '0').ToArray()).ToArray();
            Part1(nums);
            Part2(nums);
        }

        private static void Part1(int[][] nums)
        {
            var risks = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                for(int j = 0; j < nums[i].Length; j++)
                {
                    int v = nums[i][j];
                    if(v < (i != 0 && j != 0 ? nums[i-1][j-1] : 10) && 
                       v < (i != 0 ? nums[i-1][j] : 10) &&
                       v < (i != 0 && j != nums[i].Length-1 ? nums[i-1][j+1] : 10) &&
                       v < (j != 0 ? nums[i][j-1] : 10) &&
                       v < (j != nums[i].Length-1 ? nums[i][j+1] : 10) &&
                       v < (i != nums.Length-1 && j != 0 ? nums[i+1][j-1] : 10) &&
                       v < (i != nums.Length-1 ? nums[i+1][j] : 10) &&
                       v < (i != nums.Length-1 && j != nums[i].Length-1 ? nums[i+1][j+1] : 10))
                       risks += v+1;
                }
            }
            Console.WriteLine(risks);
        }

        private static void Part2(int[][] nums)
        {
            int[] maxes = new int[3] {int.MinValue, int.MinValue, int.MinValue};
            var visited = new bool[nums.Length, nums[0].Length];
            for(int i = 0; i < nums.Length; i++)
            {
                for(int j = 0; j < nums[i].Length; j++)
                {
                    if(visited[i,j])
                        continue;
                    var s = FindBasin(nums, i, j, visited);
                    for(int mm = 0; mm < 3; mm++)
                        if(s > maxes[mm])
                        {
                            int t = maxes[mm];
                            maxes[mm] = s;
                            s = t;
                        }
                }
            }
            Console.WriteLine(maxes.Aggregate(1, (a,m) => a * m));
        }

        private static int FindBasin(int[][] nums, int i, int j, bool[,] visited)
        {
            if(i < 0 || i >= nums.Length || j < 0 || j >= nums[i].Length || visited[i,j] || nums[i][j] == 9)
                return 0;
            visited[i,j] = true;
            return 1 + FindBasin(nums, i-1, j, visited) 
                + FindBasin(nums, i, j-1, visited) + FindBasin(nums, i, j+1, visited)
                + FindBasin(nums, i+1, j, visited);
        }
    }
}