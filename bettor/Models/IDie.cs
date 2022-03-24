namespace bettor.Models;


public interface IDie
{
    public int Roll();

    public bool CanRollNumber(int number);
}
