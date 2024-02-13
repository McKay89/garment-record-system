using System.Text.Json;
using System.Text.Json.Serialization;
using GarmentRecordSystem.Model;

namespace GarmentRecordSystem.Controllers;

public static class JsonFileWriter
{
    public static bool Write(IEnumerable<Garment>? garments)
    {
        try
        {
            var json = JsonSerializer.Serialize(garments, new JsonSerializerOptions 
            { 
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            });
            File.WriteAllText("./garments.json", json);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}