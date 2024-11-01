using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes.LokalClasses
{
    internal class Rooms
    {
        public static void CreateARoom(List<Rooms> rooms)
        {
            Console.WriteLine("Enter name of the room: ");
            string? roomName = Console.ReadLine();
            string roomSize = RoomSize();
            Console.WriteLine("Enter how many seats:  ");
            int seats = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Do you need a projector? Y/N");
            bool hasProjector = AskUser();
            Console.WriteLine("Do you need a whiteboard? Y/N");
            bool hasWhiteBoard = AskUser();
            Rooms room = new Rooms(roomName, roomSize, seats, hasProjector, hasWhiteBoard);
            rooms.Add(room);
        }
        public static string RoomSize()
        {

            Console.WriteLine("1.Grupprum\n2.Medium\n3.Large");
            string? size;
            switch (Console.ReadLine())
            {
                case "1":
                    size = "Grupprum";
                    break;
                case "2":
                    size = "Medium";
                    break;
                case "3":
                    size = "Large";
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    return RoomSize();
            }
            return size;
        }
        public static bool AskUser()
        {
            while (true)
            {
                string? response = Console.ReadLine().ToLower();
                if (response == "y")
                {
                    return true;
                }
                else if (response == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("invalid option");
                }

            }
        }

    }
}
