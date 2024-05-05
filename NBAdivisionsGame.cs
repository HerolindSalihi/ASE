using System;
using System.Collections.Generic;
using System.Linq;

public class NBAdivisionsGame : IGame
{
    private Dictionary<string, List<string>> divisions;

    // Konstruktor erhält DataStore-Instanz
    public NBAdivisionsGame(DataStore dataStore)
    {
        divisions = new Dictionary<string, List<string>>();
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
        divisions = dataStore.Lines.Skip(1)
                                   .Select(line => line.Split(','))
                                   .GroupBy(fields => fields[1], fields => fields[0])  // Angenommen, Division ist in der zweiten Spalte und Teamname in der ersten
                                   .ToDictionary(group => group.Key, group => group.ToList());
    }

    public void Start()
    {
        Console.WriteLine("Willkommen zum NBA Divisionen-Spiel!");

        foreach (var division in divisions)
        {
            Console.WriteLine($"\nDivision: {division.Key}. Nenne die Teams dieser Division oder schreibe 'Gebe auf', um weiterzugehen:");
            var guessedTeams = new List<string>();

            while (guessedTeams.Count < division.Value.Count)
            {
                Console.WriteLine("Nenne ein Team:");
                string? teamName = Console.ReadLine()?.Trim();

                if (string.Equals(teamName, "Gebe auf", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else if (!string.IsNullOrEmpty(teamName) && division.Value.Contains(teamName, StringComparer.OrdinalIgnoreCase) && !guessedTeams.Contains(teamName))
                {
                    guessedTeams.Add(teamName);
                    Console.WriteLine("Richtig!");
                }
                else
                {
                    Console.WriteLine("Entweder falsch oder bereits genannt.");
                }
            }

            if (guessedTeams.Count == division.Value.Count)
            {
                Console.WriteLine("Glückwunsch! Du hast alle Teams dieser Division genannt.");
            }
            else
            {
                Console.WriteLine("Weiter zur nächsten Division.");
            }
        }

        Console.WriteLine("\nSpiel beendet. Danke fürs Mitspielen!");
    }
}
