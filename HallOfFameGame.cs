using System;
using System.Collections.Generic;
using System.Linq;

public class HallOfFameGame : IGame
{
    private List<string[]> playersData;  // Speichert Spielerdaten nach dem Laden
    private HashSet<string> guessedPlayers = new HashSet<string>();  // Verfolgt die geratenen Spieler

    // Konstruktor erhält DataStore als Argument anstelle eines Dateipfads
    public HallOfFameGame(DataStore dataStore)
    {
        playersData = new List<string[]>();
        LoadData(dataStore);  // Laden der Daten aus DataStore
    }

    // Lädt die Daten aus dem DataStore
    private void LoadData(DataStore dataStore)
    {
        if (dataStore.Lines == null || dataStore.Lines.Length < 2)
        {
            throw new InvalidOperationException("Nicht genügend Daten im DataStore vorhanden.");
        }

        // Verarbeitung der Datenzeilen
        playersData = dataStore.Lines.Skip(1)  // Kopfzeile überspringen
                            .Select(line => line.Split(','))
                            .Where(fields => fields.Length > 2 && double.TryParse(fields[2], out _))  // Sicherstellen, dass die Konvertierung möglich ist
                            .OrderByDescending(fields => double.Parse(fields[2]))  // Sortierung nach der Wahrscheinlichkeit (angenommen Index 2)
                            .Take(25)  // Top 25 Kandidaten
                            .ToList();
    }

    public void Start()
    {
        try
        {
            Console.WriteLine("Willkommen zum Hall of Fame Erratespiel!");
            Console.WriteLine("Versuche, die Top 25 Spieler mit der höchsten Wahrscheinlichkeit für die Hall of Fame zu erraten.");

            while (guessedPlayers.Count < 25)
            {
                Console.WriteLine("\nGib den Namen eines Spielers ein:");
                string? playerName = Console.ReadLine()?.Trim();

                if (playerName != null && playersData.Any(p => p[1].Equals(playerName, StringComparison.OrdinalIgnoreCase) && guessedPlayers.Add(playerName)))
                {
                    Console.WriteLine("Richtig! " + playerName + " ist einer der Top 25 Kandidaten.");
                }
                else
                {
                    Console.WriteLine("Entweder falsch oder bereits erraten.");
                }

                DisplayGuessedPlayers();
            }

            Console.WriteLine("\nGlückwunsch! Du hast alle Top 25 Spieler erraten.");
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError("Ein Fehler ist aufgetreten", ex);
        }
    }

    private void DisplayGuessedPlayers()
    {
        Console.WriteLine("\nBisher erratene Spieler:");
        foreach (var player in playersData.Where(p => guessedPlayers.Contains(p[1])))
        {
            Console.WriteLine(player[1]);
        }
    }
}
