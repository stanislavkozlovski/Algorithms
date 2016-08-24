namespace AStarAlgorithm
{
    using System;

    public class AStarSearchPlaygound
    {

        //private static char[,] map =
        //{
        //    { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' },
        //    { '-', '-', '-', 'W', '*', '-', '-', '-', '-', '-', '-' },
        //    { '-', '-', '-', 'W', 'W', 'W', 'W', 'W', '-', '-', '-' },
        //    { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' },
        //    { '-', '-', '-', '-', '-', '-', '-', 'P', '-', '-', '-' },
        //    { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' }
        //};

        private static char[,] map =
        {
                    { '-', '-', '-', '-', '-' },
                    { '-', '-', '*', '-', '-' },
                    { '-', 'W', 'W', 'W', '-' },
                    { '-', '-', '-', '-', '-' },
                    { '-', '-', 'P', '-', '-' },
                    { '-', '-', '-', '-', '-' }
         };

        //private static char[,] map =
        //{
        //    { '-', '-', '-', '-', 'W', '-', '-', '-', 'W', '*', '-' },
        //    { '-', 'W', '-', '-', 'W', '-', '-', '-', 'W', '-', '-' },
        //    { 'P', '-', 'W', '-', 'W', '-', '-', '-', 'W', '-', '-' },
        //    { '-', 'W', '-', '-', 'W', 'W', 'W', '-', 'W', 'W', '-' },
        //    { '-', '-', '-', 'W', 'W', '-', '-', '-', '-', 'W', '-' },
        //    { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' }
        //};

        static void Main()
        {
            var playerCoords = FindObjectCoordinates('P');
            var destinationCoords = FindObjectCoordinates('*');

            var aStar = new AStar(map);
            var cells = aStar.FindShortestPath(playerCoords, destinationCoords);
            foreach (var cellInPath in cells)
            {
                var row = cellInPath[0];
                var col = cellInPath[1];
                map[row, col] = '@';
            }

            PrintMap();
        }

        static int[] FindObjectCoordinates(char obj)
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    if (map[row, col] == obj)
                    {
                        return new[] { row, col };
                    }
                }
            }

            throw new ArgumentException("Object not present on map");
        }

        static void PrintMap()
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    if (map[row, col] == '@')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }

                    Console.Write(map[row, col]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ");
                }

                Console.WriteLine();
            }
        }
    }
}
