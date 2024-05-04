public class OneVsOneSimulator
{
    private string[] lines;
    private string[] headers;

    public OneVsOneSimulator(string[] lines, string[] headers)
    {
        this.lines = lines;
        this.headers = headers;
    }

    public void SimulateOneVsOne()
    {
        Console.Write("Gib den Namen des ersten Spielers ein: ");
        string firstPlayerName = Console.ReadLine();
        var firstPlayerData = lines.Skip(1).Select(line => line.Split(',')).FirstOrDefault(columns => columns[1].Equals(firstPlayerName, StringComparison.OrdinalIgnoreCase));

        Console.Write("Gib den Namen des zweiten Spielers ein: ");
        string secondPlayerName = Console.ReadLine();
        var secondPlayerData = lines.Skip(1).Select(line => line.Split(',')).FirstOrDefault(columns => columns[1].Equals(secondPlayerName, StringComparison.OrdinalIgnoreCase));

        if (firstPlayerData == null || secondPlayerData == null)
        {
            Console.WriteLine("Mindestens einer der Spieler wurde nicht gefunden.");
            return;
        }

        // Statistiken, die verglichen werden sollen
        string[] stats = { "3P%", "FG%", "2P%", "STL", "BLK", "PTS", "PF" };
        int firstPlayerWins = 0;
        int secondPlayerWins = 0;

        Console.WriteLine("\n1vs1 Ergebnisse:");
        foreach (var stat in stats)
        {
            int index = Array.IndexOf(headers, stat);
            decimal firstPlayerStat = Convert.ToDecimal(firstPlayerData[index]);
            decimal secondPlayerStat = Convert.ToDecimal(secondPlayerData[index]);

            Console.WriteLine($"{stat}: {firstPlayerName} = {firstPlayerStat}, {secondPlayerName} = {secondPlayerStat}");

            if (firstPlayerStat > secondPlayerStat)
                firstPlayerWins++;
            else if (secondPlayerStat > firstPlayerStat)
                secondPlayerWins++;
        }

        Console.WriteLine($"\nErgebnisse von {firstPlayerName} vs {secondPlayerName}:");
        Console.WriteLine($"{firstPlayerName} Siege: {firstPlayerWins}");
        Console.WriteLine($"{secondPlayerName} Siege: {secondPlayerWins}");

        if (firstPlayerWins > secondPlayerWins)
            Console.WriteLine($"{firstPlayerName} gewinnt das 1vs1!");
        else if (secondPlayerWins > firstPlayerWins)
            Console.WriteLine($"{secondPlayerName} gewinnt das 1vs1!");
        else
            Console.WriteLine("Es ist ein Unentschieden!");
    }
}