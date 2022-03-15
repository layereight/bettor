using NUnit.Framework;
using bettor.Services;
using bettor.Models;

namespace bettor.Tests;

public class Tests
{
    private BetService? _betService;
    private UnfairDie? _unfairDie;
    private User? _user;

    [SetUp]
    public void Setup()
    {
        _unfairDie = new UnfairDie();
        _betService = BetService.GetInstance(_unfairDie);
        _user = new User(1, "Bob");
    }

    [Test]
    public void Test1()
    {
        // given
        _unfairDie.WillRoll(3);

        // when
        var bet = _betService.PlaceBet(_user, new Bet { Number = 3, Points = 100, UserId = 1 });

        // then
        Assert.AreEqual(10900, _user.Account.Balance);
    }
}