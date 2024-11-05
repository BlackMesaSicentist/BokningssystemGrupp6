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
        public static List<Bookings> BookingsInfo = new List<Bookings>();
        static void Main(string[] args)
        {


            List<IRoom> rooms = new List<IRoom>();
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



            //Sample data
            Rooms Delfinen = new Rooms("Delfinen","GroupRoom", 8, false, false);
            Rooms Hajen = new Rooms("Hajen","GroupRoom", 8, false, false);
            Rooms Valen = new Rooms("Valen","Medium",20, false, false);
            Rooms Maneten = new Rooms("Maneten","Medium", 20, true, false);
            Rooms Krabban = new Rooms("Krabban","Large", 40, true, true);
            Rooms Aborren = new Rooms("Aborren","Large", 40, true, true);
            Rooms Laxen = new Rooms("Laxen","Large", 40, true, true);
            Rooms Korallen = new Rooms("Korallen", "Large", 40, true, true);

            //RoomsInfo.Add(Delfinen);
            //RoomsInfo.Add(Hajen);
            //RoomsInfo.Add(Valen);
            //RoomsInfo.Add(Maneten);
            //RoomsInfo.Add(Krabban);
            //RoomsInfo.Add(Aborren);
            //RoomsInfo.Add(Laxen);
            //RoomsInfo.Add(Korallen);

            Console.WriteLine("Hello, World!");

            Menu.MainMenu( rooms );
        }
    }
}
