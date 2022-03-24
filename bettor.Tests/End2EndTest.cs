using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
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
        var content = new StringContent("{ \"points\": 1000, \"number\": 3, \"userid\": 1}", Encoding.UTF8, "application/json");

        // when, place bet
        var response = await _client.PostAsync("/bets", content);

        // then
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(await response.Content.ReadAsStringAsync(), Is.Empty);
        Assert.That(response.Headers.Location?.LocalPath, Is.EqualTo("/bets/1"));

        // when, getting results for the bet
        response = await _client.GetAsync("/bets/1/result");
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        // then
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        BetResult? betResult = JsonSerializer.Deserialize<BetResult>(responseContent, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
        Assert.That(betResult?.Status, Is.EqualTo("won"));
        Assert.That(betResult?.Account, Is.EqualTo(19000));
        Assert.That(betResult?.Points, Is.EqualTo("+9000"));
    }

}