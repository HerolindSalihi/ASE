public class GameController
{
    // Speichert die Daten, die über das Programm verwendet werden
    private DataStore dataStore;

    public void Initialize()
    {
        // Lade Daten
        string filePath = "C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/nba_stats.csv";
        dataStore = new DataStore(filePath); // Initialisiert den DataStore mit Dateipfad
    }

    // Hauptmenü des Spiels
    public void RunMainMenu()
    {
        string input;
        do
        {
            // Hauptmenü anzeigen
            Console.WriteLine("\nHauptmenü:");
            Console.WriteLine("1: Infos");
            Console.WriteLine("2: Guessing Games");
            Console.WriteLine("3: Simulations");
            Console.WriteLine("exit: Beenden");
            Console.Write("Wähle eine Option: ");
            input = Console.ReadLine().ToLower();

            switch (input)
            {
                case "1":
                    ShowInfoMenu(); // Informationen anzeigen
                    break;
                case "2":
                    ShowGuessingGamesMenu(); // Rate-Spiele anzeigen
                    break;
                case "3":
                    ShowSimulationsMenu(); // Simulationsspiele anzeigen
                    break;
                case "exit":
                    Console.WriteLine("Programm wird beendet.");
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe, bitte erneut versuchen.");
                    break;
            }
        } while (input != "exit");
    }

    // Menü für Informationen anzeigen
    public void ShowInfoMenu()
    {
        string input;
        do
        {
            // Info-Menü anzeigen
            Console.WriteLine("\nInfos-Menu:");
            Console.WriteLine("1: Matchup History");
            Console.WriteLine("2: Player Search");
            Console.WriteLine("3: Team Schedule");
            Console.WriteLine("4: Team Search");
            Console.WriteLine("5: Top Ten Players by Stat");
            Console.WriteLine("6: Division Standings");
            Console.WriteLine("back: Zurück zum Hauptmenü");
            Console.Write("Wähle eine Option: ");
            input = Console.ReadLine().ToLower();

            switch (input)
            {
                case "1":
                    var matchupHistory = new MatchupHistory(dataStore);
                    matchupHistory.DisplayMatchups(); // Anzeige der Spielhistorie
                    break;
                case "2":
                    var playerSearch = new PlayerSearch(dataStore);
                    playerSearch.SearchByPlayer(); // Spieler-Suche
                    break;
                case "3":
                    var teamSchedule = new TeamSchedule(dataStore);
                    teamSchedule.Start(); // Anzeige des Spielplans eines Teams
                    break;
                case "4":
                    var teamSearch = new TeamSearch(dataStore);
                    teamSearch.Start(); // Teamsuche
                    break;
                case "5":
                    var topTenPlayersByStat = new TopTenPlayersByStat(dataStore);
                    topTenPlayersByStat.ShowTopTenPlayersByStat(); // Anzeige der Top 10 Spieler nach Statistik
                    break;
                case "6":
                    var divisionStandings = new DivisionStandings(dataStore);
                    divisionStandings.DisplayStandings(); // Anzeige der Divisionstabelle
                    break;
                case "back":
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe, bitte erneut versuchen.");
                    break;
            }
        } while (input != "back");
    }

    // Menü für Rate-Spiele anzeigen
    public void ShowGuessingGamesMenu()
    {
        string input;
        do
        {
            // Rate-Spiele-Menü anzeigen
            Console.WriteLine("\nGuessing Games-Menu:");
            Console.WriteLine("1: All Star MVP Game");
            Console.WriteLine("2: All Time Points Game");
            Console.WriteLine("3: Championships Game");
            Console.WriteLine("4: Defensive Player of the Year Game");
            Console.WriteLine("5: Finals MVP Game");
            Console.WriteLine("6: Guess The Player Game");
            Console.WriteLine("7: Guess The Position Game");
            Console.WriteLine("8: Guess The Team Game");
            Console.WriteLine("9: Hall of Fame Game");
            Console.WriteLine("10: Most Improved Player Game");
            Console.WriteLine("11: MVP Game");
            Console.WriteLine("12: NBA Divisions Game");
            Console.WriteLine("13: Rookie of the Year Game");
            Console.WriteLine("14: Sixth Man of the Year Game");
            Console.WriteLine("15: Statistics Comparison Game");
            Console.WriteLine("back: Zurück zum Hauptmenü");
            Console.Write("Wähle eine Option: ");
            input = Console.ReadLine().ToLower();

            switch (input)
            {
                case "1":
                    var amvpGame = new AllStarMVPGame(dataStore);
                    amvpGame.Start(); // Start des All-Star MVP Spiels
                    break;
                case "2":
                    var allTimePointsGame = new AllTimePointsGame(dataStore);
                    allTimePointsGame.Start(); // Start des All-Time Points Spiels
                    break;
                case "3":
                    var championshipsGame = new ChampionshipsGame(dataStore);
                    championshipsGame.Start(); // Start des Championships Spiels
                    break;
                case "4":
                    var dpoyGame = new DefensivePlayerOfTheYearGame(dataStore);
                    dpoyGame.Start(); // Start des Defensive Player of the Year Spiels
                    break;
                case "5":
                    var fmvpGame = new FinalsMVPGame(dataStore);
                    fmvpGame.Start(); // Start des Finals MVP Spiels
                    break;
                case "6":
                    var guessThePlayerGame = new GuessThePlayerGame(dataStore);
                    guessThePlayerGame.Start(); // Start des Guess The Player Spiels
                    break;
                case "7":
                    var guessThePosition = new GuessThePosition(dataStore);
                    guessThePosition.Start(); // Start des Guess The Position Spiels
                    break;
                case "8":
                    var guessTheTeam = new GuessTheTeam(dataStore);
                    guessTheTeam.Start(); // Start des Guess The Team Spiels
                    break;
                case "9":
                    var hallOfFameGame = new HallOfFameGame(dataStore);
                    hallOfFameGame.Start(); // Start des Hall of Fame Spiels
                    break;
                case "10":
                    var mipGame = new MostImprovedPlayerGame(dataStore);
                    mipGame.Start(); // Start des Most Improved Player Spiels
                    break;
                case "11":
                    var mvpGame = new MVPGame(dataStore);
                    mvpGame.Start(); // Start des MVP Spiels
                    break;
                case "12":
                    var nbaDivisionsGame = new NBAdivisionsGame(dataStore);
                    nbaDivisionsGame.Start(); // Start des NBA Divisions Spiels
                    break;
                case "13":
                    var rookieGame = new RookieOfTheYearGame(dataStore);
                    rookieGame.Start(); // Start des Rookie of the Year Spiels
                    break;
                case "14":
                    var sixthManGame = new SixthManOfTheYearGame(dataStore);
                    sixthManGame.Start(); // Start des Sixth Man of the Year Spiels
                    break;
                case "15":
                    var statisticsGame = new StatisticsComparisonGame(dataStore);
                    statisticsGame.Start(); // Start des Statistics Comparison Spiels
                    break;
                case "back":
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe, bitte erneut versuchen.");
                    break;
            }
        } while (input != "back");
    }

    // Menü für Simulations-Spiele anzeigen
    public void ShowSimulationsMenu()
    {
        string input;
        do
        {
            // Simulations-Spiele-Menü anzeigen
            Console.WriteLine("\nSimulations-Menu:");
            Console.WriteLine("1: Create Dream Team");
            Console.WriteLine("2: Draft Team");
            Console.WriteLine("3: Dream Team vs Champs");
            Console.WriteLine("4: One vs One Simulator");
            Console.WriteLine("back: Zurück zum Hauptmenü");
            Console.Write("Wähle eine Option: ");
            input = Console.ReadLine().ToLower();

            switch (input)
            {
                case "1":
                    var createDreamTeam = new CreateDreamTeam(dataStore);
                    createDreamTeam.Start(); // Start der Dream Team Erstellung
                    break;
                case "2":
                    var draftTeam = new DraftTeam(dataStore);
                    draftTeam.Start(); // Start des Draft Teams
                    break;
                case "3":
                    var dreamTeamVsChamps = new DreamTeamVsChamps(dataStore);
                    dreamTeamVsChamps.Start(); // Start des Dream Team vs Champs Spiels
                    break;
                case "4":
                    var oneVsOneSimulator = new OneVsOneSimulator(dataStore);
                    oneVsOneSimulator.Start(); // Start des One vs One Simulators
                    break;
                case "back":
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe, bitte erneut versuchen.");
                    break;
            }
        } while (input != "back");
    }
}
