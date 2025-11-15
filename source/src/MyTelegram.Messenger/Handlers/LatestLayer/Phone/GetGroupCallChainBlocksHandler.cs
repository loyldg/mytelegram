namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Fetch the blocks of a <a href="https://corefork.telegram.org/api/end-to-end/group-calls">conference blockchain »</a>.
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.getGroupCallChainBlocks"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetGroupCallChainBlocksHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupCallChainBlocks, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestGetGroupCallChainBlocks obj)
    {
        throw new NotImplementedException();
    }
}