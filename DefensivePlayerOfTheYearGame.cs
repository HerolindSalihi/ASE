using System;
using System.Collections.Generic;
using System.Linq;

public class DefensivePlayerOfTheYearGame : IGame  // Implementierung des IGame Interfaces
{
    private Dictionary<string, string> dpoyBySeason = new Dictionary<string, string>(); // Entferne Nullable, da wir sicherstellen, dass es immer initialisiert ist
    private HashSet<string> guessedDPOYs = new HashSet<string>();

    // Konstruktor erhält DataStore-Instanz
    public DefensivePlayerOfTheYearGame(DataStore dataStore)
    {
        LoadData(dataStore);
    }

    private void LoadData(DataStore dataStore)
    {
        if (dataStore.Lines == null || dataStore.Lines.Length == 0)
        {
            Console.WriteLine("Fehler beim Laden der Daten für das Defensive Player of the Year-Ratespiel oder keine Daten vorhanden.");
            return;
        }

        string[] lines = dataStore.Lines;
        dpoyBySeason = lines.Skip(1)  
                            .Select(line => line.Split(','))
                            .Where(fields => fields.Length >= 2)
                            .ToDictionary(fields => fields[0].Trim(), fields => fields[1].Trim());  
    }

    public void Start()
    {
        if (dpoyBySeason.Count == 0)
        {
            Console.WriteLine("Keine Daten vorhanden.");
            return;
        }

        Console.WriteLine("Willkommen zum Defensive Player of the Year-Ratespiel!");
        Console.WriteLine("Versuche, alle Gewinner des Defensive Player of the Year zu erraten. Gib den Namen eines Spielers ein.");

        while (guessedDPOYs.Count < dpoyBySeason.Values.Distinct().Count())
        {
            DisplayDPOYs();

            string? guess = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(guess))
            {
                Console.WriteLine("Ungültige Eingabe, bitte versuche es erneut.");
                continue;
            }

            var matchedSeasons = dpoyBySeason.Where(kvp => kvp.Value.Equals(guess, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchedSeasons.Count > 0 && !guessedDPOYs.Contains(guess))
            {
                guessedDPOYs.Add(guess);
                Console.WriteLine($"Richtig! {guess} war Defensive Player of the Year in den folgenden Saisons:");
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

        Console.WriteLine("\nGlückwunsch! Du hast alle Defensive Players of the Year erraten.");
    }

    private void DisplayDPOYs()
    {
        foreach (var season in dpoyBySeason)
        {
            if (guessedDPOYs.Contains(season.Value))
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
