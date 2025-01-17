using System;
using System.Collections.Generic;
using System.Linq;

public class DivisionStandings : IGame
{
    private List<string[]> easternStandings = new List<string[]>();
    private List<string[]> westernStandings = new List<string[]>();
    private string[] headers = Array.Empty<string>();

    // Konstruktor erhält DataStore-Instanz
    public DivisionStandings(DataStore dataStore)
    {
        LoadData(dataStore);  // Lade die Daten aus dem DataStore
    }

    private void LoadData(DataStore dataStore)
    {
        if (dataStore.Lines == null || dataStore.Lines.Length == 0)
        {
            Console.WriteLine("Die Datei enthält keine Daten.");
            return;
        }

        string[] lines = dataStore.Lines;
        headers = lines[0].Split(',');
        var data = lines.Skip(1).Select(line => line.Split(','));

        easternStandings = data.Where(line => line.Length > 2 && line[2].Trim().ToLower() == "e").ToList();
        westernStandings = data.Where(line => line.Length > 2 && line[2].Trim().ToLower() == "w").ToList();
    }

    public void Start()
    {
        DisplayStandings();
    }

    public void DisplayStandings()
    {
        if (headers.Length == 0)
        {
            Console.WriteLine("Es wurden keine Daten geladen.");
            return;
        }

        Console.WriteLine("Wähle eine Division: 'E' für Eastern oder 'W' für Western");
        string? choice = Console.ReadLine();
        if (!string.IsNullOrEmpty(choice))
        {
            choice = choice.Trim().ToUpper();

            switch (choice)
            {
                case "E":
                    Console.WriteLine("\nEastern Conference Standings:");
                    DisplayTable(easternStandings);
                    break;
                case "W":
                    Console.WriteLine("\nWestern Conference Standings:");
                    DisplayTable(westernStandings);
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe. Bitte wähle 'E' oder 'W'.");
                    break;
            }
        }
    }

    private void DisplayTable(List<string[]> standings)
    {
        if (standings.Count == 0)
        {
            Console.WriteLine("Keine Daten verfügbar für diese Division.");
            return;
        }

        Console.WriteLine(String.Join(" | ", headers));
        foreach (var row in standings)
        {
            Console.WriteLine(String.Join(" | ", row));
        }
    }
}
