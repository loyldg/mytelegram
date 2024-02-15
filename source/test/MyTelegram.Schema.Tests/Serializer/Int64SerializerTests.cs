namespace MyTelegram.Schema.Serializer;

public class Int64SerializerTests
{
    [Fact]
    public void SerializeTest()
    {
        var expectedBytes = "6300000000000000".ToBytes();
        //var stream = new MemoryStream();
        //var bw = new BinaryWriter(stream);
        using var writer = ArrayBufferWriterPool.Rent();
        var serializer = CreateSerializer();

        serializer.Serialize(99L, writer.Writer);

        writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedBytes);
    }

    [Fact]
    public void DeserializeTest()
    {
        var expectedBytes = "6300000000000000".ToBytes();
        //var stream = new MemoryStream(expectedBytes);
        //var br = new BinaryReader(stream);
        var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(expectedBytes));

        var serializer = CreateSerializer();

        var actualBytes = serializer.Deserialize(ref reader);

        actualBytes.ShouldBeEquivalentTo(99L);
    }

    private Int64Serializer CreateSerializer() => new();
}