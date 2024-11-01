using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes
{
    public class Bookings
    {
        public string UserName { get; set; }
        public string RoomName { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }

        public Bookings()
        {
            
        }

        public Bookings(string userName, string roomName, double startTime, double endTime)
        {
            UserName = userName;
            RoomName = roomName;
            StartTime = startTime;
            EndTime = endTime;
        }

        //Method to list all bookings
        public static void ListAllBookings(List<Bookings> bookingInfo)
        {
            foreach (Bookings booking in bookingInfo)
            {
                Console.WriteLine($"Användare {booking.UserName} har bokat rum med namn {booking.RoomName} " +
                    $"med start {booking.StartTime} och slut {booking.EndTime} och den totala bokningstiden är {booking.EndTime - booking.StartTime} timmar \n"); //If needed add "Kl" or date descriptions after variable
            }
        }
        //List data from specific booking feed into it
        public static void ListSpecificBooking(Bookings booking)
        {
            Console.WriteLine($"Användare {booking.UserName}, Rum {booking.RoomName}, " +
                $"Starttid {booking.StartTime}, Sluttid {booking.EndTime}, bokningen är {booking.EndTime - booking.StartTime} timmar");
        }

    }
}
