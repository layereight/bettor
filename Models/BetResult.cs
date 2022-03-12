namespace bettor.Models;

public class BetResult {

    public string Status { get; set; }

    public long Account { get; set; }

    public string Points { get; set; }

    public BetResult(string Status, long Account, string Points) {
        this.Status = Status;
        this.Account = Account;
        this.Points = Points;
    }
}