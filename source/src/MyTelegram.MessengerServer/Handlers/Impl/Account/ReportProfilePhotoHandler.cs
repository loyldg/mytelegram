using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class ReportProfilePhotoHandler : RpcResultObjectHandler<RequestReportProfilePhoto, IBool>,
    IReportProfilePhotoHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReportProfilePhoto obj)
    {
        throw new NotImplementedException();
    }
}
