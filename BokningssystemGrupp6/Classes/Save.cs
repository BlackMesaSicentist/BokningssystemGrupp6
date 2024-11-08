﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using BokningssystemGrupp6.Classes;
using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;

namespace BokningssystemGrupp6.Classes
{
    public class Save
    {
        //Method to save lists
        public static void SaveFile<T>(List<T> listToSave)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA),
                // Converts (Deserialize) JSON to usable list 
                Converters = { new RoomsConverter() }

            };

            if (listToSave is List<Rooms>)
            {
                string listRoom = JsonSerializer.Serialize(listToSave, options);
                File.WriteAllText("RoomList.json", listRoom);
            }

            if (listToSave is List<Bookings>)
            {
                string listBooking = JsonSerializer.Serialize(listToSave, options);
                File.WriteAllText("BookingList.json", listBooking);
            }

        }
        //Method to unpack lists
        public static void UnPackFileBooking(List<Bookings> bookingList)
        {
            //To be able to read ÅÄÖ
            var options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA),
                // Converts (Deserialize) JSON to usable list using Polymorphic Deserialization
                Converters = { new RoomsConverter() }
            };
            //reads list for bookings
            if (File.Exists("BookingsList.json"))
            {
                string readBooking = File.ReadAllText("BookingList.json");
                if (String.IsNullOrEmpty(readBooking)) //Check if file contains no data
                {
                    //Todo: remove and replace with something better than just that text
                    Console.WriteLine("BookingList Json is empty!");
                }
                else
                {
                    bookingList = JsonSerializer.Deserialize<List<Bookings>>(readBooking);
                }
            }

        }
        public static void UnpackFileRooms(List<Rooms> roomList)
        {

            //To be able to read ÅÄÖ
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,

                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA),
                Converters = { new RoomsConverter() }

            };
            //If file exist we deserialize to templist
            if (File.Exists("RoomList.json"))
            {
                string readRoom = File.ReadAllText("RoomList.json");
               
                if (String.IsNullOrEmpty(readRoom)) //Check if file contains no data
                {
                    //Todo: remove and replace with something better than just that text
                    Console.WriteLine("RoomList Json is empty!");
                }
                else
                {
                    //* var tempList = JsonSerializer.Deserialize<List<JsonElement>>(readRoom);
                    //Emptying the list so we don't get duplicates when we add in the end
                    roomList.Clear();

                    // Deserializer
                    var tempList = JsonSerializer.Deserialize<List<Rooms>>(readRoom, options);
                    roomList.AddRange(tempList);

                    //* foreach (var element in tempList)
                    //* {
                    //*    var roomType = element.GetProperty("RoomType").GetString();
                    //*    var json = element.GetRawText();

                    //*    Rooms room = roomType 
                    //*    switch
                    //*    {
                    //*        "Hall" => JsonSerializer.Deserialize<Hall>(json, options),
                    //*        "Classroom" => JsonSerializer.Deserialize<Classroom>(json, options),
                    //*        "Group room" => JsonSerializer.Deserialize<GroupRoom>(json, options),
                    //*        _ => throw new JsonException($"Unknown room type: {roomType}")
                    //*    };
                    //*    //Adds back to list
                    //*    roomList.Add(room);
                    //* }
                }
            }
        }
    }
}
