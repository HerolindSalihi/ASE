using System;

class Program
{
    static string[]? lines; 
    static string[]? headers;

    static void Main(string[] args)
    {
        LoadData("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/nba_stats.csv");
        
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

    static void LoadData(string filePath)
    {
        lines = System.IO.File.ReadAllLines(filePath);
        headers = lines[0].Split(',');
    }

    static void ShowInfoMenu()
    {
        var matchupHistory = new MatchupHistory("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/spielplan.csv");
        var playerSearch = new PlayerSearch(lines, headers);
        var teamSchedule = new TeamSchedule("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/spielplan.csv");
        var teamSearch = new TeamSearch(lines, headers);
        var topTenPlayersByStat = new TopTenPlayersByStat(lines, headers);
        var divisionStandings = new DivisionStandings("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/standings.csv");

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
                    matchupHistory.DisplayMatchups();
                    break;
                case "2":
                    playerSearch.SearchByPlayer();
                    break;
                case "3":
                    teamSchedule.Start();
                    break;
                case "4":
                    teamSearch.Start();
                    break;
                case "5":
                    topTenPlayersByStat.ShowTopTenPlayersByStat();
                    break;
                case "6":
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

    static void ShowGuessingGamesMenu()
    {
        var guessThePosition = new GuessThePosition(lines);
        var guessTheTeam = new GuessTheTeam(lines);
        var statisticsGame = new StatisticsComparisonGame(lines, headers);
        var hallOfFameGame = new HallOfFameGame("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/hall_of_fame_prob.csv");
        var allTimePointsGame = new AllTimePointsGame("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/all_time_points_stats.csv");
        var championshipsGame = new ChampionshipsGame("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/most_championschips.csv");
        var nbaDivisionsGame = new NBAdivisionsGame("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/devision_teams.csv");
        var guessThePlayerGame = new GuessThePlayerGame("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/nba_stats.csv");
        var mvpGame = new MVPGame("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/all_mvps.csv");
        var rookieGame = new RookieOfTheYearGame("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/all_Rookie.csv");
        var dpoyGame = new DefensivePlayerOfTheYearGame("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/all_DPOY.csv");
        var sixthManGame = new SixthManOfTheYearGame("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/all_SMOTY.csv");
        var mipGame = new MostImprovedPlayerGame("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/all_MIP.csv");
        var fmvpGame = new FinalsMVPGame("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/all_FMVP.csv");
        var amvpGame = new AllStarMVPGame("C:/Users/Acer/OneDrive/Dokumente/ASE/Daten/all_AMVP.csv");

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
                    amvpGame.Start();
                    break;
                case "2":
                    allTimePointsGame.Start();
                    break;
                case "3":
                    championshipsGame.Start();
                    break;
                case "4":
                    dpoyGame.Start();
                    break;
                case "5":
                    fmvpGame.Start();
                    break;
                case "6":
                    guessThePlayerGame.Start();
                    break;
                case "7":
                    guessThePosition.Start();
                    break;
                case "8":
                    guessTheTeam.Start();
                    break;
                case "9":
                    hallOfFameGame.Start();
                    break;
                case "10":
                    mipGame.Start();
                    break;
                case "11":
                    mvpGame.Start();
                    break;
                case "12":
                    nbaDivisionsGame.Start();
                    break;
                case "13":
                    rookieGame.Start();
                    break;
                case "14":
                    sixthManGame.Start();
                    break;
                case "15":
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

    static void ShowSimulationsMenu()
    {
        var createDreamTeam = new CreateDreamTeam(lines, headers);
        var dreamTeamVsChamps = new DreamTeamVsChamps(lines, headers);
        var oneVsOneSimulator = new OneVsOneSimulator(lines, headers);
        var draftTeam = new DraftTeam(lines, headers);

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
                    createDreamTeam.Start();
                    break;
                case "2":
                    draftTeam.Start();
                    break;
                case "3":
                    dreamTeamVsChamps.Start();
                    break;
                case "4":
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
