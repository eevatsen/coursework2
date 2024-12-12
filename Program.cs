public class Program
{
    static void Main()
    {
        Database db = new Database();
        IUserService userService = new UserService(db);
        IGameService gameService = new GameService(db);

        while (true)
        {
            Console.WriteLine("1. Register\n2. Login\n3. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter username: ");
                var username = Console.ReadLine();
                Console.Write("Enter password: ");
                var password = Console.ReadLine();

                if (userService.Register(username, password))
                {
                    Console.WriteLine("Registration successful!");
                }
                else
                {
                    Console.WriteLine("Username already exists.");
                }
            }
            else if (choice == "2")
            {
                Console.Write("Enter username: ");
                var username = Console.ReadLine();
                Console.Write("Enter password: ");
                var password = Console.ReadLine();

                if (userService.Login(username, password))
                {
                    Console.WriteLine("Login successful!");

                    while (true)
                    {
                        Console.WriteLine("1. View Players\n2. Play Game\n3. View Player History\n4. View All Games\n5. Logout");
                        Console.Write("Choose an option: ");
                        var userChoice = Console.ReadLine();

                        if (userChoice == "1")
                        {
                            var users = userService.GetAllUsers();
                            Console.WriteLine("Players:");
                            foreach (var user in users)
                            {
                                Console.WriteLine($"Username: {user.Username}, Rating: {user.Rating}");
                            }
                        }
                        else if (userChoice == "2")
                        {
                            Console.Write("Enter player 1 username: ");
                            var player1 = Console.ReadLine();
                            Console.Write("Enter player 2 username: ");
                            var player2 = Console.ReadLine();

                            gameService.PlayGame(player1, player2);
                            Console.WriteLine("Game played successfully!");
                        }
                        else if (userChoice == "3")
                        {
                            Console.Write("Enter username to view history: ");
                            var usernameToView = Console.ReadLine();

                            var games = gameService.GetGamesByPlayer(usernameToView);
                            Console.WriteLine($"Games for {usernameToView}:");
                            foreach (var game in games)
                            {
                                Console.WriteLine($"{game.Player1} vs {game.Player2}, Winner: {game.Winner}, Date: {game.Date}");
                            }
                        }
                        else if (userChoice == "4")
                        {
                            var games = gameService.GetAllGames();
                            Console.WriteLine("All Games:");
                            foreach (var game in games)
                            {
                                Console.WriteLine($"{game.Player1} vs {game.Player2}, Winner: {game.Winner}, Date: {game.Date}");
                            }
                        }
                        else if (userChoice == "5")
                        {
                            Console.WriteLine("Logged out.");
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid username or password.");
                }
            }
            else if (choice == "3")
            {
                Console.WriteLine("Exiting...");
                break;
            }
        }
    }
}
