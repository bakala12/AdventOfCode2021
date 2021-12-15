using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day15
{
    public class Program
    {
        public static void Main()
        {
            var tab = File.ReadAllLines("input.txt").Select(s => s.Select(c => c - '0').ToArray()).ToArray();
            Part1(tab);
            Part2(tab);
        }

        private static void Part1(int[][] tab)
        {
            int size = tab.Length;
            int[,] costs = new int[size,size];
            for(int i = 0; i < size; i++)
                for(int j = 0; j < size; j++)
                    costs[i,j] = int.MaxValue;
            while(UpadateCosts(tab, size, costs));
            Console.WriteLine(costs[0,0] - tab[0][0]);
        }

        private static void Part2(int[][] tab)
        {
            int size = tab.Length*5;
            int[][] extendedTab = new int[size][];
            for(int k = 0; k < size; k++)
                extendedTab[k] = new int[size];
            for(int i = 0; i < 5; i++)
                for(int j = 0; j < 5; j++)
                    for(int ii = 0; ii < tab.Length; ii++)
                        for(int jj = 0; jj < tab.Length; jj++)
                            extendedTab[i*tab.Length+ii][j*tab.Length+jj] = 1+((tab[ii][jj]+i+j-1)%9);
            Part1(extendedTab);
        }

        private static bool UpadateCosts(int[][] tab, int size, int[,] costs)
        {
            bool costsUpdated = false;
            int diag = 2*size-3;
            costs[size-1, size-1] = tab[size-1][size-1];
            while(diag >= 0)
            {
                int j = Math.Min(diag, size-1);
                int i = diag - j;
                while(i < size && j >= 0)
                {
                    int b = int.MaxValue;
                    if(j < size - 1)
                        b = Math.Min(b, costs[i, j+1]);
                    if(j > 0)
                        b = Math.Min(b, costs[i, j-1]);
                    if(i < size - 1)
                        b = Math.Min(b, costs[i+1, j]);
                    if(i > 0)
                        b = Math.Min(b, costs[i-1, j]);
                    if(b+tab[i][j] < costs[i,j])
                    {
                        costs[i,j] = b + tab[i][j];
                        costsUpdated = true;
                    }
                    i++;
                    j--;
                } 
                diag--;
            }
            return costsUpdated;
        }
    }
}