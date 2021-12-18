using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day18
{
    public class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            Part1(lines);
            Part2(lines);
        }

        private static void Part1(string[] lines)
        {
            var sum = Parse(lines[0]);
            for(int i = 1; i < lines.Length; i++)
            {
                var p = Parse(lines[i]);
                sum = Add(sum, Parse(lines[i]));
            }
            Console.WriteLine(Magnitude(sum));
        }

        private static void Part2(string[] lines)
        {
            long max = long.MinValue;
            string str = string.Empty;
            for(int i = 0; i < lines.Length; i++)
            {
                for(int j = 0; j < lines.Length; j++)
                {
                    if(i == j) continue;
                    var p1 = Parse(lines[i]);
                    var p2 = Parse(lines[j]);
                    var pp = Add(p1, p2);
                    var m = Magnitude(pp);
                    if(m > max)
                    {
                        max = m;
                    }
                }
            }
            Console.WriteLine(max);
        }

        private static Pair Add(Pair first, Pair second)
        {
            return Reduce(new Pair(first, second));
        }

        private static Pair Reduce(Pair pair)
        {
            bool done = false;
            do
            {
                pair = Explode(pair, out done);
                if(done) continue;
                pair = Split(pair, out done);
            } while (done);
            return pair;
        }

        private static Pair Explode(Pair pair, out bool done)
        {
            done = false;
            int? lv = null, rv = null;
            return Explode(pair, null, 0, ref lv, ref rv, ref done);
        }

        private static Pair Explode(Pair pair, Pair parent, int depth, ref int? lv, ref int? rv, ref bool done)
        {
            if(pair.Left.Type == PairType.Number && pair.Right.Type == PairType.Number)
            {
                if(depth < 4)
                    return pair;
                lv = pair.Left.Value;
                rv = pair.Right.Value;
                done = true;
                return new Pair(0);
            }
            if(pair.Left.Type != PairType.Number)
            {
                pair.Left = Explode(pair.Left, pair, depth+1, ref lv, ref rv, ref done);
                if(rv.HasValue)
                {
                    var r = pair.Right;
                    while(r.Type != PairType.Number)
                        r = r.Left;
                    r.Value += rv.Value;
                    rv = null;
                }
            }
            if(pair.Right.Type != PairType.Number && !done)
            {   
                pair.Right = Explode(pair.Right, pair, depth+1, ref lv, ref rv, ref done);
                if(lv.HasValue)
                {
                    var r = pair.Left;
                    while(r.Type != PairType.Number)
                        r = r.Right;
                    r.Value += lv.Value;
                    lv = null;
                }
            }
            return pair;
        }

        private static Pair Split(Pair pair, out bool done)
        {
            done = false; 
            SplitInternal(pair, ref done);
            return pair;
        }

        private static void SplitInternal(Pair pair, ref bool done)
        {
            if(pair.Type == PairType.Number & !done)
            {
                done = false;
                if(pair.Value >= 10)
                {
                    done = true;
                    int lv = pair.Value / 2;
                    int rv = pair.Value - lv;
                    pair.Type = PairType.Pair;
                    pair.Left = new Pair(lv);
                    pair.Right = new Pair(rv);
                    pair.Value = 0;
                }
            }
            else
            {
                SplitInternal(pair.Left, ref done);
                if(!done)
                    SplitInternal(pair.Right, ref done);
            }
        }

        private static long Magnitude(Pair pair)
        {
            if(pair.Type == PairType.Number)
                return pair.Value;
            return 3*Magnitude(pair.Left)+2*Magnitude(pair.Right);
        }

        private static Pair Parse(string line)
        {
            return Parse(line, 0).Item1;
        }

        private static (Pair, int) Parse(string line, int pos)
        {
            if(line[pos] == ',' || line[pos] == ']')
                return Parse(line, pos+1);
            else if(char.IsDigit(line[pos]))
                return (new Pair(line[pos] - '0'), pos+1);
            else
            {
                var (left, pos1) = Parse(line, pos+1);
                var (right, pos2) = Parse(line, pos1);
                return (new Pair(left, right), pos2+1);
            }        
        }
    }

    public class Pair
    {
        public Pair Left;
        public Pair Right;
        public PairType Type;
        public int Value;
        public Pair Parent;

        public Pair(int value)
        {
            Type = PairType.Number;
            Value = value;
        }

        public Pair(Pair left, Pair right)
        {
            Left = left;
            Right = right;
            Type = PairType.Pair;
        }

        public override string ToString()
        {
            if(Type == PairType.Number)
                return Value.ToString();
            return $"[{Left.ToString()},{Right.ToString()}]";
        }
    }

    public enum PairType
    {
        Number,
        Pair
    }
}