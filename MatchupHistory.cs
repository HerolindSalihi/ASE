using System;
using System.Collections.Generic;
using System.Linq;

public class MatchupHistory : IGame
{
    private List<string[]> games;
    private string[] headers;

    // Konstruktor erhält DataStore-Instanz
    public MatchupHistory(DataStore dataStore)
    {
        games = new List<string[]>();
        headers = Array.Empty<string>();
        LoadData(dataStore);  // Lade die Daten aus dem DataStore
    }

    // Anpassung der LoadData Methode, um Daten aus DataStore zu verwenden
    private void LoadData(DataStore dataStore)
    {
        if (dataStore.Lines == null || dataStore.Lines.Length == 0)
        {
            Console.WriteLine("Keine Daten zum Laden verfügbar.");
            return;
        }

        // Verwende die Linien direkt aus DataStore
        string[] lines = dataStore.Lines;
        headers = lines[0].Split(',');  // CSV-Header für Spaltennamen
        games = lines.Skip(1).Select(line => line.Split(',')).ToList();
    }

    public void Start()
    {
        DisplayMatchups();
    }

    public void DisplayMatchups()
    {
        try
        {
            Console.WriteLine("Gib den Namen des ersten NBA-Teams ein:");
            string? teamOne = Console.ReadLine()?.Trim();

            Console.WriteLine("Gib den Namen des zweiten NBA-Teams ein:");
            string? teamTwo = Console.ReadLine()?.Trim();

            var matchups = games.Where(game =>
                (game.ElementAtOrDefault(1)?.Equals(teamOne, StringComparison.OrdinalIgnoreCase) == true &&
                 game.ElementAtOrDefault(2)?.Equals(teamTwo, StringComparison.OrdinalIgnoreCase) == true) ||
                (game.ElementAtOrDefault(1)?.Equals(teamTwo, StringComparison.OrdinalIgnoreCase) == true &&
                 game.ElementAtOrDefault(2)?.Equals(teamOne, StringComparison.OrdinalIgnoreCase) == true)
            ).ToList();

            if (!matchups.Any())
            {
                Console.WriteLine($"Keine Spiele gefunden zwischen: {teamOne} und {teamTwo}");
                return;
            }

            Console.WriteLine($"\nSpiele zwischen {teamOne} und {teamTwo}:");
            foreach (var game in matchups)
            {
                string date = game[0];
                string homeTeam = game[1];
                string awayTeam = game[2];
                string score = game.ElementAtOrDefault(3) ?? "Unbekannt"; // Annahme, dass der Spielstand in der vierten Spalte steht

                Console.WriteLine($"{date}: {homeTeam} vs {awayTeam} - Ergebnis: {score}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ein Fehler ist aufgetreten während der Anzeige der Spiele:");
            Console.WriteLine(ex.Message);
        }
    }
}
