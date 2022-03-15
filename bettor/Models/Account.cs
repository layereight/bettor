
namespace bettor.Models;


public class Account
{
    public long Balance { get; set; }

    public Account()
    {
        Balance = 10000;
    }

    public void Add(long amount)
    {
        Balance += amount;
    }

    public void Deduct(long amount)
    {
        Balance -= amount;
    }

    public bool CanAffordStake(long stake)
    {
        return Balance >= stake;
    }
}