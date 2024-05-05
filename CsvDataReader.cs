// Klasse zum Lesen von Daten aus einer CSV-Datei
public class CsvDataReader
{
    private string filePath; // Pfad zur CSV-Datei

    // Konstruktor
    public CsvDataReader(string filePath)
    {
        this.filePath = filePath;
    }

    // Methode zum Lesen aller Daten aus der CSV-Datei
    public List<string[]> ReadData()
    {
        List<string[]> data = new List<string[]>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Die Datei '{filePath}' wurde nicht gefunden.");
            return data;
        }

        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            Console.WriteLine($"Die Datei '{filePath}' enthält keine Daten.");
            return data;
        }

        foreach (string line in lines)
        {
            string[] fields = line.Split(',');
            data.Add(fields);
        }

        return data;
    }

    // Methode zum Lesen der Kopfzeile aus der CSV-Datei
    public string[] ReadHeaderRow()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Die Datei '{filePath}' wurde nicht gefunden.");
            return new string[0];
        }

        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            Console.WriteLine($"Die Datei '{filePath}' enthält keine Daten.");
            return new string[0];
        }

        return lines.First().Split(',');
    }

    // Methode zum Lesen aller Zeilen aus der CSV-Datei, außer der Kopfzeile
    public List<string[]> ReadAllRows()
    {
        List<string[]> data = new List<string[]>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Die Datei '{filePath}' wurde nicht gefunden.");
            return data;
        }

        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            Console.WriteLine($"Die Datei '{filePath}' enthält keine Daten.");
            return data;
        }

        foreach (string line in lines.Skip(1))
        {
            string[] fields = line.Split(',');
            data.Add(fields);
        }

        return data;
    }
}
