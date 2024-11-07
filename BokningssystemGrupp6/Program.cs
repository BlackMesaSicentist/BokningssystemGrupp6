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

            InputValidation inputValidation = new InputValidation();
            Menu menu = new Menu(inputValidation);

            string listBooking = JsonSerializer.Serialize(BookingsInfo);
            File.WriteAllText("BokningssystemGrupp6.json", listBooking);

            //Lista för lokaler (string roomName, string size, int maxPeople, bool hasWhiteboard, bool hasProjector)
            List<IRoom> rooms = new List<IRoom>();

            rooms.Add(new Hall("Katt", "Hall", 100, 120, true, true));
            rooms.Add(new ClassRoom("Hund", "Class room", 50, 60, true, false));
            rooms.Add(new GroupRoom("Kanin", "Group room", 10, 15));


            string listRoom = JsonSerializer.Serialize(rooms);
            File.WriteAllText("BokningssystemGrupp6.json", listRoom);


            Console.WriteLine("Program.cs");

            menu.MainMenu(rooms, BookingsInfo);

        }
    }
}
