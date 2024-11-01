using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes.LokalClasses
{
    internal class Rooms
    {
        public static void CreateRoom(List<Rooms> rooms)
        {
            Console.WriteLine("Enter name of the room: ");
            string? roomName = Console.ReadLine();
            string roomSize = RoomSize();
            Console.WriteLine("Enter size of room: ");
            int size = Convert.ToInt32(Console.ReadLine());
            bool hasProjector = AskUser("Do you need a projector? Y/N");
            bool hasWhiteBoard = AskUser("Do you need a whiteboard? Y/N");

            Rooms room = new Rooms(roomName, roomSize, size, hasProjector, hasWhiteBoard);
            rooms.Add(room);
        }
        public static string RoomSize()
        {
       
            Console.WriteLine("1.Small\n2.Medium\n3.Large");
            string? size;
            switch (Console.ReadLine())
            {
                case "1":
                    size = "Small";
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
        public static bool AskUser(string ask)
        {
            while (true)
            {
                string response = Console.ReadLine().ToLower();
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
