namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Report a profile photo of a dialog
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/account.reportProfilePhoto" />
///</summary>
internal sealed class ReportProfilePhotoHandler(ILogger<ReportProfilePhotoHandler> logger) : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestReportProfilePhoto, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestReportProfilePhoto obj)
    {
        long? photoId = null;
        switch (obj.PhotoId)
        {
            case TInputPhoto inputPhoto:
                photoId = inputPhoto.Id;

                break;
        }
        logger.LogInformation("ReportProfilePhotoHandler peer: {Peer}, photoId: {PhotoId}, reason: {@Reason}, message: {Message}", obj.Peer, photoId, obj.Reason, obj.Message);

        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
