using System;

public static class ErrorHandler
{
    // Diese Methode handelt Fehler, indem sie sie auf der Konsole ausgibt.
    // Du könntest hier auch eine Erweiterung vornehmen, um Fehler in einer Datei zu loggen oder an einen externen Fehlerbehandlungsdienst zu senden.
    public static void HandleError(string message, Exception ex)
    {
        // Zeigt eine benutzerfreundliche Nachricht und die technischen Details der Ausnahme
        Console.WriteLine($"{message}. Details: {ex.Message}");

        // Optional: Weitere Aktionen bei bestimmten Arten von Ausnahmen
        if (ex is FileNotFoundException)
        {
            Console.WriteLine("Bitte überprüfe, ob die Dateipfade korrekt sind.");
        }

        // Logik zum Beenden des Programms oder Fortsetzen, je nach Schwere des Fehlers
        // Beispiel: Umgebung spezifisch beenden, wenn kritisch
        // Environment.Exit(1);
    }
}

