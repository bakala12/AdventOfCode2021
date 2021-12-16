using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day16
{
    public class Program
    {
        public static void Main()
        {
            var t = File.ReadAllText("input.txt");
            var tab = Parse(t);
            Part1(tab);
            Part2(tab);
        }

        private static void Part1(int[] tab)
        {
            System.Console.WriteLine(SumVersions(tab, 0).Item1);
        }

        private static (int, int) SumVersions(int[] tab, int pos)
        {
            if(pos >= tab.Length)
                return (0, 0);
            int version = ConvertToNumber(tab, pos, 3);
            int typeId = ConvertToNumber(tab, pos+3, 3);
            if(typeId == 4)
            {
                int length = 0;
                bool cont;
                int pp = pos+6;
                do
                {
                    cont = tab[pp] == 1;
                    length += 5;
                    pp += 5;
                } while (cont);
                return (version, length+6);
            }
            else //operator
            {
                int lengthId = tab[pos+6];
                if(lengthId == 0)
                {
                    int lengthInBits = ConvertToNumber(tab, pos+7, 15);
                    int bits = 0;
                    int c = pos+22;
                    int sumVersions = 0;
                    while(bits < lengthInBits)
                    {
                        var (v, l) = SumVersions(tab, c);
                        c += l;
                        bits += l;
                        sumVersions += v;
                    }
                    return (sumVersions + version, lengthInBits+22);
                }
                else
                {
                    int numberOfSubpackets = ConvertToNumber(tab, pos+7, 11);
                    int c = pos+18;
                    int n = 1;
                    int sumVersions = 0;
                    int sumLength = 0;
                    while(n <= numberOfSubpackets)
                    {
                        var (v,l) = SumVersions(tab, c);
                        sumVersions += v;
                        c += l;
                        sumLength += l;
                        n++;
                    }
                    return (sumVersions + version, sumLength+18);
                }
            }
        }

        private static void Part2(int[] tab)
        {
            Console.WriteLine(CalculateExpression(tab, 0).Item1);
        }

        private static (long, int) CalculateExpression(int[] tab, int pos)
        {
            if(pos >= tab.Length)
                return (0, 0);
            int typeId = ConvertToNumber(tab, pos+3, 3);
            if(typeId == 4)
            {
                long val = 0;
                int length = 0;
                bool cont;
                int pp = pos+6;
                do
                {
                    cont = tab[pp] == 1;
                    long vv = ConvertToNumber(tab, pp+1, 4);
                    val <<= 4;
                    val += vv;
                    pp += 5;
                    length += 5;
                } while (cont);
                return (val, length+6);
            }
            else //operator
            {
                int lengthId = tab[pos+6];
                List<long> values = new List<long>();
                if(lengthId == 0)
                {
                    int lengthInBits = (int)ConvertToNumber(tab, pos+7, 15);
                    int bits = 0;
                    int c = pos+22;
                    while(bits < lengthInBits)
                    {
                        var (v, l) = CalculateExpression(tab, c);
                        c += l;
                        bits += l;
                        values.Add(v);
                    }
                    return (Operate(typeId, values), lengthInBits+22);
                }
                else
                {
                    int numberOfSubpackets = (int)ConvertToNumber(tab, pos+7, 11);
                    int c = pos+18;
                    int n = 1;
                    int sumLength = 0;
                    while(n <= numberOfSubpackets)
                    {
                        var (v,l) = CalculateExpression(tab, c);
                        values.Add(v);
                        c += l;
                        sumLength += l;
                        n++;
                    }
                    return (Operate(typeId, values), sumLength+18);
                }
            }
        }

        private static long Operate(int typeId, List<long> values)
        {
            switch(typeId)
            {
                case 0: //sum
                    return values.Sum();
                case 1: //product
                    return values.Aggregate(1L, (s,v) => s * v);
                case 2: //minimum
                    return values.Aggregate(long.MaxValue, (s,v) => Math.Min(s,v));
                case 3: //maximum
                    return values.Aggregate(long.MinValue, (s,v) => Math.Max(s,v));
                case 5: //greater than
                    return values[0] > values[1] ? 1L : 0L;
                case 6: //less than
                    return values[0] < values[1] ? 1L : 0L;
                case 7: //equal to
                    return values[0] == values[1] ? 1L : 0L;
            }
            return 0;
        }

        private static int[] Parse(string line)
        {
            int[] tab = new int[line.Length * 4];
            for(int i = 0; i < line.Length; i++)
            {
                switch(line[i])
                {
                    case '1':
                        tab[4*i+3] = 1;
                        break;
                    case '2':
                        tab[4*i+2] = 1;
                        break;
                    case '3':
                        tab[4*i+2] = tab[4*i+3] = 1;
                        break;
                    case '4':
                        tab[4*i+1] = 1;
                        break;
                    case '5':
                        tab[4*i+1] = tab[4*i+3] = 1;
                        break;
                    case '6':
                        tab[4*i+1] = tab[4*i+2] = 1;
                        break;
                    case '7':
                        tab[4*i+1] = tab[4*i+2] = tab[4*i+3] = 1;
                        break;
                    case '8':
                        tab[4*i] = 1;
                        break;
                    case '9':
                        tab[4*i] = tab[4*i+3] = 1;
                        break;
                    case 'A':
                        tab[4*i] = tab[4*i+2] = 1;
                        break;
                    case 'B':
                        tab[4*i] = tab[4*i+2] = tab[4*i+3] = 1;
                        break;
                    case 'C':
                        tab[4*i] = tab[4*i+1] = 1;
                        break;
                    case 'D':
                        tab[4*i] = tab[4*i+1] = tab[4*i+3] = 1;
                        break;
                    case 'E':
                        tab[4*i] = tab[4*i+1] = tab[4*i+2] = 1;
                        break;
                    case 'F':
                        tab[4*i] = tab[4*i+1] = tab[4*i+2] = tab[4*i+3] = 1;
                        break;
                    default:
                        break;
                }
            }
            return tab;
        }
    
        private static int ConvertToNumber(int[] tab, int pos, int length)
        {
            int s = tab[pos];
            for(int i = 1; i < length; i++)
            {
                s *= 2;
                s += tab[pos+i];
            }
            return s;
        }
    }
}