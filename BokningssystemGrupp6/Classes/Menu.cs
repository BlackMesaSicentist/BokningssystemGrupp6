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
        /*Might need list of Rooms and Bookings depending on what the methods need */
        public static void MainMenu(List<Rooms> rooms, List<Bookings>bookingsInfo)
        {
            bool menu1 = true;
            String? menuChoice; // Declared a variable outside the switch, makes the code friendly to modification, can be removed and placed in the switch statement
            while (menu1)
            {
                Console.WriteLine("WELCOME!\n You are now able to book rooms for the school premises!" +
                    "\n\n1. Rooms information \n2. Create a room \n3. Book a room " +
                    "\n4. Show bookings \n5. Update existing booking \n6. Show booking for specific year" +
                    "\n7. Exit program");
                switch (menuChoice = Console.ReadLine())
                {
                    case "1": Rooms.ListAll(rooms); break; //Rooms information.
                    case "2": Rooms.CreateARoom(rooms); break; //Create a room
                    case "3": Bookings.BookARoom(bookingsInfo, rooms); break; //Book a room
                    case "4": Bookings.ListAll(bookingsInfo); break; //Show bookings
                    case "5": Bookings.UpdateBooking(bookingsInfo,rooms); break; //Update existing booking
                    //Show bookings on a specific room at a specific year
                    case "6": Bookings.CreateAndDisplayListOfBookingsSpecificRoomAndDate(bookingsInfo, rooms); break;       
                    case "7": menu1 = false; break; //End program
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
