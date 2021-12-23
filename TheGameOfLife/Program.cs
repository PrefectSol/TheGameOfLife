namespace TheGameOfLife
{
    using System;
    using System.Threading;

    /// <summary>
    /// Entry point.
    /// </summary>
    internal class Program
    {
        private static readonly int Rows = 50;
        private static readonly int Columns = 120;
        private static readonly int GenerationUpdate = 500; // ms
        private static bool startGame = true;
        private static int[,] theFirstGeneration = new int[Rows, Columns];
        private static int deadCells;

        public static void Main()
        {
            Random rnd = new ();

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    theFirstGeneration[x, y] = rnd.Next(0, 2);
                    Console.Write(theFirstGeneration[x, y]);
                }

                Console.WriteLine();
            }

            while (startGame)
            {
                TheNextGeneration();
                if (deadCells == Rows * Columns)
                {
                    startGame = false;
                }
            }
        }

        private static void TheNextGeneration()
        {
            Thread.Sleep(GenerationUpdate);
            Console.Clear();

            int[,] theNextGeneration = new int[Rows, Columns];
            deadCells = 0;

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    var countOfNeighbors = NumberOfNeighbors(x, y);
                    var aLivingCell = theFirstGeneration[x, y];

                    if (aLivingCell == 0)
                    {
                        deadCells++;
                    }

                    if (aLivingCell == 1 && countOfNeighbors == 3)
                    {
                        theNextGeneration[x, y] = 1;
                        Console.Write(1);
                    }
                    else if ((aLivingCell == 1 && countOfNeighbors == 2) || countOfNeighbors == 3)
                    {
                        theNextGeneration[x, y] = 1;
                        Console.Write(1);
                    }
                    else if ((aLivingCell == 1 && countOfNeighbors < 2) || countOfNeighbors > 3)
                    {
                        theNextGeneration[x, y] = 0;
                        Console.Write(0);
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }

                Console.WriteLine();
            }

            if (theFirstGeneration == theNextGeneration)
            {
                startGame = false;
            }

            theFirstGeneration = theNextGeneration;
        }

        private static int NumberOfNeighbors(int x, int y)
        {
            int[,] standartCells = new int[3, 3];
            int[,] horizontalBorderCells = new int[2, 3];
            int[,] verticalBorderCells = new int[3, 2];
            int[,] angularCells = new int[2, 2];

            int centreCell = theFirstGeneration[x, y];

            int countForStandart = 9;
            int countForHorizontalBorderCells = 6;
            int countForVerticalBorderCells = 6;
            int countForAngular = 4;

            if (centreCell == 1)
            {
                countForStandart--;
                countForHorizontalBorderCells--;
                countForVerticalBorderCells--;
                countForAngular--;
            }

            if (x == 0 && y == 0)
            {
                for (int x1 = -1; x1 < 1; x1++)
                {
                    for (int y1 = -1; y1 < 1; y1++)
                    {
                        angularCells[x1 + 1, y1 + 1] = theFirstGeneration[x - x1, y - y1];
                    }
                }

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (angularCells[i, j] == 0)
                        {
                            countForAngular--;
                        }
                    }
                }

                return countForAngular;
            }
            else if (x == Rows - 1 && y == Columns - 1)
            {
                for (int x1 = -1; x1 < 1; x1++)
                {
                    for (int y1 = -1; y1 < 1; y1++)
                    {
                        angularCells[x1 + 1, y1 + 1] = theFirstGeneration[x + x1, y + y1];
                    }
                }

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (angularCells[i, j] == 0)
                        {
                            countForAngular--;
                        }
                    }
                }

                return countForAngular;
            }
            else if (x == Rows - 1 && y == 0)
            {
                angularCells[0, 0] = theFirstGeneration[x - 1, y];
                angularCells[0, 1] = theFirstGeneration[x, y];
                angularCells[1, 0] = theFirstGeneration[x - 1, y + 1];
                angularCells[1, 1] = theFirstGeneration[x, y + 1];

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (angularCells[i, j] == 0)
                        {
                            countForAngular--;
                        }
                    }
                }

                return countForAngular;
            }
            else if (x == 0 && y == Columns - 1)
            {
                angularCells[0, 0] = theFirstGeneration[x, y - 1];
                angularCells[0, 1] = theFirstGeneration[x, y];
                angularCells[1, 0] = theFirstGeneration[x + 1, y - 1];
                angularCells[1, 1] = theFirstGeneration[x + 1, y];

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (angularCells[i, j] == 0)
                        {
                            countForAngular--;
                        }
                    }
                }

                return countForAngular;
            }
            else if (x == 0 && y != 0 && y != Columns - 1)
            {
                horizontalBorderCells[0, 0] = theFirstGeneration[x, y - 1];
                horizontalBorderCells[0, 1] = theFirstGeneration[x, y];
                horizontalBorderCells[0, 2] = theFirstGeneration[x, y + 1];
                horizontalBorderCells[1, 0] = theFirstGeneration[x + 1, y - 1];
                horizontalBorderCells[1, 1] = theFirstGeneration[x + 1, y];
                horizontalBorderCells[1, 2] = theFirstGeneration[x + 1, y + 1];

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (horizontalBorderCells[i, j] == 0)
                        {
                            countForHorizontalBorderCells--;
                        }
                    }
                }

                return countForHorizontalBorderCells;
            }
            else if (x == 4 && y != 0 && y != Columns - 1)
            {
                horizontalBorderCells[0, 0] = theFirstGeneration[x - 1, y - 1];
                horizontalBorderCells[0, 1] = theFirstGeneration[x - 1, y];
                horizontalBorderCells[0, 2] = theFirstGeneration[x - 1, y + 1];
                horizontalBorderCells[1, 0] = theFirstGeneration[x, y - 1];
                horizontalBorderCells[1, 1] = theFirstGeneration[x, y];
                horizontalBorderCells[1, 2] = theFirstGeneration[x, y + 1];

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (horizontalBorderCells[i, j] == 0)
                        {
                            countForHorizontalBorderCells--;
                        }
                    }
                }

                return countForHorizontalBorderCells;
            }
            else if (x != 0 && x != Rows - 1 && y == 0)
            {
                verticalBorderCells[0, 0] = theFirstGeneration[x - 1, y];
                verticalBorderCells[0, 1] = theFirstGeneration[x - 1, y + 1];
                verticalBorderCells[1, 0] = theFirstGeneration[x, y];
                verticalBorderCells[1, 1] = theFirstGeneration[x, y + 1];
                verticalBorderCells[2, 0] = theFirstGeneration[x + 1, y];
                verticalBorderCells[2, 1] = theFirstGeneration[x + 1, y + 1];

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (verticalBorderCells[i, j] == 0)
                        {
                            countForVerticalBorderCells--;
                        }
                    }
                }

                return countForVerticalBorderCells;
            }
            else if (x != 0 && x != Rows - 1 && y == 4)
            {
                verticalBorderCells[0, 0] = theFirstGeneration[x - 1, y - 1];
                verticalBorderCells[0, 1] = theFirstGeneration[x - 1, y];
                verticalBorderCells[1, 0] = theFirstGeneration[x, y - 1];
                verticalBorderCells[1, 1] = theFirstGeneration[x, y];
                verticalBorderCells[2, 0] = theFirstGeneration[x + 1, y - 1];
                verticalBorderCells[2, 1] = theFirstGeneration[x + 1, y];

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (verticalBorderCells[i, j] == 0)
                        {
                            countForVerticalBorderCells--;
                        }
                    }
                }

                return countForVerticalBorderCells;
            }
            else if ((x > 0 && y != 0) && (x != Rows - 1 && y != Columns - 1))
            {
                for (int x1 = -1; x1 < 2; x1++)
                {
                    for (int y1 = -1; y1 < 2; y1++)
                    {
                        standartCells[x1 + 1, y1 + 1] = theFirstGeneration[x + x1, y + y1];
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (standartCells[i, j] == 0)
                        {
                            countForStandart--;
                        }
                    }
                }

                return countForStandart;
            }

            return 0;
        }
    }
}