using bettor.Models;

namespace bettor.Services;

public interface IUserService
{
    public IEnumerable<User> GetUsers();

    public User? GetUser(long id);
}