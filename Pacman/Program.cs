namespace Pacmen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            char[,] map = ReadMap("map.txt");
            PlayerTransform2D currentPlayerPosition = new PlayerTransform2D(1, 1);
            int score = 0;
            ConsoleKeyInfo pressedKey = default;
            RenderMap(map, ConsoleColor.Blue);

            Task.Run(() =>
            {
                while (true)
                {
                    pressedKey = Console.ReadKey();
                }
            });

            while (true)
            {
                PlayerTransform2D newPosition = HandleInput(pressedKey, currentPlayerPosition, map, ref score);
                PlayerRender(newPosition, ref currentPlayerPosition, ConsoleColor.Yellow);
                ScoreRender(score);
                Thread.Sleep(200);
            }
        }

        internal struct PlayerTransform2D
        {
            internal int x;
            internal int y;

            public PlayerTransform2D(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        };

        private static void ScoreRender(int score)
        {
            Console.SetCursorPosition(62, 0);
            Console.Write($"Score: {score}");
        }
        private static void PlayerRender(PlayerTransform2D newPlayerPosition, ref PlayerTransform2D currentPlayerPosition, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(currentPlayerPosition.x, currentPlayerPosition.y);
            Console.Write(' ');
            Console.SetCursorPosition(newPlayerPosition.x, newPlayerPosition.y);
            Console.Write('@');
            currentPlayerPosition = newPlayerPosition;
        }

        private static PlayerTransform2D HandleInput(ConsoleKeyInfo pressedKey, PlayerTransform2D currentPlayerPosition, char[,] map, ref int score)
        {
            PlayerTransform2D direction = GetDirection(pressedKey);
            int nextPlayerPositionX = currentPlayerPosition.x + direction.x;
            int nextPlayerPositionY = currentPlayerPosition.y + direction.y;

            char nextCell = map[nextPlayerPositionX, nextPlayerPositionY];

            if (nextCell == '#')
            {
                nextPlayerPositionX = currentPlayerPosition.x;
                nextPlayerPositionY = currentPlayerPosition.y;
            }

            if (nextCell == '.') score++;

            return new PlayerTransform2D(nextPlayerPositionX, nextPlayerPositionY);
        }

        private static PlayerTransform2D GetDirection(ConsoleKeyInfo pressedKey)
        {
            int pacmanX = 0;
            int pacmanY = 0;

            if (pressedKey.Key == ConsoleKey.UpArrow) pacmanY -= 1;
            else if (pressedKey.Key == ConsoleKey.DownArrow) pacmanY += 1;
            else if (pressedKey.Key == ConsoleKey.LeftArrow) pacmanX -= 1;
            else if (pressedKey.Key == ConsoleKey.RightArrow) pacmanX += 1;

            return new PlayerTransform2D(pacmanX, pacmanY);
        }
        private static void RenderMap(char[,] map, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            for (int y = 0; y < map.GetLength(1); y++)
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                    if (x == map.GetLength(0) - 1) Console.Write('\n');
                }
        }

        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);

            char[,] map = new char[GetMaxLengthOfLine(file), file.Length];

            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = file[y][x];

            return map;
        }

        private static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;
            foreach (string line in lines)
                if (line.Length > maxLength) maxLength = line.Length;
            return maxLength;
        }
    }
}