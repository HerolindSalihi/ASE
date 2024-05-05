public class DataStore
{
    public string[] Lines { get; private set; } = Array.Empty<string>();
    public string[] Headers { get; private set; } = Array.Empty<string>();
    private string filePath;

    public DataStore(string filePath)
    {
        this.filePath = filePath;
        LoadData();
    }

    private void LoadData()
    {
        try
        {
            Lines = File.ReadAllLines(filePath);
            Headers = Lines.Length > 0 ? Lines[0].Split(',') : Array.Empty<string>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ein Fehler ist aufgetreten beim Lesen der Datei: {filePath}\n{ex.Message}");
            Lines = Array.Empty<string>();  // Vermeide null, indem leere Arrays zugewiesen werden
            Headers = Array.Empty<string>();
        }
    }
}