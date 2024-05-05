public class GameController
{
    // Speichere die Daten, die über das Programm verwendet werden
    private DataStore dataStore;

    public void Initialize()
    {
        // Lade Daten
        string filePath = "C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/nba_stats.csv";
        dataStore = new DataStore(filePath);
    }

    public void RunMainMenu()
    {
        string input;
        do
        {
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
                    ShowInfoMenu();
                    break;
                case "2":
                    ShowGuessingGamesMenu();
                    break;
                case "3":
                    ShowSimulationsMenu();
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
    public void ShowInfoMenu()
    {
        string input;
        do
        {
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
                    matchupHistory.DisplayMatchups();
                    break;
                case "2":
                    var playerSearch = new PlayerSearch(dataStore);
                    playerSearch.SearchByPlayer();
                    break;
                case "3":
                    var teamSchedule = new TeamSchedule(dataStore);
                    teamSchedule.Start();
                    break;
                case "4":
                    var teamSearch = new TeamSearch(dataStore);
                    teamSearch.Start();
                    break;
                case "5":
                    var topTenPlayersByStat = new TopTenPlayersByStat(dataStore);
                    topTenPlayersByStat.ShowTopTenPlayersByStat();
                    break;
                case "6":
                    var divisionStandings = new DivisionStandings(dataStore);
                    divisionStandings.DisplayStandings();
                    break;
                case "back":
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe, bitte erneut versuchen.");
                    break;
            }
        } while (input != "back");
    }
    public void ShowGuessingGamesMenu()
    {
        string input;
        do
        {
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
                    amvpGame.Start();
                    break;
                case "2":
                    var allTimePointsGame = new AllTimePointsGame(dataStore);
                    allTimePointsGame.Start();
                    break;
                case "3":
                    var championshipsGame = new ChampionshipsGame(dataStore);
                    championshipsGame.Start();
                    break;
                case "4":
                    var dpoyGame = new DefensivePlayerOfTheYearGame(dataStore);
                    dpoyGame.Start();
                    break;
                case "5":
                    var fmvpGame = new FinalsMVPGame(dataStore);
                    fmvpGame.Start();
                    break;
                case "6":
                    var guessThePlayerGame = new GuessThePlayerGame(dataStore);
                    guessThePlayerGame.Start();
                    break;
                case "7":
                    var guessThePosition = new GuessThePosition(dataStore);
                    guessThePosition.Start();
                    break;
                case "8":
                    var guessTheTeam = new GuessTheTeam(dataStore);
                    guessTheTeam.Start();
                    break;
                case "9":
                    var hallOfFameGame = new HallOfFameGame(dataStore);
                    hallOfFameGame.Start();
                    break;
                case "10":
                    var mipGame = new MostImprovedPlayerGame(dataStore);
                    mipGame.Start();
                    break;
                case "11":
                    var mvpGame = new MVPGame(dataStore);
                    mvpGame.Start();
                    break;
                case "12":
                    var nbaDivisionsGame = new NBAdivisionsGame(dataStore);
                    nbaDivisionsGame.Start();
                    break;
                case "13":
                    var rookieGame = new RookieOfTheYearGame(dataStore);
                    rookieGame.Start();
                    break;
                case "14":
                    var sixthManGame = new SixthManOfTheYearGame(dataStore);
                    sixthManGame.Start();
                    break;
                case "15":
                    var statisticsGame = new StatisticsComparisonGame(dataStore);
                    statisticsGame.Start();
                    break;
                case "back":
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe, bitte erneut versuchen.");
                    break;
            }
        } while (input != "back");
    }
    public void ShowSimulationsMenu()
    {
        string input;
        do
        {
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
                    createDreamTeam.Start();
                    break;
                case "2":
                    var draftTeam = new DraftTeam(dataStore);
                    draftTeam.Start();
                    break;
                case "3":
                    var dreamTeamVsChamps = new DreamTeamVsChamps(dataStore);
                    dreamTeamVsChamps.Start();
                    break;
                case "4":
                    var oneVsOneSimulator = new OneVsOneSimulator(dataStore);
                    oneVsOneSimulator.Start();
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
