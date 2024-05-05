using System;
using System.Collections.Generic;
using System.Linq;

public class TeamSearch : IGame
{
    private string[] lines;
    private string[] headers;

    // Konstruktor erhält DataStore-Instanz
    public TeamSearch(DataStore dataStore)
    {
        this.lines = dataStore.Lines ?? Array.Empty<string>();
        this.headers = dataStore.Headers ?? Array.Empty<string>();
    }

    public void Start()
    {
        try
        {
            SearchByTeam();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
        }
    }

    private void SearchByTeam()
    {
        Console.Write("Gib den Teamnamen ein: ");
        string? input = Console.ReadLine();
        string teamName = input?.Trim() ?? string.Empty;

        var teamPlayers = lines.Skip(1).Select(line => line.Split(','))
                               .Where(columns => columns.Length > 4 && columns[4].Equals(teamName, StringComparison.OrdinalIgnoreCase))
                               .ToList();
        if (teamPlayers.Count == 0)
        {
            Console.WriteLine("Keine Spieler gefunden.");
        }
        else
        {
            Console.WriteLine($"Spieler im Team {teamName}:");
            foreach (var player in teamPlayers)
            {
                Console.WriteLine(player[1]); // Annahme, dass der Spielername in der zweiten Spalte steht
            }
        }
    }
}
