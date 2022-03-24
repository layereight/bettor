using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Net;

using bettor.Models;

namespace bettor.Tests;

public class End2EndTest
{
    private HttpClient? _client;
    private UnfairDie? _unfairDie;

    [SetUp]
    public void Setup()
    {
        _unfairDie = new UnfairDie();

        _client = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IDie>(sp => _unfairDie);
            });
        })
        .CreateClient();
    }

    [Test]
    public async Task ShouldWinWhenNumberIsCorrect()
    {
        // given
        _unfairDie?.WillRoll(3);

        // when, place bet
        var response = await _client.PostAsJsonAsync<Bet>("/bets", new Bet { Number = 3, Points = 1000, UserId = 1 });

        // then
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(await response.Content.ReadAsStringAsync(), Is.Empty);
        Assert.That(response.Headers.Location?.LocalPath, Is.EqualTo("/bets/1"));
        await AssertBetResult("/bets/1/result", "won", "+9000", 19000);
    }

    [Test]
    public async Task ShouldLoseWhenNumberIsWrong()
    {
        // given
        _unfairDie?.WillRoll(8);

        // when, place bet
        var response = await _client.PostAsJsonAsync<Bet>("/bets", new Bet { Number = 3, Points = 1000, UserId = 1 });

        // then
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(await response.Content.ReadAsStringAsync(), Is.Empty);
        Assert.That(response.Headers.Location?.LocalPath, Is.EqualTo("/bets/1"));
        await AssertBetResult("/bets/1/result", "lost", "-1000", 9000);
    }

    private async Task AssertBetResult(string uri, string expectedStatus, string expectedPoints, int expectedAccountBalance)
    {
        // getting results for the bet
        var response = await _client.GetAsync(uri);
        var responseContent = await response.Content.ReadAsStringAsync();

        // then
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var betResult = Deserialize<BetResult>(responseContent);
        Assert.That(betResult?.Status, Is.EqualTo(expectedStatus));
        Assert.That(betResult?.Points, Is.EqualTo(expectedPoints));
        Assert.That(betResult?.Account, Is.EqualTo(expectedAccountBalance));
    }

    private static TValue? Deserialize<TValue>(string json)
    {
        return JsonSerializer.Deserialize<TValue>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
    }
}