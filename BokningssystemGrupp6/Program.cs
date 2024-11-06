using System.Drawing;
using BokningssystemGrupp6.Classes;
using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;

namespace BokningssystemGrupp6
{
    internal class Program
    {
        //Lista för lokaler (string roomName, string size, int maxPeople, bool hasWhiteboard, bool hasProjector)
        //public static List<Rooms> RoomsInfo = new List<Rooms>();
        //public static List<IRoom> rooms2 = new List<IRoom>();
        //Lista för bokningar (string userName, string roomName, double startTime, double endTime)
        //public static List<Bookings> BookingsInfo = new List<Bookings>();
        static void Main(string[] args)
        {

            InputValidation inputValidation = new InputValidation();
            Menu menu = new Menu (inputValidation);

            List<IRoom> rooms = new List<IRoom>();
            rooms.Add(new LargeRoom("Katt", "Large", 100, 120, true, true));
            rooms.Add(new MediumRoom("Hund", "Medium", 50, 60, true, false));
            rooms.Add(new SmallRoom("Kanin", "Small", 10, 15));
            menu.MainMenu(rooms);
        }
    }
}
