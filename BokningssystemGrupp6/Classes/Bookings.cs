using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
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

        public static void CreateAndDisplayListOfBookings()
        {
            Console.WriteLine("Vilket rum vill ni se alla bokningar för?");
            String specificRoom =
            List<Bookings> roomSpecificBookings = new List<Bookings>();
        }

        public static void UpdateBooking(List<Bookings> bookingInfo)
        {
            String roomName = ""; // Todo: Call to a method to find specific room or just list them, reuse find user?
            int indexArrayOfSpecificUserBookings = 0;
            List<Bookings> specificUserBookings = new List<Bookings>();
            Boolean isValidInput = false;

            do
            {
                Console.Write("Mata in användarnamnet:");
                String userName = Console.ReadLine();
                if (userName == null)
                {
                    Console.WriteLine("Användarnamnet kan inte vara null \nFörsök igen \n");
                    continue;
                }

                foreach (Bookings booking in bookingInfo)
                {
                    if (booking.UserName == userName && booking.RoomName == roomName)
                    {
                        specificUserBookings.Add(booking);
                        indexArrayOfSpecificUserBookings++;
                    }
                }

                int[] listOfChoices = new int[indexArrayOfSpecificUserBookings];
                if (listOfChoices.Length <= 0)
                {
                    Console.WriteLine($"Kan inte hitta en booking under användarnamnet: {userName} \nFörsök igen \n");
                    continue;
                }

                for (int i = 0; i < indexArrayOfSpecificUserBookings; i++)
                {
                    listOfChoices[i] = i++;
                    Console.WriteLine($"Alternativ {i++}:");
                    ListSpecificBooking(bookingInfo[i]);
                }

                do
                {
                    Console.WriteLine("Mata in siffran för motsvarande alternativ");
                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        if (choice <= listOfChoices.Length && choice > 0)
                        {
                            List<Bookings> withoutChosenBooking = new List<Bookings>(bookingInfo);
                            int index = 0;
                            foreach (Bookings booking in withoutChosenBooking)
                            {
                                if (booking == specificUserBookings[choice])
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
