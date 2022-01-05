using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day24
{
    public class Program
    {
        /* After analysing input it executes the same loop 14 times:
        inp w
        mul x 0
        add x z
        mod x 26
        div z (1,1,1,1,26,1,1,26,26,26,1,26,26,26)
        add x (14,15,12,11,-5,14,15,-13,-16,-8,15,-8,0,-4)
        eql x w
        eql x 0
        mul y 0
        add y 25
        mul y x
        add y 1
        mul z y
        mul y 0
        add y w
        add y (12,7,1,2,4,15,11,5,3,9,2,3,3,11)
        mul y x
        add z y
        Further analysis:
        w = r (read value)
        z = z/(1 or 26)
        x = z%26 + (xnum)
        x = (x != w ? 1 : 0)
        y = 25*x+1 (26 or 1)
        z = z * y
        y = (w + (ynum))*x
        z = z + y
        */
        public static void Main()
        {
            var d = new int[]{1,1,1,1,26,1,1,26,26,26,1,26,26,26};
            var xnum = new int[]{14,15,12,11,-5,14,15,-13,-16,-8,15,-8,0,-4};
            var ynum = new int[]{12,7,1,2,4,15,11,5,3,9,2,3,3,11};
            Part12(d, xnum, ynum);
        }

        private static void Part12(int[] d, int[] xnum, int[] ynum)
        {
            var conditions = new List<(int, int, int)>();
            var s = new Stack<(int,int)>();
            for(int i = 0; i < 14; i++)
            {
                if(d[i] == 1)
                    s.Push((i, ynum[i]));
                else
                {
                    var (ind, r) = s.Pop();
                    conditions.Add((i, ind, r + xnum[i]));
                }
            }
            int[] w1 = new int[14];
            int[] w2 = new int[14];
            foreach(var (di,fi,v) in conditions)
            {
                System.Console.WriteLine($"w{di}=w{fi} {v}");
                if(v > 0)
                {
                    w1[di] = 9;
                    w1[fi] = 9-v;
                    w2[fi] = 1;
                    w2[di] = 1+v;
                }
                else
                {
                    w1[fi] = 9;
                    w1[di] = 9+v;
                    w2[di] = 1;
                    w2[fi] = 1-v;
                }
            }
            foreach(var c in w1)
                Console.Write(c);
            Console.WriteLine();
            foreach(var c in w2)
                Console.Write(c);
        }
    }
}