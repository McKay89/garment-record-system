namespace GarmentRecordSystem.Service.Logger;

public class ConsoleLogger : ILogger
{
    private static string CreateLogEntry(string message, string type) => $"[{DateTime.Now}] {type}: {message}";

    public void LogInfo(string message, string type)
    {
        var entry = CreateLogEntry(message, type);
        Console.WriteLine(entry);
    }
}