using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kartrider.API.Json.Converter
{
    internal class StringIntToIntConverter : JsonConverter<int>
    {
        public override int Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            string str = reader.GetString();
            return str == "" ? -1 : int.Parse(str);
        }

        public override void Write(
            Utf8JsonWriter writer,
            int value,
            JsonSerializerOptions options)
            =>
                writer.WriteStringValue(value.ToString());
    }
}
