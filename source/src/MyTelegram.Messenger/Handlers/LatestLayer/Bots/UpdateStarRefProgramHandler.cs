namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Create, edit or delete the <a href="https://corefork.telegram.org/api/bots/referrals">affiliate program</a> of a bot we own
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// 400 STARREF_AWAITING_END The previous referral program was terminated less than 24 hours ago: further changes can be made after the date specified in userFull.starref_program.end_date.
/// 400 STARREF_PERMILLE_INVALID The specified commission_permille is invalid: the minimum and maximum values for this parameter are contained in the <a href="https://corefork.telegram.org/api/config#starref-min-commission-permille">starref_min_commission_permille</a> and <a href="https://corefork.telegram.org/api/config#starref-max-commission-permille">starref_max_commission_permille</a> client configuration parameters.
/// 400 STARREF_PERMILLE_TOO_LOW The specified commission_permille is too low: the minimum and maximum values for this parameter are contained in the <a href="https://corefork.telegram.org/api/config#starref-min-commission-permille">starref_min_commission_permille</a> and <a href="https://corefork.telegram.org/api/config#starref-max-commission-permille">starref_max_commission_permille</a> client configuration parameters.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.updateStarRefProgram"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateStarRefProgramHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestUpdateStarRefProgram, MyTelegram.Schema.IStarRefProgram>
{
    protected override Task<MyTelegram.Schema.IStarRefProgram> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestUpdateStarRefProgram obj)
    {
        throw new NotImplementedException();
    }
}