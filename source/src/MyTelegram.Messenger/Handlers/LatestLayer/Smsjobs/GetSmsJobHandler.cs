namespace MyTelegram.Messenger.Handlers.LatestLayer.Smsjobs;

///<summary>
/// Get info about an SMS job (official clients only).
/// <para>Possible errors</para>
/// Code Type Description
/// 400 SMSJOB_ID_INVALID The specified job ID is invalid.
/// See <a href="https://corefork.telegram.org/method/smsjobs.getSmsJob" />
///</summary>
internal sealed class GetSmsJobHandler : RpcResultObjectHandler<MyTelegram.Schema.Smsjobs.RequestGetSmsJob, MyTelegram.Schema.ISmsJob>
{
    protected override Task<MyTelegram.Schema.ISmsJob> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Smsjobs.RequestGetSmsJob obj)
    {
        throw new NotImplementedException();
    }
}
