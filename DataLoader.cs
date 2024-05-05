using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class DataLoader
{
    private string filePath;
    private Encoding fileEncoding;

    // Konstruktor setzt den Dateipfad und optional die Kodierung
    public DataLoader(string filePath, Encoding encoding = null)
    {
        this.filePath = filePath;
        this.fileEncoding = encoding ?? Encoding.UTF8; // Standardmäßig UTF-8, wenn nichts anderes angegeben wird
    }

    // Methode zum Laden der Daten mit dem neuen Design
    public IEnumerable<string[]> LoadData(char delimiter = ',', bool hasHeader = true)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Die Datei '{filePath}' wurde nicht gefunden.");
        }

        string[] lines;
        try
        {
            lines = File.ReadAllLines(filePath, fileEncoding);
        }
        catch (Exception ex)
        {
            throw new Exception("Fehler beim Lesen der Datei: " + ex.Message);
        }

        if (hasHeader)
        {
            lines = lines.Skip(1).ToArray(); // Überspringe die Kopfzeile, wenn vorhanden
        }

        return lines.Select(line => line.Split(delimiter));
    }

    // Überladene Methode zur Rückwärtskompatibilität, um die Signatur der ursprünglichen LoadData Methode zu erhalten
    public IEnumerable<string[]> LoadData(string filePath, char delimiter = ',', bool hasHeader = true)
    {
        this.filePath = filePath; // Setzt den Dateipfad neu
        return LoadData(delimiter, hasHeader); // Ruft die überladene Methode auf
    }
}
