using static Pacmen.Program;

namespace Pacman
{
    internal class Ghost
    {
        private readonly char _scin = '&';
        private Vector2Int _ghostPosition;
        private char[,] _map;
        private Random _rnd;


        public Ghost(int positionX, int positionY, char[,] map)
        {
            _ghostPosition = new Vector2Int(positionX, positionY);
            _map = map;
            oldCellSymbol = ' ';
            _rnd = new Random();
        }
        private char oldCellSymbol { get; set; }

        internal Vector2Int GetNextPositionInPathToTarget(Vector2Int target)
        {
            var _startPosition = _ghostPosition;
            var _openList = new Queue<Vector2Int>();
            var _closedList = new List<Vector2Int>();

            _openList.Enqueue(target);
            _closedList.Add(target);

            while (_openList.Count > 0)
            {
                var currentPosition = _openList.Dequeue();

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x != 0 && y != 0)
                            continue;

                        var newX = x + currentPosition.x;
                        var newY = y + currentPosition.y;
                        var researchedPoint = new Vector2Int(newX, newY);
                        var nextCell = _map[researchedPoint.x, researchedPoint.y];

                        if (researchedPoint.x > 60 || researchedPoint.y > 16 || nextCell == '#')
                            continue;

                        if (_closedList.Contains(researchedPoint))
                            continue;

                        if (researchedPoint.x == _startPosition.x && researchedPoint.y == _startPosition.y)
                            return currentPosition;

                        _openList.Enqueue(researchedPoint);
                        _closedList.Add(researchedPoint);
                    }
                }
            }

            return _startPosition;
        }

        internal Vector2Int GetRandomNextPosition()
        {
            var _startPosition = _ghostPosition;
            var _possiblePositionsList = new List<Vector2Int>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x != 0 && y != 0)
                        continue;
                    var newX = x + _ghostPosition.x;
                    var newY = y + _ghostPosition.y;
                    var researchedPoint = new Vector2Int(newX, newY);
                    var nextCell = _map[researchedPoint.x, researchedPoint.y];

                    if (researchedPoint.x > 60 || researchedPoint.y > 16 || nextCell == '#')
                        continue;

                    _possiblePositionsList.Add(researchedPoint);
                }
            }

            if (_possiblePositionsList.Count > 0)
            {
                var rand = _rnd.Next(0, _possiblePositionsList.Count);
                return _possiblePositionsList[rand];
            }
            else return _startPosition;
        }

        internal void Render(Vector2Int ghostPosition, ConsoleColor mapColor, ConsoleColor ghostColor)
        {
            Console.ForegroundColor = mapColor;
            Console.SetCursorPosition(_ghostPosition.x, _ghostPosition.y);
            Console.Write(oldCellSymbol);

            oldCellSymbol = _map[ghostPosition.x, ghostPosition.y];
            Console.ForegroundColor = ghostColor;
            Console.SetCursorPosition(ghostPosition.x, ghostPosition.y);
            Console.Write(_scin);

            _ghostPosition = ghostPosition;
        }
    }
}
