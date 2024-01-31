using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyTelegram.Services.Extensions;

public class BitArrayConverter : JsonConverter<BitArray>
{
    public override BitArray? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetInt32(out var value))
        {
            return new BitArray(BitConverter.GetBytes(value));
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, BitArray value, JsonSerializerOptions options)
    {
        var bytes = new byte[4];
        value.CopyTo(bytes, 0);
        writer.WriteNumberValue(BitConverter.ToInt32(bytes));
    }
}