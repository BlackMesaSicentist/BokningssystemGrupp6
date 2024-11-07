using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes.LokalClasses
{
    internal class GroupRoom: Rooms, IRoom, IListable
    {
        public int SeatLimit { get; set; }

        public GroupRoom() : base() { }
        public GroupRoom(string roomName, string roomType, int seatAmount, int seatLimit)
            : base(roomName, roomType, seatAmount)
        {
            SeatLimit = seatLimit;
        }
    }
}
