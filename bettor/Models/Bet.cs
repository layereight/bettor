namespace bettor.Models;

public class Bet
{
    public long Id { get; set; }
    public int Points { get; set; }
    public int Number { get; set; }
    public long UserId { get; set; }
    public BetResult? BetResult;
}