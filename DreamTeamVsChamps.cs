using System;
using System.Collections.Generic;
using System.Linq;

public class DreamTeamVsChamps : IGame
{
    private List<string[]> players;
    private string[] headers;

    // Konstruktor erhält DataStore-Instanz
    public DreamTeamVsChamps(DataStore dataStore)
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
        PlayAgainstChamps();
    }

    private void PlayAgainstChamps()
    {
        string champsTeam = "DEN";  // Champion-Team, z.B. "DEN" für Denver Nuggets
        var champPlayers = players
                            .Where(columns => columns.Length > 4 && columns[4].Equals(champsTeam, StringComparison.OrdinalIgnoreCase))
                            .OrderByDescending(columns => decimal.Parse(columns.Last()))
                            .Take(5)
                            .ToList();

        decimal champsScore = champPlayers.Sum(player => decimal.Parse(player.Last()));
        Console.WriteLine($"Gesamtpunktzahl für {champsTeam} Champs: {champsScore}");

        List<decimal> dreamTeamScores = new List<decimal>();
        for (int i = 0; i < 5; i++)
        {
            Console.Write($"Gib den Namen des Spielers {i + 1} für das Dream Team ein: ");
            string? playerName = Console.ReadLine();
            if (string.IsNullOrEmpty(playerName))
            {
                Console.WriteLine("Kein Spielername eingegeben, bitte erneut versuchen.");
                i--;
                continue;
            }

            var playerData = players
                                .FirstOrDefault(columns => columns.Length > 1 && columns[1].Equals(playerName, StringComparison.OrdinalIgnoreCase));

            if (playerData == null)
            {
                Console.WriteLine("Spieler nicht gefunden, bitte erneut versuchen.");
                i--; // Schritt wiederholen
                continue;
            }

            if (decimal.TryParse(playerData.Last(), out decimal playerScore))
            {
                dreamTeamScores.Add(playerScore);
                Console.WriteLine($"{playerName} hinzugefügt mit {playerScore} PTS.");
            }
            else
            {
                Console.WriteLine("Fehler beim Lesen der Spielerpunkte.");
                i--;
            }
        }

        decimal dreamTeamScore = dreamTeamScores.Sum();
        Console.WriteLine($"Gesamtpunktzahl für Dream Team: {dreamTeamScore}");

        if (dreamTeamScore > champsScore)
        {
            Console.WriteLine("Das Dream Team hat gewonnen!");
        }
        else
        {
            Console.WriteLine("Das Dream Team hat verloren.");
        }
    }
}
