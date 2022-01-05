using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day22
{
    public partial class Program
    {
        public static void Main()
        {
            var cuboids = File.ReadAllLines("input.txt").Select(Cuboid.ParseLine).ToArray();
            Part1(cuboids);
            Part2(cuboids);
        }

        private static void Part1(Cuboid[] cuboids)
        {
            bool[,,] on = new bool[101,101,101];
            for(int i = 0; i < cuboids.Length; i++)
            {
                if(cuboids[i].RangeX.Item1 < -50 || cuboids[i].RangeX.Item2 > 50 ||
                    cuboids[i].RangeY.Item1 < -50 || cuboids[i].RangeY.Item2 > 50 ||
                    cuboids[i].RangeZ.Item1 < -50 || cuboids[i].RangeZ.Item2 > 50)
                    continue;
                for(int x = cuboids[i].RangeX.Item1; x <= cuboids[i].RangeX.Item2; x++)
                    for(int y = cuboids[i].RangeY.Item1; y <= cuboids[i].RangeY.Item2; y++)
                        for(int z = cuboids[i].RangeZ.Item1; z <= cuboids[i].RangeZ.Item2; z++)
                            on[x+50,y+50,z+50] = cuboids[i].On;
            }
            int c = 0;
            for(int x = -50; x <= 50; x++)
                for(int y = -50; y <= 50; y++)
                    for(int z = -50; z <= 50; z++)
                        if(on[x+50,y+50,z+50])
                            c++;
            Console.WriteLine(c);
        }

        public static void Part2(Cuboid[] cuboids)
        {
            List<Cuboid> boxes = new List<Cuboid>();
            foreach(var c in cuboids)
            {
                boxes.AddRange(boxes.Select(b => Overlap(c, b))
                    .Where(o => o.RangeX.Item1 <= o.RangeX.Item2
						&& o.RangeY.Item1 <= o.RangeY.Item2
						&& o.RangeZ.Item1 <= o.RangeZ.Item2).ToArray());
                if(c.On)
                    boxes.Add(c);
            }
            var res = boxes.Sum(b => b.Size * (b.On ? 1 : -1));
            Console.WriteLine(res);
        }

    	private static Cuboid Overlap(Cuboid b1, Cuboid b2) => new Cuboid( 
	    	!b2.On,
	    	(Math.Max(b1.RangeX.Item1, b2.RangeX.Item1), Math.Min(b1.RangeX.Item2, b2.RangeX.Item2)),
	    	(Math.Max(b1.RangeY.Item1, b2.RangeY.Item1), Math.Min(b1.RangeY.Item2, b2.RangeY.Item2)),
		    (Math.Max(b1.RangeZ.Item1, b2.RangeZ.Item1), Math.Min(b1.RangeZ.Item2, b2.RangeZ.Item2)));
    }
}
