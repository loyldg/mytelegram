// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Returns info on data center nearest to the user.
/// See <a href="https://corefork.telegram.org/method/help.getNearestDc" />
///</summary>
internal sealed class GetNearestDcHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetNearestDc, MyTelegram.Schema.INearestDc>,
    Help.IGetNearestDcHandler
{
    protected override Task<MyTelegram.Schema.INearestDc> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetNearestDc obj)
    {
        throw new NotImplementedException();
    }
}
