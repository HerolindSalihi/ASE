using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class GuessThePlayerGame : IGame
{
    private List<string[]> playersData;
    private string[] headers;

    public GuessThePlayerGame(string filePath)
    {
        playersData = new List<string[]>();
        headers = Array.Empty<string>();
        LoadData(filePath);
    }

    private void LoadData(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length < 2)
            {
                throw new InvalidDataException($"Die Datei '{filePath}' enthält nicht genügend Daten.");
            }

            headers = lines[0].Split(',');
            playersData = lines.Skip(1).Select(line => line.Split(',')).ToList();
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError("Fehler beim Laden der Daten", ex);
        }
    }

    public void Start()
    {
        Play();
    }

    private void Play()
    {
        try
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
                string? guess = Console.ReadLine();

                if (guess != null && guess.Equals(player[1], StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Richtig! Du hast den Spieler erraten.");
                    return;
                }
                else if (guess != null && guess.Equals("weiter", StringComparison.OrdinalIgnoreCase))
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
        catch (Exception ex)
        {
            ErrorHandler.HandleError("Ein Fehler ist aufgetreten", ex);
        }
    }
}
