using MyTelegram.Handlers.Interfaces;

namespace MyTelegram.Handlers;

internal sealed class MsgsStateReqHandler : BaseObjectHandler<TMsgsStateReq, TMsgsStateInfo>, IMsgsStateReqHandler,
    IProcessedHandler
{
    protected override Task<TMsgsStateInfo> HandleCoreAsync(IRequestInput input,
        TMsgsStateReq obj)
    {
        //https://core.telegram.org/mtproto/service_messages_about_messages
        /*
         * Here, info is a string that contains exactly one byte of message status for each message from the incoming msg_ids list:

            1 = nothing is known about the message (msg_id too low, the other party may have forgotten it)
            2 = message not received (msg_id falls within the range of stored identifiers; however, the other party has certainly not received a message like that)
            3 = message not received (msg_id too high; however, the other party has certainly not received it yet)
            4 = message received (note that this response is also at the same time a receipt acknowledgment)
            +8 = message already acknowledged
            +16 = message not requiring acknowledgment
            +32 = RPC query contained in message being processed or processing already complete
            +64 = content-related response to message already generated
            +128 = other party knows for a fact that message is already received
            This response does not require an acknowledgment. It is an acknowledgment of the relevant msgs_state_req, in and of itself.

            Note that if it turns out suddenly that the other party does not have a message that looks like it has been sent to it, the message can simply be re-sent. Even if the other party should receive two copies of the message at the same time, the duplicate will be ignored. (If too much time has passed, and the original msg_id is not longer valid, the message is to be wrapped in msg_copy).
         */
        var r = new TMsgsStateInfo { Info = "1", ReqMsgId = obj.MsgIds.FirstOrDefault() };

        return Task.FromResult(r);
    }
}