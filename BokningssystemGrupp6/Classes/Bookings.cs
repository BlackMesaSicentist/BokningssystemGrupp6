using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
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
            Console.WriteLine("Select room:");
            int i = 1;
            foreach (var r in rooms)
            {
                Console.WriteLine($"{i}. {r.RoomName}");
                i++;
            }
            int roomNumber = Convert.ToInt32(Console.ReadLine())-1;
            string roomName = rooms[roomNumber].RoomName;
            Console.Write("Enter your email:");
            string? mail = Console.ReadLine();
            Console.Write("Input start date and time for booking.\n(YYYY-MM-DD HH:MM):");
            string startTime = Console.ReadLine();
            Console.Write("Input end date and time for booking. \n(YYYY-MM-DD HH:MM):");
            string endTime = Console.ReadLine();
            Console.Clear();

            //convert the input to a DateTime object
            DateTime dateTimeStart = DateTime.Parse(startTime);
            DateTime dateTimeEnd = DateTime.Parse(endTime);
            TimeSpan totalTime= dateTimeEnd - dateTimeStart;
            try
            {
                DateTime dateTimeS = DateTime.Parse(startTime);
                DateTime dateTimeE = DateTime.Parse(endTime);

            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid format, please input date and time in correct format.");
                Menu.BackToMenu();
            }
            TimeSpan maxTime = TimeSpan.Parse("24:00:00");
            if (totalTime > maxTime)
            { 
            
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
                        Menu.BackToMenu();
                    }
                    break;
                }
            }
        }
        //Method to list all bookings
        public static void ListAll(List<Bookings> bookingInfo)
        {
            Console.WriteLine("\nALL BOOKINGS");
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

        //Method to list all bookings
        public static void ListAllBookingsByYearOrRoom(List<Bookings> bookingInfo, List<Rooms> listOfRoom)
        {
            //show all bookings
            ListAll(bookingInfo);

            //menu selection depending on how the user wants the information to display
            Console.WriteLine(new string('-', 100));
            Console.WriteLine("" +
                "\n1. Show all bookings from specific year\n" +
                "2. Show all bookings from specific room\n" +
                "\nEnter the number for the corresponding option\n");

            //New list with only bookings with the right parameters
            List<Bookings> roomSpecificBookings = new List<Bookings>();

            //input choise
            string choise = Console.ReadLine();
            Console.Clear();

            switch (choise)
            {
                //Show all bookings from year 
                case "1":
                    Console.WriteLine("\nEnter the year off the bookings you want to display in the format YYYY");
                    String yearInputString = Console.ReadLine();

                    //Convert input to datetime
                    if (DateTime.TryParseExact(yearInputString, "yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime startDate))
                    {
                        DateTime endDate = new DateTime(startDate.Year, 12, 31, 23, 59, 59);

                        foreach (Bookings yearBooking in bookingInfo)
                        {
                            if (yearBooking.DateTimeStart <= startDate && yearBooking.DateTimeEnd >= endDate)
                            // Adds bookings to list if the meet the requirmets
                            { roomSpecificBookings.Add(yearBooking); }
                        }
                        ListAll(roomSpecificBookings);
                    }
                    else
                    {
                        Console.WriteLine("Invalid choise");
                    }
                    break;

                //Show all bookings from specific room 
                case "2":
                    //int i = 1;
                    //foreach (var room in listOfRoom)
                    //{
                    //    Console.WriteLine($"{i}. {room.RoomName}");
                    //    i++;
                    //}

                    String specificRoom = Rooms.ChooseASpecificRoom(listOfRoom);
                    Console.WriteLine("\nEnter the number for the corresponding option");
                    //string sortByInt = Console.ReadLine();
                    //foreach (Bookings booking in bookingInfo)
                    //{
                    //    ListAll(roomSpecificBookings);
                    //}
                    break;

                //skrivs ut om användaren uppger ett felaktigt menyval
                default:
                    Console.WriteLine("Invalid choise, try again");
                    break;
            }
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
            //Check if ok to save list at the last step, makes sure it dosnt remove anything from the list
            Boolean isNewBookingSuccess = false;

            Bookings bookingToRemove = new Bookings();

            List<Bookings> specificUserBookings = new List<Bookings>(); //New list for all bookings for the specific user
            Boolean isValidInput = false; //Used to end loops to end method if all conditions are fulfilled 
            Boolean checkIfBookingOverlaps = false; //Used to check if booking overlaps


            do
            {
                Console.Write("\nInput email: ");
                String mail = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(mail))
                {
                    Console.WriteLine("\nEmail cant be null. \nTry again.\n");
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
                    Console.WriteLine($"\nCant find any bookings under the email:{mail} \nTry again.\n");
                    continue;
                }

                do
                {
                    for (int i = 0; i < specificUserBookings.Count; i++) //List bookings
                    {
                        Console.WriteLine($"-------------\n{i + 1}: ");
                        ListSpecific(specificUserBookings[i]);
                    }

                    Console.WriteLine("Enter the number for the corresponding option");
                    if (int.TryParse(Console.ReadLine(), out int choice)) //Input choiche form list
                    {
                        if (choice <= specificUserBookings.Count && choice > 0) //Check if inside list range
                        {
                            // Have to shrink by 1 to actually match index for list
                            choice--; 
                            // Creates a new list so a list without the booking to be change so it dosent create a booking conflict with dates
                            List<Bookings> withoutChosenBookingOnlySelectedRoom = new List<Bookings>();
                            //Adds only bookings for the chosen room, else it might check for conflicts in rooms that arent relevant
                            foreach (Bookings booking in bookingInfo)
                            {
                                if (booking.RoomName == roomName)
                                    withoutChosenBookingOnlySelectedRoom.Add(booking);
                            }
                            int index = 0;
                            foreach (Bookings booking in withoutChosenBookingOnlySelectedRoom)
                            {
                                //If booking is chosen removes old and adds new booking
                                if (booking == specificUserBookings[choice]) 
                                {
                                    bookingToRemove = booking;
                                    withoutChosenBookingOnlySelectedRoom.RemoveAt(index);


                                    Console.Write("Input start date and time for booking. \n(YYYY-MM-DD HH:MM):");
                                    string startTime = Console.ReadLine();
                                    Console.Write("Input end date and time for booking. \n(YYYY-MM-DD HH:MM):");
                                    string endTime = Console.ReadLine();
                                    Console.Clear();

                                    //Converts the input to a DateTime object
                                    DateTime dateTimeStart = DateTime.Parse(startTime);
                                    DateTime dateTimeEnd = DateTime.Parse(endTime);

                                    TimeSpan totalTime = dateTimeEnd - dateTimeStart;
                                    try
                                    {
                                        DateTime dateTimeS = DateTime.Parse(startTime);
                                        DateTime dateTimeE = DateTime.Parse(endTime);

                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Incorrect format, please enter the date and time in the correct format.");
                                        Menu.BackToMenu();

                                    }
                                    if (withoutChosenBookingOnlySelectedRoom.Count == 0)
                                    {
                                        //adds the booking to the list
                                        withoutChosenBookingOnlySelectedRoom.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));

                                        //is printed when the booking is completed
                                        Console.WriteLine("Your booking is noted with the following information: ");

                                        // skriver ut det sista objektet
                                        ListSpecific(withoutChosenBookingOnlySelectedRoom[withoutChosenBookingOnlySelectedRoom.Count]);

                                        //calculates and prints the bookings duration and the last booking made
                                        Console.WriteLine($"Total duration for your booking is: {totalTime}.");

                                        
                                    }
                                    else 
                                    {
                                        //kollar om dagen är fri från tidigare bokningar i den lokalen
                                        foreach (Bookings book in withoutChosenBookingOnlySelectedRoom)
                                        {
                                            checkIfBookingOverlaps = dateTimeStart < book.DateTimeEnd && book.DateTimeStart < dateTimeEnd;
                                            //kollar att bokningen krockar med en redan lagd bokning 
                                            if (checkIfBookingOverlaps != false)
                                            {
                                                Console.WriteLine("Din valda tid & datum krockar tyvärr med en redan lagd bokning");
                                                //skriv ut bokningen den krockar med
                                                ListSpecific(book);
                                                isValidInput = true;
                                                break;
                                            }
                                            
                       
                                        }
                                        if (checkIfBookingOverlaps == false)
                                        {
                                            //lägger till bokningen i listan
                                            withoutChosenBookingOnlySelectedRoom.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));
                                            Bookings newest = withoutChosenBookingOnlySelectedRoom[withoutChosenBookingOnlySelectedRoom.Count - 1];

                                            //skrivs ut när bokningen är genomförd
                                            Console.WriteLine("Grattis din bokning är genomförd med informationen nedan");

                                            // skriver ut det sista objektet
                                            ListSpecific(newest);

                                            Console.WriteLine($"Din bokning är totalt {totalTime} timmar.");

                                            isNewBookingSuccess = true; //It is allowed to save
                                        }


                                    }
                                    if (isNewBookingSuccess == true)
                                    {
                                        foreach (Bookings bookingRemove in bookingInfo)
                                        {
                                            if (bookingRemove == bookingToRemove)
                                            {
                                                bookingInfo.Remove(bookingRemove);
                                                break;
                                            }
                                        }
                                        bookingInfo.Add(withoutChosenBookingOnlySelectedRoom[withoutChosenBookingOnlySelectedRoom.Count - 1]);
                                        isValidInput = true;
                                        Save.SaveFile(bookingInfo);
                                    }
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
