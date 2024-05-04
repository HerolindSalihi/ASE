using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class MostImprovedPlayerGame
{
    private Dictionary<string, string> mipBySeason;
    private HashSet<string> guessedMIPs = new HashSet<string>();

    public MostImprovedPlayerGame(string filePath)
    {
        LoadData(filePath);
    }

    private void LoadData(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        mipBySeason = lines.Skip(1)
                           .Select(line => line.Split(','))
                           .ToDictionary(fields => fields[0].Trim(), fields => fields[1].Trim());
    }

    public void Play()
    {
        Console.WriteLine("Willkommen zum Most Improved Player Ratespiel!");
        Console.WriteLine("Versuche, alle Gewinner des Most Improved Player zu erraten. Gib den Namen eines Spielers ein.");

        while (guessedMIPs.Count < mipBySeason.Values.Distinct().Count())
        {
            DisplayAwards();

            string guess = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(guess))
            {
                Console.WriteLine("Ungültige Eingabe, bitte versuche es erneut.");
                continue;
            }

            var matchedSeasons = mipBySeason.Where(kvp => kvp.Value.Equals(guess, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchedSeasons.Count > 0 && !guessedMIPs.Contains(guess))
            {
                guessedMIPs.Add(guess);
                Console.WriteLine($"Richtig! {guess} war Most Improved Player in den folgenden Saisons:");
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

        Console.WriteLine("\nGlückwunsch! Du hast alle Most Improved Players erraten.");
    }

    private void DisplayAwards()
    {
        foreach (var season in mipBySeason)
        {
            if (guessedMIPs.Contains(season.Value))
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
