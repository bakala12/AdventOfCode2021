using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day8
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
            int count = 0;
            foreach(var l in lines)
            {
                var s = l.Split('|');
                foreach(var outDigit in s[1].Trim().Split(' '))
                {
                    if(outDigit.Length == 2 || outDigit.Length == 3 || outDigit.Length == 4 || outDigit.Length == 7)
                        count++;
                }
            }
            Console.WriteLine(count);
        }

        private static void Part2(string[] lines)
        {
            char[][] digitSegments = new char[][]
            {
                new char[] {'a', 'b', 'c', 'e', 'f', 'g'},
                new char[] {'c', 'f'},
                new char[] {'a', 'c', 'd', 'e', 'g'},
                new char[] {'a', 'c', 'd', 'f', 'g'},
                new char[] {'b', 'c', 'd', 'f'},
                new char[] {'a', 'b', 'd', 'f', 'g'},
                new char[] {'a', 'b', 'd', 'e', 'f', 'g'},
                new char[] {'a', 'c', 'f'},
                new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g'},
                new char[] {'a', 'b', 'c', 'd', 'f', 'g'}
            };
            int outputSum = 0;
            foreach(var line in lines)
            {
                var s = line.Split('|');
                outputSum += Decode(s[0].Trim().Split(' '), s[1].Trim().Split(' '), digitSegments);
            }
            Console.WriteLine(outputSum);     
        }

        private static int Decode(string[] input, string[] output, char[][] digitSegments)
        {
            Dictionary<char, List<char>> mappings = new Dictionary<char, List<char>>();
            for(char c = 'a'; c <= 'g'; c++)
                mappings.Add(c, new List<char>() {'a', 'b', 'c', 'd', 'e', 'f', 'g'});
            string seven = string.Empty, one = string.Empty;           
            foreach(var i in input)
            {
                if(i.Length == 2) //1
                {
                    one = i;
                    foreach(var c in i)
                        mappings[c].RemoveAll(c => !digitSegments[1].Contains(c));
                }
                else if(i.Length == 3) //7
                {
                    seven = i;
                    foreach(var c in i)
                        mappings[c].RemoveAll(c => !digitSegments[7].Contains(c));
                }
                else if(i.Length == 4) //4
                {
                    foreach(var c in i)
                        mappings[c].RemoveAll(c => !digitSegments[4].Contains(c));
                }
            }
            //Guessing 'a' segment is possible now
            var aSegmentChar = seven.SingleOrDefault(c => !one.Contains(c));
            mappings[aSegmentChar] = new List<char>() { 'a' };
            for(char c = 'a'; c <= 'g'; c++)
                if(c != aSegmentChar)
                    mappings[c].Remove('a');
            foreach(var i in input)
            {
                if(i.Length == 6) //0 or 6 or 9 
                {
                    for(char c = 'a'; c <= 'g'; c++)
                    {
                        if(!i.Contains(c))
                        {
                            if(!one.Contains(c)) //0 or 9
                                mappings[c].RemoveAll(c => c != 'd' && c != 'e');
                            else // 6
                                mappings[c] = new List<char>() { 'c' };
                        }
                    }
                }
            }
            RemoveDuplicates(mappings);
            if(mappings.Any(v => v.Value.Count != 1))
                throw new Exception();
            //encode output
            int res = 0;
            foreach(var o in output)
            {
                List<char> ocs = new List<char>();
                foreach(char oc in o)
                    ocs.Add(mappings[oc][0]);
                var foundV = digitSegments.SingleOrDefault(ca => ca.Length == ocs.Count && ocs.All(ocsc => ca.Contains(ocsc)));
                var foundDigit = digitSegments.ToList().IndexOf(foundV);
                res += foundDigit;
                res *= 10;
            }
            return res / 10;
        }

        private static void RemoveDuplicates(Dictionary<char, List<char>> mappings)
        {
            for(int i = 0; i < 8; i++)
            {
                for(char c = 'a'; c <= 'g'; c++)
                {
                    if(mappings[c].Count == 1)
                    {
                        for(char d = 'a'; d <= 'g'; d++)
                            if(c != d)
                                mappings[d].Remove(mappings[c][0]);
                    }
                }
            }
        }
    }
}