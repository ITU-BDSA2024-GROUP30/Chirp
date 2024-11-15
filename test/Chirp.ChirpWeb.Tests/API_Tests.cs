using Xunit;
using ChirpWeb;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Chirp.ChirpWeb.Tests{

public class APITests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _fixture;
    private readonly HttpClient _client;


	public APITests(WebApplicationFactory<Program> fixture)
    {
        _fixture = fixture;
        _client = _fixture.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = true, HandleCookies = true });
    }

    [Fact]
    public async void CanSeePublicTimeline()
    {
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("Chirp!", content);
        Assert.Contains("Public Timeline", content);
    }

    [Theory]
    [InlineData("Luanna Muro")]
    public async void CanSeePrivateTimeline(string author)
    {
        var response = await _client.GetAsync($"/{author}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("Chirp!", content);
        Assert.Contains($"{author}'s Timeline", content);
       
    }
    /*Data NOT included in these API tests (write statements shows in commandline
    that no cheeps are visible. Fix and test more, or simply test this somehow else?)
    [Theory]
    [InlineData("Luanna Muro")]
    public async void PrivateTimelineContainsCheep(string author) {
        var response = await _client.GetAsync($"/{author}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);
        //check that we are still on correct timeline
        Assert.Contains($"{author}'s Timeline", content);

        //check that an expected Cheep exists
        Assert.Contains("Of all the sailors called them ring-bolts, and would lay my hand into the wind''s eye.", content);
    }*/
}
}