using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SixthManOfTheYearGame
{
    private Dictionary<string, string> smotyBySeason;
    private HashSet<string> guessedSMOTYs = new HashSet<string>();

    public SixthManOfTheYearGame(string filePath)
    {
        smotyBySeason = new Dictionary<string, string>();
        LoadData(filePath);
    }

    private void LoadData(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        smotyBySeason = lines.Skip(1)  // Überspringe den Header
                            .Select(line => line.Split(','))
                            .ToDictionary(fields => fields[0].Trim(), fields => fields[1].Trim());  // Angenommen, dass die erste Spalte die Saison und die zweite den SMOTY enthält
    }

    public void Start()
    {
        Console.WriteLine("Willkommen zum Sixth Man of the Year-Ratespiel!");
        Console.WriteLine("Versuche, alle Gewinner des Sixth Man of the Year zu erraten. Gib den Namen eines Spielers ein.");

        while (guessedSMOTYs.Count < smotyBySeason.Values.Distinct().Count())
        {
            DisplaySMOTYs();

            string? guess = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(guess))
            {
                Console.WriteLine("Ungültige Eingabe, bitte versuche es erneut.");
                continue;
            }

            var matchedSeasons = smotyBySeason.Where(kvp => kvp.Value.Equals(guess, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchedSeasons.Count > 0 && !guessedSMOTYs.Contains(guess))
            {
                guessedSMOTYs.Add(guess);
                Console.WriteLine($"Richtig! {guess} war Sixth Man of the Year in den folgenden Saisons:");
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

        Console.WriteLine("\nGlückwunsch! Du hast alle Sixth Men of the Year erraten.");
    }

    private void DisplaySMOTYs()
    {
        foreach (var season in smotyBySeason)
        {
            if (guessedSMOTYs.Contains(season.Value))
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
