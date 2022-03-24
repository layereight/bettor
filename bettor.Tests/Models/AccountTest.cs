using NUnit.Framework;

namespace bettor.Models;

public class AccountTest
{
    private Account? _account;

    [SetUp]
    public void Setup()
    {
        _account = new Account(1000);
    }

    [Test]
    public void ShouldAddCorrectly()
    {
        // given
        var addedAmount = 100;

        // when
        _account?.Add(addedAmount);

        // then
        Assert.That(_account?.Balance, Is.EqualTo(1100));
    }

    [Test]
    public void ShouldDeductCorrectly()
    {
        // given
        var deductAmount = 100;

        // when
        _account?.Deduct(deductAmount);

        // then
        Assert.That(_account?.Balance, Is.EqualTo(900));
    }

    [Test]
    public void ShouldBeAbleToAffortStakeWhenSmallerThanBalance()
    {
        // given
        var stake = 100;

        // when
        var canAfford = _account?.CanAffordStake(stake);

        // then
        Assert.That(canAfford, Is.True);
    }

    [Test]
    public void ShouldBeAbleToAffortStakeWhenSmallerOrEqualToBalance()
    {
        // given
        var stake = 1000;

        // when
        var canAfford = _account?.CanAffordStake(stake);

        // then
        Assert.That(canAfford, Is.True);
    }

    [Test]
    public void ShouldNotBeAbleToAffortStakeWhenGreaterThanBalance()
    {
        // given
        var stake = 1001;

        // when
        var canAfford = _account?.CanAffordStake(stake);

        // then
        Assert.That(canAfford, Is.False);
    }

    [Test]
    public void ShouldThrowExceptionWhenTryingToDeductAmountThatsNotAffordable()
    {
        // given
        var stake = 1001;

        // when
        var exception = Assert.Throws<InsufficientFundsException>(() => _account?.Deduct(stake));

        // then
        Assert.That(exception?.Message, Is.EqualTo("Trying to deduct 1001. But only got 1000."));
    }
}
