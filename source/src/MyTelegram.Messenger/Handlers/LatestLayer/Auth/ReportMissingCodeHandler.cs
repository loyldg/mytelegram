namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;

///<summary>
/// Official apps only, reports that the SMS authentication code wasn't delivered.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PHONE_NUMBER_INVALID The phone number is invalid.
/// See <a href="https://corefork.telegram.org/method/auth.reportMissingCode" />
///</summary>
internal sealed class ReportMissingCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestReportMissingCode, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestReportMissingCode obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
