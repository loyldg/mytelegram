// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Returns the support user for the "ask a question" feature.
/// See <a href="https://corefork.telegram.org/method/help.getSupport" />
///</summary>
internal sealed class GetSupportHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetSupport, MyTelegram.Schema.Help.ISupport>,
    Help.IGetSupportHandler
{
    protected override Task<MyTelegram.Schema.Help.ISupport> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetSupport obj)
    {
        throw new NotImplementedException();
    }
}
