using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class FinalsMVPGame : IGame
{
    private Dictionary<string, string> fmvpBySeason;
    private HashSet<string> guessedFMVPs = new HashSet<string>();

    public FinalsMVPGame(string filePath)
    {
        fmvpBySeason = new Dictionary<string, string>();
        LoadData(filePath);
    }

    private void LoadData(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Die Datei '{filePath}' wurde nicht gefunden.");
            fmvpBySeason = new Dictionary<string, string>(); // Initialisiere leer, um NullReferenceExceptions zu vermeiden
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        fmvpBySeason = lines.Skip(1)
                            .Select(line => line.Split(','))
                            .ToDictionary(fields => fields[0].Trim(), fields => fields[1].Trim());
    }

    public void Start()
    {
        Console.WriteLine("Willkommen zum Finals MVP Ratespiel!");
        Console.WriteLine("Versuche, alle Gewinner des Finals MVP zu erraten. Gib den Namen eines Spielers ein.");

        while (guessedFMVPs.Count < fmvpBySeason.Values.Distinct().Count())
        {
            DisplayAwards();

            string? guess = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(guess))
            {
                Console.WriteLine("Ungültige Eingabe, bitte versuche es erneut.");
                continue;
            }

            var matchedSeasons = fmvpBySeason.Where(kvp => kvp.Value.Equals(guess, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchedSeasons.Count > 0 && !guessedFMVPs.Contains(guess))
            {
                guessedFMVPs.Add(guess);
                Console.WriteLine($"Richtig! {guess} war Finals MVP in den folgenden Saisons:");
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

        Console.WriteLine("\nGlückwunsch! Du hast alle Finals MVPs erraten.");
    }

    private void DisplayAwards()
    {
        foreach (var season in fmvpBySeason)
        {
            if (guessedFMVPs.Contains(season.Value))
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
