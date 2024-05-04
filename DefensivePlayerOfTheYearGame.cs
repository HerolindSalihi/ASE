using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class DefensivePlayerOfTheYearGame : IGame  // Implementierung des IGame Interfaces
{
    private Dictionary<string, string>? dpoyBySeason; // Feld als nullable deklarieren
    private HashSet<string> guessedDPOYs = new HashSet<string>();

    public DefensivePlayerOfTheYearGame(string filePath)
    {
        LoadData(filePath);
    }

    private void LoadData(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Die Datei '{filePath}' wurde nicht gefunden.");
            dpoyBySeason = new Dictionary<string, string>(); // Sicherstellen, dass die Variable initialisiert ist
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        dpoyBySeason = lines.Skip(1)  
                            .Select(line => line.Split(','))
                            .ToDictionary(fields => fields[0].Trim(), fields => fields[1].Trim());  
    }

    public void Start() // Ersetzt die Play Methode und implementiert die IGame.Start Methode
    {
        if (dpoyBySeason == null || dpoyBySeason.Count == 0)
        {
            Console.WriteLine("Fehler beim Laden der Daten für das Defensive Player of the Year-Ratespiel oder keine Daten vorhanden.");
            return;
        }

        Console.WriteLine("Willkommen zum Defensive Player of the Year-Ratespiel!");
        Console.WriteLine("Versuche, alle Gewinner des Defensive Player of the Year zu erraten. Gib den Namen eines Spielers ein.");

        while (guessedDPOYs.Count < dpoyBySeason.Values.Distinct().Count())
        {
            DisplayDPOYs();

            string? guess = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(guess))
            {
                Console.WriteLine("Ungültige Eingabe, bitte versuche es erneut.");
                continue;
            }

            var matchedSeasons = dpoyBySeason.Where(kvp => kvp.Value.Equals(guess, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchedSeasons.Count > 0 && !guessedDPOYs.Contains(guess))
            {
                guessedDPOYs.Add(guess);
                Console.WriteLine($"Richtig! {guess} war Defensive Player of the Year in den folgenden Saisons:");
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

        Console.WriteLine("\nGlückwunsch! Du hast alle Defensive Players of the Year erraten.");
    }

    private void DisplayDPOYs()
    {
        if (dpoyBySeason == null)
        {
            Console.WriteLine("Daten sind nicht verfügbar.");
            return;
        }

        foreach (var season in dpoyBySeason)
        {
            if (guessedDPOYs.Contains(season.Value))
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
