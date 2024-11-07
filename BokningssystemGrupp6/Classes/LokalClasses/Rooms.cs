using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes.LokalClasses
{
    //Rooms inherits interface IRoom and IListable
    public abstract class Rooms: IRoom, IListable
    {
        //Properties for Rooms
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public int SeatAmount { get; set; }

        //Construktor for Rooms
        protected Rooms()
        {
            
        }
        protected Rooms(string roomName, string roomType, int seatAmount)
        {
            RoomName = roomName;
            RoomType = roomType;
            SeatAmount = seatAmount;
        }
        //Method to get back to menu
        public static void BackToMenu()
        {
            Console.WriteLine("\nTryck valfri tangent för att återgå till meny");
            Console.ReadKey();
            Console.Clear();
        }
        //Method to create a room with the use of IRoom list
        public static void CreateARoom(List<Rooms> rooms) 
        {
            //retrieves information needed to create a new room
            Console.WriteLine("Enter name of the room: ");
            string? roomName = Console.ReadLine();
            var roomSize = RoomSize();
            Console.WriteLine("Enter how many seats: ");
            int seats = Convert.ToInt32(Console.ReadLine());
            bool hasProjector = false;
            bool hasWhiteboard = false;
            // if group room isnt picked, ask user for projector/whiteboard
            if (roomSize != "Group room")
            {
                Console.WriteLine("Does the venue have a projector? Y/N");
                hasProjector = AskUser();
                Console.WriteLine("Does the venue have a whiteboard? Y/N");
                hasWhiteboard = AskUser();
            }
            //different types of venue
            if (roomSize == "Hall")
            {
                Console.WriteLine("Adding Hall...");
                int seatLimit = 120;
                rooms.Add(new Hall(roomName, roomSize, seats, seatLimit, hasProjector, hasWhiteboard));
                Save.SaveFile(rooms);
            }
            else if (roomSize == "Classroom")
            {
                Console.WriteLine("Adding a classroom...");
                int seatLimit = 60;
                rooms.Add(new ClassRoom(roomName, roomSize, seats, seatLimit, hasProjector, hasWhiteboard));
                Save.SaveFile(rooms);
            }
            else if (roomSize == "Group room")
            {
                Console.WriteLine("Adding a group room...");
                int seatLimit = 15;
                rooms.Add(new GroupRoom(roomName, roomSize, seats, seatLimit));
                Save.SaveFile(rooms);

            }
        }
        //Method to determine size
        public static string RoomSize()
        {
            Console.WriteLine("Choose room size:\n1.Hall\n2.Classroom\n3.Group Room");
            string? option;
            string size = "";

            switch (option = Console.ReadLine())
            {
                case "1":
                    size = "Hall, max 120 seats";
                    break;
                case "2":
                    size = "Classroom, max 60 seats";
                    break;
                case "3":
                    size = "Group room, max 15 seats";
                    break;
                default:
                    Console.WriteLine("Invalid choice, please choose again.");
                    return RoomSize(); 
            }
            return size;
        }
        //Method to ask user yes or no
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
        //Method to show all rooms + properties
        public static void ListAll(List<Rooms> rooms)
        {


            foreach (var room in rooms)
            {

                Console.WriteLine($"Room Name: {room.RoomName}");
                Console.WriteLine($"Room Type: {room.GetType().Name}");
                Console.WriteLine($"Seat Amount: {room.SeatAmount}");

                if (room is Hall hall)
                {
                    Console.WriteLine($"Seat Limit: {hall.SeatLimit}"); //<-----------------VART TAR VI IN SEATLIMIT?
                    Console.WriteLine($"Has Projector: {hall.HasProjector}");
                    Console.WriteLine($"Has Whiteboard: {hall.HasWhiteboard}");
                }
                else if (room is ClassRoom classroom)
                {
                    Console.WriteLine($"Seat Limit: {classroom.SeatLimit}"); //<-----------------VART TAR VI IN SEATLIMIT?
                    Console.WriteLine($"Has Projector: {classroom.HasProjector}");
                    Console.WriteLine($"Has Whiteboard: {classroom.HasWhiteboard}");
                }
                else if (room is GroupRoom grouproom)
                {
                    Console.WriteLine($"Seat Limit: {grouproom.SeatLimit}");
                }
                Console.WriteLine("----------------------");
            }
        }
        //Method to show specific rooms
        public static String ChooseASpecificRoom(List<Rooms> rooms)
        {
            String roomName;
            int i = 1;
            foreach (var r in rooms)
            {
                Console.WriteLine($"{i}. {r.RoomName}");
                i++;
            }
            while (true)
            {
                Console.WriteLine("\nEnter the number for the corresponding option");
                string roomNum = Console.ReadLine();
                
                if (int.TryParse(roomNum, out int choice)) //Input choiche form list
                {
                    if (choice > 0 && choice <= rooms.Count)
                    {
                        roomName = rooms[choice-1].RoomName;
                        return roomName;
                    }
                    else { Console.WriteLine($"\n{choice} is not a valid choice, please try again"); continue; }
                }
                else { Console.WriteLine($"\n{choice} is not a valid choice, please try again"); continue; }
            }
        }

    }
}
