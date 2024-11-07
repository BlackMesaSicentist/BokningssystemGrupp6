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
    public class RoomConverter : JsonConverter<IRoom>
    {
        public override IRoom Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var jsonObject = doc.RootElement;
                string? roomType = jsonObject.GetProperty("RoomType").GetString();

                IRoom? roomList = roomType switch
                {
                    "Hall" => JsonSerializer.Deserialize<Hall>(jsonObject.GetRawText(), options),
                    "Class room" => JsonSerializer.Deserialize<ClassRoom>(jsonObject.GetRawText(), options),
                    "Group room" => JsonSerializer.Deserialize<GroupRoom>(jsonObject.GetRawText(), options),
                    _ => throw new NotSupportedException($"RoomType '{roomType}' is not supported")
                };
                return roomList;
            }
        }

        public override void Write(Utf8JsonWriter writer, IRoom value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }
}
