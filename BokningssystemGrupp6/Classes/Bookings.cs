using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;

namespace BokningssystemGrupp6.Classes
{
    public class Bookings: IListable
    {
        public string Mail { get; set; }
        public string RoomName { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }


        public Bookings()
        {

        }

        public Bookings(string mail, string roomName, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            Mail = mail;
            RoomName = roomName;
            DateTimeStart = dateTimeStart;
            DateTimeEnd = dateTimeEnd;
        }
        //Method to create booking
        public static void BookARoom(List<Bookings> booked,List<IRoom> rooms)
        {

            //hämtar bokningsinformation
            Console.Write("Ange din epost:");
            string? mail = Console.ReadLine();
            Console.WriteLine("Ange val av rum"); //val av rum
            foreach (var r in rooms)
            {
                int i = 1;
                Console.WriteLine($"{i}. {r.RoomName}");
                i++;
            }
            int roomNumber = Convert.ToInt32(Console.ReadLine())-1;
            Console.Clear();
            string roomName = rooms[roomNumber].RoomName;
            Console.Write("Ange start datum & tid du önskar för bokningen \n(DD-MM-ÅÅÅÅ HH:MM) :");
            string startTime = Console.ReadLine();
            Console.Write("Ange slut datum & tid du önskar för bokningen \n(DD-MM-ÅÅÅÅ HH:MM) :");
            string endTime = Console.ReadLine();
            Console.Clear();

            DateTime dateTimeStart = DateTime.Parse(startTime);
            DateTime dateTimeEnd = DateTime.Parse(endTime);

            TimeSpan totalTime = dateTimeEnd - dateTimeStart;

            //försök att omvandla input till ett DateTime-objekt
            try
            {
                DateTime dateTimeS = DateTime.Parse(startTime);
                DateTime dateTimeE = DateTime.Parse(endTime);

            }
            catch (FormatException)
            {
                Console.WriteLine("Felaktigt format, vänligen ange datum och tid i rätt format.");
                Console.WriteLine("\nTryck valfri tangent för att återgå till meny");
                Console.ReadKey();
                Console.Clear();

            }

            //kollar om dagen är fri från tidigare bokningar i den lokalen

            foreach (Bookings book in booked)
            {
                bool check = dateTimeStart < book.DateTimeEnd && book.DateTimeStart < dateTimeEnd;
                //om bokningen ej krockar
                if (check == false)
                {
                    //lägger till bokningen i listan
                    booked.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));
                    Bookings newest = booked[booked.Count];

                    //skrivs ut när bokningen är genomförd
                    Console.WriteLine("Grattis din bokning är genomförd med informationen nedan");

                    // skriver ut det sista objektet
                    ListSpecific(newest);

                    Console.WriteLine($"Din bokning är totalt {totalTime} timmar.");

                }

                //kollar att bokningen krockar med en redan lagd bokning 
                else if (check != false)
                {
                    Console.WriteLine("Din valda tid & datum krockar tyvärr med en redan lagd bokning");
                    //skriv ut bokningen den krockar med?
                    ListSpecific(book);
                    Console.WriteLine("\nTryck valfri tangent för att återgå till meny");
                    Console.ReadKey();
                    Console.Clear();

                    
                }
                break;
            }


        }

    


        //Method to list all bookings
        public static void ListAll(List<Bookings> bookingInfo)
        {
            foreach (Bookings booking in bookingInfo)
            {
                Console.WriteLine($"E-Post {booking.Mail} har bokat rum med namn {booking.RoomName} " +
                    $"med start {booking.DateTimeStart} och slut {booking.DateTimeEnd} och den totala bokningstiden är {booking.DateTimeEnd - booking.DateTimeStart} timmar \n"); //If needed add "Kl" or date descriptions after variable
            }
            
        }
        //List data from specific booking feed into it
        public static void ListSpecific(Bookings booking)
        {
            Console.WriteLine($"E-Post {booking.Mail}, Rum {booking.RoomName}, " +
                $"Starttid {booking.DateTimeStart}, Sluttid {booking.DateTimeEnd}, bokningen är {booking.DateTimeEnd - booking.DateTimeStart} timmar");
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
                    if (booking.RoomName == specificRoom && booking.DateTimeStart <= startDate && booking.DateTimeEnd >= endDate) { roomSpecificBookings.Add(booking); } // Adds bookings to list if the meet the requirmets
                }
            }
            ListAll(roomSpecificBookings);
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
                    ListSpecific(bookingInfo[i]);
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
                                    break;
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
