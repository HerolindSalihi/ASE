using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class TeamSchedule
{
    private List<string[]> games;
    private string[] headers;

    public TeamSchedule(string filePath)
    {
        LoadData(filePath);
    }

    private void LoadData(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        headers = lines[0].Split(',');  // Angenommen, die CSV-Header beinhalten die notwendigen Spalten
        games = lines.Skip(1).Select(line => line.Split(',')).ToList();
    }

    public void DisplayTeamGames()
    {
        Console.WriteLine("Gib den Namen des NBA-Teams ein:");
        string teamName = Console.ReadLine().Trim();

        var teamGames = games.Where(game => game[1].Equals(teamName, StringComparison.OrdinalIgnoreCase) || game[2].Equals(teamName, StringComparison.OrdinalIgnoreCase)).ToList();

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
            string score = game[3]; // Angenommen, dass der Spielstand in der vierten Spalte steht

            Console.WriteLine($"{date}: {homeTeam} vs {awayTeam} - Ergebnis: {score}");
        }
    }
}