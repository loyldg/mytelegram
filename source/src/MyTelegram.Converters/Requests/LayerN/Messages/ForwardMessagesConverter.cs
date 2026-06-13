using MyTelegram.Schema.Messages.LayerN;

namespace MyTelegram.Converters.Requests.LayerN.Messages;

internal sealed class ForwardMessagesConverter
    : IRequestConverter<
        RequestForwardMessages,
        Schema.Messages.RequestForwardMessages
    >, ITransientDependency
{
    public Schema.Messages.RequestForwardMessages ToLatestLayerData(IRequestInput request, RequestForwardMessages obj)
    {
        return new Schema.Messages.RequestForwardMessages
        {
            Silent = obj.Silent,
            Background = obj.Background,
            WithMyScore = obj.WithMyScore,
            DropAuthor = obj.DropAuthor,
            DropMediaCaptions = obj.DropMediaCaptions,
            Noforwards = obj.Noforwards,
            AllowPaidFloodskip = obj.AllowPaidFloodskip,
            FromPeer = obj.FromPeer,
            Id=obj.Id,
            RandomId = obj.RandomId,
            ToPeer = obj.ToPeer,
            TopMsgId = obj.TopMsgId,
            ReplyTo = obj.ReplyTo,
            ScheduleDate = obj.ScheduleDate,
            ScheduleRepeatPeriod = obj.ScheduleRepeatPeriod,
            SendAs = obj.SendAs,
            QuickReplyShortcut = obj.QuickReplyShortcut,
            VideoTimestamp = obj.VideoTimestamp,
            AllowPaidStars = obj.AllowPaidStars,
            SuggestedPost = obj.SuggestedPost
        };
    }
}