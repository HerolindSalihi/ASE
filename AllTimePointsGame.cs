using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AllTimePointsGame
{
    private List<string[]> playersData = new List<string[]>();
    private HashSet<string> guessedPlayers = new HashSet<string>();

    public AllTimePointsGame(string filePath)
    {
        if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
        {
            LoadData(filePath);
        }
        else
        {
            Console.WriteLine("Dateipfad ist ungültig oder leer.");
        }
    }

    private void LoadData(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        foreach (var line in lines.Skip(1))  // Header überspringen
        {
            string[] fields = line.Split(',');
            if (fields.Length >= 3 && int.TryParse(fields[2], out _))  // Mindestens drei Felder und das dritte Feld sind die Punkte
            {
                playersData.Add(fields);
            }
            else
            {
                Console.WriteLine($"Ungültige Datensatzformatierung: {line}");
            }
        }

        playersData = playersData.OrderByDescending(fields => int.Parse(fields[2])).Take(30).ToList();  // Punkte absteigend sortieren, Top 30 Spieler
    }

    public void Play()
    {
        Console.WriteLine("Willkommen zum All-Time Points Erratespiel!");
        Console.WriteLine("Versuche, die Top 30 Spieler mit den meisten Punkten in der Geschichte der NBA zu erraten.");

        while (guessedPlayers.Count < 30 && playersData.Any())
        {
            Console.WriteLine("\nGib den Namen eines Spielers ein:");
            string playerName = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(playerName))
            {
                if (playersData.Any(p => string.Equals(p.ElementAtOrDefault(1), playerName, StringComparison.OrdinalIgnoreCase) && guessedPlayers.Add(playerName)))
                {
                    Console.WriteLine($"Richtig! {playerName} ist einer der Top 30 Spieler nach Punkten.");
                }
                else
                {
                    Console.WriteLine("Entweder falsch oder bereits erraten.");
                }

                DisplayGuessedPlayers();
            }
        }

        Console.WriteLine("\nGlückwunsch! Du hast alle Top 30 Spieler erraten.");
    }

    private void DisplayGuessedPlayers()
    {
        Console.WriteLine("\nBisher erratene Spieler:");
        foreach (var player in playersData.Where(p => guessedPlayers.Contains(p.ElementAtOrDefault(1))))
        {
            Console.WriteLine(player.ElementAtOrDefault(1));
        }
    }
}
