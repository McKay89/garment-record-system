using System.Text.Json;
using System.Text.Json.Serialization;
using GarmentRecordSystem.Model;

namespace GarmentRecordSystem.Controllers;

public static class JsonFileWriter
{
    private static readonly string filePath = "./garments.json";
    
    public static bool Write(IEnumerable<Garment>? garments)
    {
        try
        {
            var json = JsonSerializer.Serialize(garments, new JsonSerializerOptions 
            { 
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            });
            File.WriteAllText(filePath, json);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}