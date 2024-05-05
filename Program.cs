using System;

class Program
{
    static void Main(string[] args)
    {
        // Erzeuge die Hauptanwendungskontrolle
        GameController controller = new GameController();
        controller.Initialize(); // Initialisiere Daten und Konfigurationen
        controller.RunMainMenu(); // Starte das Hauptmenü
    }
}
