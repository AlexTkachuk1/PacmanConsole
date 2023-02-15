using static Pacmen.Program;

namespace Pacman
{
    internal class GameStateController
    {
        private Vector2Int _escapePosition;
        private char _escapeScin;
        public GameStateController(int positionX, int positionY)
        {
            _escapePosition = new Vector2Int(positionX, positionY);
            _escapeScin = '0';
        }

        internal void RenderEscapePoint(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(_escapePosition.x, _escapePosition.y);
            Console.Write(_escapeScin);
        }

        internal void GetGameState(Vector2Int playerPosition, List<Vector2Int> ghostsPositions, ref bool gameIsRuning)
        {
            if (playerPosition.Equals(_escapePosition))
            {
                WrightStatusMessage("You win the Game!!!");
                gameIsRuning = false;
            }
            else if (ghostsPositions.Contains(playerPosition))
            {
                WrightStatusMessage("Game Over");
                gameIsRuning = false;
            }
        }

        private void WrightStatusMessage(string statusMessage)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(statusMessage);
        }
    }
}
