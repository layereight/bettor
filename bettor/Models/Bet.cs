namespace bettor.Models;

public class Bet
{
    private static long s_sequence = 0;

    public long Id { get; }
    public int Points { get; set; }
    public int Number { get; set; }
    public long UserId { get; set; }
    public BetResult? BetResult;

    public Bet()
    {
        Id = ++s_sequence;
    }
}