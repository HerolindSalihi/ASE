using System;
using System.Collections.Generic;
using System.Linq;

public class MVPGame : IGame
{
    private Dictionary<string, string> mvpBySeason;
    private HashSet<string> guessedMVPs = new HashSet<string>();

    // Konstruktor erhält DataStore-Instanz
    public MVPGame(DataStore dataStore)
    {
        mvpBySeason = new Dictionary<string, string>();
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
        mvpBySeason = dataStore.Lines.Skip(1)  // Kopfzeile überspringen
                                     .Select(line => line.Split(','))
                                     .ToDictionary(fields => fields[0].Trim(), fields => fields[1].Trim());  // Spalten für Saison und MVP
    }

    public void Start()
    {
        try
        {
            Console.WriteLine("Willkommen zum MVP-Ratespiel!");
            Console.WriteLine("Versuche, die MVPs der letzten 40 Saisons zu erraten. Gib den Namen eines Spielers ein.");

            while (guessedMVPs.Count < mvpBySeason.Values.Distinct().Count())
            {
                DisplayMVPs();

                string? guess = Console.ReadLine()?.Trim();
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
        catch (Exception ex)
        {
            ErrorHandler.HandleError("Ein Fehler ist aufgetreten", ex);
        }
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
