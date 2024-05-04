using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class RookieOfTheYearGame : IGame
{
    private Dictionary<string, string> rookieBySeason;
    private HashSet<string> guessedRookies = new HashSet<string>();

    public RookieOfTheYearGame(string filePath)
    {
        rookieBySeason = new Dictionary<string, string>();
        LoadData(filePath);
    }

    private void LoadData(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        rookieBySeason = lines.Skip(1)  // Überspringe den Header
                            .Select(line => line.Split(','))
                            .ToDictionary(fields => fields[0].Trim(), fields => fields[1].Trim());  // Angenommen, dass die erste Spalte die Saison und die zweite den Rookie enthält
    }

    public void Start()
    {
        Console.WriteLine("Willkommen zum Rookie of the Year-Ratespiel!");
        Console.WriteLine("Versuche, alle Rookies of the Year zu erraten. Gib den Namen eines Spielers ein.");

        while (guessedRookies.Count < rookieBySeason.Values.Distinct().Count())
        {
            DisplayRookies();

            string? input = Console.ReadLine();
            if (input == null || string.IsNullOrEmpty(input.Trim()))
            {
                Console.WriteLine("Ungültige Eingabe, bitte versuche es erneut.");
                continue;
            }

            string guess = input.Trim();

            var matchedSeasons = rookieBySeason.Where(kvp => kvp.Value.Equals(guess, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchedSeasons.Count > 0 && !guessedRookies.Contains(guess))
            {
                guessedRookies.Add(guess);
                Console.WriteLine($"Richtig! {guess} war Rookie of the Year in den folgenden Saisons:");
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

        Console.WriteLine("\nGlückwunsch! Du hast alle Rookies of the Year erraten.");
    }

    private void DisplayRookies()
    {
        foreach (var season in rookieBySeason)
        {
            if (guessedRookies.Contains(season.Value))
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
