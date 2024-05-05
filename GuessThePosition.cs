using System;
using System.Collections.Generic;
using System.Linq;

public class GuessThePosition : IGame
{
    private List<string[]> players;
    private int score = 0;

    // Anpassen des Konstruktors, um DataStore zu akzeptieren
    public GuessThePosition(DataStore dataStore)
    {
        players = new List<string[]>();
        // Methode, um Daten aus DataStore zu laden
        LoadData(dataStore);
    }

    // Methode zum Laden der Daten aus dem DataStore
    private void LoadData(DataStore dataStore)
    {
        // Sicherstellen, dass Lines nicht null und ausreichend Daten enthalten sind
        if (dataStore.Lines == null || dataStore.Lines.Length < 2)
        {
            throw new InvalidOperationException("Nicht genügend Daten im DataStore vorhanden.");
        }

        // Überspringen der Kopfzeile und Verarbeiten der restlichen Zeilen
        players = dataStore.Lines.Skip(1).Select(line => line.Split(',')).ToList();
    }

    public void Start()
    {
        try
        {
            Console.WriteLine("Spiel start: Errate die Position des Spielers!");

            for (int i = 0; i < 20; i++)
            {
                var player = players.OrderBy(x => Guid.NewGuid()).First();
                Console.WriteLine($"Spieler: {player[1]}");  // Spielername ist an Index 1

                Console.Write("Welche Position spielt dieser Spieler? ");
                string? userGuess = Console.ReadLine();

                if (userGuess != null && userGuess.Equals(player.ElementAtOrDefault(2), StringComparison.OrdinalIgnoreCase))  // Position ist an Index 2
                {
                    score++;
                    Console.WriteLine("Richtig!");
                }
                else
                {
                    Console.WriteLine($"Falsch! Die richtige Antwort ist: {player.ElementAtOrDefault(2)}");
                }
            }

            Console.WriteLine($"Spiel beendet. Deine Punktzahl: {score}/20");
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError("Ein Fehler ist aufgetreten", ex);
        }
    }
}
