using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kartrider.API.Json.Converter
{
    internal class MetadataConverter : JsonConverter<Dictionary<string, string>>
    {
        public override Dictionary<string, string> Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    // example: {"id":"ID","name":"VALUE"}
                    case JsonTokenType.StartObject:
                        {
                            //current: {
                            reader.Read();
                            //string idKey = reader.GetString(); // "id"
                            reader.Read();
                            string idValue = reader.GetString(); // "ID"
                            reader.Read();
                            //string nameKey = reader.GetString(); // "name"
                            reader.Read();
                            string nameValue = reader.GetString(); // "VALUE"
                            keyValues.Add(idValue, nameValue);
                            reader.Read(); // } (JsonTokenType.EndObject)
                            break;
                        }
                    case JsonTokenType.EndArray:
                        return keyValues;
                }
            }
            throw new JsonException();
        }

        public override void Write(
            Utf8JsonWriter writer,
            Dictionary<string, string> value,
            JsonSerializerOptions options)
            =>
                throw new NotSupportedException("Json write not supported");
    }
}
