using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;

namespace BokningssystemGrupp6.Classes
{
    internal class Save
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
        public static void UnPackFile(List<Rooms> roomList, List<Bookings> bookingList)
        {
            //To be able to read ÅÄÖ
            var options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin,
                    UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA)
            };
            //reads list for rooms
            if(File.Exists("RoomList.json"))
            {
                string readRoom = File.ReadAllText("RoomList.json");
                roomList = JsonSerializer.Deserialize<List<Rooms>>(readRoom);
                
            }
            //reads list for bookings
            if (File.Exists("BookingsList.json"))
            {
                string readBooking = File.ReadAllText("BookingList.json");
                bookingList = JsonSerializer.Deserialize<List<Bookings>>(readBooking);
            }

        }

    }
}
