using CommandLine;

namespace CLI;

class Program
{
    private static readonly HttpClient Client = new HttpClient();
    
    
    static async Task Main(string[] args)
    {
        await Parser.Default.ParseArguments<Options>(args)
            .WithParsedAsync(async options =>
            {
                var baseUrl = options.UseHttps ? "https://localhost:7168" : "http://localhost:5233";
                var sessionsUrl = $"{baseUrl}/api/sessions";
                
                var creator = new SessionCreator(sessionsUrl, options.NumSessions, options.Verbose);
                await creator.CreateSessionsAsync();
            });
    }
}