using bettor.Models;

namespace bettor.Services;

public interface IBetService
{
    public Bet PlaceBet(User user, Bet bet);

    public IEnumerable<Bet> GetBets();

    public Bet? GetBet(long id);
}