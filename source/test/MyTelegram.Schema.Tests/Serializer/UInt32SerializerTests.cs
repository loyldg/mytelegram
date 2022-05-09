namespace MyTelegram.Schema.Serializer;

public class UInt32SerializerTests
{
    [Fact]
    public void SerializeTest()
    {
        var expectedBytes = "09000080".ToBytes();
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);
        var value = int.MaxValue + 10u;
        var serializer = CreateSerializer();

        serializer.Serialize(value, bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedBytes);
    }

    [Fact]
    public void DeserializeTest()
    {
        var expectedBytes = "09000080".ToBytes();
        var stream = new MemoryStream(expectedBytes);
        var br = new BinaryReader(stream);
        var expectedValue = int.MaxValue + 10u;
        var serializer = CreateSerializer();

        var actualBytes = serializer.Deserialize(br);

        actualBytes.ShouldBeEquivalentTo(expectedValue);
    }

    private UInt32Serializer CreateSerializer() => new();
}