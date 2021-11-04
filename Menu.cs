using System.Linq;
using ConsoleTables;

namespace 3task
{
    static class ConsoleTableGenerator
    {
        public static void GenerateTable(string[] massValues)
        {
            string[] firstRow = massValues.Prepend("PC \\ User").ToArray();

            var table = new ConsoleTable(firstRow);

            for (int i = 0; i < massValues.Length; i++)
            {
                table.AddRow(GenerateRow(i, massValues));
            }

            table.Write(Format.Alternative);
        }

        private static string[] GenerateRow(int rowValueIndex, string[] massValues)
        {
            string[] newRowValues = new string[] { massValues[rowValueIndex] };

            for (int j = 0; j < massValues.Length; j++)
            {
                int winnerIndex = GameRules.GetWinner(rowValueIndex, j, massValues.Length);

                string result;
                if (winnerIndex == -1) result = "Draw";
                else if (winnerIndex == j) result = "Win";
                else result = "Lose";

                newRowValues = newRowValues.Append(result).ToArray();
            }

            return newRowValues;
        }
    }
}