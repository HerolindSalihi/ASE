using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class TeamSchedule : IGame
{
    private List<string[]> games;
    private string[] headers;

    public TeamSchedule(DataStore dataStore)
    {
        games = new List<string[]>();
        headers = Array.Empty<string>();
        LoadData(dataStore);
    }

    private void LoadData(DataStore dataStore)
    {
        if (dataStore.Lines == null || dataStore.Lines.Length == 0)
        {
            Console.WriteLine("Keine Daten zum Laden verfügbar.");
            return;
        }

        string[] lines = dataStore.Lines;
        headers = lines[0].Split(',');  // Angenommen, die CSV-Header beinhalten die notwendigen Spalten
        games = lines.Skip(1).Select(line => line.Split(',')).ToList();
    }


    public void Start()
    {
        Console.WriteLine("Willkommen zur Anzeige des Spielplans eines NBA-Teams!");
        DisplayTeamGames();
    }

    private void DisplayTeamGames()
    {
        Console.WriteLine("Gib den Namen des NBA-Teams ein:");
        string? input = Console.ReadLine();
        string teamName = input?.Trim() ?? string.Empty;

        var teamGames = games.Where(game => game.Length > 2 && (game[1].Equals(teamName, StringComparison.OrdinalIgnoreCase) || game[2].Equals(teamName, StringComparison.OrdinalIgnoreCase))).ToList();

        if (teamGames.Count == 0)
        {
            Console.WriteLine("Keine Spiele gefunden für: " + teamName);
            return;
        }

        Console.WriteLine($"\nSpiele für {teamName}:");
        foreach (var game in teamGames)
        {
            string homeTeam = game[1];
            string awayTeam = game[2];
            string date = game[0];
            string score = game.Length > 3 ? game[3] : "Ergebnis unbekannt"; // Sicherstellen, dass die Indexgrenzen eingehalten werden

            Console.WriteLine($"{date}: {homeTeam} vs {awayTeam} - Ergebnis: {score}");
        }
    }
}