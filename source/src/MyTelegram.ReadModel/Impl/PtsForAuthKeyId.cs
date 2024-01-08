using EventFlow.Core;

namespace MyTelegram.ReadModel.Impl;

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