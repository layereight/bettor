using NUnit.Framework;
using bettor.Models;

namespace bettor.Services;

public class BetServiceTest
{
    private BetService? _betService;
    private UnfairDie? _unfairDie;
    private User _user;

    [SetUp]
    public void Setup()
    {
        _unfairDie = new UnfairDie();
        _betService = new BetService(LoggerFactory.Create(loggingBuilder => loggingBuilder.AddConsole()).CreateLogger<BetService>(), _unfairDie);
        _user = new User(1, "Bob");
    }

    [Test]
    public void ShouldWinWithRightBet()
    {
        // given
        _unfairDie?.WillRoll(3);

        // when
        var bet = _betService?.PlaceBet(_user, new Bet { Number = 3, Points = 100, UserId = 1 });

        // then
        Assert.That(bet?.BetResult?.Status, Is.EqualTo("won"));
        Assert.That(bet?.BetResult?.Points, Is.EqualTo("+900"));
        Assert.That(bet?.BetResult?.Account, Is.EqualTo(10900));
        Assert.That(_user?.Account.Balance, Is.EqualTo(10900));
    }

    [Test]
    public void ShouldLoseWithWrongBet()
    {
        // given
        _unfairDie?.WillRoll(3);

        // when
        var bet = _betService?.PlaceBet(_user, new Bet { Number = 8, Points = 100, UserId = 1 });

        // then
        Assert.That(bet?.BetResult?.Status, Is.EqualTo("lost"));
        Assert.That(bet?.BetResult?.Points, Is.EqualTo("-100"));
        Assert.That(bet?.BetResult?.Account, Is.EqualTo(9900));
        Assert.That(_user?.Account.Balance, Is.EqualTo(9900));
    }

    [Test]
    public void ShouldThrowExceptionWithInvalidStake()
    {
        // given
        var invalidStake = -1;

        // when
        var exception = Assert.Throws<InvalidStakeException>(() => _betService?.PlaceBet(_user, new Bet { Number = 8, Points = invalidStake, UserId = 1 }));

        // then
        Assert.That(exception?.Message, Is.EqualTo("Invalid stake of -1."));
    }

    [Test]
    public void ShouldThrowExceptionWithInvalidBetNumber()
    {
        // given
        var invalidBetNumber = 10;

        // when
        var exception = Assert.Throws<InvalidBetNumberException>(() => _betService?.PlaceBet(_user, new Bet { Number = invalidBetNumber, Points = 100, UserId = 1 }));

        // then
        Assert.That(exception?.Message, Is.EqualTo("Invalid bet number of 10."));
    }

    [Test]
    public void ShouldThrowExceptionWithStakeThatsNotAffordable()
    {
        // given
        var tooHighStake = 10001;

        // when
        var exception = Assert.Throws<InsufficientFundsException>(() => _betService?.PlaceBet(_user, new Bet { Number = 8, Points = tooHighStake, UserId = 1 }));

        // then
        Assert.That(exception?.Message, Is.EqualTo("Trying to bet a stake of 10001 when user 1 only has an account balance of 10000."));
    }
}
