using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class GuessThePlayerGame
{
    private List<string[]> playersData;
    private string[] headers;

    public GuessThePlayerGame(string filePath)
    {
        LoadData(filePath);
    }

    private void LoadData(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        headers = lines[0].Split(',');  
        playersData = lines.Skip(1).Select(line => line.Split(',')).ToList();
    }

    public void Play()
    {
        Random random = new Random();
        var player = playersData[random.Next(playersData.Count)];
        List<int> availableIndexes = Enumerable.Range(0, headers.Length).ToList();  // Indizes aller Spalten

        Console.WriteLine("Guess The Player: Rate den Namen des Spielers basierend auf den folgenden Fakten.");

        while (availableIndexes.Count > 0)
        {
            int factIndex = random.Next(availableIndexes.Count);
            int columnIndex = availableIndexes[factIndex];
            availableIndexes.RemoveAt(factIndex);  // Entferne den Index, damit der Fakt nicht wiederholt wird

            Console.WriteLine($"{headers[columnIndex]}: {player[columnIndex]}");
            Console.Write("Rate den Namen des Spielers oder gib 'weiter' ein, um einen weiteren Fakt zu erhalten: ");
            string guess = Console.ReadLine();

            if (guess.Equals(player[1], StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Richtig! Du hast den Spieler erraten.");
                return;
            }
            else if (guess.Equals("weiter", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }
            else
            {
                Console.WriteLine("Falsch. Versuche es noch einmal oder gib 'weiter' ein.");
            }
        }

        Console.WriteLine($"Keine weiteren Fakten verfügbar. Der Spieler war: {player[1]}");
    }
}
