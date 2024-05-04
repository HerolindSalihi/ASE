public class TeamSearch
{
    private string[] lines;
    private string[] headers;

    public TeamSearch(string[] lines, string[] headers)
    {
        this.lines = lines;
        this.headers = headers;
    }

    public void SearchByTeam()
    {
        Console.Write("Gib den Teamnamen ein: ");
        string teamName = Console.ReadLine();
        var teamPlayers = lines.Skip(1).Select(line => line.Split(',')).Where(columns => columns[4].Equals(teamName, StringComparison.OrdinalIgnoreCase)).ToList();
        if (teamPlayers.Count == 0)
        {
            Console.WriteLine("Keine Spieler gefunden.");
        }
        else
        {
            Console.WriteLine($"Spieler im Team {teamName}:");
            foreach (var player in teamPlayers)
            {
                Console.WriteLine(player[1]);
            }
        }
    }
}