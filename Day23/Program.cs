using System.Diagnostics;

namespace Day23
{
    public class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            var map = ConstructMap(lines);
            var sw = new Stopwatch();
            sw.Start();
            Part1(map);
            System.Console.WriteLine($"Part1: {sw.ElapsedMilliseconds}ms");
        }

        private static void Part1(int[] map)
        {
            System.Console.WriteLine(FindCheapest2(map));
        }

        private static int FindCheapest2(int[] map)
        {
            if(VerifyEnd(map))
                return 0;
            var key = GetKey(map);
            if(TT.TryGetValue(key, out int v))
                return v;
            int best = int.MaxValue;
            foreach(var m in GenerateAllMoves(map))
            {
                int c = m.Item3;
                map[m.Item2] = map[m.Item1];
                map[m.Item1] = 0;
                int cc = FindCheapest2(map);
                if(cc < int.MaxValue && cc + c < best)
                    best = cc + c;
                map[m.Item1] = map[m.Item2];
                map[m.Item2] = 0;
            }
            TT[key] = best;
            return best;
        }

        private static readonly Dictionary<ulong, int> TT = new Dictionary<ulong, int>();

        private static ulong GetKey(int[] map)
        {
            ulong k = 0UL;
            int m = 0;
            for(int i = 0; i < 19; i++)
            {
                if(i != 2 && i != 4 && i != 6 && i != 8)
                {
                    k |= ((ulong)map[i] << m);
                    m += 3;
                }
            }
            return k;
        }      

        /* Input format
        #############
        #0 1 2 3 4 5 6 7 8 9 10#
        ## # 11# 13# 15# 17# ##
           # 12# 14# 16# 18#
           #########
        */

        private static readonly int[,] Costs = new int[19,19]
        {
            {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 3, 4, 5, 6, 7, 8, 9, 10},
            {1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 6, 7, 8, 9},
            {2, 1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8},
            {3, 2, 1, 0, 1, 2, 3, 4, 5, 6, 7, 2, 3, 2, 3, 4, 5, 6, 7},
            {4, 3, 2, 1, 0, 1, 2, 3, 4, 5, 6, 3, 4, 1, 2, 3, 4, 5, 6},
            {5, 4, 3, 2, 1, 0, 1, 2, 3, 4, 5, 4, 5, 2, 3, 2, 3, 4, 5},
            {6, 5, 4, 3, 2, 1, 0, 1, 2, 3, 4, 5, 6, 3, 4, 1, 2, 3, 4},
            {7, 6, 5, 4, 3, 2, 1, 0, 1, 2, 3, 6, 7, 4, 5, 2, 3, 2, 3},
            {8, 7, 6, 5, 4, 3, 2, 1, 0, 1, 2, 7, 8, 5, 6, 3, 4, 1, 2},
            {9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 1, 8, 9, 6, 7, 4, 5, 2, 3},
            {10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 9, 10, 7, 8, 5, 6, 3, 4},
            {3, 2, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 4, 5, 6, 7, 8, 9},
            {4, 3, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 0, 5, 6, 7, 8, 9, 10},
            {5, 4, 3, 2, 1, 2, 3, 4, 5, 6, 7, 4, 5, 0, 1, 4, 5, 6, 7},
            {6, 5, 4, 3, 2, 3, 4, 5, 6, 7, 8, 5, 6, 1, 0, 5, 6, 7, 8},
            {7, 6, 5, 4, 3, 2, 1, 2, 3, 4, 5, 6, 7, 4, 5, 0, 1, 4, 5},
            {8, 7, 6, 4, 4, 3, 2, 3, 4, 5, 6, 7, 8, 5, 6, 1, 0, 5, 6},
            {9, 8, 7, 6, 5, 4, 3, 2, 1, 2, 3, 8, 9, 6, 7, 4, 5, 0, 1},
            {10, 9, 8, 7, 6, 5, 4, 3, 2, 3, 4, 9, 10, 7, 8, 5, 6, 1, 0}
        };

        private static int[] CostByPiece = new int[] {0, 1, 10, 100, 1000};

        private static int GetCost(int[] map, (int, int) m) => CostByPiece[map[m.Item1]]*Costs[m.Item1, m.Item2];

        private static bool VerifyEnd(int[] map)
        {
            return map[11] == 1 && map[12] == 1 && map[13] == 2 && map[14] == 2 &&
                map[15] == 3 && map[16] == 3 && map[17] == 4 && map[18] == 4;
        }

        private static IEnumerable<(int, int, int)> GenerateAllMoves(int[] map)
        {
            for(int i = 0; i < 19; i++)
            {
                if(map[i] != 0)
                {
                    foreach(var m in MovesFrom(map, i, map[i]))
                        yield return (m.Item1, m.Item2, GetCost(map, m));
                }
            }
        }

        private static IEnumerable<(int, int)> MovesFrom(int[] map, int from, int p)
        {
            int room1 = 10 + 2 * p-1;
            int room2 = 10 + 2 * p;
            int above = 2 * p;
            bool ok = false;
            if(from <= 10)
            {
                if(map[room1] == 0)    
                {
                    ok = true;
                    for(int f = Math.Min(above, from)+1; f < Math.Max(above, from); f++)
                        if(map[f] != 0)
                        {
                            ok = false;
                            break;
                        }
                    if(ok && map[room2] == 0 && map[room1] == 0)
                        yield return (from, room2);
                    if(ok && map[room2] == p && map[room1] == 0)
                        yield return (from, room1);
                }
            }
            else if(from != room2)
            {
                if(from == room1 && map[room1] == map[room2])
                    yield break;
                int a = 0;
                if(from % 2 == 0 && map[from - 1] == 0)
                {
                    a = from - 10;
                }
                else if(from % 2 == 1)
                {
                    a = from - 9;
                }
                else
                    yield break;
                if(map[room1] == 0)
                {
                    ok = true;
                    for(int f = Math.Min(a, from)+1; f < Math.Max(a, from); f++)
                        if(map[f] != 0)
                        {
                            ok = false;
                            break;
                        }
                }
                if(ok && map[room2] == 0 && map[room1] == 0)
                    yield return (from, room2);
                if(ok && map[room2] == p && map[room1] == 0)
                    yield return (from, room1);
                if(ok)
                    yield break;
                for(int aa = a+1; aa <= 10; aa++)
                {
                    if(map[aa] != 0)
                        break;
                    if(aa == 10 || aa % 2 == 1)
                        yield return (from, aa);
                    if(aa == above && map[room1] == 0)
                    {
                        if(map[room2] == p)
                            yield return (from, room1);
                        if(map[room2] == 0)
                            yield return (from, room2);
                    }
                }
                for(int aa = a-1; aa >= 0; aa--)
                {
                    if(map[aa] != 0)
                        break;
                    if(aa == 0 || aa % 2 == 1)
                        yield return (from, aa);
                    if(aa == above && map[room1] == 0)
                    {
                        if(map[room2] == p)
                            yield return (from, room1);
                        if(map[room2] == 0)
                            yield return (from, room2);
                    }
                }
                yield break;
            }
        }

        private static int[] ConstructMap(string[] lines)
        {
            int[] map = new int[19];
            for(int i = 11; i <= 17; i+=2)
            {
                map[i] = lines[2][i-8] - 'A' + 1;
                map[i+1] = lines[3][i-8] - 'A' + 1;
            }
            return map;
        }
    }
}