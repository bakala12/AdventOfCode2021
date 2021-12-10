using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day10
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
            int score = 0;
            foreach(var line in lines)
            {
                var stack = new Stack<char>();
                bool finish = false;
                foreach(var c in line)
                {
                    switch(c)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            stack.Push(c);
                            break;
                        case ')':
                            if(stack.Pop() != '(')
                            {
                                score += 3;
                                finish = true;
                            }
                            break;  
                        case ']':
                            if(stack.Pop() != '[')
                            {
                                score += 57;
                                finish = true;
                            }
                            break; 
                        case '}':
                            if(stack.Pop() != '{')
                            {
                                score += 1197;
                                finish = true;
                            }
                            break; 
                        case '>':
                            if(stack.Pop() != '<')
                            {
                                score += 25137;
                                finish = true;
                            }
                            break; 
                        default:
                            throw new Exception();
                    }
                }
                if(finish)
                    continue;
            }
            Console.WriteLine(score);
        }

        private static void Part2(string[] lines)
        {
            List<long> scores = new List<long>();
            foreach(var line in lines)
            {
                var stack = new Stack<char>();
                bool finish = false;
                foreach(var c in line)
                {
                    switch(c)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            stack.Push(c);
                            break;
                        case ')':
                            if(stack.Pop() != '(')
                                finish = true;
                            break;  
                        case ']':
                            if(stack.Pop() != '[')
                                finish = true;
                            break; 
                        case '}':
                            if(stack.Pop() != '{')
                                finish = true;
                            break; 
                        case '>':
                            if(stack.Pop() != '<')
                                finish = true;
                            break; 
                        default:
                            throw new Exception();
                    }
                }
                if(finish)
                    continue;
                long s = 0;
                while(stack.Count > 0)
                {
                    switch(stack.Pop())
                    {
                        case '(':
                            s = 5 * s + 1;
                            break;
                        case '[':
                            s = 5 * s + 2;
                            break;
                        case '{':
                            s = 5 * s + 3;
                            break;
                        case '<':
                            s = 5 * s + 4;
                            break;
                    }
                }
                scores.Add(s);
            }
            scores.Sort();
            Console.WriteLine(scores.ElementAt(scores.Count / 2));
        }
    }
}