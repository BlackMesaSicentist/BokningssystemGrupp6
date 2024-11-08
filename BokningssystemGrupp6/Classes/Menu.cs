using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes
{
    internal class Menu
    {

        private readonly InputValidation _inputValidation;
        private readonly Rooms _rooms;
        private readonly RoomsListAndSort _roomsListAndSort;

        public Menu(InputValidation inputValidation)
        {
            _inputValidation = inputValidation;
            _rooms = new Rooms(inputValidation);
            _roomsListAndSort = new RoomsListAndSort(inputValidation);
        }


        public void MainMenu(/*Might need list of Rooms and Bookings depending on what the methods need */List<Rooms> rooms, List<Bookings> bookingsInfo)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            bool menu1 = true;
            // Declared a variable outside the switch, makes the code friendly to modification, can be removed and placed in the switch statement
            String? menuChoice;
            while (menu1)
            {
                Console.WriteLine("\n       WELCOME!\nYou are now able to book rooms on the school premises!" +
                    "\n\n1. Create a room \n2. Book a room \n3. Show rooms \n4. Show bookings \n5. Update existing booking \n" +
                    "\n7. Show room list with sort" + "\n0. Exit program");
                switch (menuChoice = Console.ReadLine())
                {
                    case "1": Console.Clear(); _rooms.CreateARoom(rooms); Rooms.BackToMenu(); break; //Create a room

                    case "2": Console.Clear(); Bookings.BookARoom(bookingsInfo, rooms); Rooms.BackToMenu(); break; //Book a room
                    case "3": Console.Clear(); Rooms.ListAll(rooms); Rooms.BackToMenu(); break; //Rooms information.
                    case "4": Console.Clear(); Bookings.ListAllBookingsByYearOrRoom(bookingsInfo, rooms); Rooms.BackToMenu(); break; //Show bookings
                    case "5": Console.Clear(); Bookings.UpdateBooking(bookingsInfo, rooms); Rooms.BackToMenu(); break; //Update existing booking
                                                                                                                       //Show bookings on a specific room at a specific year
                                                                                                                       //case "6": Console.Clear(); Bookings.CreateAndDisplayListOfBookingsSpecificRoomAndDate(bookingsInfo, rooms); Rooms.BackToMenu(); break;       

                    // Show room list with sort
                    case "7": _roomsListAndSort.RoomsListAndSortStart(rooms); Rooms.BackToMenu(); break;
                    case "0": menu1 = false; break; //Ends program
                    default:
                        {
                            Console.WriteLine("Invalid option, press \"Enter\"to try again");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                }
            }
        }
    }
}
