public class CreateDreamTeam
{
    private string[] lines;
    private Safe<string[]> headers;

    public CreateDreamTeam(string[] lines, Safe<string[]> headers)
    {
        this.lines = lines;
        this.headers = headers;
    }

    public void FormDreamTeam()
    {
        List<string> selectedPlayers = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            Console.Write($"Gib den Namen des Spielers {i + 1} ein: ");
            string? playerName = Console.ReadLine(); // playerName als nullable deklarieren
            if (string.IsNullOrEmpty(playerName))
            {
                Console.WriteLine("Ungültige Eingabe. Bitte gib den Namen erneut ein.");
                i--;
                continue;
            }
            if (selectedPlayers.Contains(playerName))
            {
                Console.WriteLine("Dieser Spieler wurde bereits ausgewählt, bitte wähle einen anderen Spieler.");
                i--;
                continue;
            }
            var playerData = lines.Skip(1).Select(line => line.Split(',')).FirstOrDefault(columns => columns.Length > 1 && columns[1].Equals(playerName, StringComparison.OrdinalIgnoreCase));
            if (playerData == null)
            {
                Console.WriteLine("Keine Daten gefunden, bitte erneut versuchen.");
                i--;
            }
            else
            {
                selectedPlayers.Add(playerName); // playerName ist nicht null, da es zuvor überprüft wurde
                Console.WriteLine($"{playerName} wurde zum Dream Team hinzugefügt.");
            }
        }
    }
}
