using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Interfaces
{
    // Interface for room properties
    public interface IRoom
    {
        string RoomName { get; set; }
        string RoomType { get; set; }
        int SeatAmount { get; set; }
    }
}
