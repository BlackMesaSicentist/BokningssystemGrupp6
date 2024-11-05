using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes
{
    public class Bookings
    {
        public string Mail { get; set; }
        public string RoomName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Bookings()
        {
            
        }

        public Bookings(string mail, string roomName, DateTime startTime, DateTime endTime)
        {
            Mail = mail;
            RoomName = roomName;
            StartTime = startTime;
            EndTime = endTime;
        }

        //Method to list all bookings
        public static void ListAllBookingsFromList(List<Bookings> bookingInfo)
        {
            foreach (Bookings booking in bookingInfo)
            {
                Console.WriteLine($"E-Post {booking.Mail} har bokat rum med namn {booking.RoomName} " +
                    $"med start {booking.StartTime} och slut {booking.EndTime} och den totala bokningstiden är {booking.EndTime - booking.StartTime} timmar \n"); //If needed add "Kl" or date descriptions after variable
            }
            
        }
        //List data from specific booking feed into it
        public static void ListSpecificBooking(Bookings booking)
        {
            Console.WriteLine($"E-Post {booking.Mail}, Rum {booking.RoomName}, " +
                $"Starttid {booking.StartTime}, Sluttid {booking.EndTime}, bokningen är {booking.EndTime - booking.StartTime} timmar");
        }
        // Display list of bookings for a specific room and a specific 1 year interwall
        public static void CreateAndDisplayListOfBookingsSpecificRoomAndDate(List<Bookings> bookingInfo)
        {
            Console.WriteLine("Vilket rum vill ni se alla bokningar för?");
            String specificRoom = ""; //Todo: Call to a method to find room, or build one in this method
            List<Bookings> roomSpecificBookings = new List<Bookings>(); //New list with only bookings with the right parameters
            Console.WriteLine("Mata in över vilket år du vill se bokningarna i formatet \"yyyy\"");

            String yearInputString = Console.ReadLine();
            if (DateTime.TryParseExact(yearInputString, "yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime startDate)) //Convert input to datetime
            {
                DateTime endDate = new DateTime(startDate.Year, 12, 31, 23, 59, 59);

                foreach (Bookings booking in bookingInfo)
                {
                    if (booking.RoomName == specificRoom && booking.StartTime <= startDate && booking.EndTime >= endDate) { roomSpecificBookings.Add(booking); } // Adds bookings to list if the meet the requirmets
                }
            }
            ListAllBookingsFromList(roomSpecificBookings);
        }
        //Update an alreade existing booking
        public static void UpdateBooking(List<Bookings> bookingInfo)
        {
            String roomName = ""; // Todo: Call to a method to find specific room or just list them, reuse find user?
            int indexArrayOfSpecificUserBookings = 0;
            List<Bookings> specificUserBookings = new List<Bookings>(); //New list for all bookings for the specific user
            Boolean isValidInput = false;

            do
            {
                Console.Write("Mata in e-postadressen: ");
                String mail = Console.ReadLine();
                if (mail == null)
                {
                    Console.WriteLine("E-postadressen kan inte vara null \nFörsök igen \n");
                    continue;
                }

                foreach (Bookings booking in bookingInfo)
                {
                    if (booking.Mail == mail && booking.RoomName == roomName) // Adds bookings for a specific person and room to list
                    {
                        specificUserBookings.Add(booking);
                        indexArrayOfSpecificUserBookings++;
                    }
                }

                if (specificUserBookings.Count <= 0) // Checks if finding any bookings
                {
                    Console.WriteLine($"Kan inte hitta en booking under användarnamnet: {mail} \nFörsök igen \n");
                    continue;
                }

                for (int i = 0; i < specificUserBookings.Count; i++) //List bookings
                {
                    Console.WriteLine($"Alternativ {i++}: ");
                    ListSpecificBooking(bookingInfo[i]);
                }

                do
                {
                    Console.WriteLine("Mata in siffran för motsvarande alternativ");
                    if (int.TryParse(Console.ReadLine(), out int choice)) //Input choiche form list
                    {
                        if (choice <= specificUserBookings.Count && choice > 0) //CHekc if inside list range
                        {
                            List<Bookings> withoutChosenBooking = new List<Bookings>(bookingInfo); // Creates a new list so a list without the booking to be change so it dosent create a booking conflict with dates
                            int index = 0;
                            foreach (Bookings booking in withoutChosenBooking)
                            {
                                if (booking == specificUserBookings[choice]) //If booking chosen remove old and add new booking
                                {
                                    withoutChosenBooking.RemoveAt(index);
                                    // Implement booking method, depending on method, might need to put this in a while loop in case
                                    bookingInfo.RemoveAt(index);
                                    isValidInput = true;
                                }
                                index++;
                            }
                        }
                        else { Console.WriteLine($"{choice} är inte ett giltigt val \nFörsök igen \n"); continue; }
                    }
                }
                while (isValidInput == false);
            }
            while (isValidInput == false);
        }
    }
}
