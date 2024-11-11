using System;
using System.Collections;
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
    public class Bookings : IListable
    {
        //Properties for Bookings
        public string Mail { get; set; }
        public string RoomName { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }

        //Input validations
        private readonly InputValidation _inputValidation;
        public Bookings(InputValidation inputValidation)
        {
            _inputValidation = inputValidation;
        }

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
        public void BookARoom(List<Bookings> booked, List<Rooms> rooms)
        {            
            Console.WriteLine("Select room, or type 'quit' to cancel:");
            int i = 1;
            foreach (var r in rooms)
            {
                Console.WriteLine($"{i}. {r.RoomName}, {r.RoomType}");
                i++;


            }
            if (i == 1)
            {
                Console.WriteLine("No room is available. Press a key to go back to continue.");
                Console.ReadKey();
                return;
            }
            int roomNumber;
            
            //Room select input
            string roomNumberStr = Console.ReadLine().Trim();

            // Check if input is "quit" and if it is cancel the booking
            if (roomNumberStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;

            //Room selection validation
            while (true)
            {

                try
                {
                    if (roomNumberStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;

                    //Check if input is empty
                    if (_inputValidation.IsEmpty(roomNumberStr))
                    {
                        Console.WriteLine("Field cannot be empty, Try again");
                    }
                    //Check if input is a number
                    else if (!_inputValidation.IsNumber(roomNumberStr))
                    {
                        Console.WriteLine("Input must be a number. Try again.");
                    }
                    else if (_inputValidation.IsGreaterThanZero(roomNumberStr))
                    {
                        Console.WriteLine("Input cannot be 0");
                    }
                    //Check if the number i negative
                    else if (_inputValidation.IsNumberNegative(roomNumberStr))
                    {
                        Console.WriteLine("Input must be a positive number. Try Again");
                    }
                    //Check if seats is larger than seat limit
                    else if (_inputValidation.IsNumberlargerThanCompare(roomNumberStr, i - 1))
                    {
                        Console.WriteLine("Input cannot be larger the amount of rooms, Try Again");
                    }
                    else
                    {
                        //Convert string to int
                        roomNumber = _inputValidation.ConvertToInt(roomNumberStr) - 1;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.");
                    // Check if input is "quit" and if it is cancel the booking
                    if (roomNumberStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;

                }
                roomNumberStr = Console.ReadLine().Trim();
            }

            string roomName = rooms[roomNumber].RoomName;

            //Email input
            Console.Write("Enter your email, or type 'quit' to cancel:");
            string? mailStr = Console.ReadLine().Trim();
            // Check if input is "quit" and if it is cancel the booking
            if (mailStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
            string mail;
            while (true)
            {
                try
                {
                    //Check if the email is in the correct format (ex. abc@mail.com) or empty 
                    if (!_inputValidation.IsEmail(mailStr))
                    {
                        Console.WriteLine("Email is incorrect, Try again");
                    }
                    else
                    {
                        mail = mailStr;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.");
                    // Check if input is "quit" and if it is cancel the booking
                    if (mailStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
                }
                mailStr = Console.ReadLine().Trim();
            }
            //Booking start time
            Console.Write("Input start date and time for booking.\n(YYYY-MM-DD HH:MM):");
            string startTimeStr = Console.ReadLine().Trim();
            // Check if input is "quit" and if it is cancel the booking
            if (startTimeStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
            DateTime dateTimeStart;
            while (true)
            {
                try
                {
                    //Check if input is empty
                    if (_inputValidation.IsEmpty(startTimeStr))
                    {
                        Console.WriteLine("Field cannot be empty, Try again");
                    }
                    //Check if input is in datetime
                    else if (!_inputValidation.IsDateTime(startTimeStr))
                    {
                        Console.WriteLine("The time must be in the shown date time format (YYYY-MM-DD HH:MM). Try again.");
                    }
                    //Check if the datetime is after the current time
                    else if (!_inputValidation.IsDateTimeAfterNowTime(startTimeStr))
                    {
                        Console.WriteLine("Start time must be after the current time. Try Again");
                    }
                    else
                    {
                        //Convert string to datetime
                        dateTimeStart = _inputValidation.ConvertToDateTime(startTimeStr);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.");
                    // Check if input is "quit" and if it is cancel the booking
                    if (startTimeStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
                }
                startTimeStr = Console.ReadLine().Trim();
            }
            //Booking end time
            Console.Write("Input end date and time for booking. \n(YYYY-MM-DD HH:MM):");
            string endTimeStr = Console.ReadLine().Trim();
            // Check if input is "quit" and if it is cancel the booking
            if (endTimeStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
            DateTime dateTimeEnd;
            while (true)
            {
                try
                {
                    //Check if input is empty
                    if (_inputValidation.IsEmpty(endTimeStr))
                    {
                        Console.WriteLine("Field cannot be empty, Try again");
                    }
                    //Check if input is in datetime
                    else if (!_inputValidation.IsDateTime(endTimeStr))
                    {
                        Console.WriteLine("The time must be in the shown date time format (YYYY-MM-DD HH:MM). Try again.");
                    }
                    else if (!_inputValidation.IsDateTimeAfterCompareTime(endTimeStr, dateTimeStart))
                    {
                        Console.WriteLine("End time must be after the start time. Try Again");
                    }
                    //Check if the booking is longer than 24 hours
                    else if (!_inputValidation.IsBookingTooLong(endTimeStr, dateTimeStart))
                    {
                        Console.WriteLine("Booking can be a maximum of 24 hours. Try Again");
                    }
                    else
                    {
                        //Convert string to datetime
                        dateTimeEnd = _inputValidation.ConvertToDateTime(endTimeStr);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.");
                    // Check if input is "quit" and if it is cancel the booking
                    if (endTimeStr.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
                }
                endTimeStr = Console.ReadLine().Trim();
            }
            Console.Clear();
            if (booked.Count == 0)
            {
                //Adds the booking to the list
                booked.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));
                Save.SaveFile(booked);

                //Is printed when the booking is completed
                Console.WriteLine("Your booking is noted with the following information: ");

                //Calculates and prints the bookings duration and the last booking made                
                Console.WriteLine(booked.Last());
                ListAll(booked);
                
            }
            else
            {
                //Checks if the day is free from previous bookings in that venue
                foreach (Bookings book in booked)
                {
                    try
                    {
                        bool check = dateTimeStart < book.DateTimeEnd && book.DateTimeStart < dateTimeEnd;
                        //Free from previous bookings
                        if (check == false)
                        {
                            //Adds the booking to the list
                            booked.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));
                            Save.SaveFile(booked);
                            if (booked.Count > 0)
                            {
                                //Is printed when the booking is completed
                                Bookings lastBooking = booked[booked.Count - 1];
                                Console.WriteLine($"\nYour booking is noted with the following information: ");
                                //Skriver ut det sista objektet
                                ListSpecific(lastBooking);
                            }
                            else
                            {
                                Console.WriteLine("List is empty.");
                            }
                        }
                        //If the booking conflicts with a previously made booking
                        else if (check != false)
                        {
                            Console.WriteLine("Unfortunately, your selected time & date clashes with an previous booking");
                            //Prints the booking it conflicts with
                            ListSpecific(book);
                            Menu.BackToMenu();
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}. Please try again.");
                    }
                }
            }
        }

        //Method to list all bookings
        public static void ListAll(List<Bookings> bookingInfo)
        {
            Console.WriteLine("ALL BOOKINGS");
            Console.WriteLine("{0,-4}{1,-24}{2,-14}{3,-26}{4,-23}{5,-20}", "", "Email", "Room", "Booking starts", "Booking ends ", "Duration");
            Console.WriteLine(new string('-', 100));
            int i = 1;
            foreach (Bookings booking in bookingInfo)
            {
                Console.WriteLine("{0,-4}{1,-24}{2,-14}{3,-26}{4,-23}{5,-20}", i + ".",
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
            //Show all bookings
            ListAll(bookingInfo);

            //Menu selection depending on how the user wants the information to display
            Console.WriteLine(new string('-', 100));
            Console.WriteLine("" +
                "\nSelect:" +
                "\n1. Show all bookings from specific year" +
                "\n2. Show all bookings from specific room\n" +
                "\nEnter the number for the corresponding option");

            //Input choise
            string choise = Console.ReadLine();
            Console.Clear();

            //New list with only bookings with the right parameters
            List<Bookings> specificBookings = new List<Bookings>();

            switch (choise)
            {
                //Show all bookings from year 
                case "1":
                    Console.WriteLine("Enter the year off the bookings you want to display in the format YYYY");
                    String yearInputString = Console.ReadLine();

                    //Convert input to datetime
                    if (DateTime.TryParseExact(yearInputString, "yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime startDate))
                    {
                        DateTime endDate = new DateTime(startDate.Year, 12, 31, 23, 59, 59);

                        foreach (Bookings yearBooking in bookingInfo)
                        {
                            if (yearBooking.DateTimeStart >= startDate && yearBooking.DateTimeEnd <= endDate)
                            //Adds bookings to list if it meet the requirmets
                            {
                                specificBookings.Add(yearBooking);
                            }
                        }
                        ListAll(specificBookings);
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("Invalid choise");
                    }
                    break;
                //Show all bookings from specific room 
                case "2":

                    String specificRoom = Rooms.ChooseASpecificRoom(listOfRoom);
                    Console.Clear();
                    foreach (Bookings roomBooking in bookingInfo)
                    {
                        if (roomBooking.RoomName == specificRoom)
                        {
                            specificBookings.Add(roomBooking);
                        }
                    }
                    ListAll(specificBookings);
                    break;
                //Skrivs ut om användaren uppger ett felaktigt menyval
                default:
                    Console.WriteLine("Invalid choise, try again");
                    break;
            }
        }

        //Update an alreade existing booking
        public static void UpdateBooking(List<Bookings> bookingInfo, List<Rooms> roomList)
        {
            String roomName = Rooms.ChooseASpecificRoom(roomList);
            //Check if ok to save list at the last step, makes sure it dosnt remove anything from the list
            Boolean isNewBookingSuccess = false;

            Bookings bookingToRemove = new Bookings();

            //New list for all bookings for the specific user
            List<Bookings> specificUserBookings = new List<Bookings>();
            //Used to end loops to end method if all conditions are fulfilled 
            Boolean isValidInput = false;
            //Used to check if booking overlaps
            Boolean checkIfBookingOverlaps = false; 

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
                    //Adds bookings for a specific person and room to list
                    if (booking.Mail == mail && booking.RoomName == roomName)
                    {
                        specificUserBookings.Add(booking);
                    }
                }
                //Checks if finding any bookings
                if (specificUserBookings.Count <= 0) 
                {
                    Console.WriteLine($"\nCant find any bookings under the email:{mail} \nTry again.\n");
                    continue;
                }

                do
                {
                    //List bookings
                    for (int i = 0; i < specificUserBookings.Count; i++) 
                    {
                        Console.WriteLine($"-------------\n{i + 1}: ");
                        ListSpecific(specificUserBookings[i]);
                    }

                    Console.WriteLine("Enter the number for the corresponding option");
                    //Input choiche form list
                    if (int.TryParse(Console.ReadLine(), out int choice)) 
                    {
                        //Check if inside list range
                        if (choice <= specificUserBookings.Count && choice > 0)
                        {
                            //Have to shrink by 1 to actually match index for list
                            choice--;
                            //Creates a new list so a list without the booking to be change so it dosent create a booking conflict with dates
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
                                        //Adds the booking to the list
                                        withoutChosenBookingOnlySelectedRoom.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));

                                        //Is printed when the booking is completed
                                        Console.WriteLine("Your booking is noted with the following information: ");

                                        //Prints the last item
                                        ListSpecific(withoutChosenBookingOnlySelectedRoom[withoutChosenBookingOnlySelectedRoom.Count]);

                                        //Calculates and prints the bookings duration and the last booking made
                                        Console.WriteLine($"Total duration for your booking is: {totalTime}.");


                                    }
                                    else
                                    {
                                        //Check if the day is free from previous bookings in that venue
                                        foreach (Bookings book in withoutChosenBookingOnlySelectedRoom)
                                        {
                                            checkIfBookingOverlaps = dateTimeStart < book.DateTimeEnd && book.DateTimeStart < dateTimeEnd;
                                            //Checks that the booking conflicts with an already made booking
                                            if (checkIfBookingOverlaps != false)
                                            {
                                                Console.WriteLine("Din valda tid & datum krockar tyvärr med en redan lagd bokning");
                                                //Print the booking it conflicts with
                                                ListSpecific(book);
                                                isValidInput = true;
                                                break;
                                            }


                                        }
                                        if (checkIfBookingOverlaps == false)
                                        {
                                            //Adds the booking to the list
                                            withoutChosenBookingOnlySelectedRoom.Add(new Bookings(mail, roomName, dateTimeStart, dateTimeEnd));
                                            Bookings newest = withoutChosenBookingOnlySelectedRoom[withoutChosenBookingOnlySelectedRoom.Count - 1];

                                            //Printed when the booking is completed
                                            Console.WriteLine("Grattis din bokning är genomförd med informationen nedan");

                                            //Prints the last item
                                            ListSpecific(newest);

                                            Console.WriteLine($"Din bokning är totalt {totalTime} timmar.");

                                            //It is allowed to save
                                            isNewBookingSuccess = true; 
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

        //Method to delete previous booking
        public static void DeleteBooking(List<Bookings>bookingInfo, List<Rooms>roomList)
        {
            String roomName = Rooms.ChooseASpecificRoom(roomList);
            //Check if ok to save list at the last step, makes sure it dosnt remove anything from the list
            Boolean isNewBookingSuccess = false;

            //Used to specify what booking to remove
            Bookings bookingToRemove = new Bookings();

            //New list for all bookings for the specific user
            List<Bookings> specificUserBookings = new List<Bookings>();
            //Used to end loops to end method if all conditions are fulfilled 
            Boolean isValidInput = false; 

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
                    //Adds bookings for a specific person and room to list
                    if (booking.Mail == mail && booking.RoomName == roomName) 
                    {
                        specificUserBookings.Add(booking);
                    }
                }
                //Checks if finding any bookings
                if (specificUserBookings.Count <= 0) 
                {
                    Console.WriteLine($"\nCant find any bookings under the email:{mail} \nTry again.\n");
                    continue;
                }

                do
                {
                    //List bookings
                    for (int i = 0; i < specificUserBookings.Count; i++) 
                    {
                        Console.WriteLine($"-------------\n{i + 1}: ");
                        ListSpecific(specificUserBookings[i]);
                    }

                    Console.WriteLine("Enter the number for the corresponding option");
                    //Input choiche form list
                    if (int.TryParse(Console.ReadLine(), out int choice)) 
                    {
                        //Check if inside list range
                        if (choice <= specificUserBookings.Count && choice > 0) 
                        {
                            bookingToRemove = specificUserBookings[choice - 1];
                            foreach (Bookings booking in bookingInfo)
                            {
                                if (booking == bookingToRemove)
                                {
                                    Console.WriteLine($"Du har tagit bort bokning:");
                                    ListSpecific(booking);
                                    bookingInfo.Remove(booking);
                                    Save.SaveFile(bookingInfo);
                                    isValidInput = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Inte ett giltigt val, tryck på enter för  försök igen");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                } while (isValidInput = false);
            }while (isValidInput = false);
        }
    }
}
