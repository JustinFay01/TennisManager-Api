namespace CLI;

using CommandLine;

public class Options
{
    [Option('n', "num_customers", Default = 10, HelpText = "Number of customers to create.")]
    public int NumCustomers { get; set; }

    [Option('s', "num_sessions", Default = 10, HelpText = "Number of sessions to create.")]
    public int NumSessions { get; set; }

    [Option('u', "use_https", Default = false, HelpText = "Use HTTPS instead of HTTP.")]
    public bool UseHttps { get; set; }

    [Option('v', "verbose", Default = false, HelpText = "Print debug information.")]
    public bool Verbose { get; set; }
}
