public class Database
{
    private List<User> _users = new();
    private List<Game> _games = new();

    public void AddUser(User user) => _users.Add(user);
    public User GetUser(string username) => _users.FirstOrDefault(u => u.Username == username);
    public bool ValidateUser(string username, string password) => _users.Any(u => u.Username == username && u.Password == password);
    public List<User> GetAllUsers() => _users;
    public void AddGame(Game game) => _games.Add(game);
    public List<Game> GetGamesByPlayer(string username) => _games.Where(g => g.Player1 == username || g.Player2 == username).ToList();
    public List<Game> GetAllGames() => _games;
}
