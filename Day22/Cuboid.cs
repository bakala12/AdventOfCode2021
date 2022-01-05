namespace Day22
{
    public readonly record struct Cuboid
    {
        public readonly bool On;
        public readonly (int,int) RangeX;
        public readonly (int,int) RangeY;
        public readonly (int,int) RangeZ;
        
        public Cuboid(bool on, (int,int) rangeX, (int,int) rangeY, (int,int) rangeZ)
        {
            On = on;
            RangeX = rangeX;
            RangeY = rangeY;
            RangeZ = rangeZ;            
        }

        public static Cuboid ParseLine(string line)
        {
            var s = line.Split(" ");
            bool on = s[0] == "on";
            s = s[1].Split(",").Select(ss => ss.Substring(2)).ToArray();
            var x = s[0].Split("..").Select(int.Parse).ToArray();
            var y = s[1].Split("..").Select(int.Parse).ToArray();
            var z = s[2].Split("..").Select(int.Parse).ToArray();
            var rangeX = (x[0], x[1]);
            var rangeY = (y[0], y[1]);
            var rangeZ = (z[0], z[1]);
            return new Cuboid(on, rangeX, rangeY, rangeZ);
        }

        public long Size => (long)(RangeX.Item2 - RangeX.Item1 + 1) * (long)(RangeY.Item2 - RangeY.Item1 + 1) * (long)(RangeZ.Item2 - RangeZ.Item1 + 1);
    }
}