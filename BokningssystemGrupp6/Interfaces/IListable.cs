using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Interfaces
{
    //Interface for listing methods that both rooom and bookings use
    internal interface IListable
    {
        public void ListSpecific() //List a speicifc object from list
        { }

        public void ListAll() //List all objects from a list
        { }

    }
}
