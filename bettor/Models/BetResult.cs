namespace bettor.Models;

public class BetResult
{
    public string Status { get; set; }
    public int Account { get; set; }
    public string Points { get; set; }

    public BetResult(string status, int account, string points)
    {
        Status = status;
        Account = account;
        Points = points;
    }
}