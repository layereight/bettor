namespace bettor.Models;

public class InvalidStakeException : Exception
{
    public InvalidStakeException(string message) : base(message)
    {
    
    }
}