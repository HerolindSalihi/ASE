using System;
using System.Collections.Generic;
using System.Linq;

public class AllTimePointsGame : IGame  // Implementiere das IGame Interface
{
    private List<string[]> playersData = new List<string[]>();
    private HashSet<string> guessedPlayers = new HashSet<string>();

    // Konstruktor erhält DataStore-Instanz
    public AllTimePointsGame(DataStore dataStore)
    {
        if (dataStore.Lines == null || dataStore.Lines.Length == 0)
        {
            Console.WriteLine("Es wurden keine Daten geladen.");
            return;
        }
        LoadData(dataStore);
    }

    private void LoadData(DataStore dataStore)
    {
        string[] lines = dataStore.Lines;
        foreach (var line in lines.Skip(1))  // Header überspringen
        {
            string[] fields = line.Split(',');
            if (fields.Length >= 3 && int.TryParse(fields[2], out _))  // Stelle sicher, dass genügend Felder vorhanden sind und das Punktefeld gültig ist
            {
                playersData.Add(fields);
            }
            else
            {
                Console.WriteLine($"Ungültige Datensatzformatierung: {line}");
            }
        }
        playersData = playersData.OrderByDescending(fields => int.Parse(fields[2])).Take(30).ToList();  // Top 30 Spieler nach Punkten
    }

    public void Start()
    {
        Console.WriteLine("Willkommen zum All-Time Points Erratespiel!");
        Console.WriteLine("Versuche, die Top 30 Spieler mit den meisten Punkten in der Geschichte der NBA zu erraten.");

        while (guessedPlayers.Count < 30 && playersData.Any())
        {
            Console.WriteLine("\nGib den Namen eines Spielers ein:");
            string? playerName = Console.ReadLine()?.Trim();

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
        foreach (var player in playersData)
        {
            string? playerName = player.ElementAtOrDefault(1);
            if (playerName != null && guessedPlayers.Contains(playerName))
            {
                Console.WriteLine(playerName);
            }
        }
    }
}
