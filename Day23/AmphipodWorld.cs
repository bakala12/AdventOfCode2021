namespace Day23
{
    public abstract class AmphipodWorld
    {
        protected readonly int[] Map;
        protected readonly int[,] Costs;

        protected AmphipodWorld(int[] map, int[,] costs)
        {
            Map = map;
            Costs = costs;
        }

        public void DoMove(int from, int to)
        {
            Map[to] = Map[from];
            Map[from] = 0;
        }

        public void UndoMove(int from, int to)
        {
            Map[from] = Map[to];
            Map[to] = 0;
        }

        public IEnumerable<(int,int,int)> GenerateAllMoves()
        {
            for(int i = 0; i < Map.Length; i++)
            {
                if(Map[i] != 0)
                {
                    foreach(var m in MovesFrom(i))
                        yield return (m.Item1, m.Item2, Program.CostByPiece[Map[m.Item1]]*Costs[m.Item1, m.Item2]);
                }
            }
        }

        public AmphipodWorldKey GetKey()
        {
            return new AmphipodWorldKey(Map);
        } 

        public abstract bool VerifyEnd();
    
        protected abstract IEnumerable<(int, int)> MovesFrom(int from);
    }
}