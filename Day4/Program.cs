using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day4
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var boards = ParseInput(input);
            Console.WriteLine(Part1(boards));
            var boards2 = ParseInput(input);
            Console.WriteLine(Part2(boards2));
        }

        private static int Part1((List<int>, List<BingoBoard>) input)
        {
            foreach(var num in input.Item1)
            {
                for(int b = 0; b < input.Item2.Count; b++)
                {
                    for(int i = 0; i < 5; i++)
                    {
                        for(int j = 0; j < 5; j++)
                        {
                            if(input.Item2[b].Board[i, j] == num)
                                input.Item2[b].Marked[i,j] = true;
                            if(input.Item2[b].IsWinning())
                            {
                                return input.Item2[b].GetWinResult(num);
                            }
                        }
                    }
                }
            }
            return -1;
        }

        private static int Part2((List<int>, List<BingoBoard>) input)
        {
            List<int> wonTables = new List<int>();
            List<int> winningNums = new List<int>();
            foreach(var num in input.Item1)
            {
                for(int b = 0; b < input.Item2.Count; b++)
                {
                    for(int i = 0; i < 5; i++)
                    {
                        if(wonTables.Contains(b))
                            break;
                        for(int j = 0; j < 5; j++)
                        {
                            if(input.Item2[b].Board[i, j] == num)
                                input.Item2[b].Marked[i,j] = true;
                            if(input.Item2[b].IsWinning())
                            {
                                wonTables.Add(b);
                                winningNums.Add(num);
                            }
                        }
                    }
                }
            }
            return input.Item2[wonTables.Last()].GetWinResult(winningNums.Last());
        }

        private static (List<int>, List<BingoBoard>) ParseInput(string[] lines)
        {
            var numbers = lines[0].Split(',').Select(int.Parse).ToList();
            var boards = new List<BingoBoard>();
            for(int i = 2; i < lines.Length; i++)
            {
                boards.Add(BingoBoard.Parse(lines, i));
                i+= 5;
            }
            return (numbers, boards);
        }
    }

    public class BingoBoard
    {
        public int[,] Board;
        public bool[,] Marked;

        public BingoBoard()
        {
            Board = new int[5,5];
            Marked = new bool[5,5];
        }

        public static BingoBoard Parse(string[] lines, int pos)
        {
            var b = new BingoBoard();
            for(int i = 0; i < 5; i++)
            {
                var l = lines[pos++].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if(l.Length != 5) throw new Exception();
                for(int j = 0; j < 5; j++)
                    b.Board[i, j] = int.Parse(l[j]);
            }
            return b;
        }

        public bool IsWinning()
        {
            int i,j;
            for(i = 0; i < 5; i++)
            {
                for(j = 0; j < 5; j++)
                {
                    if(!Marked[i,j])
                        break;
                }
                if(j==5)
                    return true;
            }
            for(i = 0; i < 5; i++)
            {
                for(j = 0; j < 5; j++)
                {
                    if(!Marked[j,i])
                        break;
                }
                if(j==5)
                    return true;
            }
            return false;
        }

        public int GetWinResult(int num)
        {
            int sum = 0;
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if(!Marked[i,j])
                        sum += Board[i,j];
                }
            }
            return sum * num;
        }
    }
}