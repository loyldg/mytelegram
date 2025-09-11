namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

///<summary>
/// Submit requested order information for validation
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.validateRequestedInfo" />
///</summary>
internal sealed class ValidateRequestedInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestValidateRequestedInfo, MyTelegram.Schema.Payments.IValidatedRequestedInfo>
{
    protected override Task<MyTelegram.Schema.Payments.IValidatedRequestedInfo> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestValidateRequestedInfo obj)
    {
        throw new NotImplementedException();
    }
}
