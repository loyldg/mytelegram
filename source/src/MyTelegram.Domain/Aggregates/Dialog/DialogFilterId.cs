namespace MyTelegram.Domain.Aggregates.Dialog;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<DialogFilterId>))]
public class DialogFilterId : MyIdentity<DialogFilterId>
{
    public DialogFilterId(string value) : base(value)
    {
    }

    public static DialogFilterId Create(long ownerUserId,
        int filterId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"dialogfilter_{ownerUserId}_{filterId}");
    }
}
