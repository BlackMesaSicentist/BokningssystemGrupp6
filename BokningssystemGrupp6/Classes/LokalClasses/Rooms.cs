using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes.LokalClasses
{
    internal class Rooms: IRoom, IListable
    {

        private readonly InputValidation _inputValidation;
        public Rooms(InputValidation inputValidation)
        {
            _inputValidation = inputValidation;
        }

        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public int SeatAmount { get; set; }

        protected Rooms(string roomName, string roomType, int seatAmount)
        {
            RoomName = roomName;
            RoomType = roomType;
            SeatAmount = seatAmount;
        }

        public void CreateARoom(List<IRoom> rooms) {

            Console.Clear();

            // Room name
            string roomName = RoomNameInput(rooms);
            //Console.WriteLine("Enter name of the room: ");
            //string? roomName = Console.ReadLine();

            // Room type / size / seat limit
            var (roomSizeSelect, roomSizeName, seatLimit) = RoomSize();


            //Console.WriteLine("Enter how many seats: ");
            //int seats = Convert.ToInt32(Console.ReadLine());

            // Seats
            int seats = SeatsInput(rooms, seatLimit);

            // Projector and whiteboard select
            bool hasProjector = false;
            bool hasWhiteBoard = false;
            // If user selects group room skip asking user for projector/whiteboard.
            if (roomSizeSelect != 3)
            {
                Console.WriteLine("Do you need a projector? Y/N");
                hasProjector = AskUser();
                Console.WriteLine("Do you need a whiteboard? Y/N");
                hasWhiteBoard = AskUser();
            }
            if (roomSizeSelect == 1)
            {
                Console.WriteLine("Adding a Large Room...");
                rooms.Add(new Hall(roomName, roomSizeName, seats, seatLimit, hasProjector, hasWhiteBoard));
            }
            else if (roomSizeSelect == 2)
            {
                Console.WriteLine("Adding a Medium Room...");
                rooms.Add(new ClassRoom(roomName, roomSizeName, seats, seatLimit, hasProjector, hasWhiteBoard));
            }
            else if (roomSizeSelect == 3)
            {
                Console.WriteLine("Adding a Small Room...");
                rooms.Add(new GroupRoom(roomName, roomSizeName, seats, seatLimit));
            }

            ShowList(rooms);

        }


        private string RoomNameInput(List<IRoom> rooms)
        {
            Console.WriteLine("Enter name of the room: ");
            string tempName = Console.ReadLine().Trim();
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


        public static (int, string, int) RoomSize()
        {
            Console.WriteLine("Choose room size:\n1.Large\n2.Medium\n3.Small");
            string? option;
            int roomSelect;
            string sizeName = "";
            int seatLimit;
            switch (option = Console.ReadLine())
            {
                case "1":
                    roomSelect = 1;
                    sizeName = "Hall";
                    seatLimit = 120;
                    break;
                case "2":
                    roomSelect = 2;
                    sizeName = "Class room";
                    seatLimit = 60;
                    break;
                case "3":
                    roomSelect = 3;
                    sizeName = "Group room";
                    seatLimit = 15;
                    break;
                default:
                    Console.WriteLine("Invalid choice, please choose again.");
                    return RoomSize(); 
            }
            return (roomSelect, sizeName, seatLimit);
        }


        private int SeatsInput(List<IRoom> rooms, int seatLimit)
        {
            Console.WriteLine($"Enter seats: (Cant exceed: {seatLimit}) ");
            int seatsOk;
            string tempSeatsStr = Console.ReadLine().Trim();


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

                if (room is Hall largeRoom)
                {
                    Console.WriteLine($"Seat Limit: {largeRoom.SeatLimit}");
                    Console.WriteLine($"Has Projector: {largeRoom.HasProjector}");
                    Console.WriteLine($"Has Whiteboard: {largeRoom.HasWhiteboard}");
                }
                else if (room is ClassRoom mediumRoom)
                {
                    Console.WriteLine($"Seat Limit: {mediumRoom.SeatLimit}");
                    Console.WriteLine($"Has Projector: {mediumRoom.HasProjector}");
                    Console.WriteLine($"Has Whiteboard: {mediumRoom.HasWhiteboard}");
                }
                else if (room is GroupRoom smallRoom)
                {
                    Console.WriteLine($"Seat Limit: {smallRoom.SeatLimit}");
                }

                Console.WriteLine("----------------------");
            }
        }

        // What is this used for or should be used for?
        public static void ChooseASpecificRoom(List<IRoom> rooms, String roomName)
        {
            int index = 0;
            foreach (var room in rooms)
            {
                Console.WriteLine($"Alternativ {index++} \nNamn på lokal: {room.RoomName}, Typ av lokal: {room.RoomType}, Hur många personer får plats i lokalen: {room.SeatAmount}");
                index++;
            }
            while (true);
            {
                Console.WriteLine("Mata in siffran för motsvarande alternativ");
                if (int.TryParse(Console.ReadLine(), out int choice)) //Input choiche form list
                {
                    if (choice > 0 && choice <= rooms.Count)
                    {
                        choice--; // Have to shrink by 1 to match list index
                        roomName = rooms[choice].RoomName;
                    }
                }
            }

        }

    }
}
