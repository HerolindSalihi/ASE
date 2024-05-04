using System;
using System.Collections.Generic;
using System.Linq;

public class DraftTeam
{
    private List<string[]> availablePlayers;
    private List<string[]> chosenPlayers;
    private string[] headers;
    private int championshipsWon = 0;

    public DraftTeam(string[] lines, string[] headers)
    {
        this.headers = headers;
        availablePlayers = lines.Skip(1).Select(line => line.Split(',')).ToList();
        chosenPlayers = new List<string[]>();
    }

    public void StartDraft()
    {
        Console.WriteLine("Willkommen zum Spieler-Draft! Wähle 10 Spieler für dein Team.");
        for (int season = 1; season <= 10; season++)
        {
            if (season > 1) // Nicht in der ersten Saison draften
            {
                PerformMidSeasonDraft();
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    DraftPlayer();
                }
            }

            SimulateSeason();
            if (season < 10)
            {
                PerformMidSeasonDraft();
            }
        }

        Console.WriteLine($"\nDu hast {championshipsWon} Mal die NBA-Meisterschaft gewonnen!");
    }

    private void DraftPlayer()
    {
        var roundPlayers = GetRandomPlayers(5);
        Console.WriteLine("\nWähle einen Spieler aus den folgenden Optionen:");
        for (int i = 0; i < roundPlayers.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {roundPlayers[i][1]}"); // Annahme: Index 1 ist der Spielername
        }

        int choice = GetUserChoice(roundPlayers.Count);
        chosenPlayers.Add(roundPlayers[choice - 1]);
        availablePlayers.Remove(roundPlayers[choice - 1]);

        Console.WriteLine($"{roundPlayers[choice - 1][1]} wurde zu deinem Team hinzugefügt.");
    }

    private void SimulateSeason()
    {
        Console.WriteLine("\nSaison Start...");
        Random rnd = new Random();
        int result = rnd.Next(1, 5); // 1 bis 4, wobei 4 bedeutet, dass sie gewonnen haben
        if (result == 4)
        {
            championshipsWon++;
            Console.WriteLine("Sie sind NBA Champion!");
        }
        else
        {
            Console.WriteLine($"Sie haben es nur in die Playoffs {result}. Runde geschafft.");
        }
        Console.WriteLine("Saison Ende.");
    }

    private void PerformMidSeasonDraft()
    {
        Console.WriteLine("\nMid-Season Draft: Wähle einen neuen Spieler oder keinen.");
        var roundPlayers = GetRandomPlayers(2);
        Console.WriteLine("0: Keinen Spieler auswählen.");
        for (int i = 0; i < roundPlayers.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {roundPlayers[i][1]}");
        }

        int choice = GetUserChoice(roundPlayers.Count + 1);
        if (choice != 0)
        {
            chosenPlayers.Add(roundPlayers[choice - 1]);
            availablePlayers.Remove(roundPlayers[choice - 1]);
            Console.WriteLine($"Du hast {roundPlayers[choice - 1][1]} ausgewählt. Bitte wähle einen Spieler zum Entfernen.");
            RemovePlayer();
        }
    }

    private void RemovePlayer()
    {
        Console.WriteLine("Wähle einen Spieler aus deinem Team zum Entfernen:");
        for (int i = 0; i < chosenPlayers.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {chosenPlayers[i][1]}");
        }

        int playerToRemove = GetUserChoice(chosenPlayers.Count);
        Console.WriteLine($"{chosenPlayers[playerToRemove - 1][1]} wurde aus deinem Team entfernt.");
        chosenPlayers.RemoveAt(playerToRemove - 1);
    }

    private List<string[]> GetRandomPlayers(int count)
    {
        return availablePlayers.OrderBy(x => Guid.NewGuid()).Take(count).ToList();
    }

    private int GetUserChoice(int maxOption)
    {
        int selectedOption = 0;
        while (true)
        {
            Console.Write("Wähle einen Spieler (0-" + (maxOption - 1) + "): ");
            if (int.TryParse(Console.ReadLine(), out selectedOption) && selectedOption >= 0 && selectedOption <= maxOption)
            {
                return selectedOption;
            }
            Console.WriteLine("Ungültige Eingabe, bitte versuche es erneut.");
        }
    }
}