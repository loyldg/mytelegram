using MyTelegram.Schema.Help.LayerN;

namespace MyTelegram.Messenger.Handlers.LayerN.Impl.Help;

public class TestHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.LayerN.RequestTest, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput request, RequestTest obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
