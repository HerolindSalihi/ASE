using System;
using System.Collections.Generic;
using System.Linq;

public class AllStarMVPGame : IGame
{
    private Dictionary<string, string> amvpBySeason = new Dictionary<string, string>();
    private HashSet<string> guessedAMVPs = new HashSet<string>();

    // Konstruktor erhält DataStore-Instanz
    public AllStarMVPGame(DataStore dataStore)
    {
        LoadData(dataStore); // Lade die Daten aus dem DataStore
    }

    private void LoadData(DataStore dataStore)
    {
        if (dataStore.Lines == null || dataStore.Lines.Length <= 1)
        {
            Console.WriteLine("Es wurden keine All-Star MVP-Daten geladen.");
            return;
        }

        string[] lines = dataStore.Lines;
        amvpBySeason = lines.Skip(1)
                            .Select(line => line.Split(','))
                            .Where(parts => parts.Length >= 2)
                            .ToDictionary(fields => fields[0].Trim(), fields => fields[1].Trim());
    }

    public void Start()
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

            string? input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("Keine Eingabe erkannt. Bitte erneut versuchen.");
                continue;
            }
            string guess = input.Trim();
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
