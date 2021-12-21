using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Day19
{
    public class Program
    {
        private static void Main()
        {
            var p = File.ReadLines("input.txt").Select(l => l[28] - '0').ToArray();
            Part1(p[0],p[1]);
            Part2(p[0],p[1]);
        }

        private static void Part1(int p1, int p2)
        {
            var s = new int[2];
            var p = new int[2] {p1, p2};
            int start = 0;
            int dice = 0;
            int d = 0;
            do
            {
                int d1 = (dice++) % 100 + 1;
                int d2 = (dice++) % 100 + 1;
                int d3 = (dice++) % 100 + 1;
                int n = (p[start]+d1+d2+d3-1)%10+1;
                p[start] = n;
                s[start] += n;
                start++;
                if(start == 2)
                    start = 0;
                d += 3;
            }while(s[0] < 1000 && s[1] < 1000);
            Console.WriteLine(Math.Min(s[0], s[1]) * d);        
        }

        private static void Part2(int p1, int p2)
        {
            for(int s1 = 20; s1 >= 0; s1--)
                for(int s2 = 20; s2 >= 0; s2--)
                    for(int who = 0; who <= 1; who++)
                        for(int pos1 = 1; pos1 <= 10; pos1++)
                            for(int pos2 = 1; pos2 <= 10; pos2++)
                            {
                                long w1 = 0, w2 = 0;
                                for(int d1=1; d1<=3;d1++)
                                    for(int d2=1; d2<=3;d2++)
                                        for(int d3 = 1; d3 <=3; d3++)
                                        {
                                            var (ww1, ww2) = CalcWin(pos1, pos2, s1, s2, who, d1+d2+d3);
                                            w1 += ww1;
                                            w2 += ww2;
                                        }
                                Wins[pos1 - 1, pos2 - 1, s1, s2, who] = (w1,w2);
                            }
            var (f1, f2) = Wins[p1-1,p2-1,0,0,0];
            Console.WriteLine(Math.Max(f1, f2));
        }

        private static (long,long) CalcWin(int p1, int p2, int s1, int s2, int who, int d)
        {
            int n = ((who == 0 ? p1 : p2) + d - 1) % 10 + 1;
            if(who == 0)
            {
                p1 = n;
                s1 += n;
            }
            else
            {
                p2 = n;
                s2 += n;
            }
            if(s1 >= 21)
                return (1L,0L);
            if(s2 >= 21)
                return (0L, 1L);
            return Wins[p1-1, p2-1, s1, s2, (who+1) % 2];
        }

        private static readonly (long, long)[,,,,] Wins = new (long,long)[10,10,21,21,2];
    }
}