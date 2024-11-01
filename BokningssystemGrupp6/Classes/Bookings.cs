using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    }
}
