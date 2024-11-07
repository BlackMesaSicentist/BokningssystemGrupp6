using System.Drawing;
using System.Text.Json;
using BokningssystemGrupp6.Classes;
using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;
using System.Text.Encodings.Web;
using System.Text.Unicode;
//Av: Angelica Bergström, David Berglin, Adam Axelsson-Hedman, Alexander Bullerjahn

namespace BokningssystemGrupp6
{

    internal class Program
    {
        static void Main(string[] args)
        {

            //List for bookings (string userName, string roomName, DateTime DateTimeStart, DateTime DateTimeEnd)
            List<Bookings> bookingsInfo = new List<Bookings>();

            //List for rooms (string roomName, string size, int maxPeople, bool hasWhiteboard, bool hasProjector)
            List<IRoom> rooms = new List<IRoom>();

            //Method to deserialize lists
            Save.UnPackFile(rooms, bookingsInfo);

            //Method to show and use menu
            Menu.MainMenu(rooms, bookingsInfo);

        }
    }
}
