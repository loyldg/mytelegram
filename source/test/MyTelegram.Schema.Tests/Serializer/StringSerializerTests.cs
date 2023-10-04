// ReSharper disable StringLiteralTypo
using MyTelegram.Schema.Extensions;

namespace MyTelegram.Schema.Serializer;

public class StringSerializerTests
{
    [Fact]
    public void SerializeTest()
    {
        var value = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+<>?:{}[],./;'";
        var expectedValue =
            "576162636465666768696A6B6C6D6E6F707172737475767778797A4142434445464748494A4B4C4D4E4F505152535455565758595A3132333435363738393021402324255E262A28295F2B3C3E3F3A7B7D5B5D2C2E2F3B27"
                .ToBytes();
        //var stream = new MemoryStream();
        //var bw = new BinaryWriter(stream);
        using var writer = ArrayBufferWriterPool.Rent();
        var serializer = CreateSerializer();

        serializer.Serialize(value, writer.Writer);

        writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void DeserializeTest()
    {
        var expectedValue = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+<>?:{}[],./;'";
        var value =
            "576162636465666768696A6B6C6D6E6F707172737475767778797A4142434445464748494A4B4C4D4E4F505152535455565758595A3132333435363738393021402324255E262A28295F2B3C3E3F3A7B7D5B5D2C2E2F3B27"
                .ToBytes();
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);
        var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(value));
        var serializer = CreateSerializer();

        var actualValue = serializer.Deserialize(ref reader);

        actualValue.ShouldBeEquivalentTo(expectedValue);
    }

    private StringSerializer CreateSerializer() => new();
}