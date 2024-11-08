using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

            //retrieve booking information
            Console.WriteLine("Select room:");
            int i = 1;
            foreach (var r in rooms)
            {
                Console.WriteLine($"{i}. {r.RoomName}");
                i++;

            }
            int roomNumber;
            Console.WriteLine(i.ToString());
            // Room select input
            string roomNumberStr = Console.ReadLine().Trim();

            // Room selection validation
            while (true)
            {

                try
                {
                    // Check if input is empty
                    if (_inputValidation.IsEmpty(roomNumberStr))
                    {
                        Console.WriteLine("Field cannot be empty, Try again");
                    }
                    // Check if input is a number
                    else if (!_inputValidation.IsNumber(roomNumberStr))
                    {
                        Console.WriteLine("Input must be a number. Try again.");
                    }
                    // Check if the number i negative
                    else if (_inputValidation.IsNumberNegative(roomNumberStr))
                    {
                        Console.WriteLine("Input must be a positive number. Try Again");
                    }
                    // Check if seats is larger than seat limit
                    else if (_inputValidation.IsNumberlargerThanCompare(roomNumberStr, i-1))
                    {
                        Console.WriteLine("Input cannot be larger the amount of rooms, Try Again");
                    }
                    else
                    {
                        // Convert string to int
                        roomNumber = _inputValidation.ConvertToInt(roomNumberStr) - 1;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.");
                }
                roomNumberStr = Console.ReadLine().Trim();

            }

            string roomName = rooms[roomNumber].RoomName;

            // Email input
            Console.Write("Enter your email:");
            string? mailStr = Console.ReadLine().Trim();
            string mail;

            while (true)
            {
                try
                {
                    // Check if the email is in the correct format (ex. abc@mail.com) or empty 
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
                }
                mailStr = Console.ReadLine().Trim();
            }

            // Booking start time
            Console.Write("Input start date and time for booking.\n(YYYY-MM-DD HH:MM):");
            string startTimeStr = Console.ReadLine();
            DateTime dateTimeStart;
            while (true)
            {
                try
                {
                    // Check if input is empty
                    if (_inputValidation.IsEmpty(startTimeStr))
                    {
                        Console.WriteLine("Field cannot be empty, Try again");
                    }
                    // Check if input is in datetime
                    else if (!_inputValidation.IsDateTime(startTimeStr))
                    {
                        Console.WriteLine("The time must be in the shown date time format (YYYY-MM-DD HH:MM). Try again.");
                    }
                    // Check if the datetime is after the current time
                    else if (!_inputValidation.IsDateTimeAfterNowTime(startTimeStr))
                    {
                        Console.WriteLine("Start time must be after the current time. Try Again");
                    }
                    else
                    {
                        // Convert string to datetime
                        dateTimeStart = _inputValidation.ConvertToDateTime(startTimeStr);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.");
                }
                startTimeStr = Console.ReadLine().Trim();
            }

            // Booking end time
            Console.Write("Input end date and time for booking. \n(YYYY-MM-DD HH:MM):");
            string endTimeStr = Console.ReadLine();
            DateTime dateTimeEnd;
            while (true)
            {
                try
                {
                    // Check if input is empty
                    if (_inputValidation.IsEmpty(endTimeStr))
                    {
                        Console.WriteLine("Field cannot be empty, Try again");
                    }
                    // Check if input is in datetime
                    else if (!_inputValidation.IsDateTime(endTimeStr))
                    {
                        Console.WriteLine("The time must be in the shown date time format (YYYY-MM-DD HH:MM). Try again.");
                    }
                    // Check if the booking is longer than 24 hours
                    else if (!_inputValidation.IsBookingTooLong(endTimeStr, dateTimeStart))
                    {
                        Console.WriteLine("Booking can be a maximum of 24 hours. Try Again");
                    }
                    else
                    {
                        // Convert string to datetime
                        dateTimeEnd = _inputValidation.ConvertToDateTime(endTimeStr);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.");
                }
                endTimeStr = Console.ReadLine().Trim();
            }
            Console.Clear();


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
                    try
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
            Console.WriteLine("\nALL BOOKINGS");
            Console.WriteLine("{0,-10}{1,-18}{2,-14}{3,-26}{4,-24}{5,-20}", "", "Email", "Room", "Booking starts", "Booking ends ", "Duration");
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

            List<Bookings> specificUserBookings = new List<Bookings>(); //New list for all bookings for the specific user
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

                for (int i = 0; i < specificUserBookings.Count; i++) //List bookings
                {
                    Console.WriteLine($"-------------\n{i + 1}: ");
                    ListSpecific(bookingInfo[i]);
                }

                do
                {
                    Console.WriteLine("Enter the number for the corresponding option");
                    if (int.TryParse(Console.ReadLine(), out int choice)) //Input choiche form list
                    {
                        if (choice <= specificUserBookings.Count && choice > 0) //Check if inside list range
                        {
                            // Have to shrink by 1 to actually match index for list
                            choice--;
                            // Creates a new list so a list without the booking to be change so it dosent create a booking conflict with dates
                            List<Bookings> withoutChosenBooking = new List<Bookings>(bookingInfo);
                            int index = 0;
                            foreach (Bookings booking in withoutChosenBooking)
                            {
                                //If booking is chosen removes old and adds new booking
                                if (booking == specificUserBookings[choice])
                                {
                                    withoutChosenBooking.RemoveAt(index);


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
                                                Bookings newest = withoutChosenBooking[withoutChosenBooking.Count - 1];

                                                //skrivs ut när bokningen är genomförd
                                                Console.WriteLine("Grattis din bokning är genomförd med informationen nedan");

                                                // skriver ut det sista objektet
                                                ListSpecific(newest);

                                                Console.WriteLine($"Din bokning är totalt {totalTime} timmar.");
                                                //break;
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
                                                break;

                                            }


                                        }
                                        break;

                                    }
                                    bookingInfo.RemoveAt(index);
                                    bookingInfo.Add(withoutChosenBooking[withoutChosenBooking.Count - 1]);
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
