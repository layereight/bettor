using bettor.Models;

public class UnfairDie : Die
{

    private int number;

    public UnfairDie() {
        number = 0;
    }

    public int roll() {
        return number;
    }

    public void WillRoll(int number) {
        this.number = number;
    }
}