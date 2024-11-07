using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes
{
    public static class JsonFunctions
    {
        // Save the rooms list to JSON
        public static void SaveRoomsToJson(List<IRoom> rooms, JsonSerializerOptions options)
        {
            string filePath = "BokningssystemGrupp6.json";
            string jsonData = JsonSerializer.Serialize(rooms, options);
            File.WriteAllText(filePath, jsonData);
        }

        // Load rooms data from JSON
        public static List<IRoom>? LoadRoomsFromJson(JsonSerializerOptions options)
        {
            string filePath = "BokningssystemGrupp6.json";

            if (!File.Exists(filePath))
                return null;

            string jsonData = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(jsonData))
                return null;

            try
            {
                return JsonSerializer.Deserialize<List<IRoom>>(jsonData, options);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error loading rooms: {ex.Message}");
                return null;
            }
        }

        // Method to reload the rooms list, or use default rooms if file is not available or empty
        public static void ReloadRoomsFromJson(ref List<IRoom> rooms, JsonSerializerOptions options)
        {
            List<IRoom>? loadedRooms = LoadRoomsFromJson(options); // Try loading from JSON

            if (loadedRooms != null)
            {
                rooms = loadedRooms; // Update the rooms list if data is available
            }
            else
            {
                Console.WriteLine("No room data found, adds new list");
                // If no rooms are found in the JSON file, add rooms
                rooms = new List<IRoom>
                {
                    //For testing
                    //new Hall("Katt", "Hall", 100, 120, true, true),
                    //new ClassRoom("Hund", "Class room", 50, 60, true, false),
                    //new GroupRoom("Kanin", "Group room", 10, 15)
                };
            }
        }


    }
}
