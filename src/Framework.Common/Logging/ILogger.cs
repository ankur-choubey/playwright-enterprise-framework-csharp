namespace Framework.Common.Logging;

/// <summary>
/// Defines a logger for framework events.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Writes a log entry.
    /// </summary>
    /// <param name="level">The severity level.</param>
    /// <param name="message">The log message.</param>
    void Log(LogLevel level, string message);
}