namespace GarmentRecordSystem.Service.Logger;

public class ConsoleLogger : ILogger
{
    private static string CreateLogEntry(string message, string type) => $"\n [{type}] {message}";
    private static string CreateMenuEntry(int num, string title) => $"   [{num}] - {title}";
    private static string CreateMenuTitleEntry(string text) => $"\n ------ [ {text} ] ------";

    public void LogMessage(string message, string type)
    {
        var entry = CreateLogEntry(message, type);
        ChangeLogColor(type);
        
        Console.WriteLine(entry);
        Console.ResetColor();
    }
    
    public void LogMenu(int num, string title)
    {
        var entry = CreateMenuEntry(num, title);
        Console.WriteLine(entry);
    }

    public void LogMenuTitle(string text, string type)
    {
        var entry = CreateMenuTitleEntry(text);
        ChangeLogColor(type);
        
        Console.WriteLine(entry);
        Console.ResetColor();
    }

    private void ChangeLogColor(string type)
    {
        switch (type)
        {
            case "INFO":
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case "ERROR":
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case "WARNING":
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case "TITLE":
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
        } 
    }
}