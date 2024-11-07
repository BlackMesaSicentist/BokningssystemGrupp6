using BokningssystemGrupp6.Interfaces;

namespace BokningssystemGrupp6.Classes.LokalClasses
{
    internal class RoomsListAndSort
    {

        private readonly InputValidation _inputValidation;
        private int sortOption;
        public RoomsListAndSort(InputValidation inputValidation)
        {
            _inputValidation = inputValidation;
            sortOption = -1; // Default value, needed for menu selection validation
        }




        public void RoomsListAndSortStart(List<Rooms> rooms)
        {
            Console.WriteLine("List of rooms\nSelect list option:\n0. Show all rooms\n1. Show halls\n2. Show classrooms\n3. Show group rooms\n4. Rooms in order of number of seats, large - small\n5. Rooms in order of number of seats, small - large\n6. Rooms with projector\n7. Rooms with whiteboard\n8. Go back\n");

            string? menuChoice;
            sortOption = -1;

            while (true)
            {
                Console.Write("Enter your choice: ");
                menuChoice = Console.ReadLine();

                if (_inputValidation.IsEmpty(menuChoice))
                {
                    Console.WriteLine("Field cannot be empty. Try again.");
                    continue;
                }
                else if (!_inputValidation.IsNumber(menuChoice))
                {
                    Console.WriteLine("Input must be a number. Try again.");
                    continue;
                }
                else if (_inputValidation.IsNumberNegative(menuChoice))
                {
                    Console.WriteLine("Input must be a positive number. Try again.");
                    continue;
                }

                switch (menuChoice)
                {
                    case "0":
                        sortOption = 0;
                        Console.WriteLine("Show all rooms");
                        break;
                    case "1":
                        sortOption = 1;
                        Console.WriteLine("Show halls");
                        break;
                    case "2":
                        sortOption = 2;
                        Console.WriteLine("Show classrooms");
                        break;
                    case "3":
                        sortOption = 3;
                        Console.WriteLine("Show group rooms");
                        break;
                    case "4":
                        sortOption = 4;
                        Console.WriteLine("Rooms in order of number of seats, large to small");
                        break;
                    case "5":
                        sortOption = 5;
                        Console.WriteLine("Rooms in order of number of seats, small to large");
                        break;
                    case "6":
                        sortOption = 6;
                        Console.WriteLine("Rooms with projector");
                        break;
                    case "7":
                        sortOption = 7;
                        Console.WriteLine("Rooms with whiteboard");
                        break;
                    case "8":
                        Console.WriteLine("Going back...");
                        sortOption = 8;
                        break;
                    default:
                        Console.WriteLine("Not a valid selection. Press \"Enter\" and try again.");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                }

                if (sortOption != -1) break;
            }

            RoomInfoWithSort(rooms);
        }

        private void RoomInfoWithSort(List<Rooms> rooms)
        {
            List<Rooms> sortRoomList = rooms;

            switch (sortOption)
            {
                case 1: // Show halls
                    Console.WriteLine("Show halls\n");
                    sortRoomList = rooms.OfType<Hall>().Cast<Rooms>().ToList();
                    break;
                case 2: // Show classrooms
                    Console.WriteLine("Show classrooms\n");
                    sortRoomList = rooms.OfType<Classroomawda>().Cast<Rooms>().ToList();
                    break;
                case 3: // Show group rooms
                    Console.WriteLine("Show group rooms\n");
                    sortRoomList = rooms.OfType<GroupRoom>().Cast<Rooms>().ToList();
                    break;
                case 4: // Rooms in order of number of seats, large - small
                    Console.WriteLine("Rooms in order of number of seats, large - small\n");
                    sortRoomList = sortRoomList.OrderByDescending(room => room.SeatAmount).ToList();
                    break;
                case 5: // Rooms in order of number of seats, small - large
                    Console.WriteLine("Rooms in order of number of seats, small - large\n");
                    sortRoomList = sortRoomList.OrderBy(room => room.SeatAmount).ToList();
                    break;
                case 6: // Rooms with projector
                    Console.WriteLine("Rooms with projector\n");
                    sortRoomList = rooms.Where(room => room is Hall hall && hall.HasProjector).Cast<Rooms>().ToList();
                    break;
                case 7: // Rooms with whiteboard
                    Console.WriteLine("Rooms with whiteboard\n");
                    sortRoomList = rooms.Where(room => room is Hall hall && hall.HasWhiteboard).Cast<Rooms>().ToList();
                    break;
                case 8: // Show all rooms
                    Console.WriteLine("Show all rooms\n");
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }



            // Display the filtered or sorted list
            foreach (var room in sortRoomList)
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
                else if (room is Classroomawda classroom)
                {
                    Console.WriteLine($"Seat Limit: {classroom.SeatLimit}");
                    Console.WriteLine($"Has Projector: {classroom.HasProjector}");
                    Console.WriteLine($"Has Whiteboard: {classroom.HasWhiteboard}");
                }
                else if (room is GroupRoom groupRoom)
                {
                    Console.WriteLine($"Seat Limit: {groupRoom.SeatLimit}");
                }

                Console.WriteLine("----------------------");
            }




        }





    }
}
