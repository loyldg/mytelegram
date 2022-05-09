namespace MyTelegram.Schema.Serializer;

public class Int32SerializerTests
{
    [Fact]
    public void SerializeTest()
    {
        var expectedValue = new byte[] { 01, 0, 0, 0 };
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);
        var serializer = CreateSerializer();

        serializer.Serialize(1, bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void DeserializeTest()
    {
        var value = new byte[] { 01, 0, 0, 0 };
        var expectedValue = 1;
        var stream = new MemoryStream(value);
        var br = new BinaryReader(stream);
        var serializer = CreateSerializer();

        var actualValue = serializer.Deserialize(br);

        actualValue.ShouldBeEquivalentTo(expectedValue);
    }

    private Int32Serializer CreateSerializer() => new();
}