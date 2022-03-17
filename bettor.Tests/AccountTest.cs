using NUnit.Framework;
using bettor.Models;

namespace bettor.Tests;

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
        _account.Add(addedAmount);

        // then
        Assert.AreEqual(1100, _account.Balance);
    }

    [Test]
    public void ShouldDeductCorrectly()
    {
        // given
        var deductAmount = 100;

        // when
        _account.Deduct(deductAmount);

        // then
        Assert.AreEqual(900, _account.Balance);
    }

    [Test]
    public void ShouldBeAbleToAffortStakeWhenSmallerThanBalance()
    {
        // given
        var stake = 100;

        // when
        var canAfford = _account.CanAffordStake(stake);

        // then
        Assert.True(canAfford);
    }

    [Test]
    public void ShouldBeAbleToAffortStakeWhenSmallerOrEqualToBalance()
    {
        // given
        var stake = 1000;

        // when
        var canAfford = _account.CanAffordStake(stake);

        // then
        Assert.True(canAfford);
    }

    [Test]
    public void ShouldNotBeAbleToAffortStakeWhenGreaterThanBalance()
    {
        // given
        var stake = 1001;

        // when
        var canAfford = _account.CanAffordStake(stake);

        // then
        Assert.False(canAfford);
    }

    [Test]
    public void ShouldThrowExceptionWhenTryingToDeductAmountThatsNotAffordable()
    {
        // given
        var stake = 1001;

        try
        {
            // when
            _account.Deduct(stake);
            Assert.Fail($"Was expecting an {nameof(InsufficientFundsException)}!");
        }
        catch (InsufficientFundsException e)
        {
            // then
            Assert.AreEqual("Trying to deduct 1001. But only got 1000.", e.Message);
        }
    }
}
