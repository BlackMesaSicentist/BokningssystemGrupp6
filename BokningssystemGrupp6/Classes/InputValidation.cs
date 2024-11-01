using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes
{
    // Klass som ansvarar för validering av användarens input
    public class InputValidator
    {

        // Kollar om input är tom
        public bool IsEmpty(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        // Kollar om input är ett nummer
        public bool IsNumber(string input)
        {
            return int.TryParse(input, out _);
        }

        // Kollar om input är en positivt int
        public bool IsNumberNegative(string input)
        {
            if (int.TryParse(input, out int result))
            {
                return result < 0;
            }
            return false;
        }

        // Kollar om input är ett decimaltal (behöver inte innehålla decimaler)
        public bool IsNumberDecimal(string input)
        {
            return decimal.TryParse(input, out _);
        }

        // Kollar om input är en positivt decimal
        public bool IsDecimalNegative(string input)
        {
            if (decimal.TryParse(input, out decimal result))
            {
                return result < 0;
            }
            return false;
        }

        // Kollar om input är mellan 0 - 5
        public bool IsDecimalBetweenZeroAndFive(string input)
        {
            if (decimal.TryParse(input, out decimal result))
            {
                return result >= 0 && result <= 5;
            }
            return false;
        }

        // Kollar om input är större än ett annat tal
        public bool IsGreaterThanBalance(string input, decimal accountBalance)
        {
            if (decimal.TryParse(input, out decimal result))
            {
                return result > accountBalance;
            }
            return false;
        }
        // Kollar om input är noll

        public bool IsGreaterThanZero(string input)
        {
            decimal zeroBalance = 0;
            if (decimal.TryParse(input, out decimal result))
            {
                return result == zeroBalance;
            }
            return false;
        }

        // Kollar om kontot finns
        //public bool AccountExists(List<Account> accountList, string accountNrCheck)
        //{
        //    if (int.TryParse(accountNrCheck, out int accountNr))
        //    {
        //        return accountList.Any(a => a.AccountNr == accountNr);
        //    }
        //    return false;
        //}

        // Konverterar string till int
        public int ConvertToInt(string input)
        {
            return int.Parse(input);
        }

        // Konverterar string till decimal och avrundar till 2 decimaler
        public decimal ConvertToDecimal(string input)
        {
            return Math.Round(decimal.Parse(input), 2);
        }

    }

}
