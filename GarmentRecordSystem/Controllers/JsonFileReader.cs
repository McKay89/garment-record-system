using System.Text.Json;
using GarmentRecordSystem.Model;

namespace GarmentRecordSystem.Controllers;

public static class JsonFileReader
{
    private static readonly string filePath = "./garments.json";
    
    public static List<GarmentJson>? Read()
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
            File.WriteAllText(filePath, "");

            return new List<GarmentJson>();
        }
        catch (JsonException)
        {
            Console.WriteLine($"Wrong JSON format: {filePath}");
            return null;
        }
    }
}