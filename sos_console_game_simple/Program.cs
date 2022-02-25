using System;

namespace sos_console_game_simple
{
   

    //This game was supposed to be xox at first but it is clear that xox is not played like that so it has converted sos game after
        class Program
        {
            static void Main(string[] args)
            {
                string control = "";
                do
                {
                    bool jobFinished = false;
                    bool fullOr = false;

                    const int row = 3;
                    const int column = 3;
                    int i = 0;
                    char location;
                    char playerCharacter = 's';
                    int counter = 1;
                    int xocounter = 0;
                    string[,] user = new string[2, 2];
                    char[,] table = new char[row, column];
                    char xo = 's';

                    fillTable(table, row, column);//fill for the start;
                    Console.Write("Enter the name of player1 ");
                    user[0, 0] = Console.ReadLine();
                    Console.Write("Enter the name of player2 ");
                    user[1, 0] = Console.ReadLine();
                    //assing s and o character to player when user entered a number from keyboard the character they has will be replaced instead of it
                    user[0, 1] = "s";
                    user[1, 1] = "o";


                    do
                    {
                        jobFinished = generalControl(table, row, column);
                        if (jobFinished || counter == 10)
                        {

                            break;
                        }
                        Console.Write($"Please enter the number you wanna play for {user[i, 0]} that represents \"({user[i, 1]})\" : ");

                        location = Console.ReadKey().KeyChar;
                        Console.WriteLine("\n");

                        if (isClear(table, row, column, location))//if it is true it means the box is clear
                        {
                            if (location >= '1' && location <= '9')//if location entered correctly
                            {

                                if (xocounter > 0)
                                {
                                    if (i == 0)
                                    {
                                        i = 1;
                                        playerCharacter = 'o';
                                        user[i, 1] = playerCharacter.ToString();

                                    }
                                    else
                                    {
                                        i = 0;
                                        playerCharacter = 's';
                                        user[i, 1] = playerCharacter.ToString();
                                    }
                                }
                                else
                                {
                                    playerCharacter = 's';
                                    i = 1;
                                }
                                xocounter++;

                                refreshTable(table, row, column, location, xo);
                                Console.Clear();
                                printTable(table, row, column);

                                counter++;
                                if (counter % 2 == 0)
                                    xo = 'o';
                                else
                                    xo = 's';



                            }
                            else
                            {

                                Console.WriteLine("you cannot make this move please try again");

                                continue;
                            }


                        }
                        else
                        {

                            Console.WriteLine("This move is not possible try again");
                            continue;
                        }


                    } while (!jobFinished && counter <= 10);
                    fullOr = isFull(table, row, column);

                    if (fullOr)
                    {
                        if (i == 0)
                            i = 1;
                        else
                            i = 0;
                        if (jobFinished)
                            Console.WriteLine($"Player {user[i, 0]} \" ({user[i, 1]}) \"won");
                        else
                            Console.WriteLine("Draw");
                    }
                    Console.WriteLine("Enter any key to play again, 'E' to exit");
                    control = Console.ReadLine();
                    Console.Clear();
                } while (control != "E");
            }


            static void fillTable(char[,] arr, int row, int column)
            {
                char counter = '0';
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        arr[i, j] = ++counter;
                        Console.Write($"{arr[i, j]} | ");
                    }

                    Console.Write("\n__|___|___|\n");
                }

            }
            static void refreshTable(char[,] arr, int row, int column, char adress, char which)
            {
                char temp;
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        if (arr[i, j] == adress)//swap arr[i,j] to which character sended from main ('x'or 'o')
                        {
                            temp = which;
                            arr[i, j] = temp;
                        }
                    }
                }
            }
            static void printTable(char[,] arr, int row, int column)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        Console.Write($"{arr[i, j]} | ");
                    }
                    Console.Write("\n__|___|___|\n");
                }
            }
            static bool isClear(char[,] arr, int row, int column, char specialLoc)//special location means just to control that locations if is empty or not
            {

                bool isTrue = false;
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        if (arr[i, j] != 's' || arr[i, j] != 'o')
                        {
                            if (arr[i, j] == specialLoc)
                            {//if any index of 2d array doesnot have any specialLoc e.g 5 sended as a parameter
                             //if that index of array still has 5 in it ,it means box is empty if it has x or o box is empty
                                isTrue = true;//true indicates that box is not empty
                                break;
                            }

                        }


                    }
                }

                return isTrue;
            }

            static bool isFull(char[,] arr, int row, int column)
            {
                bool isTrue = false;
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        if (arr[i, j] == 's' || arr[i, j] == 'o')
                        {
                            isTrue = true;//if all indexes of array has neither x or o it means all boxes are not filled yet
                        }
                    }
                }
                return isTrue;
            }
            static bool generalControl(char[,] arr, int row, int column)
            {
                bool isTrue = false;//this section is horizontal

                if ('s' == arr[0, 0] && arr[0, 0] == arr[2, 0] && arr[1, 0] == 'o')
                    isTrue = true;
                else if ('s' == arr[0, 1] && arr[0, 1] == arr[2, 1] && arr[1, 1] == 'o')
                    isTrue = true;
                else if ('s' == arr[0, 2] && arr[0, 2] == arr[2, 2] && arr[1, 2] == 'o')
                    isTrue = true;

                //this section is vertical
                else if ('s' == arr[0, 0] && arr[0, 0] == arr[0, 2] && arr[0, 1] == 'o')
                    isTrue = true;
                else if ('s' == arr[1, 0] && arr[1, 0] == arr[1, 2] && arr[1, 1] == 'o')
                    isTrue = true;
                else if ('s' == arr[2, 0] && arr[2, 0] == arr[2, 2] && arr[2, 1] == 'o')
                    isTrue = true;



                //this section is cross/diogonal

                else if ('s' == arr[0, 0] && arr[0, 0] == arr[2, 2] && arr[1, 1] == 'o')
                    isTrue = true;
                else if ('s' == arr[2, 0] && arr[2, 0] == arr[0, 2] && arr[1, 1] == 'o')
                    isTrue = true;

                return isTrue;

            }







        }
    

}
