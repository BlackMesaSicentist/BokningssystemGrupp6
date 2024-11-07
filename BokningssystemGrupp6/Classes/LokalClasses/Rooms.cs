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
    public class Rooms: IRoom, IListable
    {

        private readonly InputValidation _inputValidation;
        public Rooms(InputValidation inputValidation)
        {
            _inputValidation = inputValidation;
        }

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

        public void CreateARoom(List<Rooms> rooms) {

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
            // Adding Hall
            if (roomSizeSelect == 1)
            {
                Console.WriteLine("Adding a Hall...");
                rooms.Add(new Hall(roomName, roomSizeName, seats, seatLimit, hasProjector, hasWhiteBoard));
                Save.SaveFile(rooms);
            }
            // Adding Classroom
            else if (roomSizeSelect == 2)
            {
                Console.WriteLine("Adding a Classroom...");
                rooms.Add(new Classroom(roomName, roomSizeName, seats, seatLimit, hasProjector, hasWhiteBoard));
                Save.SaveFile(rooms);
            }
            // Adding Group room
            else if (roomSizeSelect == 3)
            {
                Console.WriteLine("Adding a Group room...");
                rooms.Add(new GroupRoom(roomName, roomSizeName, seats, seatLimit));
                Save.SaveFile(rooms);
            }

            

        }


        private string RoomNameInput(List<Rooms> rooms)
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
            Console.WriteLine("Choose room size:\n1.Hall\n2.Classroom\n3.Group Room");
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
                    sizeName = "Classroom";
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


        private int SeatsInput(List<Rooms> rooms, int seatLimit)
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

        public static void ListAll(List<Rooms> rooms)
        {
            foreach (var room in rooms)
            {
                Console.WriteLine($"Room Type: {room.GetType().Name}");
                Console.WriteLine($"Room Name: {room.RoomName}");
                Console.WriteLine($"Room Type Description: {room.RoomType}");
                Console.WriteLine($"Seat Amount: {room.SeatAmount}");

                if (room is Hall hall)
                {
                    Console.WriteLine($"Seat Limit: {hall.SeatLimit}");
                    Console.WriteLine($"Has Projector: {hall.HasProjector}");
                    Console.WriteLine($"Has Whiteboard: {hall.HasWhiteboard}");
                }
                else if (room is Classroom classroom)
                {
                    Console.WriteLine($"Seat Limit: {classroom.SeatLimit}");
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

        // What is this used for or should be used for?
        public static void ChooseASpecificRoom(List<Rooms> rooms, String roomName)
        {
            int index = 0;
            foreach (var room in rooms)
            {
                Console.WriteLine($"Alternativ {index++} \nNamn på lokal: {room.RoomName}, Typ av lokal: {room.RoomType}, Hur många personer får plats i lokalen: {room.SeatAmount}");
                index++;
            }
            while (true)
            {
                Console.WriteLine("Mata in siffran för motsvarande alternativ");
                if (int.TryParse(Console.ReadLine(), out int choice)) //Input choiche form list
                {
                    if (choice > 0 && choice <= rooms.Count)
                    {
                        choice--; // Have to shrink by 1 to match list index
                        roomName = rooms[choice].RoomName;
                        break;
                    }
                    else { Console.WriteLine($"{choice} är inte ett giltigt val, försök igen"); continue; }
                }
                else { Console.WriteLine($"{choice} är inte ett giltigt val, försök igen"); continue; }
            }
        }
    }
}
