using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Interfaces
{
    //Interface for rooms properties
    public interface IRoom
    {
        string RoomName { get; set; } //Name of room
        string RoomType { get; set; } //Type of room
        int SeatAmount { get; set; } //Amount of seats in room
    }
}
