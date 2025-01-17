using System;
using System.Collections.Generic;
using System.Linq;

public class OneVsOneSimulator : IGame
{
    private List<string[]> players;
    private string[] headers;

    // Konstruktor erhält DataStore-Instanz
    public OneVsOneSimulator(DataStore dataStore)
    {
        if (dataStore.Lines == null || dataStore.Lines.Length < 2)
        {
            throw new InvalidOperationException("Nicht genügend Daten im DataStore vorhanden.");
        }

        headers = dataStore.Lines[0].Split(',');  // Setzt die Kopfzeilen
        players = dataStore.Lines.Skip(1).Select(line => line.Split(',')).ToList();
    }

    public void Start()
    {
        try
        {
            SimulateOneVsOne();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
        }
    }

    public void SimulateOneVsOne()
    {
        Console.Write("Gib den Namen des ersten Spielers ein: ");
        string? firstPlayerName = Console.ReadLine();
        var firstPlayerData = players.FirstOrDefault(columns => columns.Length > 1 && columns[1].Equals(firstPlayerName, StringComparison.OrdinalIgnoreCase));

        Console.Write("Gib den Namen des zweiten Spielers ein: ");
        string? secondPlayerName = Console.ReadLine();
        var secondPlayerData = players.FirstOrDefault(columns => columns.Length > 1 && columns[1].Equals(secondPlayerName, StringComparison.OrdinalIgnoreCase));

        if (firstPlayerData == null || secondPlayerData == null)
        {
            Console.WriteLine("Mindestens einer der Spieler wurde nicht gefunden.");
            return;
        }

        // Statistiken, die verglichen werden sollen
        string[] stats = { "3P%", "FG%", "2P%", "STL", "BLK", "PTS", "PF" };
        int firstPlayerWins = 0;
        int secondPlayerWins = 0;

        Console.WriteLine("\n1vs1 Ergebnisse:");
        foreach (var stat in stats)
        {
            int index = Array.IndexOf(headers, stat);
            
            if (index < 0 || index >= firstPlayerData.Length || index >= secondPlayerData.Length)
            {
                Console.WriteLine($"Statistik '{stat}' nicht gefunden für einen der Spieler.");
                continue;
            }

            decimal firstPlayerStat = Convert.ToDecimal(firstPlayerData[index]);
            decimal secondPlayerStat = Convert.ToDecimal(secondPlayerData[index]);

            Console.WriteLine($"{stat}: {firstPlayerName} = {firstPlayerStat}, {secondPlayerName} = {secondPlayerStat}");

            if (firstPlayerStat > secondPlayerStat)
                firstPlayerWins++;
            else if (secondPlayerStat > firstPlayerStat)
                secondPlayerWins++;
        }

        Console.WriteLine($"\nErgebnisse von {firstPlayerName} vs {secondPlayerName}:");
        Console.WriteLine($"{firstPlayerName} Siege: {firstPlayerWins}");
        Console.WriteLine($"{secondPlayerName} Siege: {secondPlayerWins}");

        if (firstPlayerWins > secondPlayerWins)
            Console.WriteLine($"{firstPlayerName} gewinnt das 1vs1!");
        else if (secondPlayerWins > firstPlayerWins)
            Console.WriteLine($"{secondPlayerName} gewinnt das 1vs1!");
        else
            Console.WriteLine("Es ist ein Unentschieden!");
    }
}
