namespace MyTelegram.Schema;

[AttributeUsage(AttributeTargets.Class)]
public class TlObjectAttribute : Attribute
{
    public TlObjectAttribute(uint constructorId)
    {
        ConstructorId = constructorId;
    }

    public uint ConstructorId { get; }
}
