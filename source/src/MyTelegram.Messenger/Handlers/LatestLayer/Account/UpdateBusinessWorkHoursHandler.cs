namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Specify a set of <a href="https://corefork.telegram.org/api/business#opening-hours">Telegram Business opening hours</a>.<br/>
/// This info will be contained in <a href="https://corefork.telegram.org/constructor/userFull">userFull</a>.<code>business_work_hours</code>.To remove all opening hours, invoke the method without setting the <code>business_work_hours</code> field.Note that the opening hours specified by the user must be appropriately validated and transformed before invoking the method, as specified <a href="https://corefork.telegram.org/api/business#opening-hours">here »</a>.
/// Possible errors
/// Code Type Description
/// 400 BUSINESS_WORK_HOURS_EMPTY No work hours were specified.
/// 400 BUSINESS_WORK_HOURS_PERIOD_INVALID The specified work hours are invalid, see <a href="https://corefork.telegram.org/api/business#opening-hours">here »</a> for the exact requirements.
/// 400 TIMEZONE_INVALID The specified timezone does not exist.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.updateBusinessWorkHours"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateBusinessWorkHoursHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateBusinessWorkHours, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestUpdateBusinessWorkHours obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}