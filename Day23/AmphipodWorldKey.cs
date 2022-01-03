using System.Diagnostics.CodeAnalysis;

namespace Day23
{
    public record struct AmphipodWorldKey
    {
        public readonly int AKey;  
        public readonly int BKey;  
        public readonly int CKey;  
        public readonly int DKey;  

        public AmphipodWorldKey(int[] map)
        {
            int aMove = 0, bMove = 0, cMove = 0, dMove = 0;
            AKey = BKey = CKey = DKey = 0;
            for(int i = 0; i < map.Length; i++)
            {
                switch(map[i])
                {
                    case 1:
                        AKey |= (i << aMove);
                        aMove += 5;
                        break;
                    case 2:
                        BKey |= (i << bMove);
                        bMove += 5;
                        break;
                    case 3: 
                        CKey |= (i << cMove);
                        cMove += 5;
                        break;
                    case 4:
                        DKey |= (i << dMove);
                        dMove += 5;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}