using System;
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
            if (listToSave is List<Rooms>)
            {
                string listRoom = JsonSerializer.Serialize(listToSave);
                File.WriteAllText("RoomList.json", listRoom);
            }

            if (listToSave is List<Bookings>)
            {
                string listBooking = JsonSerializer.Serialize(listToSave);
                File.WriteAllText("BookingList.json", listBooking);
            }

        }
        //Method to unpack lists
        public static void UnPackFileBooking(List<Bookings> bookingList)
        {
            //To be able to read ÅÄÖ
            var options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin,
                    UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA)
            };
            //reads list for bookings
            if (File.Exists("BookingsList.json"))
            {
                string readBooking = File.ReadAllText("BookingList.json");
                bookingList = JsonSerializer.Deserialize<List<Bookings>>(readBooking);
            }

        }
        public static void UnpackFileRooms(List<Rooms> roomList)
        {


        var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA) }; 
        
        if (File.Exists("RoomList.json"))
            {
            string readRoom = File.ReadAllText("RoomList.json");
        var tempList = JsonSerializer.Deserialize<List<JsonElement>>(readRoom);
        roomList.Clear(); 
            
            foreach (var element in tempList)
            {
                var roomType = element.GetProperty("RoomType").GetString(); var json = element.GetRawText();

        Rooms room = roomType
            switch
        { 
            "Hall" => JsonSerializer.Deserialize<Hall>(json),
            "Classroom"=> JsonSerializer.Deserialize<ClassRoom>(json),
            "Group room" => JsonSerializer.Deserialize<GroupRoom>(json), 
            _ => throw new JsonException("$Unknown room type: { roomType }") };
        roomList.Add(room);
            }
        }
        }
    }
}

////Method to save lists
////public static void SaveFile<T>(List<T> listToSave)
////{
////    if (listToSave is List<Rooms>)
////    {
////        string listRoom = JsonSerializer.Serialize(listToSave);
////        File.WriteAllText("RoomList.json", listRoom);
////    }

////    if (listToSave is List<Bookings>)
////    {
////    string listBooking = JsonSerializer.Serialize(listToSave);
////    File.WriteAllText("BookingList.json", listBooking);
////    }

////}

//public static void SaveFileRooms(List<Rooms> listToSave)
//{
//    string listRoom = JsonSerializer.Serialize(listToSave);
//    File.WriteAllText("RoomList.json", listRoom);
//}
//public static void SaveFileBookings(List<Bookings> listToSave)
//{
//    string listBooking = JsonSerializer.Serialize(listToSave);
//    File.WriteAllText("BookingList.json", listBooking);
//}

////Method to unpack lists
//public static void UnPackFile(List<Rooms> roomList, List<Bookings> bookingList)
//{
//    //To be able to read ÅÄÖ
//    var options = new JsonSerializerOptions()
//    {
//        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin,
//            UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA)
//    };
//    //reads list for rooms
//    if (File.Exists("RoomList.json"))
//    {
//        string readRoom = File.ReadAllText("RoomList.json");
//        roomList = JsonSerializer.Deserialize<List<Rooms>>(readRoom);

//    }
//    //reads list for bookings
//    if (File.Exists("BookingsList.json"))
//    {
//        string readBooking = File.ReadAllText("BookingList.json");
//        bookingList = JsonSerializer.Deserialize<List<Bookings>>(readBooking);
//    }

//}








//var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA) }; if (File.Exists(
//"RoomList.json"
//))
//{
//    string readRoom = File.ReadAllText(
//"RoomList.json"
//); var tempList = JsonSerializer.Deserialize<List<JsonElement>>(readRoom); roomList.Clear(); foreach (var element in tempList)
//    {
//        var roomType = element.GetProperty(
//"RoomType"
//).GetString(); var json = element.GetRawText(); Rooms room = roomType switch
//{
//"Hall"
//=> JsonSerializer.Deserialize<Hall>(json),
//"Classroom"
//=> JsonSerializer.Deserialize<ClassRoom>(json),
//"Group room"
//=> JsonSerializer.Deserialize<GroupRoom>(json),
//_ => throw new JsonException(
//$"Unknown room type:
//{ roomType }
//"
//)            }; roomList.Add(room);
//}