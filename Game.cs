using System;

namespace battleships_v2
{
    public class Start
    {        
        static void Main(string[] args)
        {
            Game game = new();
            game.Launch();
        }
    }

    public class Game
    {
        // variables related with ships
        public static int[] ships = { 5, 4, 4 }; // number of ships (and their size) to be placed on grid while creating new game, better not to make it too many!
        public int[] hitsCounter = new int[ships.Length];
        public int sunkCounter;

        // variables related with battlefield grid
        public int[,] grid = new int[10, 10]; // indicates if there is any ship places on field
        public int[,] gridStatus = new int[10, 10]; // indicicate how field looks in console (not bombarded, hit, miss)

        // messages
        public string newBattle = "New Battle has begun!";
        public string incorrectInput = "Incorrect input - formula for picking field should be: column [A - J], row [1 - 10] (without space), example: A1.";

        public string lastAction;

        // variables controlling game status
        public int gameWon, newGame;

        public void Launch()
        {
            while(true)
            {
                Console.Clear();

                if(sunkCounter == ships.Length) // winning game condition - ale ships destryoed
                {
                    DrawGrid();
                    Console.WriteLine(lastAction);
                    Console.WriteLine("ALL SHIPS SUNK! YOU WON!");
                    Console.WriteLine("Play again? (y/n)");

                    string command = Console.ReadLine();

                    if (command == "y") SetNewGame();
                    else if (command == "n") CloseGame();
                }
                else
                {
                    if (newGame == 0)
                    {
                        SetNewGame();
                        newGame = 1;
                    }

                    DrawGrid();

                    Console.WriteLine(lastAction);
                    Console.WriteLine("Other commands: new - for starting a new game; exit - for closing the app;");
                    Console.WriteLine("Select field to bombard (for example: A1):");

                    string command = Console.ReadLine();

                    if (command == "new") SetNewGame();
                    else if (command == "exit") CloseGame();

                    else
                    {
                        int x = -1;
                        int y = -1;

                        if (command.Length == 2) // checking for standard 2-character input, for example B2 (cases with '10' will be chcked in next step)
                        {

                            switch (command[1])
                            {
                                case '1':
                                    {
                                        x = 0;
                                    }
                                    break;

                                case '2':
                                    {
                                        x = 1;
                                    }
                                    break;

                                case '3':
                                    {
                                        x = 2;
                                    }
                                    break;

                                case '4':
                                    {
                                        x = 3;
                                    }
                                    break;

                                case '5':
                                    {
                                        x = 4;
                                    }
                                    break;

                                case '6':
                                    {
                                        x = 5;
                                    }
                                    break;

                                case '7':
                                    {
                                        x = 6;
                                    }
                                    break;

                                case '8':
                                    {
                                        x = 7;
                                    }
                                    break;

                                case '9':
                                    {
                                        x = 8;
                                    }
                                    break;
                            }

                            y = AssignColumn(command[0]);
                        }
                        else if (command.Length == 3)
                        {
                            if (command[1] == '1' && command[2] == '0') x = 9;
                            y = AssignColumn(command[0]);
                        }

                        if (x == -1 || y == -1) lastAction = incorrectInput;
                        else // correct input - check field
                        {
                            //gridStatus[x, y] = 1; // test if input is correctly marked on grid

                            if (gridStatus[x, y] == 0) // not bombarded yet
                            {
                                if (grid[x, y] == 0) // MISS
                                {
                                    lastAction = "Selected field: (" + command + ") - MISS!";
                                    gridStatus[x, y] = 2;
                                }
                                else if (grid[x, y] > 0) // HIT / SINK
                                {
                                    gridStatus[x, y] = 1;
                                    hitsCounter[grid[x, y] - 1]++;

                                    if (hitsCounter[grid[x, y] - 1] == ships[grid[x, y] - 1]) // SINK
                                    {
                                        string shipName = GiveShipName(ships[grid[x, y] - 1]);

                                        lastAction = "Selected field: (" + command + ") - SINK! (" + shipName + ")";
                                        sunkCounter++;                                     
                                    }
                                    else // HIT (only, without SINK)
                                    {
                                        lastAction = "Selected field: (" + command + ") - HIT!";
                                    }
                                }
                            }
                            else
                            {
                                switch (gridStatus[x, y]) // info about attempt of bombarding previously selected field
                                {
                                    case 1:
                                        {
                                            lastAction = "Selected field (" + command + ") was already bombared. It was HIT.";
                                        }
                                        break;

                                    case 2:
                                        {
                                            lastAction = "Selected field (" + command + ") was already bombared. It was MISS.";
                                        }
                                        break;
                                }
                            }
                        }

                    }
                }
            }
        }

        public string GiveShipName(int shipSize)
        {
            string name;

            switch (shipSize)
            {
                case 4:
                    {
                        name = "Destroyer";
                    }
                    break;

                case 5:
                    {
                        name = "Battleship";
                    }
                    break;

                default:
                    {
                        name = "Unknown";
                    }
                    break;
            }

            return name;
        }

        public int AssignColumn(char Letter)
        {
            int y = -1;

            switch (Letter)
            {
                case 'A':
                case 'a':
                    {
                        y = 0;
                    }
                    break;

                case 'B':
                case 'b':
                    {
                        y = 1;
                    }
                    break;

                case 'C':
                case 'c':
                    {
                        y = 2;
                    }
                    break;

                case 'D':
                case 'd':
                    {
                        y = 3;
                    }
                    break;

                case 'E':
                case 'e':
                    {
                        y = 4;
                    }
                    break;

                case 'F':
                case 'f':
                    {
                        y = 5;
                    }
                    break;

                case 'G':
                case 'g':
                    {
                        y = 6;
                    }
                    break;

                case 'H':
                case 'h':
                    {
                        y = 7;
                    }
                    break;

                case 'I':
                case 'i':
                    {
                        y = 8;
                    }
                    break;

                case 'J':
                case 'j':
                    {
                        y = 9;
                    }
                    break;
            }

            return y;
        }

        public void SetNewGame()
        {
            ResetGrid();
            sunkCounter = 0;

            for (int i = 0; i < ships.Length; i++)
            {
                hitsCounter[i] = 0;
                PlaceShipOnGrid(i + 1, ships[i]);
            }

            lastAction = newBattle;
        }

        public void PlaceShipOnGrid(int shipID, int shipSize)
        {
            int cond = 0; // condition checker if space for ship was found (possible when value changed to 1)

            while (cond == 0)
            {
                var rnd = new Random();

                int x = rnd.Next(11 - shipSize);
                int y = rnd.Next(11 - shipSize);
                int dir = rnd.Next(2);

                int fieldCheck = 0; // checking if all fields for being created ship are available


                for (int i = 0; i < shipSize; i++)
                {
                    switch (dir)
                    {
                        case 0: // horizontal
                            {
                                if (grid[x + i, y] > 0) fieldCheck = 1; // an invalid field for ship
                            }
                            break;

                        case 1: // vertical
                            {
                                if (grid[x, y + i] > 0) fieldCheck = 1; // an invalid field for ship
                            }
                            break;
                    }
                }
                if (fieldCheck == 0) // all fields are avaialable tu put there ship
                {
                    cond = 1;

                    for (int i = 0; i < shipSize; i++)
                    {
                        switch (dir)
                        {
                            case 0: // horizontal
                                {
                                    grid[x + i, y] = shipID; // assigning fields with ship ID
                                                             //gridStatus[x + i, y] = 1;
                                }
                                break;

                            case 1: // vertical
                                {
                                    grid[x, y + i] = shipID; // assigning fields with ship ID
                                                             //gridStatus[x, y + i] = 1;
                                }
                                break;
                        }
                    }
                }
            }
        }

        public void ResetGrid()
        {
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    grid[i, j] = 0;
                    gridStatus[i, j] = 0;
                }
            }
        }

        public void DrawGrid()
        {
            Console.WriteLine("     ----- BATTLESHIPS -----\n");
            Console.WriteLine("   1  2  3  4  5  6  7  8  9  10");

            for (int i = 0; i <= 9; i++)
            {
                string newGgridline = "";

                switch (i)
                {
                    case 0:
                        {
                            newGgridline = "A ";
                        }
                        break;
                    case 1:
                        {
                            newGgridline = "B ";
                        }
                        break;
                    case 2:
                        {
                            newGgridline = "C ";
                        }
                        break;
                    case 3:
                        {
                            newGgridline = "D ";
                        }
                        break;
                    case 4:
                        {
                            newGgridline = "E ";
                        }
                        break;
                    case 5:
                        {
                            newGgridline = "F ";
                        }
                        break;
                    case 6:
                        {
                            newGgridline = "G ";
                        }
                        break;
                    case 7:
                        {
                            newGgridline = "H ";
                        }
                        break;
                    case 8:
                        {
                            newGgridline = "I ";
                        }
                        break;
                    case 9:
                        {
                            newGgridline = "J ";
                        }
                        break;
                        /*
                    default:
                        {
                            newGgridline = "X ";
                        }
                        break;
                        */
                }

                for (int j = 0; j <= 9; j++)
                {
                    // swtich with "[-]" - unexplored (value 0), "[X]" - hit (value 1), "[0]" - miss (value 2)
                    switch (gridStatus[j, i])
                    {
                        case 0:
                            {
                                newGgridline += "[-]";
                            }
                            break;
                        case 1:
                            {
                                newGgridline += "[X]";
                            }
                            break;
                        case 2:
                            {
                                newGgridline += "[0]";
                            }
                            break;
                    }

                }

                Console.WriteLine(newGgridline);
            }

            Console.Write("\n");
        }

        public void CloseGame()
        {
            Environment.Exit(0);
        }
    }
}