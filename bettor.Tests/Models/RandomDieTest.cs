using NUnit.Framework;

namespace bettor.Models;

public class RandomDieTest
{
    private RandomDie? _randomDie;

    [SetUp]
    public void Setup()
    {
        _randomDie = new RandomDie();
    }

    [Test]
    public void ShouldRollNumberInBounds()
    {
        // when
        var number = _randomDie?.Roll();

        // then
        Assert.That(number, Is.InRange(0, 9));
    }

    [Test]
    [TestCase(-1, ExpectedResult = false)]
    [TestCase(0, ExpectedResult = true)]
    [TestCase(1, ExpectedResult = true)]
    [TestCase(2, ExpectedResult = true)]
    [TestCase(3, ExpectedResult = true)]
    [TestCase(4, ExpectedResult = true)]
    [TestCase(5, ExpectedResult = true)]
    [TestCase(6, ExpectedResult = true)]
    [TestCase(7, ExpectedResult = true)]
    [TestCase(8, ExpectedResult = true)]
    [TestCase(9, ExpectedResult = true)]
    [TestCase(10, ExpectedResult = false)]
    public bool? ShouldBeAbleToRollNumberOrNot(int givenNumber)
    {
        // when
        return _randomDie?.CanRollNumber(givenNumber);
    }
}
