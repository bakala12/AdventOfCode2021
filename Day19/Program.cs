using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Day19
{
    public class Program
    {
        public struct Location
        {
            public int X;
            public int Y;
            public int Z;

            public Location(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public static Location Parse(string line)
            {
                var s = line.Split(',');
                return new Location()
                {
                    X = int.Parse(s[0]),
                    Y = int.Parse(s[1]),
                    Z = int.Parse(s[2])
                };
            }

            public static Location operator+(Location l1, Location l2)
            {
                return new Location(l1.X+l2.X, l1.Y+l2.Y, l1.Z+l2.Z);
            }

            public static Location operator-(Location l1, Location l2)
            {
                return new Location(l1.X-l2.X, l1.Y-l2.Y, l1.Z-l2.Z);
            }
        }

        private static List<Location>[] Parse(string[] lines)
        {
            var input = new List<List<Location>>();
            var scanner = 0;
            for(int i = 0; i < lines.Length; i++)
            {
                if(lines[i].StartsWith("--- scanner"))
                {
                    input.Add(new List<Location>());
                    scanner = input.Count - 1;
                }
                else if(!string.IsNullOrEmpty(lines[i]))
                    input[scanner].Add(Location.Parse(lines[i]));
            }
            return input.ToArray();
        }

        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            var input = Parse(lines);
            var centers = Part1(input);
            Part2(centers);
        }

        private static List<Location> Part1(List<Location>[] locations)
        {
            var unsolvedScanners = Enumerable.Range(1, locations.Length-1).ToList();
            var solvedScanner = locations[0];
            List<Location> centers = new List<Location>();
            centers.Add(new Location(0,0,0));
            while(unsolvedScanners.Count > 0)
            {
                var tried = unsolvedScanners[0];
                unsolvedScanners.RemoveAt(0);
                Stopwatch sw =  new Stopwatch();
                sw.Start();
                if(!Overlap(solvedScanner, locations[tried], out Location foundTranslation))
                    unsolvedScanners.Add(tried);
                else
                    centers.Add(foundTranslation);
            }
            Console.WriteLine(solvedScanner.Count);
            return centers;
        }

        private static void Part2(List<Location> centers)
        {
            int max = int.MinValue;
            for(int i = 0; i < centers.Count; i++)
            {
                for(int j = i+1; j < centers.Count; j++)
                {
                    var dv = (centers[i] - centers[j]);
                    var d = Math.Abs(dv.X) + Math.Abs(dv.Y) + Math.Abs(dv.Z);
                    if(d > max)
                        max = d;
                }
            }
            Console.WriteLine(max);
        }

        public struct Rotation
        {
            public Func<Location, Location> UnconvertFunc;

            public Rotation(Func<Location, Location> unconvert)
            {
                UnconvertFunc = unconvert;
            }
        }

        private static IEnumerable<Rotation> GetAllRotations()
        {
            yield return new Rotation(l => new Location(l.X, l.Y, l.Z));
            yield return new Rotation(l => new Location(l.X, l.Z, l.Y));
            yield return new Rotation(l => new Location(l.Y, l.X, l.Z));
            yield return new Rotation(l => new Location(l.Y, l.Z, l.X));
            yield return new Rotation(l => new Location(l.Z, l.X, l.Y));
            yield return new Rotation(l => new Location(l.Z, l.Y, l.X));

            yield return new Rotation(l => new Location(-l.X, l.Y, l.Z));
            yield return new Rotation(l => new Location(-l.X, l.Z, l.Y));
            yield return new Rotation(l => new Location(-l.Y, l.X, l.Z));
            yield return new Rotation(l => new Location(-l.Y, l.Z, l.X));
            yield return new Rotation(l => new Location(-l.Z, l.X, l.Y));
            yield return new Rotation(l => new Location(-l.Z, l.Y, l.X));

            yield return new Rotation(l => new Location(l.X, -l.Y, l.Z));
            yield return new Rotation(l => new Location(l.X, -l.Z, l.Y));
            yield return new Rotation(l => new Location(l.Y, -l.X, l.Z));
            yield return new Rotation(l => new Location(l.Y, -l.Z, l.X));
            yield return new Rotation(l => new Location(l.Z, -l.X, l.Y));
            yield return new Rotation(l => new Location(l.Z, -l.Y, l.X));

            yield return new Rotation(l => new Location(l.X, l.Y, -l.Z));
            yield return new Rotation(l => new Location(l.X, l.Z, -l.Y));
            yield return new Rotation(l => new Location(l.Y, l.X, -l.Z));
            yield return new Rotation(l => new Location(l.Y, l.Z, -l.X));
            yield return new Rotation(l => new Location(l.Z, l.X, -l.Y));
            yield return new Rotation(l => new Location(l.Z, l.Y, -l.X));

            yield return new Rotation(l => new Location(-l.X, -l.Y, l.Z));
            yield return new Rotation(l => new Location(-l.X, -l.Z, l.Y));
            yield return new Rotation(l => new Location(-l.Y, -l.X, l.Z));
            yield return new Rotation(l => new Location(-l.Y, -l.Z, l.X));
            yield return new Rotation(l => new Location(-l.Z, -l.X, l.Y));
            yield return new Rotation(l => new Location(-l.Z, -l.Y, l.X));

            yield return new Rotation(l => new Location(l.X, -l.Y, -l.Z));
            yield return new Rotation(l => new Location(l.X, -l.Z, -l.Y));
            yield return new Rotation(l => new Location(l.Y, -l.X, -l.Z));
            yield return new Rotation(l => new Location(l.Y, -l.Z, -l.X));
            yield return new Rotation(l => new Location(l.Z, -l.X, -l.Y));
            yield return new Rotation(l => new Location(l.Z, -l.Y, -l.X));

            yield return new Rotation(l => new Location(-l.X, l.Y, -l.Z));
            yield return new Rotation(l => new Location(-l.X, l.Z, -l.Y));
            yield return new Rotation(l => new Location(-l.Y, l.X, -l.Z));
            yield return new Rotation(l => new Location(-l.Y, l.Z, -l.X));
            yield return new Rotation(l => new Location(-l.Z, l.X, -l.Y));
            yield return new Rotation(l => new Location(-l.Z, l.Y, -l.X));

            yield return new Rotation(l => new Location(-l.X, -l.Y, -l.Z));
            yield return new Rotation(l => new Location(-l.X, -l.Z, -l.Y));
            yield return new Rotation(l => new Location(-l.Y, -l.X, -l.Z));
            yield return new Rotation(l => new Location(-l.Y, -l.Z, -l.X));
            yield return new Rotation(l => new Location(-l.Z, -l.X, -l.Y));
            yield return new Rotation(l => new Location(-l.Z, -l.Y, -l.X));
        }

        private static readonly Rotation[] AllRotations = GetAllRotations().ToArray();

        private static bool Overlap(List<Location> solvedScanner, List<Location> unsolvedScanner, out Location foundTranslation)
        {
            Rotation foundRotation = default;
            foundTranslation = default;
            foreach(var cr in AllRotations)
            {
                if(TestRotation(solvedScanner, unsolvedScanner, cr, out foundTranslation))
                {
                    foundRotation = cr;
                    return true;
                }
            }
            return false;
        }

        private static bool TestRotation(List<Location> solvedScanner, List<Location> unsolvedScanner, Rotation candidateRotation, out Location foundTranslation)
        {
            foundTranslation = new Location(0,0,0);
            var unsolvedRotated = unsolvedScanner.Select(l => candidateRotation.UnconvertFunc(l)).ToList();
            var translations = new Dictionary<Location, int>();
            foreach(var usl in unsolvedRotated)
            {
                foreach(var sl in solvedScanner)
                {
                    var t = sl - usl;
                    if(translations.ContainsKey(t))
                    {    
                        translations[t]++;
                        if(translations[t] >= 12)
                        {
                            foundTranslation = t;
                            foreach(var l in unsolvedRotated)
                                if(!solvedScanner.Contains(l + t))
                                    solvedScanner.Add(l+t);
                            return true;
                        }
                    }
                    else
                        translations.Add(t,1);
                }                
            }
            return false;
        }
    }
}