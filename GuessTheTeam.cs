using System;
using System.Collections.Generic;
using System.Linq;

public class GuessTheTeam
{
    private List<string[]> players;
    private int score = 0;

    public GuessTheTeam(string[] lines)
    {
        players = lines.Skip(1).Select(line => line.Split(',')).ToList();
    }

    public void Play()
    {
        Console.WriteLine("Spiel start: Errate das Team des Spielers!");

        for (int i = 0; i < 20; i++)
        {
            var player = players.OrderBy(x => Guid.NewGuid()).First();
            Console.WriteLine($"Spieler: {player[1]}");  // Spielername ist an Index 1

            Console.Write("Für welches Team spielt dieser Spieler? ");
            string userGuess = Console.ReadLine();
            if (userGuess.Equals(player[4], StringComparison.OrdinalIgnoreCase))  // Team ist an Index 4
            {
                score++;
                Console.WriteLine("Richtig!");
            }
            else
            {
                Console.WriteLine($"Falsch! Die richtige Antwort ist: {player[4]}");
            }
        }

        Console.WriteLine($"Spiel beendet. Deine Punktzahl: {score}/20");
    }
}