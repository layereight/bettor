
namespace bettor.Models;


public class Account
{
    public int Balance { get; set; }

    public Account(int initial)
    {
        Balance = initial;
    }

    internal void Add(int amount)
    {
        Balance += amount;
    }

    internal void Deduct(int amount)
    {
        if (!CanAffordStake(amount))
        {
            throw new InsufficientFundsException($"Trying to deduct {amount}. But only got {Balance}.");
        }

        Balance -= amount;
    }

    public bool CanAffordStake(int stake)
    {
        return Balance >= stake;
    }

    public static bool IsValidStake(int stake)
    {
        return stake >= 0;
    }
}