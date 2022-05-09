namespace MyTelegram.Domain.Aggregates.RpcResult;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<RpcResultId>))]
public class RpcResultId : MyIdentity<RpcResultId>
{
    public RpcResultId(string value) : base(value)
    {
    }

    public static RpcResultId Create(string sourceId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, sourceId);
    }
}
