public class PlayerSearch
{
    private string[] lines;
    private string[] headers;

    public PlayerSearch(string[] lines, string[] headers)
    {
        this.lines = lines;
        this.headers = headers;
    }

    public void SearchByPlayer()
    {
        Console.Write("Gib den Namen des Spielers ein: ");
        string? playerName = Console.ReadLine();
        var playerData = lines.Skip(1).Select(line => line.Split(',')).FirstOrDefault(columns => columns[1].Equals(playerName, StringComparison.OrdinalIgnoreCase));
        if (playerData == null)
        {
            Console.WriteLine("Keine Daten gefunden.");
        }
        else
        {
            Console.WriteLine($"Daten für {playerName}:");
            for (int i = 0; i < headers.Length; i++)
            {
                Console.WriteLine($"{headers[i]}: {playerData[i]}");
            }
        }
    }
}