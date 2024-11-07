﻿using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes.LokalClasses
{
    internal class Classroom: Rooms, IRoom, IListable
    {
        public int SeatLimit { get; set; }
        public bool HasProjector { get; set; }
        public bool HasWhiteboard { get; set; }

        public Classroom() :base (){ }
        public Classroom(string roomName, string roomType, int seatAmount, int seatLimit, bool hasProjector, bool hasWhiteboard)
            : base(roomName, roomType, seatAmount)
        {
            SeatLimit = seatLimit;
            HasProjector = hasProjector;
            HasWhiteboard = hasWhiteboard;
        }
    }
}
