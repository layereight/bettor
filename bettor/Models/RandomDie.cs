namespace bettor.Models;


public class RandomDie : IDie
{
    public int Roll()
    {
        return Random.Shared.Next(0, 10);
    }
}
