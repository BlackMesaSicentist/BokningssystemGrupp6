using System.Drawing;
using BokningssystemGrupp6.Classes.LokalClasses;

namespace BokningssystemGrupp6
{
    internal class Program
    {
        //Lista för lokaler (string roomName, string size, int maxPeople, bool hasWhiteboard, bool hasProjector)
        public static List<Rooms> RoomsInfo = new List<Rooms>();
        //Lista för bokningar (string userName, string roomName, double startTime, double endTime)
        public static List<Bookings> BookingsInfo = new List<Bookings>();
        static void Main(string[] args)
        {
            //Sample data
            Rooms Delfinen = new("Delfinen","GroupRoom", 8, false, false);
            Rooms Hajen = new("Hajen","GroupRoom", 8, false, false);
            Rooms Valen = new("Valen","Medium",20, false, false);
            Rooms Maneten = new("Maneten","Medium", 20, true, false);
            Rooms Krabban = new("Krabban","Large", 40, true, true);
            Rooms Aborren = new("Aborren","Large", 40, true, true);
            Rooms Laxen = new("Laxen","Large", 40, true, true);
            Rooms Korallen = new("Korallen", "Large", 40, true, true);

            RoomsInfo.Add(Delfinen);
            RoomsInfo.Add(Hajen);
            RoomsInfo.Add(Valen);
            RoomsInfo.Add(Maneten);
            RoomsInfo.Add(Krabban);
            RoomsInfo.Add(Aborren);
            RoomsInfo.Add(Laxen);
            RoomsInfo.Add(Korallen);

            Console.WriteLine("Hello, World!");
        }
    }
}
