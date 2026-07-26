namespace Framework.Common.Logging;

/// <summary>
/// Writes log messages to the console.
/// </summary>
public sealed class ConsoleLogger : ILogger
{
    public void Log(LogLevel level, string message)
    {
        Console.WriteLine(
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}");
    }
}