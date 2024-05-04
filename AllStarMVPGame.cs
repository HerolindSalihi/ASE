using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AllStarMVPGame
{
    private Dictionary<string, string> amvpBySeason = new Dictionary<string, string>(); // Feld initialisieren
    private HashSet<string> guessedAMVPs = new HashSet<string>();

    public AllStarMVPGame(string filePath)
    {
        try
        {
            LoadData(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Es gab einen Fehler beim Laden der Daten: {ex.Message}");
            Environment.Exit(1); // Beendet die Ausführung des Programms, wenn ein Fehler auftritt.
        }
    }

    private void LoadData(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Die Datei '{filePath}' wurde nicht gefunden.");
        }
        string[] lines = File.ReadAllLines(filePath);
        amvpBySeason = lines.Skip(1)
                            .Select(line => line.Split(','))
                            .Where(parts => parts.Length >= 2)
                            .ToDictionary(fields => fields[0].Trim(), fields => fields[1].Trim());
    }

    public void Play()
    {
        if (amvpBySeason.Count == 0)
        {
            Console.WriteLine("Es wurden keine Daten geladen.");
            return;
        }

        Console.WriteLine("Willkommen zum All-Star MVP Ratespiel!");
        Console.WriteLine("Versuche, alle Gewinner des All-Star MVP zu erraten. Gib den Namen eines Spielers ein.");

        while (guessedAMVPs.Count < amvpBySeason.Values.Distinct().Count())
        {
            DisplayAwards();

            string guess = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(guess))
            {
                Console.WriteLine("Ungültige Eingabe, bitte versuche es erneut.");
                continue;
            }

            var matchedSeasons = amvpBySeason.Where(kvp => kvp.Value.Equals(guess, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchedSeasons.Count > 0 && !guessedAMVPs.Contains(guess))
            {
                guessedAMVPs.Add(guess);
                Console.WriteLine($"Richtig! {guess} war All-Star MVP in den folgenden Saisons:");
                foreach (var season in matchedSeasons)
                {
                    Console.WriteLine($"- {season.Key}");
                }
            }
            else
            {
                Console.WriteLine("Falsch oder bereits erraten, versuche es erneut.");
            }
        }

        Console.WriteLine("\nGlückwunsch! Du hast alle All-Star MVPs erraten.");
    }

    private void DisplayAwards()
    {
        foreach (var season in amvpBySeason)
        {
            if (season.Value != null && guessedAMVPs != null) // Überprüfen, ob die Werte nicht null sind
            {
                if (guessedAMVPs.Contains(season.Value))
                {
                    Console.WriteLine($"{season.Key}: {season.Value}");
                }
                else
                {
                    Console.WriteLine($"{season.Key}: ?");
                }
            }
        }
    }
}
    
