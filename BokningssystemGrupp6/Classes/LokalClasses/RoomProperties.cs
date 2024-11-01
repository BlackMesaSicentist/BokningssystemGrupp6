using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BokningssystemGrupp6.Interfaces;

namespace BokningssystemGrupp6.Classes.LokalClasses
{
    public class RoomProperties : IRoomInterface
    {
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public int SeatAmount { get; set; }

        public RoomProperties(string roomName, string roomType, int seatAmount)
        {
            RoomName = roomName;
            RoomType = roomType;
            SeatAmount = seatAmount;
        }


    }


    public class LargeRoomProperties : RoomProperties
    {
        public int SeatLimit { get; set; }
        public bool HasProjector { get; set; }
        public bool HasWhiteboard { get; set; }

        public LargeRoomProperties(string roomName, string roomType, int seatAmount, int seatLimit, bool hasProjector, bool hasWhiteboard) : base(roomName, roomType, seatAmount)
        {
            SeatLimit = seatLimit;
            HasProjector = hasProjector;
            HasWhiteboard = hasWhiteboard;
        }

    }

    public class MediumRoomProperties : RoomProperties
    {
        public int SeatLimit { get; set; }
        public bool HasProjector { get; set; }
        public bool HasWhiteboard { get; set; }

        public MediumRoomProperties(string roomName, string roomType, int seatAmount, int seatLimit, bool hasProjector, bool hasWhiteboard) : base(roomName, roomType, seatAmount)
        {
            SeatLimit = seatLimit;
            HasProjector = hasProjector;
            HasWhiteboard = hasWhiteboard;
        }
    }
    public class SmallRoomProperties : RoomProperties
    {
        public int SeatLimit { get; set; }

        public SmallRoomProperties(string roomName, string roomType, int seatAmount, int seatLimit) : base(roomName, roomType, seatAmount)
        {
            SeatLimit = seatLimit;
        }
    }
}
