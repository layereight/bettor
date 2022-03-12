using NUnit.Framework;
using bettor.Services;
using bettor.Models;

namespace bettor.Tests;

public class Tests
{
    private BetService betService;
    private UnfairDie unfairDie;
    private User user;

    [SetUp]
    public void Setup() {
        unfairDie = new UnfairDie();
        betService = BetService.getInstance(unfairDie);
        user = new User(1, "Bob");
    }

    [Test]
    public void Test1() {
        // given
        unfairDie.WillRoll(3);

        // when
        var bet = betService.PlaceBet(user, new Bet{Number = 3, Points = 100, UserId = 1});
        
        // then
        Assert.AreEqual(10900, user.Account.Balance);
    }
}