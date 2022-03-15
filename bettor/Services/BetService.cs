using bettor.Models;

namespace bettor.Services;

public class BetService
{
    private static BetService? s_instance;

    private readonly IDie _die;
    private readonly Dictionary<long, Bet> _bets = new Dictionary<long, Bet>();

    private BetService(IDie die)
    {
        _die = die;
    }

    public static BetService GetInstance(IDie die)
    {
        if (s_instance == null)
        {
            s_instance = new BetService(die);
        }

        return s_instance;
    }

    public Bet PlaceBet(User user, Bet bet)
    {
        if (!user.Account.CanAffordStake(bet.Points))
        {
            // TODO: throw exception
        }

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
