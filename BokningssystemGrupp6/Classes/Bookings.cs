using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes
{
    public class Bookings
    {
        public static void BookARoom(List<Bookings> booked)
        {
            Console.WriteLine("Ange din epost:");
            string? mail = Console.ReadLine();
            Console.WriteLine("Ange val av rum");
            int roomName = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ange datum DD/MM/ÅÅÅÅ:");
            string? date = Console.ReadLine();
            Console.WriteLine("Ange starttid XX,XX :");
            double startTime = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Ange sluttid XX,XX :");
            double endTime = Convert.ToDouble(Console.ReadLine());

            booked.Add(new Bookings(mail,roomName,date,startTime,endTime));

            double totalTime = endTime - startTime;
            Console.WriteLine($"Din totala bokningstid är: {totalTime}");


        }


        public string Mail { get; set; }
        public int RoomName { get; set; }
        public string Date { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }

        public Bookings()
        {
            
        }

        public Bookings(string mail, int roomName, string date, double startTime, double endTime)
        {
            Mail=mail;
            RoomName = roomName;
            StartTime = startTime;
            EndTime = endTime;
        }

    }
}
