using System.IO;

namespace Day25
{
    public class Program
    {
        static void Main()
        {
            var input = File.ReadAllLines("input.txt").Select(l => l.ToArray()).ToArray();
            Part1(input);
        }

        public static void Part1(char[][] input)
        {
            int step = 0;
            var (h,w) = (input.Length, input[0].Length);
            var to = new char[h][];
            for(int i = 0; i < h; i++)
                to[i] = new char[w];
            while(Move(input, to, h, w))
            {
                char[][] temp = input;
                input = to;
                to = temp;
                step++;
            }
            Console.WriteLine(step+1);
        }

        private static bool Move(char[][] from, char[][] to, int height, int width)
        {
            bool zeroFree = false;
            bool moved = false;
            for(int i = 0; i < height;i++)
                for(int j = 0; j < width; j++)
                    to[i][j] = from[i][j];
            for(int i = 0; i < height; i++)
            {
                zeroFree = from[i][0] == '.';
                for(int j = 0; j < width; j++)
                {
                    if(from[i][j] == '>')
                    {
                        var jj = (j+1)%width;
                        if((jj == 0 && zeroFree) || (jj != 0 && from[i][jj] == '.'))
                        {
                            to[i][jj] = to[i][j];
                            to[i][j] = '.';
                            moved = true;
                            j++;
                        }
                    }
                }
            }
            from = to;
            for(int j = 0; j < width; j++)
            {
                zeroFree = from[0][j] == '.';
                for(int i = 0; i < height; i++)
                {
                    if(from[i][j] == 'v')
                    {
                        var ii = (i+1)%height;
                        if((ii == 0 && zeroFree) || (ii != 0 && from[ii][j] == '.'))
                        {
                            to[ii][j] = to[i][j];
                            to[i][j] = '.';
                            moved = true;
                            i++;
                        }
                    }
                }
            }
            return moved;
        }
    }
}