// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Get localized name of the telegram support user
/// See <a href="https://corefork.telegram.org/method/help.getSupportName" />
///</summary>
internal sealed class GetSupportNameHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetSupportName, MyTelegram.Schema.Help.ISupportName>,
    Help.IGetSupportNameHandler
{
    protected override Task<MyTelegram.Schema.Help.ISupportName> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetSupportName obj)
    {
        throw new NotImplementedException();
    }
}
