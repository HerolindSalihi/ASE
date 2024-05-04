using System;
using System.Collections.Generic;
using System.Linq;

public class StatisticsComparisonGame
{
    private List<string[]> players;
    private string[] headers;
    private Random random = new Random();

    public StatisticsComparisonGame(string[] lines, string[] headers)
    {
        this.headers = headers;
        players = lines.Skip(1).Select(line => line.Split(',')).ToList();
    }

    public void Play()
    {
        Console.WriteLine("Willkommen zum Statistiken-Vergleichsspiel!");
        int score = 0;
        string[] categories = { "PTS", "AST", "REB" };  // Beispielkategorien: Punkte, Assists, Rebounds

        for (int i = 0; i < 20; i++)
        {
            var playerOne = players[random.Next(players.Count)];
            var playerTwo = players[random.Next(players.Count)];
            string category = categories[random.Next(categories.Length)];
            int categoryIndex = Array.IndexOf(headers, category);

            Console.WriteLine($"\nRunde {i+1}: Wer hat mehr {category}?");
            Console.WriteLine($"1: {playerOne[1]} ({playerOne[categoryIndex]})");
            Console.WriteLine($"2: {playerTwo[1]} ({playerTwo[categoryIndex]})");
            Console.Write("Wähle Spieler 1 oder 2: ");
            string input = Console.ReadLine();
            int userChoice = int.Parse(input.Trim());

            int playerOneStat = int.Parse(playerOne[categoryIndex]);
            int playerTwoStat = int.Parse(playerTwo[categoryIndex]);

            int correctAnswer = playerOneStat > playerTwoStat ? 1 : 2;

            if (userChoice == correctAnswer)
            {
                Console.WriteLine("Richtig!");
                score++;
            }
            else
            {
                Console.WriteLine("Leider falsch.");
            }
        }

        Console.WriteLine($"\nSpiel beendet. Deine Punktzahl: {score}/20");
    }
}
