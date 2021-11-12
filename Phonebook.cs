using System;

namespace Phonebook_F3
{
    /// <summary>
    /// A phonebook app NOW WITH 2D ARRAYS XDD
    /// </summary>
    class Phonebook
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Ultra Phonebook App.");

            int tempNum = ReadInt("How big would you like the phonebook to be ?");
            int arraySize = LimitNumber(tempNum, false, false, 2, 10);
            string[,] phonebook = new string[arraySize,arraySize];
            Console.WriteLine("Created a phonebook of " + arraySize + " entry slots!");

            InitializeArrayEmpty(phonebook);
            PrintSeparatorLines();

            MenuSelectionSequence(phonebook);
        }

        private static void MenuSelectionSequence(string[,] phonebook)
        {
            string userChoice = null;

            while (userChoice != "0")
            {
                Console.WriteLine("Main Menu\n\n" +
                "1) List phonebook entries\n" +
                "2) Add/modify entry\n" +
                "3) Delete entry\n" +
                "4) Search\n" +
                "0) Exit\n" +
                "\n" +
                "Choice?");

                userChoice = ReadString("").Trim();
                PrintSeparatorLines();

                switch (userChoice)
                {
                    case "1":
                        ListEntries(phonebook);
                        PrintSeparatorLines();
                        break;
                    case "2":
                        AddModifyEntry(phonebook);
                        PrintSeparatorLines();
                        break;
                    case "3":
                        DeleteEntry(phonebook);
                        PrintSeparatorLines();
                        break;
                    case "4":
                        SearchEntry(phonebook);
                        PrintSeparatorLines();
                        break;
                    case "0":
                        Console.WriteLine("Terminating program");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        PrintSeparatorLines();
                        break;
                }
            }
        }

        static void ListEntries(string[,] phonebook)
        {
            int rows, cols;
            rows = phonebook.GetLength(0);
            cols = phonebook.GetLength(1);

            Console.WriteLine("Saved Entries \n");

            for (int i = 0; i < rows; i++)
            {
                Console.Write("Entry#" + i + " ");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(phonebook[i, j]+ " ");

                    if (j==cols-1)
                    {
                        Console.Write("\n");
                    }
                }

                if (i==rows-1)
                {
                    Console.Write("\n");
                }
            }
        }

        private static void AddModifyEntry(string[,] phonebook)
        {
            string name, number;
            int arrayIdx=0;

            arrayIdx = LimitNumber(ReadInt("Entry number?"), false, true, 0, phonebook.GetLength(1) - 1);

            name = ReadString("Name? ");
            number = ReadString("Phone? ");

            phonebook[arrayIdx, 0] = name;
            phonebook[arrayIdx, 1] = number;
        }

        private static void DeleteEntry(string[,] phonebook)
        {
            int arrayIdx;
            arrayIdx = LimitNumber(ReadInt("Number to delete?"), false, true, 0, phonebook.GetLength(0) - 1);

            phonebook[arrayIdx, 0] = string.Empty;
            phonebook[arrayIdx, 1] = string.Empty;
        }


        private static void SearchEntry(string[,] phonebook)
        {
            int results = 0, rows;
            rows = phonebook.GetLength(0);

            string searchedName;
            searchedName = ReadString("Which name to search for?").Trim();

            for (int i = 0; i < rows; i++)
            {
                if (phonebook[i,0].ToUpper() == searchedName.ToUpper())
                {
                    Console.WriteLine(phonebook[i, 0] + " " + phonebook[i, 1]);
                    results++;
                }
            }

            Console.WriteLine(results + " result(s) found");
        }

        //Utility methods below

        /// <summary>
        /// Prompt the user for a string input,
        /// If the input is Empty or Spaces then prompt the user again.
        /// </summary>
        /// <param name="prompt">The message to print to the user</param>
        /// <returns>A string of the users input</returns>
        static string ReadString(string prompt)
        {
            string tempStr = "";

            do
            {
                Console.WriteLine(prompt);
                tempStr = Console.ReadLine();
            } while (String.IsNullOrEmpty(tempStr) || String.IsNullOrWhiteSpace(tempStr));

            return tempStr;
        }

        /// <summary>
        /// Prompt the user to give a number, try parsing it,
        /// if the parsing fails print an Invalid input message.
        /// </summary>
        /// <param name="prompt">The message to print to the console for the user</param>
        /// <returns>The parsed string as an int</returns>
        static int ReadInt(string prompt)
        {
            string tempStr = "";
            int tempNum = 0;

            while (true)
            {
                tempStr = ReadString(prompt);

                if (int.TryParse(tempStr, out tempNum))
                {
                    //Parsing was succesfull
                    break;
                }
                else
                {
                    //Parsing failed, prompt user for input again
                    Console.WriteLine("Invalid input");
                }
            }

            return tempNum;
        }

        /// <summary>
        /// Limit the number the user game as input
        /// </summary>
        /// <param name="value">The value to check on</param>
        /// <param name="printQuestion">Print a pre made question?</param>
        /// <param name="allowZero">Allow zeros as answers?</param>
        /// <param name="min">The minimum acceptable value</param>
        /// <param name="max">The maximum acceptable value</param>
        /// <returns>The accepted number as int</returns>
        static int LimitNumber(int value, bool printQuestion, bool allowZero, int min, int max)
        {
            int tempNum = value;

            while (true)
            {
                if (printQuestion)
                {
                    tempNum = ReadInt("Please give me a number");
                }

                if (tempNum == 0 && allowZero)
                {
                    return tempNum;
                }
                else if (tempNum >= min && tempNum <= max)
                {
                    //Size is acceptable
                    break;
                }
                else
                {
                    Console.WriteLine("Value is invalid");
                    printQuestion = true;
                }
            }

            return tempNum;
        }

        /// <summary>
        /// Initialize the specified array with empty strings so we can .ToUpper() every position even if the element is empty
        /// We prevent exceptions this way.
        /// </summary>
        /// <param name="phonebook">The array to init with empty strings</param>
        private static void InitializeArrayEmpty(string[,] phonebook)
        {
            for (int i = 0; i < phonebook.GetLength(0); i++)
            {
                for (int j = 0; j < phonebook.GetLength(1); j++)
                {
                    phonebook[i, j] = string.Empty;
                }
            }
        }

        /// <summary>
        /// Print ---- for Style (and better readability)
        /// </summary>
        static void PrintSeparatorLines()
        {
            for (int i = 0; i < 35; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine();
        }
    }
}
