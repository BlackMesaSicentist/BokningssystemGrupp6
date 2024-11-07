﻿using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes
{
    internal class Menu
    {

        private readonly InputValidation _inputValidation;
        private readonly Rooms _rooms;

        public Menu(InputValidation inputValidation)
        {
            _inputValidation = inputValidation;
            _rooms = new Rooms(inputValidation);
        }

        public void MainMenu(/*Might need list of Rooms and Bookings depending on what the methods need */List<IRoom> rooms, List<Bookings>bookingsInfo, JsonSerializerOptions options)
        {
            String? menuChoice; // Declared a variable outside the switch, makes the code friendly to modification, can be removed and placed in the switch statement

            // Reload the rooms list from the JSON file at the start of the program
            JsonFunctions.ReloadRoomsFromJson(ref rooms, options);

            while (true) // Todo: make an exit condition to break loop, either as a universal method or specific in this menu
            {
                Console.WriteLine("Välkommen till bokningssystemet för skolans lokaler! \nDu har följande alternativ:" +
                    "\n1. Lista och filtrerar lokaler efter egenskaper \n2. Skapa en sal \n3. Boka en sal " +
                    "\n4. Se bokningar \n5. Uppdatera en befintlig bokning\n0. Avsluta programmet och spara rumslistan");
                switch (menuChoice = Console.ReadLine())
                {
                    case "1": //Rooms.ListAndSortRooms(); break; 
                    case "2": _rooms.CreateARoom(rooms, options); break;
                    case "3": //Bookings.BookARoom(bookingsInfo); break;
                    case "4": //Bookings.ListBookings(); break;
                    case "5": //Bookings.UppdateBookings(); break;
                    case "0":
                        Console.WriteLine("Avslutar programmet...");
                        return; // Exit MainMenu, returning to Main
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
