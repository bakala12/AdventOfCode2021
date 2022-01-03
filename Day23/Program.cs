namespace Day23
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
            var map = ConstructMap(lines);
            var world = new AmphipodWorldPart1(map);
            var tt = new Dictionary<AmphipodWorldKey, int>();
            int best = int.MaxValue;
            Console.WriteLine(FindCheapest(world, tt, ref best, 0));
        }

        private static void Part2(string[] lines)
        {
            var map = ConstructMap2(lines);
            var world = new AmphipodWorldPart2(map);
            var tt = new Dictionary<AmphipodWorldKey, int>();
            int best = int.MaxValue;
            Console.WriteLine(FindCheapest(world, tt, ref best, 0));
        }

        private static int FindCheapest(AmphipodWorld world, Dictionary<AmphipodWorldKey, int> tt, ref int bestKnown, int current = 0)
        {
            if(world.VerifyEnd())
            {
                bestKnown = Math.Min(bestKnown, current);
                return 0;
            }
            if(current > bestKnown)
                return current;
            var key = world.GetKey();
            if(tt.TryGetValue(key, out int v))
                return v;
            int best = int.MaxValue;
            var moves = world.GenerateAllMoves().ToList();
            foreach(var (from, to, c) in moves)
            {
                world.DoMove(from, to);
                int cc = FindCheapest(world, tt, ref bestKnown, current+c);
                if(cc < int.MaxValue && cc + c < best)
                    best = cc + c;
                world.UndoMove(from, to);
            }
            tt[key] = best;
            return best;
        }   

        public static int[] CostByPiece = new int[] {0, 1, 10, 100, 1000};

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

        private static int[] ConstructMap2(string[] lines)
        {
            lines[0] = lines[2];
            lines[1] = "###D#C#B#A###";
            lines[2] = "###D#B#A#C###";
            int[] map = new int[27];
            for(int i = 0; i < 4; i++)
            {
                map[4*i+11] = lines[0][2*i+3] - 'A' + 1;
                map[4*i+12] = lines[1][2*i+3] - 'A' + 1;
                map[4*i+13] = lines[2][2*i+3] - 'A' + 1;
                map[4*i+14] = lines[3][2*i+3] - 'A' + 1;
            }
            return map;
        }
    }
}