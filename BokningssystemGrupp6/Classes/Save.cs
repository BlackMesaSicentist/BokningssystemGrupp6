using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

    }
}
