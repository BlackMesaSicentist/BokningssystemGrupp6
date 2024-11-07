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


            // Load rooms data from JSON
            List<IRoom> rooms = JsonFunctions.LoadRoomsFromJson(options) ?? new List<IRoom>
            {
                // testing data
                //new Hall("Katt", "Hall", 100, 120, true, true),
                //new ClassRoom("Hund", "Class room", 50, 60, true, false),
                //new GroupRoom("Kanin", "Group room", 10, 15)
            };

            InputValidation inputValidation = new InputValidation();
            Menu menu = new Menu(inputValidation);






            menu.MainMenu(rooms, BookingsInfo, options);

            // Save the rooms list to JSON after program exit
            JsonFunctions.SaveRoomsToJson(rooms, options);
        }

        
    }
}

