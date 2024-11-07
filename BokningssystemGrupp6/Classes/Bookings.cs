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
    //Bookings inherits interface IListable
    public class Bookings: IListable
    {
        //Properties for Bookings
        public string Mail { get; set; }
        public string RoomName { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }

        //Construktor for Bookings      
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
        public static void BookARoom(List<Bookings> booked,List<Rooms> rooms)
        {

            //retrieve booking information
            Console.Write("Enter your email:");
            string? mail = Console.ReadLine();
            Console.WriteLine("Select room:");
            int i = 1;
            foreach (var r in rooms)
            {
                Console.WriteLine($"{i}. {r.RoomName}");
                i++;
            }
            int roomNumber = Convert.ToInt32(Console.ReadLine())-1;
            string roomName = rooms[roomNumber].RoomName;
            Console.Write("Input start date and time for booking.\n(ÅÅÅÅ-MM-DD HH:MM):");
            string startTime = Console.ReadLine();
            Console.Write("Input end date and time for booking. \n(ÅÅÅÅ-MM-DD HH:MM):");
            string endTime = Console.ReadLine();
            Console.Clear();

            //convert the input to a DateTime object
            DateTime dateTimeStart = DateTime.Parse(startTime);
            DateTime dateTimeEnd = DateTime.Parse(endTime);
            try
            {
                DateTime dateTimeS = DateTime.Parse(startTime);
                DateTime dateTimeE = DateTime.Parse(endTime);

            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid format, please input date and time in correct format.");
                Rooms.BackToMenu();
            }
            if (booked.Count == 0)
            {
                //adds the booking to the list??
                booked.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));
                Save.SaveFile(booked);

                //is printed when the booking is completed
                Console.WriteLine("Your booking is noted with the following information: ");

                //calculates and prints the bookings duration and the last booking made
                ListAll(booked); 
            }
            else 
            {
                //checks if the day is free from previous bookings in that venue
                foreach (Bookings book in booked)
                {
                    bool check = dateTimeStart < book.DateTimeEnd && book.DateTimeStart < dateTimeEnd;
                    //free from previous bookings
                    if (check == false)
                    {
                        //adds the booking to the list
                        booked.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));
                        Save.SaveFile(booked);
                        if (booked.Count > 0)
                        {
                            //is printed when the booking is completed
                            Bookings lastBooking = booked[booked.Count - 1];
                            Console.WriteLine($"\nYour booking is noted with the following information: ");

                            // skriver ut det sista objektet
                            ListSpecific(lastBooking);

                            //calculates and prints the bookings duration and the last booking made
                            TimeSpan totalTime = dateTimeEnd - dateTimeStart;
                            Console.WriteLine($"\nTotal duration for your booking is: {totalTime}.");
                        }
                        else
                        {
                            Console.WriteLine("List is empty.");
                        }
                    }
                    //if the booking conflicts with a previously made booking
                    else if (check != false)
                    {
                        Console.WriteLine("Unfortunately, your selected time & date clashes with an previous booking");
                        //prints the booking it conflicts with
                        ListSpecific(book);
                        Rooms.BackToMenu();
                    }
                    break;
                }
            }
        }

        //Method to list all bookings
        public static void ListAll(List<Bookings> bookingInfo)
        {
            Console.WriteLine("ALL BOOKINGS");
            Console.WriteLine("{0,-10}{1,-18}{2,-14}{3,-26}{4,-24}{5,-20}","", "Email", "Room", "Booking starts", "Booking ends ", "Duration");
            Console.WriteLine(new string('-', 100));
                int i = 1;
            foreach (Bookings booking in bookingInfo)
            {
                Console.WriteLine("{0,-4}{1,-24}{2,-14}{3,-26}{4,-23}{5,-20}", i+".",
                    booking.Mail, booking.RoomName, booking.DateTimeStart, booking.DateTimeEnd, booking.DateTimeEnd - booking.DateTimeStart);
                i++;
            }
            
        }
        //Method to list data from specific booking feed into it
        public static void ListSpecific(Bookings booking)
        {
            Console.WriteLine($"" +
                $"\nEmail: {booking.Mail}" +
                $"\nRoom: {booking.RoomName}" +
                $"\nBooking starts at: {booking.DateTimeStart} " +
                $"\nBooking ends at: {booking.DateTimeEnd} " +
                $"\nTotal duration for this booking is: {booking.DateTimeEnd - booking.DateTimeStart}");
        }
        //Method to display list of bookings for a specific room and a specific 1 year interwall
        public static void CreateAndDisplayListOfBookingsSpecificRoomAndDate(List<Bookings> bookingInfo, List<Rooms> listOfRoom)
        {
            Console.WriteLine("Show bookings for which room?\n");

            String specificRoom = Rooms.ChooseASpecificRoom(listOfRoom);

            List<Bookings> roomSpecificBookings = new List<Bookings>(); //New list with only bookings with the right parameters
            Console.WriteLine("\nEnter the year off the bookings you want to display in the format yyyy");

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
        public static void UpdateBooking(List<Bookings> bookingInfo, List<Rooms> roomList)
        {
            String roomName = Rooms.ChooseASpecificRoom(roomList);
            
            List<Bookings> specificUserBookings = new List<Bookings>(); //New list for all bookings for the specific user
            Boolean isValidInput = false;

            do
            {
                Console.Write("Input email: ");
                String mail = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(mail))
                {
                    Console.WriteLine("Email cant be null. \nTry again.\n");
                    continue;
                }

                foreach (Bookings booking in bookingInfo)
                {
                    if (booking.Mail == mail && booking.RoomName == roomName) // Adds bookings for a specific person and room to list
                    {
                        specificUserBookings.Add(booking);
                    }
                }

                if (specificUserBookings.Count <= 0) // Checks if finding any bookings
                {
                    Console.WriteLine($"Cant find any bookings under the email:{mail} \nTry again.\n");
                    continue;
                }

                for (int i = 0; i < specificUserBookings.Count; i++) //List bookings
                {
                    Console.WriteLine($"Alternativ {i + 1}: ");
                    ListSpecific(bookingInfo[i]);
                }

                do
                {
                    Console.WriteLine("Mata in siffran för motsvarande alternativ");
                    if (int.TryParse(Console.ReadLine(), out int choice)) //Input choiche form list
                    {
                        if (choice <= specificUserBookings.Count && choice > 0) //Check if inside list range
                        {
                            choice--; // Have to shrink by 1 to actually match index for list
                            List<Bookings> withoutChosenBooking = new List<Bookings>(bookingInfo); // Creates a new list so a list without the booking to be change so it dosent create a booking conflict with dates
                            int index = 0;
                            foreach (Bookings booking in withoutChosenBooking)
                            {
                                if (booking == specificUserBookings[choice]) //If booking chosen remove old and add new booking
                                {
                                    withoutChosenBooking.RemoveAt(index);
                                    
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

                                    

                                    if (withoutChosenBooking.Count == 0)
                                    {
                                        //adds the booking to the list
                                        withoutChosenBooking.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));

                                        //is printed when the booking is completed
                                        Console.WriteLine("Your booking is noted with the following information: ");

                                        // skriver ut det sista objektet
                                        ListAll(withoutChosenBooking);

                                        //calculates and prints the bookings duration and the last booking made
                                        Console.WriteLine($"Total duration for your booking is: {totalTime}.");

                                        break;
                                    }
                                    else 
                                    { 
                                        //kollar om dagen är fri från tidigare bokningar i den lokalen
                                        foreach (Bookings book in withoutChosenBooking)
                                        {
                                            bool check = dateTimeStart < book.DateTimeEnd && book.DateTimeStart < dateTimeEnd;
                                            //om bokningen ej krockar
                                            if (check == false)
                                            {
                                                //lägger till bokningen i listan
                                                withoutChosenBooking.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));
                                                Bookings newest = withoutChosenBooking[withoutChosenBooking.Count];

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
                                            
                       
                                        }
                                        break;
                                    }
                                    bookingInfo.RemoveAt(index);
                                    bookingInfo.Add(withoutChosenBooking[withoutChosenBooking.Count]);
                                    isValidInput = true;
                                    Save.SaveFile(bookingInfo);
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
