﻿using System.Drawing;
using System.Text.Json;
using BokningssystemGrupp6.Classes;
using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Collections.Generic;
//Av: Angelica Bergström, David Berglin, Adam Axelsson-Hedman, Alexander Bullerjahn

namespace BokningssystemGrupp6
{

    internal class Program
    {
        static void Main(string[] args)
        {

            InputValidation inputValidation = new InputValidation();
            Menu menu = new Menu(inputValidation);

            string listBooking = JsonSerializer.Serialize(BookingsInfo);
            File.WriteAllText("BokningssystemGrupp6.json", listBooking);
            //List for rooms (string roomName, string size, int maxPeople, bool hasWhiteboard, bool hasProjector)
            List<Rooms> rooms = new List<Rooms>();

            //List for bookings (string userName, string roomName, DateTime DateTimeStart, DateTime DateTimeEnd)
            List<Bookings> bookingsInfo = new List<Bookings>();

            //Method to deserialize lists
            Save.UnpackFileRooms(rooms);
            Save.UnPackFileBooking(bookingsInfo);

            //Method to show and use menu
            Menu.MainMenu(rooms, bookingsInfo);
        }
    }
}
