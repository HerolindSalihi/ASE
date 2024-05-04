public class DreamTeamVsChamps
{
    private string[] lines;
    private string[] headers;

    public DreamTeamVsChamps(string[] lines, string[] headers)
    {
        this.lines = lines;
        this.headers = headers;
    }

    public void PlayAgainstChamps()
    {
        // Definieren Sie das Champion-Team (z.B. "DEN" für Denver Nuggets)
        string champsTeam = "DEN";
        var champPlayers = lines.Skip(1)
                                .Select(line => line.Split(','))
                                .Where(columns => columns[4].Equals(champsTeam))
                                .OrderByDescending(columns => decimal.Parse(columns.Last()))
                                .Take(5)
                                .ToList();

        decimal champsScore = champPlayers.Sum(player => decimal.Parse(player.Last()));
        Console.WriteLine($"Gesamtpunktzahl für {champsTeam} Champs: {champsScore}");

        // Lassen Sie den Benutzer das Dream Team zusammenstellen
        List<decimal> dreamTeamScores = new List<decimal>();
        for (int i = 0; i < 5; i++)
        {
            Console.Write($"Gib den Namen des Spielers {i + 1} für das Dream Team ein: ");
            string playerName = Console.ReadLine();
            var playerData = lines.Skip(1)
                                  .Select(line => line.Split(','))
                                  .FirstOrDefault(columns => columns[1].Equals(playerName, StringComparison.OrdinalIgnoreCase));

            if (playerData == null)
            {
                Console.WriteLine("Spieler nicht gefunden, bitte erneut versuchen.");
                i--; // Schritt wiederholen
            }
            else
            {
                decimal playerScore = decimal.Parse(playerData.Last());
                dreamTeamScores.Add(playerScore);
                Console.WriteLine($"{playerName} hinzugefügt mit {playerScore} PTS.");
            }
        }

        decimal dreamTeamScore = dreamTeamScores.Sum();
        Console.WriteLine($"Gesamtpunktzahl für Dream Team: {dreamTeamScore}");

        // Ergebnis des Spiels
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