using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class TopTenPlayersByStat : IGame
{
    private string[] lines;
    private string[] headers;

    public TopTenPlayersByStat(string[] lines, string[] headers)
    {
        this.lines = lines;
        this.headers = headers;
    }

    public void Start()
    {
        try
        {
            ShowTopTenPlayersByStat();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
        }
    }

    public void ShowTopTenPlayersByStat()
    {
        Console.WriteLine("Verfügbare Statistiken:");
        for (int i = 0; i < headers.Length; i++)
        {
            Console.WriteLine($"{i + 1}: {headers[i]}");
        }
        Console.Write("Wähle die Nummer der Statistik: ");
        if (int.TryParse(Console.ReadLine(), out int column) && column >= 1 && column <= headers.Length)
        {
            column--;
            var topPlayers = lines.Skip(1).Select(line => line.Split(',')).Where(columns => decimal.TryParse(columns[column], out _)).Select(columns => new { Player = columns[1], StatValue = decimal.Parse(columns[column]) }).OrderByDescending(x => x.StatValue).Take(10).ToList();
            if (topPlayers.Count == 0)
            {
                Console.WriteLine("Keine gültigen Daten gefunden.");
            }
            else
            {
                Console.WriteLine($"Top 10 Spieler für {headers[column]}:");
                foreach (var player in topPlayers)
                {
                    Console.WriteLine($"Spieler: {player.Player}, {headers[column]}: {player.StatValue}");
                }
            }
        }
        else
        {
            Console.WriteLine("Ungültige Eingabe.");
        }
    }
}
