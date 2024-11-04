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
            _inputValidation = inputValidation;
        }


        public void CreateARoom(List<IRoom> rooms) {

            string roomName = RoomNameInput(rooms);
            //Console.WriteLine("Enter name of the room: ");
            //string? roomName = Console.ReadLine();
            var (roomSize, seatLimit) = RoomSize();
            //string roomSize = RoomSize();

            int seats = SeatsInput(rooms, seatLimit);

            //Console.WriteLine("Enter how many seats: ");
            //int seats = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Do you need a projector? Y/N");
            bool hasProjector = AskUser();
            Console.WriteLine("Do you need a whiteboard? Y/N");
            bool hasWhiteBoard = AskUser();

            if (roomSize == "Large")
            {
                Console.WriteLine("Adding a Large Room...");
                rooms.Add(new LargeRoom(roomName, roomSize, seats, seatLimit, hasProjector, hasWhiteBoard));
            }
            else if (roomSize == "Medium")
            {
                Console.WriteLine("Adding a Medium Room...");
                rooms.Add(new MediumRoom(roomName, roomSize, seats, seatLimit, hasProjector, hasWhiteBoard));
            }
            else if (roomSize == "Small")
            {
                Console.WriteLine("Adding a Small Room...");
                rooms.Add(new SmallRoom(roomName, roomSize, seats, seatLimit));
            }

            ShowList(rooms);

        }
        public static (string, int) RoomSize()
        {
            Console.WriteLine("Choose room size:\n1.Large\n2.Medium\n3.Small");
            string? option;
            string size = ""; 
            int seatLimit = 0;

            switch (option = Console.ReadLine())
            {
                case "1":
                    size = "Large";
                    seatLimit = 120;
                    break;
                case "2":
                    size = "Medium";
                    seatLimit = 60;
                    break;
                case "3":
                    size = "Small";
                    seatLimit = 15;
                    break;
                default:
                    Console.WriteLine("Invalid choice, please choose again.");
                    return RoomSize(); 
            }
            return (size, seatLimit);
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

        // Create a room namn, name cannot be an already used name
        private string RoomNameInput(List<IRoom> rooms)
        {
            Console.WriteLine("Enter name of the room: ");
            string tempName = Console.ReadLine().Trim();
            Console.WriteLine(tempName);
            // Input validation
            while (true)
            {
                // Check if input is empty
                if (_inputValidation.IsEmpty(tempName))
                {
                    Console.WriteLine("Field cannot be empty, Try again");
                }
                // Check if input is already used
                else if (_inputValidation.IsNameUsed(rooms, tempName))
                {
                    Console.WriteLine("Name is alread used. Try again.");
                }
                else 
                {
                    break;
                }

                // If input is wrong, a new input can be input
                tempName = Console.ReadLine();
            }

            return tempName;
        }

        private int SeatsInput(List<IRoom> rooms, int seatLimit)
        {
            Console.WriteLine("Enter seats amount: ");
            int seatsOk;
            string tempSeatsStr = Console.ReadLine().Trim();

            Console.WriteLine(tempSeatsStr);


            while (true)
            {
                // Check if input is empty

                if (_inputValidation.IsEmpty(tempSeatsStr))
                {
                    Console.WriteLine("Field cannot be empty, Try again");
                }
                // Check if input is a number
                else if (!_inputValidation.IsNumber(tempSeatsStr))
                {
                    Console.WriteLine("Input must be a number. Try again.");
                }
                // Check if the number i negative
                else if (_inputValidation.IsNumberNegative(tempSeatsStr))
                {
                    Console.WriteLine("Input must be a positive number. Try Again");
                }
                // Check if seats is larger than seat limit
                else if (_inputValidation.IsGreaterThanSeatLimit(tempSeatsStr, seatLimit)) 
                {
                    Console.WriteLine("Input cannot be larger than seat limit, Try Again");
                }
                else
                {
                    // Convert string to int
                    seatsOk = _inputValidation.ConvertToInt(tempSeatsStr);
                    break;
                }
                tempSeatsStr = Console.ReadLine();
            }
            return seatsOk;
        }
    }

}
