using System.Drawing;
using System.Text.Json;
using BokningssystemGrupp6.Classes;
using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;
using Microsoft.VisualBasic;

namespace BokningssystemGrupp6
{
    internal class Program
    {
        //Lista för bokningar (string userName, string roomName, double startTime, double endTime)
        public static List<Bookings> BookingsInfo = new List<Bookings>();
        static void Main(string[] args)
        {

            // Configure JSON options with RoomConverter for polymorphic deserialization
            var options = new JsonSerializerOptions
            {
                Converters = { new RoomConverter() },
                WriteIndented = true
            };


            // Load rooms data from JSON, Adds rooms if empty
            List<IRoom> rooms = LoadRoomsFromJson(options) ?? new List<IRoom>
            {
                new Hall("Katt", "Hall", 100, 120, true, true),
                new ClassRoom("Hund", "Class room", 50, 60, true, false),
                new GroupRoom("Kanin", "Group room", 10, 15)
            };

            InputValidation inputValidation = new InputValidation();
            Menu menu = new Menu(inputValidation);
            menu.MainMenu(rooms, BookingsInfo);

            // Save the rooms list to JSON after program exit or modifications
            SaveRoomsToJson(rooms, options);
        }

        private static void SaveRoomsToJson(List<IRoom> rooms, JsonSerializerOptions options)
        {
            string filePath = "BokningssystemGrupp6.json";
            string jsonData = JsonSerializer.Serialize(rooms, options);
            File.WriteAllText(filePath, jsonData);
        }

        private static List<IRoom>? LoadRoomsFromJson(JsonSerializerOptions options)
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



        //string listBooking = JsonSerializer.Serialize(BookingsInfo);
        //File.WriteAllText("BokningssystemGrupp6.json", listBooking);

        ////Lista för lokaler (string roomName, string size, int maxPeople, bool hasWhiteboard, bool hasProjector)
        //List<IRoom> rooms = new List<IRoom>();

        //rooms.Add(new Hall("Katt", "Hall", 100, 120, true, true));
        //rooms.Add(new ClassRoom("Hund", "Class room", 50, 60, true, false));
        //rooms.Add(new GroupRoom("Kanin", "Group room", 10, 15));


        //string listRoom = JsonSerializer.Serialize(rooms);
        //File.WriteAllText("BokningssystemGrupp6.json", listRoom);









        //File.WriteAllText("BokningssystemGrupp6.json", listRoom);



        //Console.WriteLine("Program.cs");


    }
}

