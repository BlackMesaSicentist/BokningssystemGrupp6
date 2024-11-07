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
            //Saves list of rooms
            if (listToSave is List<Rooms>)
            {
                string listRoom = JsonSerializer.Serialize(listToSave);
                File.WriteAllText("RoomList.json", listRoom);
            }
            //Saves list of bookings
            if (listToSave is List<Bookings>)
            {
                string listBooking = JsonSerializer.Serialize(listToSave);
                File.WriteAllText("BookingList.json", listBooking);
            }

        }
        //Method to unpack bookinglist
        public static void UnPackFileBooking(ref List<Bookings> bookingList)
        {
            //To be able to read ÅÄÖ
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA)
            };

            //Reads list for bookings
            if (File.Exists("BookingList.json"))
            {
                string readBooking = File.ReadAllText("BookingList.json");

                //Check if file contains no data
                if (string.IsNullOrEmpty(readBooking))
                {
                    //Todo: remove and replace with something better than just that text
                    Console.WriteLine("BookingList JSON is empty!");
                }
                else
                {
                    //Deserialize into the reference parameter
                    bookingList = JsonSerializer.Deserialize<List<Bookings>>(readBooking, options);
                }
            }
        }
        //Method to unpack roomlist
        public static void UnpackFileRooms(List<Rooms> roomList)
        {

            //To be able to read ÅÄÖ
            var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA) };
            //If file exist we deserialize to templist
            if (File.Exists("RoomList.json"))
            {
                string readRoom = File.ReadAllText("RoomList.json");
                //Check if file contains no data
                if (String.IsNullOrEmpty(readRoom))
                {
                    //Todo: remove and replace with something better than just that text
                    Console.WriteLine("RoomList Json is empty!"); 
                }
                else 
                {
                    var tempList = JsonSerializer.Deserialize<List<JsonElement>>(readRoom);
                    //Emptying the list so we don't get duplicates when we add in the end
                    roomList.Clear();

                    foreach (var element in tempList)
                    {
                        var roomType = element.GetProperty("RoomType").GetString();
                        var json = element.GetRawText();

                        Rooms room = roomType
                        switch
                        {
                            "Hall" => JsonSerializer.Deserialize<Hall>(json),
                            "Classroom" => JsonSerializer.Deserialize<ClassRoom>(json),
                            "Group room" => JsonSerializer.Deserialize<GroupRoom>(json),
                            _ => throw new JsonException($"Unknown room type: {roomType}")
                        };
                        //Adds back to list
                        roomList.Add(room);
                    }
                }
            }
        }
       
    }
}
