namespace bettor.Models;

public class BetResult
{

    public string Status { get; set; }
    public long Account { get; set; }
    public string Points { get; set; }

    public BetResult(string status, long account, string points)
    {
        Status = status;
        Account = account;
        Points = points;
    }
}