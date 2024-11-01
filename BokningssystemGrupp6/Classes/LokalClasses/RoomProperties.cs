using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BokningssystemGrupp6.Interfaces;

namespace BokningssystemGrupp6.Classes.LokalClasses
{

    // Main room properties, can not create objects, must be inherited first. Uses properties from IRoom.
    public abstract class RoomProperties : IRoom
    {
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public int SeatAmount { get; set; }

        protected RoomProperties(string roomName, string roomType, int seatAmount)
        {
            RoomName = roomName;
            RoomType = roomType;
            SeatAmount = seatAmount;
        }
    }

    // Properties for large rooms, can create objects
    public class LargeRoom : RoomProperties
    {
        public int SeatLimit { get; set; }
        public bool HasProjector { get; set; }
        public bool HasWhiteboard { get; set; }

        public LargeRoom(string roomName, string roomType, int seatAmount, int seatLimit, bool hasProjector, bool hasWhiteboard)
            : base(roomName, roomType, seatAmount)
        {
            SeatLimit = seatLimit;
            HasProjector = hasProjector;
            HasWhiteboard = hasWhiteboard;
        }
    }

    // Properties for medium rooms, can create objects
    public class MediumRoom : RoomProperties
    {
        public int SeatLimit { get; set; }
        public bool HasProjector { get; set; }
        public bool HasWhiteboard { get; set; }

        public MediumRoom(string roomName, string roomType, int seatAmount, int seatLimit, bool hasProjector, bool hasWhiteboard) 
            : base(roomName, roomType, seatAmount)
        {
            SeatLimit = seatLimit;
            HasProjector = hasProjector;
            HasWhiteboard = hasWhiteboard;
        }
    }

    // Properties for small room, can create objects
    public class SmallRoom : RoomProperties
    {
        public int SeatLimit { get; set; }

        public SmallRoom(string roomName, string roomType, int seatAmount, int seatLimit) 
            : base(roomName, roomType, seatAmount)
        {
            SeatLimit = seatLimit;
        }
    }
}
