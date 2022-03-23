
namespace bettor.Models;

public class UnfairDie : IDie
{
    private int number;

    public UnfairDie() {
        number = 0;
    }

    public int Roll() {
        return number;
    }

    public void WillRoll(int number) {
        this.number = number;
    }
}