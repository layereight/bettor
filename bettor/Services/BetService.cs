using bettor.Models;

namespace bettor.Services;

public class BetService : IBetService
{
    private readonly ILogger<BetService> _logger;

    private readonly IDie _die;
    private readonly Dictionary<long, Bet> _bets = new Dictionary<long, Bet>();

    private long _idSequence = 0;

    public BetService(ILogger<BetService> logger, IDie die)
    {
        _logger = logger;
        _die = die;
    }

    public Bet PlaceBet(User user, Bet bet)
    {
        // input validation
        if (!Account.IsValidStake(bet.Points)) {
            throw new InvalidStakeException($"Invalid stake of {bet.Points}.");
        }

        if (!_die.CanRollNumber(bet.Number)) {
            throw new InvalidBetNumberException($"Invalid bet number of {bet.Number}.");
        }

        if (!user.Account.CanAffordStake(bet.Points))
        {
            throw new InsufficientFundsException($"Trying to bet a stake of {bet.Points} when user {user.Id} only has an account balance of {user.Account.Balance}.");
        }

        // give bet an id and add it as valid bet
        bet.Id = ++_idSequence;
        _bets.Add(bet.Id, bet);

        // roll the dice
        var diced = _die.Roll();
        _logger.LogInformation($"Diced {diced}");

        // win or lose
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
