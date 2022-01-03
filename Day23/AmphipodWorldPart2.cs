using System.Text;

namespace Day23
{
    /*
    Input format:
    #  0  1  2  3  4  5  6  7  8  9 10 #
            11    15    19    23
            12    16    20    24
            13    17    21    25
            14    18    22    26
    */
    public class AmphipodWorldPart2 : AmphipodWorld
    {
        private static readonly int[,] CostMatrix = new int[27,27]
        {
            {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 3, 4, 5, 6, 5, 6, 7, 8, 7, 8, 9, 10, 9, 10, 11, 12},
            {1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 4, 5, 6, 7, 6, 7, 8, 9, 8, 9, 10, 11},
            {2, 1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 3, 4, 5, 6, 5, 6, 7, 8, 7, 8, 9, 10},
            {3, 2, 1, 0, 1, 2, 3, 4, 5, 6, 7, 2, 3, 4, 5, 2, 3, 4, 5, 4, 5, 6, 7, 6, 7, 8, 9},
            {4, 3, 2, 1, 0, 1, 2, 3, 4, 5, 6, 3, 4, 5, 6, 1, 2, 3, 4, 3, 4, 5, 6, 5, 6, 7, 8},
            {5, 4, 3, 2, 1, 0, 1, 2, 3, 4, 5, 4, 5, 6, 7, 2, 3, 4, 5, 2, 3, 4, 5, 4, 5, 6, 7},
            {6, 5, 4, 3, 2, 1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 3, 4, 5, 6, 1, 2, 3, 4, 3, 4, 5, 6},
            {7, 6, 5, 4, 3, 2, 1, 0, 1, 2, 3, 6, 7, 8, 9, 4, 5, 6, 7, 2, 3, 4, 5, 2, 3, 4, 5},
            {8, 7, 6, 5, 4, 3, 2, 1, 0, 1, 2, 7, 8, 9, 10, 5, 6, 7, 8, 3, 4, 5, 6, 1, 2, 3, 4},
            {9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 1, 8, 9, 10, 11, 6, 7, 8, 9, 4, 5, 6, 7, 2, 3, 4, 5},
            {10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 9, 10, 11, 12, 7, 8, 9, 10, 5, 6, 7, 8, 3, 4, 5, 6},
            {3, 2, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 6, 7, 8, 9, 8, 9, 10, 11},
            {4, 3, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 0, 1, 2, 5, 6, 7, 8, 7, 8, 9, 10, 9, 10, 11, 12},
            {5, 4, 3, 4, 5, 6, 7, 8, 9, 10, 11, 2, 1, 0, 1, 6, 7, 8, 9, 8, 9, 10, 11, 10, 11, 12, 13},
            {6, 5, 4, 5, 6, 7, 8, 9, 10, 11, 12, 3, 2, 1, 0, 7, 8, 9, 10, 9, 10, 11, 12, 11, 12, 13, 14},
            {5, 4, 3, 2, 1, 2, 3, 4, 5, 6, 7, 3, 4, 5, 6, 0, 1, 2, 3, 4, 5, 6, 7, 6, 7, 8, 9},
            {6, 5, 4, 3, 2, 3, 4, 5, 6, 7, 8, 4, 5, 6, 7, 1, 0, 1, 2, 5, 6, 7, 8, 7, 8, 9, 10},
            {7, 6, 5, 4, 3, 4, 5, 6, 7, 8, 9, 5, 6, 7, 8, 2, 1, 0, 1, 6, 7, 8, 9, 8, 9, 10, 11},
            {8, 7, 6, 5, 4, 5, 6, 7, 8, 9, 10, 6, 7, 8, 9, 3, 2, 1, 0, 7, 8, 9, 10, 9, 10, 11, 12},
            {7, 6, 5, 4, 3, 2, 1, 2, 3, 4, 5, 6, 7, 8, 9, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7},
            {8, 7, 6, 5, 4, 3, 2, 3, 4, 5, 6, 7, 8, 9, 10, 5, 6, 7, 8, 1, 0, 1, 2, 5, 6, 7, 8},
            {9, 8, 7, 6, 5, 4, 3, 4, 5, 6, 7, 8, 9, 10, 11, 6, 7, 8, 9, 2, 1, 0, 1, 6, 7, 8, 9},
            {10, 9, 8, 7, 6, 5, 4, 5, 6, 7, 8, 9, 10, 11, 12, 7, 8, 9, 10, 3, 2, 1, 0, 7, 8, 9, 10},
            {9, 8, 7, 6, 5, 4, 3, 2, 1, 2, 3, 8, 9, 10, 11, 6, 7, 8, 9, 4, 5, 6, 7, 0, 1, 2, 3},
            {10, 9, 8, 7, 6, 5, 4, 3, 2, 3, 4, 9, 10, 11, 12, 7, 8, 9, 10, 5, 6, 7, 8, 1, 0, 1, 2},
            {11, 10, 9, 8, 7, 6, 5, 4, 3, 4, 5, 10, 11, 12, 13, 8, 9, 10, 11, 6, 7, 8, 9, 2, 1, 0, 1},
            {12, 11, 10, 9, 8, 7, 6, 5, 4, 5, 6, 11, 12, 13, 14, 9, 10, 11, 12, 7, 8, 9, 10, 3, 2, 1, 0}
        };

        public AmphipodWorldPart2(int[] map) : base(map, CostMatrix) {}

        public override bool VerifyEnd()
        {
            return Map[11] == 1 && Map[12] == 1 && Map[13] == 1 && Map[14] == 1 && 
                Map[15] == 2 && Map[16] == 2 && Map[17] == 2 && Map[18] == 2 &&
                Map[19] == 3 && Map[20] == 3 && Map[21] == 3 && Map[22] == 3 &&
                Map[23] == 4 && Map[24] == 4 && Map[25] == 4 && Map[26] == 4;
        }

        protected override IEnumerable<(int, int)> MovesFrom(int from)
        {
            int p = Map[from];
            int room1 = 7+4*p;
            int room2 = room1+1;
            int room3 = room1+2;
            int room4 = room1+3;
            int above = 2*p;
            bool ok;
            if(from <= 10)
            {
                if(Map[room1] == 0)    
                {
                    ok = true;
                    for(int f = Math.Min(above, from)+1; f < Math.Max(above, from); f++)
                        if(Map[f] != 0)
                        {
                            ok = false;
                            break;
                        }
                    if(ok && Map[room4] == 0)
                        yield return (from, room4);
                    else if(ok && Map[room4] == p && Map[room3] == 0)
                        yield return (from, room3);
                    else if(ok && Map[room4] == p && Map[room3] == p && Map[room2] == 0)
                        yield return (from, room2);
                    else if(ok && Map[room4] == p && Map[room3] == p && Map[room2] == p)
                        yield return (from, room1);
                }
            }
            else
            {
                int roomEntry = (from - 11) % 4;
                int roomNum = (from - 11) / 4 + 1;
                if(roomNum == p)
                {
                    ok = true;
                    for(int r = 0; from+r <= room4; r++)
                        if(Map[from + r] != roomNum)
                        {
                            ok = false;
                            break;
                        }
                    if(ok)
                        yield break; //all below are fine - no move
                }
                for(int r = roomEntry; r > 0; r--)
                    if(Map[from - r] != 0) yield break; //piece blocked - no move
                ok = false;
                int a = 2 * roomNum;
                if(Map[room1] == 0)
                {
                    ok = true;
                    for(int f = Math.Min(above, a)+1; f < Math.Max(above, a); f++)
                        if(Map[f] != 0)
                        {
                            ok = false;
                            break;
                        }
                }
                if(ok && Map[room4] == 0)
                    yield return (from, room4);
                else if(ok && Map[room4] == p && Map[room3] == 0)
                    yield return (from, room3);
                else if(ok && Map[room4] == p && Map[room3] == p && Map[room2] == 0)
                    yield return (from, room2);
                else if(ok && Map[room4] == p && Map[room3] == p && Map[room2] == p)
                    yield return (from, room1);
                for(int aa = a+1; aa <= 10; aa++)
                {
                    if(Map[aa] != 0)
                        break;
                    if(aa == 10 || aa % 2 == 1)
                        yield return (from, aa);
                }
                for(int aa = a-1; aa >= 0; aa--)
                {
                    if(Map[aa] != 0)
                        break;
                    if(aa == 0 || aa % 2 == 1)
                        yield return (from, aa);
                }
                yield break;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('#', 13);
            sb.AppendLine();
            sb.Append('#');
            for(int i = 0; i <= 10; i++)
                sb.Append(SymbolToChar(Map[i]));
            sb.AppendLine("#");
            for(int l = 0; l < (Map.Length-10)/4; l++)
            {
                sb.Append("###");
                for(int r = 0; r < 4; r++)
                {
                    sb.Append(SymbolToChar(Map[11+4*r+l]));
                    sb.Append('#');
                }
                sb.AppendLine("##");
            }
            return sb.ToString();
        }

        private static char SymbolToChar(int item)
        {
            return item switch 
                {
                    1 => 'A',
                    2 => 'B',
                    3 => 'C',
                    4 => 'D',
                    _ => '.'
                };
        }
    }
}