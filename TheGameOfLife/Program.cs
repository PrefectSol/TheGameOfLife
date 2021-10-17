using System;
using System.Threading;

namespace TheGameOfLife
{
    class Program
    {
        private static int columns;
        private static int rows;
        private static int sizeTable = columns = rows = 50;
        static bool[,] firstGeneration;
        static bool theLastCellisLife = true;
        static void Main(string[] args)
        {
            int consoleWidth = rows;
            int consoleHeight = columns;
            bool startGame = true;
            firstGeneration = new bool[rows, columns];

#pragma warning disable CA1416 // Проверка совместимости платформы
            Console.SetWindowSize(width: consoleWidth,
                                  height: consoleHeight);

            Random rnd = new Random();

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    firstGeneration[x, y] = rnd.Next(0, 2) == 0;
                    if (firstGeneration[x, y] == true)
                        Console.Write(1);
                    else if (firstGeneration[x, y] == false)
                        Console.Write(0);
                }
                Console.WriteLine();
            }
            while (startGame)
            {
                NextGeneration();
                if (!theLastCellisLife)
                    startGame = false;
            }
        }
        static void NextGeneration()
        {
            bool[,] nextGeneration;
            nextGeneration = new bool[columns, rows];
            int countHasLife = rows * columns;

            Thread.Sleep(500);
            Console.Clear();

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var countOfNeighbour = NumberOfNeighbour(x, y);
                    var hasLife = firstGeneration[x, y];

                    if (hasLife == false)
                        countHasLife--;

                    if (!hasLife && countOfNeighbour == 3)
                    {
                        nextGeneration[x, y] = true;
                        Console.Write(1);
                    }
                    else if (hasLife && (countOfNeighbour < 2 || countOfNeighbour > 3))
                    {
                        nextGeneration[x, y] = false;
                        Console.Write(0);
                    }
                    else
                    {
                        nextGeneration[x, y] = firstGeneration[x, y];
                        Console.Write("-");
                    }
                }
                Console.WriteLine();
            }
            firstGeneration = nextGeneration;

            if (countHasLife == 0)
                theLastCellisLife = false;
        }
        static int NumberOfNeighbour(int x, int y)
        {
            int count = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var col = (x + i + columns) % columns;
                    var row = (y + j + rows) % rows;

                    var isLifeCheking = col == x && row == y;
                    var hasLife = firstGeneration[col, row];

                    if (hasLife && !isLifeCheking)
                        count++;
                }
            }
            return count;
        }
    }
}
