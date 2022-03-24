namespace bettor.Models;


public class RandomDie : IDie
{
    private static readonly int LowerBound = 0;
    private static readonly int UpperBound = 10;

    public virtual int Roll()
    {
        return Random.Shared.Next(LowerBound, UpperBound);
    }

    public bool CanRollNumber(int number) {
        return number >= LowerBound && number < UpperBound;
    }
}
