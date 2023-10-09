// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Report a profile photo of a dialog
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/account.reportProfilePhoto" />
///</summary>
internal sealed class ReportProfilePhotoHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestReportProfilePhoto, IBool>,
    Account.IReportProfilePhotoHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestReportProfilePhoto obj)
    {
        throw new NotImplementedException();
    }
}
