using System;
using System.Collections.Generic;
using System.Linq;

public class GuessThePosition
{
    private List<string[]> players;
    private int score = 0;

    public GuessThePosition(string[] lines)
    {
        players = lines.Skip(1).Select(line => line.Split(',')).ToList();
    }

    public void Play()
    {
        Console.WriteLine("Spiel start: Errate die Position des Spielers!");

        for (int i = 0; i < 20; i++)
        {
            var player = players.OrderBy(x => Guid.NewGuid()).First();
            Console.WriteLine($"Spieler: {player[1]}");  // Spielername ist an Index 1

            Console.Write("Welche Position spielt dieser Spieler? ");
            string userGuess = Console.ReadLine();
            if (userGuess.Equals(player[2], StringComparison.OrdinalIgnoreCase))  // Position ist an Index 2
            {
                score++;
                Console.WriteLine("Richtig!");
            }
            else
            {
                Console.WriteLine($"Falsch! Die richtige Antwort ist: {player[2]}");
            }
        }

        Console.WriteLine($"Spiel beendet. Deine Punktzahl: {score}/20");
    }
}