public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int Rating { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        Rating = 1000; // Default rating
    }
}

public class Game
{
    public string Player1 { get; set; }
    public string Player2 { get; set; }
    public string Winner { get; set; }
    public DateTime Date { get; set; }

    public Game(string player1, string player2, string winner)
    {
        Player1 = player1;
        Player2 = player2;
        Winner = winner;
        Date = DateTime.Now;
    }
}
