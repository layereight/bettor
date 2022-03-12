using bettor.Models;

namespace bettor.Services;

public class BetService {

    private static BetService instance = new BetService();

    public static BetService getInstance() {
        return instance;
    }

    private readonly Dictionary<long, Bet> _bets = new Dictionary<long, Bet>();

    private BetService() {

    }

    public Bet PlaceBet(User user, Bet bet) {
        _bets.Add(bet.Id, bet);


        if (!user.Account.CanAffordStake(bet.Points)) {
            // TODO: throw exception
        }

        var diced = Random.Shared.Next(0, 10);

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
