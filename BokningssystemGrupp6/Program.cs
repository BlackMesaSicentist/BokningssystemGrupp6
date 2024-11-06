using System.Drawing;
using System.Text.Json;
using BokningssystemGrupp6.Classes;
using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;

namespace BokningssystemGrupp6
{
    internal class Program
    {
        //Lista för bokningar (string userName, string roomName, double startTime, double endTime)
        public static List<Bookings> BookingsInfo = new List<Bookings>();
        static void Main(string[] args)
        {
            
            string listBooking = JsonSerializer.Serialize(BookingsInfo);
            File.WriteAllText("BokningssystemGrupp6.json", listBooking);

            //Lista för lokaler (string roomName, string size, int maxPeople, bool hasWhiteboard, bool hasProjector)
            List<IRoom> rooms = new List<IRoom>();
            string listRoom = JsonSerializer.Serialize(rooms);
            File.WriteAllText("BokningssystemGrupp6.json", listRoom);

            rooms.Add(new LargeRoom("Katt", "Large", 100, 120, true, true));
            rooms.Add(new MediumRoom("Hund Room B", "Medium", 50, 60, true, false));
            rooms.Add(new SmallRoom("Kanin", "Small", 10, 15));

            foreach (var room in rooms)
            {
                Console.WriteLine($"Room Type: {room.GetType().Name}");
                Console.WriteLine($"Room Name: {room.RoomName}");
                Console.WriteLine($"Room Type Description: {room.RoomType}");
                Console.WriteLine($"Seat Amount: {room.SeatAmount}");

                if (room is LargeRoom largeRoom)
                {
                    Console.WriteLine($"Seat Limit: {largeRoom.SeatLimit}");
                    Console.WriteLine($"Has Projector: {largeRoom.HasProjector}");
                    Console.WriteLine($"Has Whiteboard: {largeRoom.HasWhiteboard}");
                }
                else if (room is MediumRoom mediumRoom)
                {
                    Console.WriteLine($"Seat Limit: {mediumRoom.SeatLimit}");
                    Console.WriteLine($"Has Projector: {mediumRoom.HasProjector}");
                    Console.WriteLine($"Has Whiteboard: {mediumRoom.HasWhiteboard}");
                }
                else if (room is SmallRoom smallRoom)
                {
                    Console.WriteLine($"Seat Limit: {smallRoom.SeatLimit}");
                }

                Console.WriteLine("----------------------");
            }

            Console.WriteLine("Hello, World!");

            Menu.MainMenu( rooms );
        }
    }
}
