using System;
using System.Linq;
using System.Security.Cryptography;

namespace 3task
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!ValidateInputArgs(args))
            {
                Console.WriteLine("Incorrect input. Please enter odd number of unique rows(>=3). Example: dotnet run a b c");
            }
            else
            {
                RunGame(args);
            }
        }

        static bool ValidateInputArgs(string[] inputArgs)
        {
            var lst = inputArgs.ToList();

            var query = lst.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            if (inputArgs.Length <= 1 || inputArgs.Length % 2 == 0 || query.Count != 0)
            {
                return false;
            }

            return true;
        }

        static void RunGame(string[] args)
        {
            int computerMoveIndex = RandomNumberGenerator.GetInt32(args.Length);
            string computerMove = args[computerMoveIndex];

            byte[] hmacKey = HMAC.GenerateRandomKey(16);
            byte[] hmacHash = HMAC.HashHMAC(hmacKey, HMACGenerator.StringEncode(computerMove));
            Console.WriteLine("HMAC: " + HMACGenerator.HashEncode(hmacHash));

            GenerateConsoleMenu(args);

            ProcessUserChoice(computerMoveIndex, computerMove, args);

            Console.WriteLine("HMAC key: " + HMACGenerator.HashEncode(hmacKey));
        }

        static void GenerateConsoleMenu(string[] args)
        {
            Console.WriteLine("Available moves: ");
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {args[i]}");
            }

            Console.WriteLine($"0 - Exit");
            Console.WriteLine($"? - Help");
        }

        static string GetUserEnter()
        {
            Console.Write("Enter your move: ");

            string enteredText = Console.ReadLine().Trim();

            if (enteredText == "") return null;

            return enteredText;
        }

        static void ProcessUserChoice(int computerMoveIndex, string computerMove, string[] args)
        {
            while (true)
            {
                string userEnter = GetUserEnter();

                if (userEnter == "0")
                {
                    Console.WriteLine("Program exit");

                    break;
                }
                else if (userEnter == "?")
                {
                    ConsoleTableGenerator.GenerateTable(args);

                    break;
                }
                else
                {
                    int userEnterIndex;
                    bool tryParseMove = int.TryParse(userEnter, out userEnterIndex);

                    if (!tryParseMove || userEnterIndex < 0 || userEnterIndex > args.Length)
                    {
                        Console.WriteLine("Incorrect input. Please enter one of available move");

                        continue;
                    }
                    else
                    {
                        int userMoveIndex = userEnterIndex - 1;

                        Console.WriteLine($"Your move: {args[userMoveIndex]}");
                        Console.WriteLine($"Computer move: {computerMove}");

                        ProcessGameResult(userMoveIndex, computerMoveIndex, args.Length);

                        break;
                    }
                }
            }
        }

        static void ProcessGameResult(int userMoveIndex, int computerMoveIndex, int argsMassLength)
        {
            int result = GameRules.GetWinner(userMoveIndex, computerMoveIndex, argsMassLength);

            string winnerLine;
            if (result == computerMoveIndex) winnerLine = "Computer win.";
            else if (result == -1) winnerLine = "Draw.";
            else winnerLine = "You win.";

            Console.WriteLine(winnerLine);
        }
    }
}