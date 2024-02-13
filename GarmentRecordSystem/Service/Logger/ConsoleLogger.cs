using GarmentRecordSystem.Model;

namespace GarmentRecordSystem.Service.Logger;

public class ConsoleLogger : ILogger
{
    private static string CreateLogEntry(string message, string type) => $"\n [{type}] {message}";
    private static string CreateMenuEntry(int num, string title) => $"   [{num}] - {title}";
    private static string CreateMenuTitleEntry(string text) => $"\n ------ [ {text} ] ------";
    private static string CreateGarmentEntry(Garment garment) => 
        $"  Garment ID: {garment.GarmentId} || " +
        $"Brand Name: {garment.BrandName} || " +
        $"Purchase Date: {garment.PurchaseDate} || " +
        $"Color: {garment.Color} || " +
        $"Size: {garment.Size}";

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

    public void LogGarment(Garment garment)
    {
        var entry = CreateGarmentEntry(garment);
        Console.WriteLine(entry);
    }

    private static void ChangeLogColor(string type)
    {
        Console.ForegroundColor = type switch
        {
            "SUCCESS" => ConsoleColor.Green,
            "INFO" => ConsoleColor.Magenta,
            "ERROR" => ConsoleColor.Red,
            "WARNING" => ConsoleColor.Yellow,
            "TITLE" => ConsoleColor.Blue,
            _ => ConsoleColor.Cyan
        };
    }
}