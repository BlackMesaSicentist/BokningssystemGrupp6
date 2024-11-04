using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes.LokalClasses
{
    internal class Rooms
    {
        private readonly InputValidation _inputValidation;
        public Rooms(InputValidation inputValidation)
        {
            InputValidation = inputValidation;
        }

        public InputValidation InputValidation { get; }

        public void CreateARoom(List<IRoom> rooms) {


            string roomName = RoomName(rooms);
            //Console.WriteLine("Enter name of the room: ");
            //string? roomName = Console.ReadLine();
            string roomSize = RoomSize();
            Console.WriteLine("Enter how many seats: ");
            int seats = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Do you need a projector? Y/N");
            bool hasProjector = AskUser();
            Console.WriteLine("Do you need a whiteboard? Y/N");
            bool hasWhiteBoard = AskUser();

            if (roomSize == "Large")
            {
                Console.WriteLine("Adding a Large Room...");
                int seatLimit = 120;
                rooms.Add(new LargeRoom(roomName, roomSize, seats, seatLimit, hasProjector, hasWhiteBoard));
            }
            else if (roomSize == "Medium")
            {
                Console.WriteLine("Adding a Medium Room...");
                int seatLimit = 60;
                rooms.Add(new MediumRoom(roomName, roomSize, seats, seatLimit, hasProjector, hasWhiteBoard));
            }
            else if (roomSize == "Small")
            {
                Console.WriteLine("Adding a Small Room...");
                int seatLimit = 15;
                rooms.Add(new SmallRoom(roomName, roomSize, seats, seatLimit));
            }

            ShowList(rooms);

        }
        public static string RoomSize()
        {
            Console.WriteLine("Choose room size:\n1.Large\n2.Medium\n3.Small");
            string? option;
            string size = ""; 

            switch (option = Console.ReadLine())
            {
                case "1":
                    size = "Large";
                    break;
                case "2":
                    size = "Medium";
                    break;
                case "3":
                    size = "Small";
                    break;
                default:
                    Console.WriteLine("Invalid choice, please choose again.");
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

        public static void ShowList(List<IRoom> rooms)
        {
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
        }

        // Skapa en Idnummer (till skillnad från kontonumret så behöver detta inte vara unikt)
        private string RoomName(List<IRoom> rooms)
        {
            Console.WriteLine("Enter name of the room: ");
            string nameOk = "";
            string tempName = Console.ReadLine();

            // Input validering
            while (true)
            {
                // Kollar om input är tom
                if (_inputValidation.IsEmpty(tempName))
                {
                    Console.WriteLine("Valet kan inte vara tomt. Försök igen.");
                }
                // Kollar om input är ett nummer
                else if (!_inputValidation.IsNameUsed(rooms, tempName))
                {
                    Console.WriteLine("Valet måste vara ett nummer. Försök igen.");
                }

                // Kollar om talet är negativt
                //else if (_inputValidator.IsNumberNegative(tempIdNrStr))
                //{
                //    Console.WriteLine("Valet måste vara ett positivt tal. Försök igen");
                //}
                //else
                //{
                //    // Konverterar string till nummer
                //    accountIdNrOk = _inputValidator.ConvertToInt(tempIdNrStr);
                //    break;
                //}

                // Om input är fel får man mata in ett nytt tal
                tempName = Console.ReadLine();
            }

            return nameOk;
        }

    }
}
