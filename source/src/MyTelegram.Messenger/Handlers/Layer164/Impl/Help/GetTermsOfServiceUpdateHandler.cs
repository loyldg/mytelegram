// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Look for updates of telegram's terms of service
/// See <a href="https://corefork.telegram.org/method/help.getTermsOfServiceUpdate" />
///</summary>
internal sealed class GetTermsOfServiceUpdateHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetTermsOfServiceUpdate, MyTelegram.Schema.Help.ITermsOfServiceUpdate>,
    Help.IGetTermsOfServiceUpdateHandler
{
    protected override Task<MyTelegram.Schema.Help.ITermsOfServiceUpdate> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetTermsOfServiceUpdate obj)
    {
        throw new NotImplementedException();
    }
}
