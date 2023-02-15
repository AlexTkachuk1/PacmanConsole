using Pacman;

namespace Pacmen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var gameIsRuning = true;
            Console.CursorVisible = false;
            GameStateController stateController = new GameStateController(59, 15);
            MapRepository mapRepository = new MapRepository();
            char[,] map = mapRepository.ReadMap("map.txt");
            mapRepository.RenderMap(map, ConsoleColor.Blue);
            stateController.RenderEscapePoint(ConsoleColor.Green);

            Ghost redGhost = new Ghost(59, 13, map);
            Ghost greenGhost = new Ghost(1, 15, map);
            Ghost blueGhost = new Ghost(29, 6, map);
            List<Vector2Int> ghostsPositions = new List<Vector2Int>();

            Player player = new Player(1, 1);
            var currentPlayerPosition = player.Position;
            var score = player.Score;

            ConsoleKeyInfo pressedKey = default;

            Task.Run(() =>
            {
                while (true)
                {
                    pressedKey = Console.ReadKey();
                }
            });

            while (gameIsRuning)
            {
                ghostsPositions = new List<Vector2Int>();
                Vector2Int newPlayerPosition = player.HandleInput(pressedKey, currentPlayerPosition, map, ref score);

                Vector2Int newRedGhostPosition = redGhost.GetNextPositionInPathToTarget(currentPlayerPosition);
                redGhost.Render(newRedGhostPosition, ConsoleColor.Blue, ConsoleColor.Red);
                ghostsPositions.Add(newRedGhostPosition);

                Vector2Int newGreenGhostPosition = greenGhost.GetNextPositionInPathToTarget(currentPlayerPosition);
                greenGhost.Render(newGreenGhostPosition, ConsoleColor.Blue, ConsoleColor.Green);
                ghostsPositions.Add(newGreenGhostPosition);

                Vector2Int newBlueGhostPosition = blueGhost.GetRandomNextPosition();
                blueGhost.Render(newBlueGhostPosition, ConsoleColor.Blue, ConsoleColor.Blue);
                ghostsPositions.Add(newBlueGhostPosition);

                player.PlayerRender(newPlayerPosition, ref currentPlayerPosition, ConsoleColor.Yellow);
                player.ScoreRender(score);
                stateController.GetGameState(newPlayerPosition, ghostsPositions, ref gameIsRuning);
                Thread.Sleep(200);
            }
        }

        internal struct Vector2Int
        {
            private int _x;
            private int _y;

            public Vector2Int(int x, int y)
            {
                _x = x;
                _y = y;
            }
            public int x
            {
                get
                {
                    return _x;
                }
            }
            public int y
            {
                get
                {
                    return _y;
                }
            }
        };
    }
}