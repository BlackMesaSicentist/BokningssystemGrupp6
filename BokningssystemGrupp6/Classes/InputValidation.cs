using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes
{
    //Class responsible for validation of user input
    public class InputValidation
    {

        //Check if input is empty
        public bool IsEmpty(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        //Check if input is a number
        public bool IsNumber(string input)
        {
            return int.TryParse(input, out _);
        }

        //Check if input is positive
        public bool IsNumberNegative(string input)
        {
            if (int.TryParse(input, out int result))
            {
                return result < 0;
            }
            return false;
        }

        // Check if input is larger than seat limit
        public bool IsGreaterThanSeatLimit(string input, int seatLimit)
        {
            if (int.TryParse(input, out int result))
            {
                return result > seatLimit;
            }
            return false;
        }

        // Check if input is zero
        public bool IsGreaterThanZero(string input)
        {
            int zeroBalance = 0;
            if (int.TryParse(input, out int result))
            {
                return result == zeroBalance;
            }
            return false;
        }

        // Check if name is used
        public bool IsNameUsed(List<Rooms> rooms, string input)
        {
            return rooms.Any(a => a.RoomName.ToLower() == input.ToLower());

        }

        // Convert string to int
        public int ConvertToInt(string input)
        {
            return int.Parse(input);
        }


    }

}
