using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class HallOfFameGame : IGame
{
    private List<string[]> playersData;
    private HashSet<string> guessedPlayers = new HashSet<string>();

    public HallOfFameGame(string filePath)
    {
        playersData = new List<string[]>();
        LoadData(filePath);
    }

    private void LoadData(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        playersData = lines.Skip(1)  // Header überspringen
                            .Select(line => line.Split(','))
                            .OrderByDescending(fields => double.Parse(fields[2]))  // Angenommen, Wahrscheinlichkeit ist in der dritten Spalte
                            .Take(25)  // Top 25 Spieler
                            .ToList();
    }

    public void Start()
    {
        try
        {
            Console.WriteLine("Willkommen zum Hall of Fame Erratespiel!");
            Console.WriteLine("Versuche, die Top 25 Spieler mit der höchsten Wahrscheinlichkeit für die Hall of Fame zu erraten.");

            while (guessedPlayers.Count < 25)
            {
                Console.WriteLine("\nGib den Namen eines Spielers ein:");
                string? playerName = Console.ReadLine()?.Trim();

                if (playerName != null && playersData.Any(p => p[1].Equals(playerName, StringComparison.OrdinalIgnoreCase) && guessedPlayers.Add(playerName)))
                {
                    Console.WriteLine("Richtig! " + playerName + " ist einer der Top 25 Kandidaten.");
                }
                else
                {
                    Console.WriteLine("Entweder falsch oder bereits erraten.");
                }

                DisplayGuessedPlayers();
            }

            Console.WriteLine("\nGlückwunsch! Du hast alle Top 25 Spieler erraten.");
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError("Ein Fehler ist aufgetreten", ex);
        }
    }

    private void DisplayGuessedPlayers()
    {
        Console.WriteLine("\nBisher erratene Spieler:");
        foreach (var player in playersData.Where(p => guessedPlayers.Contains(p[1])))
        {
            Console.WriteLine(player[1]);
        }
    }
}
