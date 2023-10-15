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

public class PtsForAuthKeyId : Identity<PtsForAuthKeyId>
{
    public PtsForAuthKeyId(string value) : base(value)
    {
    }

    public static PtsForAuthKeyId Create(long ownerPeerId, long permAuthKeyId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"ptsforauthkeyidreadmodel-{ownerPeerId}-{permAuthKeyId}");
    }
}