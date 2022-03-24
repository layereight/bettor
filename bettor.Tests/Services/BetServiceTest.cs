using NUnit.Framework;
using bettor.Models;

namespace bettor.Services;

public class BetServiceTest
{
    private BetService? _betService;
    private UnfairDie? _unfairDie;
    private User? _user;

    [SetUp]
    public void Setup()
    {
        _unfairDie = new UnfairDie();
        _betService = new BetService(_unfairDie);
        _user = new User(1, "Bob");
    }

    [Test]
    public void Test1()
    {
        // given
        _unfairDie?.WillRoll(3);

        // when
        var bet = _betService?.PlaceBet(_user, new Bet { Number = 3, Points = 100, UserId = 1 });

        // then
        Assert.That(_user?.Account.Balance, Is.EqualTo(10900));
    }
}