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
                    // Json example: {"id":"ID","name":"VALUE"}
                    case JsonTokenType.StartObject:
                        {
                            //아래 코드가 실행되기 전 현재: {
                            reader.Read();
                            //현재: "id"
                            reader.Read();
                            //현재: "ID"
                            string idValue = reader.GetString(); // "ID" 가져오기
                            reader.Read();
                            //현재: "name"
                            reader.Read();
                            //현재: "VALUE"
                            string nameValue = reader.GetString(); // "VALUE" 가져오기
                            keyValues.Add(idValue, nameValue);
                            reader.Read(); 
                            //현재: } (JsonTokenType.EndObject)
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
