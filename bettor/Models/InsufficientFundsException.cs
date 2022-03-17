namespace bettor.Models;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message) : base(message)
    {
    
    }
}