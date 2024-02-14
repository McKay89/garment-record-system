using GarmentRecordSystemWPF.Model;
using System.IO;
using System.Text.Json;

namespace GarmentRecordSystemWPF.JsonController
{
    internal static class JsonFileController
    {
        private static readonly string _filePath = "../../../garments.json";

        public static List<GarmentJson> Read()
        {
            var deserializedText = new List<GarmentJson>();

            try
            {
                var text = File.ReadAllText(_filePath);
                deserializedText = string.IsNullOrWhiteSpace(text)
                    ? new List<GarmentJson>()
                    : JsonSerializer.Deserialize<List<GarmentJson>>(text);

                if(deserializedText == null) Environment.Exit(0);
                return deserializedText;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: {_filePath}");
                Environment.Exit(1);   
            }
            catch (JsonException)
            {
                Console.WriteLine($"Wrong JSON format: {_filePath}");
                Environment.Exit(2);
            }

            return deserializedText;
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
