
namespace bettor.Models;

public class User
{
    private const int InitialAccountBalance = 10000;

    public long Id { get; set; }
    public string? Name { get; set; }
    public Account Account;

    public User(long id, string name)
    {
        Id = id;
        Name = name;
        Account = new Account(InitialAccountBalance);
    }

    internal void Wins(Bet bet)
    {
        var win = 9 * bet.Points;

        Account.Add(win);
        bet.BetResult = new BetResult("won", Account.Balance, $"+{win}");
    }

    internal void Loses(Bet bet)
    {
        Account.Deduct(bet.Points);
        bet.BetResult = new BetResult("lost", Account.Balance, $"-{bet.Points}");
    }
}
