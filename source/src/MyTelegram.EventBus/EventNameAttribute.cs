namespace MyTelegram.EventBus;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class EventNameAttribute : Attribute
{
    public EventNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
