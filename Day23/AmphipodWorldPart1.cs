namespace Day23
{
    /* Input format
        #############
        #0 1 2 3 4 5 6 7 8 9 10#
        ## # 11# 13# 15# 17# ##
           # 12# 14# 16# 18#
           #########
    */
    public class AmphipodWorldPart1 : AmphipodWorld
    {
        private static readonly int[,] CostsMatrix = new int[19,19]
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

        public AmphipodWorldPart1(int[] map) : base(map, CostsMatrix) {}

        public override bool VerifyEnd()
        {
            return Map[11] == 1 && Map[12] == 1 && Map[13] == 2 && Map[14] == 2 &&
                Map[15] == 3 && Map[16] == 3 && Map[17] == 4 && Map[18] == 4;
        }

        protected override IEnumerable<(int, int)> MovesFrom(int from)
        {
            int p = Map[from];
            int room1 = 10 + 2 * p-1;
            int room2 = 10 + 2 * p;
            int above = 2 * p;
            bool ok = false;
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
                    if(ok && Map[room2] == 0)
                        yield return (from, room2);
                    if(ok && Map[room2] == p)
                        yield return (from, room1);
                }
            }
            else if(from != room2)
            {
                if(from == room1 && Map[room1] == Map[room2])
                    yield break;
                int a = 0;
                if(from % 2 == 0 && Map[from - 1] == 0)
                    a = from - 10;
                else if(from % 2 == 1)
                    a = from - 9;
                else
                    yield break;
                if(Map[room1] == 0)
                {
                    ok = true;
                    for(int f = Math.Min(a, from)+1; f < Math.Max(a, from); f++)
                        if(Map[f] != 0)
                        {
                            ok = false;
                            break;
                        }
                }
                if(ok && Map[room2] == 0 && Map[room1] == 0)
                    yield return (from, room2);
                if(ok && Map[room2] == p && Map[room1] == 0)
                    yield return (from, room1);
                if(ok)
                    yield break;
                for(int aa = a+1; aa <= 10; aa++)
                {
                    if(Map[aa] != 0)
                        break;
                    if(aa == 10 || aa % 2 == 1)
                        yield return (from, aa);
                    if(aa == above && Map[room1] == 0)
                    {
                        if(Map[room2] == p)
                            yield return (from, room1);
                        if(Map[room2] == 0)
                            yield return (from, room2);
                    }
                }
                for(int aa = a-1; aa >= 0; aa--)
                {
                    if(Map[aa] != 0)
                        break;
                    if(aa == 0 || aa % 2 == 1)
                        yield return (from, aa);
                    if(aa == above && Map[room1] == 0)
                    {
                        if(Map[room2] == p)
                            yield return (from, room1);
                        if(Map[room2] == 0)
                            yield return (from, room2);
                    }
                }
                yield break;
            }           
        }
    }
}