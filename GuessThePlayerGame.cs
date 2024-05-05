using System;
using System.Collections.Generic;
using System.Linq;

public class GuessThePlayerGame : IGame  // Implementiert das IGame Interface
{
    private List<string[]> playersData; // Speichert Spielerdaten nach dem Laden
    private string[] headers; // Speichert die Kopfzeilen der Daten, d.h. die Spaltennamen

    // Konstruktor, der DataStore als Datenquelle verwendet
    public GuessThePlayerGame(DataStore dataStore)
    {
        playersData = new List<string[]>();
        headers = Array.Empty<string>();
        LoadData(dataStore); // Lädt die Daten beim Erstellen der Instanz
    }

    // Lädt Daten aus dem DataStore
    private void LoadData(DataStore dataStore)
    {
        if (dataStore.Lines == null || dataStore.Lines.Length < 2)
        {
            throw new InvalidDataException("Die Datenquelle enthält nicht genügend Daten.");
        }

        headers = dataStore.Lines[0].Split(','); // Erste Zeile als Kopfzeilen verwenden
        playersData = dataStore.Lines.Skip(1).Select(line => line.Split(',')).ToList(); // Restliche Zeilen als Daten speichern
    }

    // Startet das Ratespiel
    public void Start()
    {
        Play();
    }

    // Kernfunktion des Spiels
    private void Play()
    {
        try
        {
            Random random = new Random();
            var player = playersData[random.Next(playersData.Count)]; // Zufälligen Spieler auswählen
            List<int> availableIndexes = Enumerable.Range(0, headers.Length).ToList(); // Liste aller möglichen Indexe für Spalten

            Console.WriteLine("Guess The Player: Rate den Namen des Spielers basierend auf den folgenden Fakten.");

            // Bietet dem Spieler Fakten, um den Namen des Spielers zu erraten
            while (availableIndexes.Count > 0)
            {
                int factIndex = random.Next(availableIndexes.Count);
                int columnIndex = availableIndexes[factIndex];
                availableIndexes.RemoveAt(factIndex); // Verbrauchten Index entfernen

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
