using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class MVPGame
{
    private Dictionary<string, string> mvpBySeason;
    private HashSet<string> guessedMVPs = new HashSet<string>();

    public MVPGame(string filePath)
    {
        LoadData(filePath);
    }

    private void LoadData(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        mvpBySeason = lines.Skip(1)  // Überspringe den Header
                            .Select(line => line.Split(','))
                            .ToDictionary(fields => fields[0].Trim(), fields => fields[1].Trim());  // Angenommen, dass die erste Spalte die Saison und die zweite den MVP enthält
    }

    public void Play()
    {
        Console.WriteLine("Willkommen zum MVP-Ratespiel!");
        Console.WriteLine("Versuche, die MVPs der letzten 40 Saisons zu erraten. Gib den Namen eines Spielers ein.");

        while (guessedMVPs.Count < mvpBySeason.Values.Distinct().Count())
        {
            DisplayMVPs();

            string guess = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(guess))
            {
                Console.WriteLine("Ungültige Eingabe, bitte versuche es erneut.");
                continue;
            }

            var matchedSeasons = mvpBySeason.Where(kvp => kvp.Value.Equals(guess, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchedSeasons.Count > 0 && !guessedMVPs.Contains(guess))
            {
                guessedMVPs.Add(guess);
                Console.WriteLine($"Richtig! {guess} war MVP in den folgenden Saisons:");
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

        Console.WriteLine("\nGlückwunsch! Du hast alle MVPs erraten.");
    }

    private void DisplayMVPs()
    {
        foreach (var season in mvpBySeason)
        {
            if (guessedMVPs.Contains(season.Value))
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
