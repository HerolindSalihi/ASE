using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class MatchupHistory
{
    private List<string[]> games;
    private string[] headers;

    public MatchupHistory(string filePath)
    {
        LoadData(filePath);
    }

    private void LoadData(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        headers = lines[0].Split(',');  // CSV-Header für Spaltennamen
        games = lines.Skip(1).Select(line => line.Split(',')).ToList();
    }

    public void DisplayMatchups()
    {
        Console.WriteLine("Gib den Namen des ersten NBA-Teams ein:");
        string teamOne = Console.ReadLine().Trim();

        Console.WriteLine("Gib den Namen des zweiten NBA-Teams ein:");
        string teamTwo = Console.ReadLine().Trim();

        var matchups = games.Where(game =>
            (game[1].Equals(teamOne, StringComparison.OrdinalIgnoreCase) && game[2].Equals(teamTwo, StringComparison.OrdinalIgnoreCase)) ||
            (game[1].Equals(teamTwo, StringComparison.OrdinalIgnoreCase) && game[2].Equals(teamOne, StringComparison.OrdinalIgnoreCase))
        ).ToList();

        if (matchups.Count == 0)
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
            string score = game[3]; // Angenommen, dass der Spielstand in der vierten Spalte steht

            Console.WriteLine($"{date}: {homeTeam} vs {awayTeam} - Ergebnis: {score}");
        }
    }
}
