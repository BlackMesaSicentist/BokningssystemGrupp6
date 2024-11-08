using BokningssystemGrupp6.Classes.LokalClasses;
using BokningssystemGrupp6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace BokningssystemGrupp6.Classes
{
    public class RoomsConverter : JsonConverter<Rooms>
    {
        public override Rooms Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //Read the JSON object
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;

                //Extract the RoomType to determine the class type
                string roomType = root.GetProperty("RoomType").GetString();

                //
                Rooms room = roomType switch
                {
                    "Hall" => JsonSerializer.Deserialize<Hall>(root.GetRawText(), options),
                    "Classroom" => JsonSerializer.Deserialize<Classroom>(root.GetRawText(), options),
                    "Group room" => JsonSerializer.Deserialize<GroupRoom>(root.GetRawText(), options),
                    _ => throw new JsonException($"Unknown room type: {roomType}")
                };

                return room;
            }
        }

        public override void Write(Utf8JsonWriter writer, Rooms value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}