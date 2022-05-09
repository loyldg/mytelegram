namespace MyTelegram.Schema.Serializer;

public class Int64SerializerTests
{
    [Fact]
    public void SerializeTest()
    {
        var expectedBytes = "6300000000000000".ToBytes();
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);
        var serializer = CreateSerializer();

        serializer.Serialize(99L, bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedBytes);
    }

    [Fact]
    public void DeserializeTest()
    {
        var expectedBytes = "6300000000000000".ToBytes();
        var stream = new MemoryStream(expectedBytes);
        var br = new BinaryReader(stream);
        var serializer = CreateSerializer();

        var actualBytes = serializer.Deserialize(br);

        actualBytes.ShouldBeEquivalentTo(99L);
    }

    private Int64Serializer CreateSerializer() => new();
}