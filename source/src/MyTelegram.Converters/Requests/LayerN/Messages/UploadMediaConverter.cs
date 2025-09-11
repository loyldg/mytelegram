using MyTelegram.Schema.Messages.LayerN;

namespace MyTelegram.Converters.Requests.LayerN.Messages;

internal sealed class UploadMediaConverter
    : IRequestConverter<
        RequestUploadMedia,
        Schema.Messages.RequestUploadMedia
    >, ITransientDependency
{
    public Schema.Messages.RequestUploadMedia ToLatestLayerData(IRequestInput request, RequestUploadMedia obj)
    {
        return new Schema.Messages.RequestUploadMedia
        {
            Media = obj.Media,
            Peer = obj.Peer
        };
    }
}