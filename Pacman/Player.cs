using static Pacmen.Program;

namespace Pacman
{
    internal class Player
    {
        private Vector2Int _currentPlayerPosition;
        private int _score;
        public Player(int positionX, int positionY)
        {
            _currentPlayerPosition = new Vector2Int(positionX, positionY);
            _score = 0;
        }
        public Vector2Int Position
        {
            get
            {
                return _currentPlayerPosition;
            }
        }
        public int Score
        {
            get
            {
                return _score;
            }
        }

        internal void ScoreRender(int score)
        {
            Console.SetCursorPosition(62, 0);
            Console.Write($"Score: {score}");
        }
        internal void PlayerRender(Vector2Int newPlayerPosition, ref Vector2Int currentPlayerPosition, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(currentPlayerPosition.x, currentPlayerPosition.y);
            Console.Write(' ');
            Console.SetCursorPosition(newPlayerPosition.x, newPlayerPosition.y);
            Console.Write('@');
            currentPlayerPosition = newPlayerPosition;
        }

        internal Vector2Int HandleInput(ConsoleKeyInfo pressedKey, Vector2Int currentPlayerPosition, char[,] map, ref int score)
        {
            Vector2Int direction = GetDirection(pressedKey);
            int nextPlayerPositionX = currentPlayerPosition.x + direction.x;
            int nextPlayerPositionY = currentPlayerPosition.y + direction.y;

            char nextCell = map[nextPlayerPositionX, nextPlayerPositionY];

            if (nextCell == '#')
            {
                nextPlayerPositionX = currentPlayerPosition.x;
                nextPlayerPositionY = currentPlayerPosition.y;
            }

            if (nextCell == '.') score++;

            return new Vector2Int(nextPlayerPositionX, nextPlayerPositionY);
        }

        internal Vector2Int GetDirection(ConsoleKeyInfo pressedKey)
        {
            int pacmanX = 0;
            int pacmanY = 0;

            if (pressedKey.Key == ConsoleKey.UpArrow) pacmanY -= 1;
            else if (pressedKey.Key == ConsoleKey.DownArrow) pacmanY += 1;
            else if (pressedKey.Key == ConsoleKey.LeftArrow) pacmanX -= 1;
            else if (pressedKey.Key == ConsoleKey.RightArrow) pacmanX += 1;

            return new Vector2Int(pacmanX, pacmanY);
        }
    }
}
