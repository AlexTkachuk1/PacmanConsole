namespace Pacman
{
    internal class MapRepository
    {
        internal void RenderMap(char[,] map, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            for (int y = 0; y < map.GetLength(1); y++)
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                    if (x == map.GetLength(0) - 1) Console.Write('\n');
                }
        }

        internal char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);

            char[,] map = new char[GetMaxLengthOfLine(file), file.Length];

            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = file[y][x];

            return map;
        }

        private int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;
            foreach (string line in lines)
                if (line.Length > maxLength) maxLength = line.Length;
            return maxLength;
        }
    }
}
