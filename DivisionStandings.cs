using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class DivisionStandings
{
    private List<string[]> easternStandings = new List<string[]>(); // Feld initialisieren
    private List<string[]> westernStandings = new List<string[]>(); // Feld initialisieren
    private string[] headers = Array.Empty<string>(); // Feld initialisieren

    public DivisionStandings(string filePath)
    {
        LoadData(filePath);
    }

    private void LoadData(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        if (lines.Length > 0) // Überprüfen, ob lines nicht leer ist
        {
            headers = lines[0].Split(',');  // Annahme, dass die erste Zeile die Kopfzeilen enthält
            var data = lines.Skip(1).Select(line => line.Split(','));

            easternStandings = data.Where(line => line.Length > 2 && line[2].Trim().ToLower() == "e").ToList();
            westernStandings = data.Where(line => line.Length > 2 && line[2].Trim().ToLower() == "w").ToList();
        }
        else
        {
            Console.WriteLine("Die Datei enthält keine Daten.");
        }
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
        if (choice != null)
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
        if (headers.Length == 0)
        {
            Console.WriteLine("Es wurden keine Daten geladen.");
            return;
        }

        Console.WriteLine(String.Join(" | ", headers));
        foreach (var row in standings)
        {
            Console.WriteLine(String.Join(" | ", row));
        }
    }
}
