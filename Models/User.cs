
namespace bettor.Models;

public class User {
    public long Id { get; set; }

    public string? Name { get; set; }

    public Account Account;

    public User(long Id, string Name) {
        this.Id = Id;
        this.Name = Name;
        this.Account = new Account();
    }

    internal void wins(Bet bet)
    {
        var win = 9 * bet.Points;

        Account.Add(win);
        bet.BetResult = new BetResult("won", Account.Balance, $"+{win}");
    }

    internal void loses(Bet bet)
    {
        Account.Deduct(bet.Points);
        bet.BetResult = new BetResult("lost", Account.Balance, $"-{bet.Points}");
    }
}