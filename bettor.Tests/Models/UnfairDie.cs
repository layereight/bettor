
namespace bettor.Models;

public class UnfairDie : RandomDie
{
    private int number;

    public UnfairDie() {
        number = 0;
    }

    public override int Roll() {
        return number;
    }

    public void WillRoll(int number) {
        this.number = number;
    }
}