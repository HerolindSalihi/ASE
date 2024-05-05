using System;
using System.Collections.Generic;
using System.Linq;

public class RookieOfTheYearGame : IGame
{
    private Dictionary<string, string> rookieBySeason;
    private HashSet<string> guessedRookies = new HashSet<string>();

    // Konstruktor erhält DataStore-Instanz
    public RookieOfTheYearGame(DataStore dataStore)
    {
        rookieBySeason = new Dictionary<string, string>();
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
        rookieBySeason = dataStore.Lines.Skip(1)  // Kopfzeile überspringen
                                    .Select(line => line.Split(','))
                                    .ToDictionary(fields => fields[0].Trim(), fields => fields[1].Trim());  // Spalten für Saison und Rookie
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
