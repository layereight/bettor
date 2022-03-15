using bettor.Models;

namespace bettor.Services;

public class UserService
{
    private static readonly UserService s_instance = new UserService();

    private readonly Dictionary<long, User> _users = new Dictionary<long, User>();

    private UserService()
    {
        _users = new List<User>(new[] { new User(1, "Bob"), new User(2, "Alice") }).ToDictionary(keySelector: u => u.Id);
    }

    public static UserService GetInstance()
    {
        return s_instance;
    }

    public IEnumerable<User> GetUsers()
    {
        return _users.Values;
    }

    public User? GetUser(long id)
    {
        if (_users.ContainsKey(id))
        {
            return _users[id];
        }

        return null;
    }
}
