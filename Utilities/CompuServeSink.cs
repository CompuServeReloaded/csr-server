using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Configuration;

namespace CSRServer.Utilities;

public class CompuServeSink : ILogEventSink
{
    private readonly IFormatProvider? _formatProvider;

    private readonly Dictionary<LogEventLevel, ConsoleColor> logLevelColors = new() {
        { LogEventLevel.Verbose, ConsoleColor.Magenta },
        { LogEventLevel.Debug, ConsoleColor.Green },
        { LogEventLevel.Information, ConsoleColor.DarkGreen },
        { LogEventLevel.Warning, ConsoleColor.Yellow },
        { LogEventLevel.Error, ConsoleColor.Red },
        { LogEventLevel.Fatal, ConsoleColor.DarkRed }
    };

    public CompuServeSink(IFormatProvider? formatProvider)
    {
        _formatProvider = formatProvider;
    }

    public void Emit(LogEvent logEvent)
    {
        var message = logEvent.RenderMessage(_formatProvider);
        var formattedDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
        var levelString = logEvent.Level.ToString();

        Console.Write($"[{formattedDate}] [");
        
        Console.ForegroundColor = logLevelColors[logEvent.Level];
        Console.Write(levelString.ToUpper());
        Console.ResetColor();

        Console.WriteLine($"] {message}");
    }
}

public static class CompuServeSinkExtensions
{
    public static LoggerConfiguration CompuServeSink(this LoggerSinkConfiguration loggerConfiguration, IFormatProvider? formatProvider = null)
    {
        return loggerConfiguration.Sink(new CompuServeSink(formatProvider));
    }
}
