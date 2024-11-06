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

    //Student student1 = new Student(24, "Bob", "Slob");

    //String text = JsonSerializer.Serialize(student1);
    //Console.WriteLine(text);

    //        Student student2 = new Student();
    //student2 = JsonSerializer.Deserialize<Student>(text);

    //        Console.WriteLine(student2.FirstName);

    internal class Program
    {
        //Lista för bokningar (string userName, string roomName, double startTime, double endTime)
        public static List<Bookings> BookingsInfo = new List<Bookings>();
        static void Main(string[] args)
        {
            
            string listBooking = JsonSerializer.Serialize(BookingsInfo);
            File.WriteAllText("BokningssystemGrupp6.json", listBooking);

            //Lista för lokaler (string roomName, string size, int maxPeople, bool hasWhiteboard, bool hasProjector)
            List<IRoom> rooms = new List<IRoom>();
            string listRoom = JsonSerializer.Serialize(rooms);
            File.WriteAllText("BokningssystemGrupp6.json", listRoom);

            rooms.Add(new Hall("Katt", "Large", 100, 120, true, true));
            rooms.Add(new ClassRoom("Hund Room B", "Medium", 50, 60, true, false));
            rooms.Add(new GroupRoom("Kanin", "Small", 10, 15));

            var options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin,
                UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA)
            };

            File.ReadAllText("JsonTest.json", lista);

            string read = File.ReadAllText("JsonTest.json");
            UserSettings loadedSettings = JsonSerializer.Deserialize<UserSettings>(loadedJson);

        }
    }
}
