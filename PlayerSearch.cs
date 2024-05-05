public class PlayerSearch
{
    private string[] lines;
    private string[] headers;

    // Konstruktor erhält DataStore-Instanz
    public PlayerSearch(DataStore dataStore)
    {
        this.lines = dataStore.Lines;
        this.headers = dataStore.Headers;
    }

    public void SearchByPlayer()
    {
        Console.Write("Gib den Namen des Spielers ein: ");
        string? playerName = Console.ReadLine();
        var playerData = lines.Skip(1).Select(line => line.Split(',')).FirstOrDefault(columns => columns.Length > 1 && columns[1].Equals(playerName, StringComparison.OrdinalIgnoreCase));

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
