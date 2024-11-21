using System.Text;
using System.Text.Json;
using CLI.Extensions;
using tennismanager.service.DTO.Session;

namespace CLI;

public class SessionCreator
{
    private readonly HttpClient _client;
    private readonly string _sessionsUrl;
    private readonly int _numSessions;
    private readonly bool _verbose;

    public int NumSessions => _numSessions;

    public SessionCreator(string sessionsUrl, int numSessions, bool verbose)
    {
        _client = new HttpClient();
        _sessionsUrl = sessionsUrl;
        _numSessions = numSessions;
        _verbose = verbose;
    }

    public async Task CreateSessionsAsync()
    {
        Console.WriteLine("Creating sessions...");
        for (int i = 0; i < _numSessions; i++)
        {
            await CreateSessionAsync();
        }
    }

    private async Task CreateSessionAsync()
    {
        var session = SessionDtoExtensions.BuildRandom();
        try
        {
            var response = await _client.PostAsync(
                _sessionsUrl,
                new StringContent(JsonSerializer.Serialize(session), Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error creating session: {await response.Content.ReadAsStringAsync()}");
                Console.ResetColor();
            }

            if (_verbose)
            {
                Console.WriteLine($"Response status code: {response.StatusCode}");
                Console.WriteLine($"Response content: {await response.Content.ReadAsStringAsync()}");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error creating session: {ex.Message}");
            Console.ResetColor();
        }
    }
}
