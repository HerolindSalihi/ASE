using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ChampionshipsGame : IGame  // Implementiere das IGame Interface
{
    private List<string[]>? playersData; // Feld als nullable deklarieren
    private HashSet<string> guessedPlayers = new HashSet<string>();

    public ChampionshipsGame(string filePath)
    {
        playersData = new List<string[]>();
        try
        {
            LoadData(filePath);
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError("Ein Fehler ist aufgetreten beim Laden der Championships Daten", ex);
            // Entscheidung, ob das Programm beendet wird, oder nicht
            throw;  // Fehler weiter nach oben werfen, falls notwendig
        }
    }

    private void LoadData(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Die Datei '{filePath}' wurde nicht gefunden.");
        }
        
        string[] lines = File.ReadAllLines(filePath);
        try
        {
            playersData = lines.Skip(1)
                                .Select(line => line.Split(','))
                                .Where(fields => fields.Length >= 3 && int.TryParse(fields[2], out _))
                                .OrderByDescending(fields => int.Parse(fields[2]))
                                .Take(20)
                                .ToList();
        }
        catch (FormatException fe)
        {
            ErrorHandler.HandleError("Formatfehler bei der Verarbeitung der Daten", fe);
            throw;  // Werfen, damit weiter oben behandelt werden kann
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError("Allgemeiner Fehler beim Verarbeiten der Daten", ex);
            throw;  // Werfen, damit weiter oben behandelt werden kann
        }
    }

    public void Start() // Ersetzt die "Play" Methode durch "Start"
    {
        if (playersData == null || !playersData.Any())
        {
            Console.WriteLine("Keine Daten verfügbar zum Start des Spiels.");
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
