using System.Runtime.InteropServices;
using Tictactoe.Core;

public static class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Console application started.");

        var tictactoeGame = new TictactoeEngine();
        ObjectResult result = new ObjectResult();
        do
        {

            Console.WriteLine("Showing the board...");
            ShowTictactoeBoard(tictactoeGame.GetBoard());

            Console.WriteLine("Symbols allowed: X or O (letters)");
            Console.WriteLine("Type the line and column number between 0 and 2...");
            Console.WriteLine("Line: ");
            int line = int.Parse(Console.ReadLine());
            Console.WriteLine("Column: ");
            int column = int.Parse(Console.ReadLine());
            Console.WriteLine("Symbol: ");
            string symbol = Console.ReadLine();

            result = tictactoeGame.ExecuteAndVerifyWinner(symbol, new Coordinates()
            {
                Line = line,
                Column = column
            });

        } while (!result.GameCompleted);

        Console.WriteLine(result.Winner);
        Console.WriteLine("Press any key to exit the application.");
        Console.Read();
    }

    private static void ShowTictactoeBoard(List<List<BoardInsertion>> board) 
    {
        foreach (var line in board) 
        {
            Console.WriteLine(string.Join(" | ", line.Select(x => x.Symbol)));
        }
    }
}