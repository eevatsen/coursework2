public class UserService : IUserService
{
    private readonly Database _db;

    public UserService(Database db)
    {
        _db = db;
    }

    public bool Register(string username, string password)
    {
        if (_db.GetUser(username) != null) return false;

        _db.AddUser(new User(username, password));
        return true;
    }

    public bool Login(string username, string password) => _db.ValidateUser(username, password);

    public User GetUser(string username) => _db.GetUser(username);

    public List<User> GetAllUsers() => _db.GetAllUsers();
}
