using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kartrider.API.Json.Converter
{
    internal class StringToTimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            string str = reader.GetString();
            if(str == null || str == "")
            {
                return TimeSpan.Zero;
            }
            int sec = int.Parse(str.Substring(0, str.Length - 3));
            int mill = int.Parse(str.Substring(str.Length - 3, 3));
            return new TimeSpan(0, 0, 0, sec, mill);
        }
        public override void Write(
            Utf8JsonWriter writer,
            TimeSpan value,
            JsonSerializerOptions options)
        {
            string str;
            if(value == TimeSpan.Zero)
            {
                str = "";
            }
            else
            {
                str = Convert.ToInt32(value.TotalSeconds).ToString() + value.Milliseconds.ToString("000");
            }
            writer.WriteStringValue(str);
        }
    }
}
