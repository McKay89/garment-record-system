﻿using GarmentRecordSystemWPF.Model;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GarmentRecordSystemWPF.JsonController
{
    internal static class JsonFileController
    {
        private static readonly string _filePath = "./garments.json";

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
                Trace.WriteLine($"File not found: {_filePath}");
                File.WriteAllText(_filePath, "");

                return new List<GarmentJson>();
            }
            catch (JsonException)
            {
                Console.WriteLine($"Wrong JSON format: {_filePath}");
                Environment.Exit(2);
            }

            return deserializedText;
        }

        public static bool Write(IEnumerable<GarmentJson> garments)
        {
            try
            {
                var json = JsonSerializer.Serialize(garments, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(_filePath, json);

                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                return false;
            }
        }
    }
}
