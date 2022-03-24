namespace bettor.Models;

public class InvalidBetNumberException : Exception
{
    public InvalidBetNumberException(string message) : base(message)
    {
    
    }
}