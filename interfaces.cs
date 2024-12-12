public interface IUserService
{
    bool Register(string username, string password);
    bool Login(string username, string password);
    User GetUser(string username);
    List<User> GetAllUsers();
}

public interface IGameService
{
    void PlayGame(string player1, string player2);
    List<Game> GetGamesByPlayer(string username);
    List<Game> GetAllGames();
}
