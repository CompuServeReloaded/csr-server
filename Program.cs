using CSRServer.Utilities;
using Serilog;
using Serilog.Core;

namespace CSRServer;

internal class Program 
{
    static void Main(string[] args)
    {
        // Set up logging
        Log.Logger = new LoggerConfiguration()
            .WriteTo.CompuServeSink()
            .CreateLogger();

        // Test logging
        Log.Information("Hello, CompuServe!");
    }
}
