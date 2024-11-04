using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes
{
    public class Bookings
    {
        public static void BookARoom(List<Bookings> booked)
        {
            //hämtar bokningsinformation
            Console.WriteLine("Ange din epost:");
            string? mail = Console.ReadLine();
            Console.WriteLine("Ange val av rum");
            int roomName = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ange datum DD-MM-ÅÅÅÅ:");
            string? date = Console.ReadLine();
            Console.WriteLine("Ange starttid HH:MM :");
            string startTime = Console.ReadLine();
            Console.WriteLine("Ange sluttid HH:MM :");
            string endTime = Console.ReadLine();

            if (DateTime.TryParseExact(date, "dd-MM-yyyy", new CultureInfo("sv-SE"), DateTimeStyles.None, out DateTime dateTimeStart) && TimeSpan.TryParseExact(startTime, "hh\\:mm", new CultureInfo("sv-SE"), out TimeSpan timeSpanStart))
            {
                DateTime combinedDateTimeStart = dateTimeStart.Add(timeSpanStart);
            }
            if (DateTime.TryParseExact(date, "dd-MM-yyyy", new CultureInfo("sv-SE"), DateTimeStyles.None, out DateTime dateTimeEnd) && TimeSpan.TryParseExact(endTime, "hh\\:mm", new CultureInfo("sv-SE"), out TimeSpan timeSpanEnd))
            {
                DateTime combinedDateTimeEnd = dateTimeEnd.Add(timeSpanEnd);
            }

            for (int i = 0; i<booked.Count; i++ )
            {

            foreach (Bookings booking in booked)
            { 
                string bookedDate = booking.Date;
                string bookedStartTime = booking.StartTime;
                string bookedEndTime = booking.EndTime;

                if (DateTime.TryParseExact(bookedDate, "dd-MM-yyyy", new CultureInfo("sv-SE"), DateTimeStyles.None, out DateTime bookedDateTimeStart) && TimeSpan.TryParseExact(bookedStartTime, "hh\\:mm", new CultureInfo("sv-SE"), out TimeSpan bookedTimeSpanStart))
                {
                    DateTime combiDateTimeBookedStart = bookedDateTimeStart.Add(bookedTimeSpanStart);
                }
                if (DateTime.TryParseExact(bookedDate, "dd-MM-yyyy", new CultureInfo("sv-SE"), DateTimeStyles.None, out DateTime bookedDateTimeEnd) && TimeSpan.TryParseExact(bookedEndTime, "hh\\:mm", new CultureInfo("sv-SE"), out TimeSpan bookedTimeSpanEnd))
                {
                    DateTime combiDateTimeBookedEnd = bookedDateTimeEnd.Add(bookedTimeSpanEnd);
                }

                    DateTime date1 = new DateTime(2009, 8, 1, 0, 0, 0);
                    DateTime date2 = new DateTime(2009, 8, 1, 12, 0, 0);
                    int result = DateTime.Compare(date1, date2);
                    string relationship;

                    if (result < 0)
                        relationship = "is earlier than";
                    else if (result == 0)
                        relationship = "is the same time as";
                    else
                        relationship = "is later than";

                    Console.WriteLine("{0} {1} {2}", date1, relationship, date2);

                    // The example displays the following output for en-us culture:
                    //    8/1/2009 12:00:00 AM is earlier than 8/1/2009 12:00:00 PM

                }

            }



            

            double totalTime = endTime - startTime;
            Console.WriteLine($"Bokningen är lagd för totalt {totalTime} antal timmar");


                //kollar om dagen är fri från tidigare bokningar i den lokalen
             
            //om helt fri fortsätt med bokningen

            //kolla att bokningen inte krockar med en redan lagd bokning 

            //om bokning krockar

            //lägger till bokningen i listan
            booked.Add(new Bookings(mail,roomName,date,startTime,endTime));

            //skrivs ut när bokningen är genomförd
            Console.WriteLine("Grattis, din bokning är nu genomförd!\nHär nedad kan du se all information om din bokning.");

            if (booked.Count > 0)
            {
                // skriver ut det sista objektet
                string lastbooking = booked[booked.Count - 1];
                Console.WriteLine(lastbooking);
            }

            Console.WriteLine($"Din totala bokningstid är: {totalTime}");




        }


        public string Mail { get; set; }
        public int RoomName { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public Bookings()
        {
            
        }

        public Bookings(string mail, int roomName, string date, string startTime, string endTime)
        {
            Mail=mail;
            RoomName = roomName;
            StartTime = startTime;
            EndTime = endTime;
        }

    }
}
