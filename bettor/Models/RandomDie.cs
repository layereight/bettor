namespace bettor.Models;


public class RandomDie : Die
{
    public int roll() {
        return Random.Shared.Next(0, 10);
    }
}