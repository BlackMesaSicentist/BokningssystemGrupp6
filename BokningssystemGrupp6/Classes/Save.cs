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

        public static void SaveFile<T>(List<T> listToSave)
        {
            if (listToSave is List<IRoom>)
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

        public static void UnPackFile(List<IRoom> roomList, List<Bookings> bookingList)
        {

            var options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin,
                    UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA)
            };
            if(!File.Exists("RoomList.Json") |&)
            {
                
            }
            else
            {
                string readRoom = File.ReadAllText("RoomList.json");
                string readBooking = File.ReadAllText("BookingList.json");

                roomList = JsonSerializer.Deserialize<List<IRoom>>(readRoom);
                bookingList = JsonSerializer.Deserialize<List<Bookings>>(readBooking);
            }

        }

    }
}
