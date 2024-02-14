using GarmentRecordSystemWPF.Model;
using System.IO;
using System.Text.Json;

namespace GarmentRecordSystemWPF.JsonController
{
    internal static class JsonFileController
    {
        private static readonly string _filePath = "../../../garments.json";

        public static List<GarmentJson>? Read()
        {
            try
            {
                var text = File.ReadAllText(_filePath);

                return string.IsNullOrWhiteSpace(text)
                    ? new List<GarmentJson>()
                    : JsonSerializer.Deserialize<List<GarmentJson>>(text);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: {_filePath}");
                return null;
            }
            catch (JsonException)
            {
                Console.WriteLine($"Wrong JSON format: {_filePath}");
                return null;
            }
        }

        public static void Write()
        {
            /*
            try
            {

                var json = JsonSerializer.Serialize(garments, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new JsonStringEnumConverter() }
                });
                File.WriteAllText(_filePath, json);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            */
        }
    }
}
