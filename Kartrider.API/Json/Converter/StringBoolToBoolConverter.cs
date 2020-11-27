using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kartrider.API.Json.Converter
{

    internal class StringBoolToBoolConverter : JsonConverter<bool>
    {
        public override bool Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            string str = reader.GetString();
            return str == "1";
        }
        public override void Write(
            Utf8JsonWriter writer,
            bool value,
            JsonSerializerOptions options)
            =>
               writer.WriteStringValue(value ? "1" : "0");
    }
}
