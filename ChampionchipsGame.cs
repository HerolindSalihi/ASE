using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ChampionshipsGame
{
    private List<string[]>? playersData; // Feld als nullable deklarieren
    private HashSet<string> guessedPlayers = new HashSet<string>();

    public ChampionshipsGame(string filePath)
    {
        playersData = new List<string[]>(); // Initialisierung des Feldes im Konstruktor
        LoadData(filePath);
    }

    private void LoadData(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        playersData = lines.Skip(1)
                            .Select(line => line.Split(','))
                            .OrderByDescending(fields => int.Parse(fields[2]))
                            .Take(20)
                            .ToList();
    }

    public void Play()
    {
        if (playersData == null)
        {
            Console.WriteLine("Fehler beim Laden der Daten für das Championships Erratespiel.");
            return;
        }

        Console.WriteLine("Willkommen zum Championships Erratespiel!");
        Console.WriteLine("Versuche, die Top 20 Spieler mit den meisten Championships zu erraten.");

        while (guessedPlayers.Count < 20)
        {
            Console.WriteLine("\nGib den Namen eines Spielers ein:");
            string? input = Console.ReadLine();
            string playerName = input?.Trim() ?? string.Empty;

            if (playersData.Any(p => p.Length > 1 && p[1].Equals(playerName, StringComparison.OrdinalIgnoreCase) && guessedPlayers.Add(playerName)))
            {
                Console.WriteLine("Richtig! " + playerName + " ist einer der Top 20 Spieler mit den meisten Championships.");
            }
            else
            {
                Console.WriteLine("Entweder falsch oder bereits erraten.");
            }

            DisplayGuessedPlayers();
        }

        Console.WriteLine("\nGlückwunsch! Du hast alle Top 20 Spieler erraten.");
    }

    private void DisplayGuessedPlayers()
    {
        if (playersData == null)
        {
            Console.WriteLine("Daten sind nicht verfügbar.");
            return;
        }

        Console.WriteLine("\nBisher erratene Spieler:");
        foreach (var player in playersData.Where(p => guessedPlayers.Contains(p[1])))
        {
            if (player != null && player.Length > 1)
            {
                Console.WriteLine(player[1]);
            }
        }
    }
}

