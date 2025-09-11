namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Report a <a href="https://corefork.telegram.org/api/antispam">native antispam</a> false positive
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.reportAntiSpamFalsePositive" />
///</summary>
internal sealed class ReportAntiSpamFalsePositiveHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReportAntiSpamFalsePositive, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestReportAntiSpamFalsePositive obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
