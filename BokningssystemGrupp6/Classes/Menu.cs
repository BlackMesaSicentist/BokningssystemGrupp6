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
        public static Menu MainMenu(/*Might need list of Rooms and Bookings depending on what the methods need */List<IRoom> rooms)
        {
            String? menuChoice; // Declared a variable outside the switch, makes the code friendly to modification, can be removed and placed in the switch statement
            while (true) // Todo: make an exit condition to break loop, either as a universal method or specific in this menu
            {
                Console.WriteLine("Välkommen till bokningssystemet för skolans lokaler! \nDu har följande alternativ:" +
                    "\n1. Lista och filtrerar lokaler efter egenskaper \n2. Skapa en sal \n3. Boka en sal " +
                    "\n4. Se bokningar \n5. Uppdatera en befintlig bokning");
                switch (menuChoice = Console.ReadLine())
                {
                    //case "1": Rooms.ListAndSortRooms(); break; 
                    case "2": Rooms.CreateARoom(rooms); break;
                    //case "3": Bookings.BookARoom(); break;
                    //case "4": Bookings.ListBookings(); break;
                    //case "5": Bookings.UppdateBookings(); break;
                    default:
                    {
                        Console.WriteLine("Inte ett giltigt val, tryck på \"Enter\" och försök igen");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                }
            }
        }
    }
}
