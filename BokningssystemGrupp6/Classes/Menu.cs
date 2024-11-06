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
        public static Menu MainMenu(/*Might need list of Rooms and Bookings depending on what the methods need */List<IRoom> rooms, List<Bookings>bookingsInfo)
        {
            String? menuChoice; // Declared a variable outside the switch, makes the code friendly to modification, can be removed and placed in the switch statement
            while (true) // Todo: make an exit condition to break loop, either as a universal method or specific in this menu
            {
                Console.WriteLine("Välkommen till bokningssystemet för skolans lokaler! \nFollowing options:" +
                    "\n1. Rooms information. \n2. Create a room \n3. Book a room " +
                    "\n4. Show bookings \n5. Update existing booking.");
                switch (menuChoice = Console.ReadLine())
                {
                    case "1": //Rooms.ListAndSortRooms(); break; 
                    case "2": //Rooms.CreateARoom(); break;
                    case "3": Bookings.BookARoom(bookingsInfo, rooms); break;
                    case "4": //Bookings.ListBookings(); break;
                    case "5": //Bookings.UppdateBookings(); break;
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
