using bettor.Models;

namespace bettor.Services;

public class BetService {

    private static BetService instance;

    private readonly Die _die;

    public static BetService getInstance(Die die) {

        if (instance == null) {
            instance = new BetService(die);
        }

        return instance;
    }

    private readonly Dictionary<long, Bet> _bets = new Dictionary<long, Bet>();

    private BetService(Die die) {
        _die = die;
    }

    public Bet PlaceBet(User user, Bet bet) {
        


        if (!user.Account.CanAffordStake(bet.Points)) {
            // TODO: throw exception
        }

        _bets.Add(bet.Id, bet);

        var diced = _die.roll();

        Console.WriteLine($"Diced {diced}");

        if (bet.Number == diced) {
            user.wins(bet);
        } else {
            user.loses(bet);
        }

        return bet;
    }

    public IEnumerable<Bet> GetBets() {
        return _bets.Values;
    }

    public Bet? GetBet(long id) {
        if (_bets.ContainsKey(id)) {
            return _bets[id];
        }

        return null;
    }
}
