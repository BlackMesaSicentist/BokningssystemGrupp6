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
        
        // Create a room
        public void CreateARoom(List<Rooms> rooms) {

            Console.Clear();

            // Room name
            string roomName = RoomNameInput(rooms);
            //Console.WriteLine("Enter name of the room: ");
            //string? roomName = Console.ReadLine();

            // Room type int / list type / seat limit
            var (roomSizeSelect, roomType, seatLimit) = RoomSize();


            //Console.WriteLine("Enter how many seats: ");
            //int seats = Convert.ToInt32(Console.ReadLine());

            // Seats
            int seats = SeatsInput(rooms, seatLimit);

            // Projector and whiteboard select
            bool hasProjector = false;
            bool hasWhiteboard = false;
            // If user selects group room skip asking user for projector/whiteboard.
            if (roomSizeSelect != 3)
            {
                Console.WriteLine("Does the venue have a projector? Y/N");
                hasProjector = AskUser();
                Console.WriteLine("Does the venue have a whiteboard? Y/N");
                hasWhiteboard = AskUser();
            }
            //different types of venue
            if (roomSizeSelect == 1)
            {
                Console.WriteLine("\nNew hall has been added");

                rooms.Add(new Hall(roomName, roomType, seats, seatLimit, hasProjector, hasWhiteboard));
                Save.SaveFile(rooms);
            }
            else if (roomSizeSelect == 2)
            {
                Console.WriteLine("\nNew classroom has been added");
              
                rooms.Add(new Classroom(roomName, roomType, seats, seatLimit, hasProjector, hasWhiteboard));
                Save.SaveFile(rooms);
            }
            else if (roomSizeSelect == 3)
            {
                Console.WriteLine("\nNew group room has been added");
                
                rooms.Add(new GroupRoom(roomName, roomType, seats, seatLimit));
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

        //Method to determine size
        private static (int, string, int) RoomSize()
        {
            Console.WriteLine("Choose room size:\n1.Hall, 120 seat limit \n2.Classroom, 60 seat limit \n3.Group Room, 15 seat limit ");
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

        // Seat input
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

        //Method to ask user yes or no
        private static bool AskUser()
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
            Console.WriteLine("ALL ROOMS");
            Console.WriteLine("{0,-12}{1,-20}{2,-14}{3,-14}{4,-18}{5,-14}", "Type", "Name", "Seat amount", "Seat Limit", "Has Projector", "Has Whiteboard");
            Console.WriteLine(new string('-', 100));
          
            foreach (var room in rooms)
            {

                Console.Write($"\n{room.GetType().Name,-12}");
                Console.Write($"{room.RoomName,-20}");
                Console.Write($"{room.SeatAmount,-15}");

                if (room is Hall hall)
                {
                    Console.Write($"{hall.SeatLimit,-15}");
                    Console.Write($"{hall.HasProjector,-18}");
                    Console.Write($"{hall.HasWhiteboard,-18}");
                }
                else if (room is Classroom classroom)
                {

                    Console.Write($"{classroom.SeatLimit,-15}");
                    Console.Write($"{classroom.HasProjector,-18}");
                    Console.Write($"{classroom.HasWhiteboard,-18}");
                }
                else if (room is GroupRoom grouproom)
                {
                  
                    Console.Write($"{grouproom.SeatLimit,-15}");
                }
                
                
            }
        }
        //Method to show specific rooms
        //Used by UpdateBooking method and CreateAndDisplayListOfBookingsSpecificRoomAndDate
        //They need a room name and this is a selection method to choose room and then returns room name
        public static String ChooseASpecificRoom(List<Rooms> rooms)
        {
            String roomName; //String that gets name and is returned
            int i = 1; //Used to display each room numbered from 1 to rooms.count
            foreach (var r in rooms)
            {
                Console.WriteLine($"{i}. {r.RoomName}");
                i++;
            }
            while (true)
            {
                Console.WriteLine("\nEnter the number for the corresponding option");
                string roomNum = Console.ReadLine(); //Input number to choose room
                
                if (int.TryParse(roomNum, out int choice)) //Convert input to int
                {
                    //Makes sure the choice of room is within the lenght span of room
                    if (choice > 0 && choice <= rooms.Count) 
                    {
                        roomName = rooms[choice-1].RoomName; //-1 on choice so it matches indexing of lists
                        return roomName; //Returns room name
                    }
                    else { Console.WriteLine($"\n{roomNum} is not a valid choice, please try again"); continue; }
                }
                else { Console.WriteLine($"\n{roomNum} is not a valid choice, please try again"); continue; }
            }
        }

    }
}
