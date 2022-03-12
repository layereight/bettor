namespace bettor.Models;

public class Bet {

    static long Sequence = 0;

    public Bet() {
        Id = ++Sequence;
    }

    public long Id { get; }

    public int Points { get; set; }

    public int Number { get; set; }

    public long UserId { get; set; }

    public BetResult BetResult;

}