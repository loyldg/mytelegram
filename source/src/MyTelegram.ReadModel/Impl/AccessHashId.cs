using EventFlow.Core;

namespace MyTelegram.ReadModel.Impl;

public class AccessHashId : Identity<AccessHashId>
{
    public AccessHashId(string value) : base(value)
    {
    }

    public static AccessHashId Create(long id,
        long accessHash)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"{id}_{accessHash}");
    }
}