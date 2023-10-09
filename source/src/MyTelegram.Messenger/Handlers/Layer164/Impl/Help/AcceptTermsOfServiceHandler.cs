// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Accept the new terms of service
/// <para>Possible errors</para>
/// Code Type Description
/// 400 DATA_JSON_INVALID The provided JSON data is invalid.
/// See <a href="https://corefork.telegram.org/method/help.acceptTermsOfService" />
///</summary>
internal sealed class AcceptTermsOfServiceHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestAcceptTermsOfService, IBool>,
    Help.IAcceptTermsOfServiceHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestAcceptTermsOfService obj)
    {
        throw new NotImplementedException();
    }
}
