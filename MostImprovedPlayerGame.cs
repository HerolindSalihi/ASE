using System;
using System.Collections.Generic;
using System.Linq;

public class MostImprovedPlayerGame : IGame
{
    private Dictionary<string, string> mipBySeason;
    private HashSet<string> guessedMIPs = new HashSet<string>();

    // Konstruktor erhält DataStore-Instanz
    public MostImprovedPlayerGame(DataStore dataStore)
    {
        mipBySeason = new Dictionary<string, string>();
        LoadData(dataStore);  // Laden der Daten aus DataStore
    }

    // Lädt die Daten aus dem DataStore
    private void LoadData(DataStore dataStore)
    {
        if (dataStore.Lines == null || dataStore.Lines.Length < 2)
        {
            throw new InvalidOperationException("Nicht genügend Daten im DataStore vorhanden.");
        }

        // Verarbeitung der Datenzeilen
        mipBySeason = dataStore.Lines.Skip(1)
                                    .Select(line => line.Split(','))
                                    .ToDictionary(fields => fields[0].Trim(), fields => fields[1].Trim());
    }

    public void Start()
    {
        try
        {
            Console.WriteLine("Willkommen zum Most Improved Player Ratespiel!");
            Console.WriteLine("Versuche, alle Gewinner des Most Improved Player zu erraten. Gib den Namen eines Spielers ein.");

            while (guessedMIPs.Count < mipBySeason.Values.Distinct().Count())
            {
                DisplayAwards();

                string? guess = Console.ReadLine()?.Trim();
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
        catch (Exception ex)
        {
            ErrorHandler.HandleError("Ein Fehler ist aufgetreten", ex);
        }
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
