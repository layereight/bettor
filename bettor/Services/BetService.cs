using bettor.Models;

namespace bettor.Services;

public class BetService : IBetService
{
    private readonly IDie _die;
    private readonly Dictionary<long, Bet> _bets = new Dictionary<long, Bet>();

    private long _idSequence = 0;

    public BetService(IDie die)
    {
        _die = die;
    }

    public Bet PlaceBet(User user, Bet bet)
    {
        if (!user.Account.CanAffordStake(bet.Points))
        {
            throw new InsufficientFundsException($"Trying to bet a stake of {bet.Points} when user {user.Id} only has an account balance of {user.Account.Balance}.");
        }

        bet.Id = ++_idSequence;

        _bets.Add(bet.Id, bet);

        var diced = _die.Roll();

        Console.WriteLine($"Diced {diced}");

        if (bet.Number == diced)
        {
            user.Wins(bet);
        }
        else
        {
            user.Loses(bet);
        }

        return bet;
    }

    public IEnumerable<Bet> GetBets()
    {
        return _bets.Values;
    }

    public Bet? GetBet(long id)
    {
        if (_bets.ContainsKey(id))
        {
            return _bets[id];
        }

        return null;
    }
}
