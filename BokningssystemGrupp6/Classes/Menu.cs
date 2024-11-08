﻿using BokningssystemGrupp6.Classes.LokalClasses;
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

        public void MainMenu(List<Rooms> rooms, List<Bookings> bookingsInfo)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            bool menu1 = true;
            // Declared a variable outside the switch, makes the code friendly to modification, can be removed and placed in the switch statement
            String? menuChoice;
            while (menu1)
            {
                Console.WriteLine("\n       WELCOME!\nYou are now able to book rooms on the school premises!" +
                    "\n\n1. Create a room " +
                    "\n2. Book a room " +
                    "\n3. Show rooms " +
                    "\n4. Show bookings " +
                    "\n5. Update existing booking" +
                    "\n0. Exit program");
                switch (menuChoice = Console.ReadLine())
                {
                    //Create a room
                    case "1": Console.Clear(); _rooms.CreateARoom(rooms); BackToMenu(); break; 
                    //Book a room
                    case "2": Console.Clear(); Bookings.BookARoom(bookingsInfo, rooms); BackToMenu(); break;
                    //Show bookings by year or room
                    case "4": Console.Clear(); Bookings.ListAllBookingsByYearOrRoom(bookingsInfo, rooms); BackToMenu(); break;
                    //Update existing booking
                    case "5": Console.Clear(); Bookings.UpdateBooking(bookingsInfo, rooms); BackToMenu(); break;
                    //case "7": Console.Clear(); _roomsListAndSort.RoomsListAndSortStart(rooms); BackToMenu(); break;
                    //Ends program
                    case "0": menu1 = false; break;
                    case "99": Console.Clear(); Bookings.DeleteBooking(bookingsInfo, rooms); break; //Todo: remove after test of this method and fixing of menu
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
        // Back to menu and clear function
        public static void BackToMenu()
        {
            Console.WriteLine("\nTryck valfri tangent för att återgå till meny");
            Console.ReadKey();
            Console.Clear();
        }

    }
}
