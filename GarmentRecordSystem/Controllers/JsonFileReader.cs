using System.Text.Json;
using GarmentRecordSystem.Model;

namespace GarmentRecordSystem.Controllers;

public static class JsonFileReader
{
    public static List<GarmentJson>? Read(string filePath)
    {
        try
        {
            var text = File.ReadAllText(filePath);
            
            return string.IsNullOrWhiteSpace(text)
                ? new List<GarmentJson>()
                : JsonSerializer.Deserialize<List<GarmentJson>>(text);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"File not found: {filePath}");
            return null;
        }
        catch (JsonException)
        {
            Console.WriteLine($"Wrong JSON format: {filePath}");
            return null;
        }
    }
}