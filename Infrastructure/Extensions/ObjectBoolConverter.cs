using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Extensions
{
    public class ObjectBoolConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                return reader.GetBoolean();
            }
            catch
            {
                int value = reader.GetInt32();
                if (value == 1)
                {
                    return true;
                }
                if (value == 0)
                {
                    return false;
                }
            }

            throw new JsonException();

        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case true:
                    writer.WriteBooleanValue(true);
                    break;
                case false:
                    writer.WriteBooleanValue(false);
                    break;

            }

        }

    }
}
