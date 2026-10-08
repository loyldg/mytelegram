namespace MyTelegram.Services.Serializers;

public class SessionMessageEventDataSerializer(IEventDataSerializer<DataResultResponseReceivedEvent> dataResultResponseEventDataSerializer,
    IEventDataSerializer<DataResultResponseWithUserIdReceivedEvent> dataResultResponseWithUserIdReceivedEventDataSerializer,
    IEventDataSerializer<FileDataResultResponseReceivedEvent> fileDataResultResponseReceivedEventDataSerializer,
    IEventDataSerializer<LayeredAuthKeyIdMessageCreatedIntegrationEvent> layeredAuthKeyIdMessageCreatedIntegrationEventDataSerializer,
    IEventDataSerializer<LayeredPushMessageCreatedIntegrationEvent> layeredPushMessageCreatedIntegrationEventDataSerializer,
    IEventDataSerializer<PushMessageToPeerEvent> pushMessageToPeerEventDataSerializer
) : IEventDataSerializer<ISessionMessage>, ITransientDependency
{
    public object Deserialize(Type type, ReadOnlyMemory<byte> buffer)
    {
        return Deserialize(buffer);
    }

    public void Serialize(IBufferWriter<byte> writer, ISessionMessage data)
    {
        switch (data)
        {
            case DataResultResponseReceivedEvent dataResultResponseReceivedEvent:
                dataResultResponseEventDataSerializer.Serialize(writer, dataResultResponseReceivedEvent);
                break;
            case DataResultResponseWithUserIdReceivedEvent dataResultResponseWithUserIdReceivedEvent:
                dataResultResponseWithUserIdReceivedEventDataSerializer.Serialize(writer, dataResultResponseWithUserIdReceivedEvent);
                break;
            case FileDataResultResponseReceivedEvent fileDataResultResponseReceivedEvent:
                fileDataResultResponseReceivedEventDataSerializer.Serialize(writer, fileDataResultResponseReceivedEvent);
                break;
            case LayeredAuthKeyIdMessageCreatedIntegrationEvent layeredAuthKeyIdMessageCreatedIntegrationEvent:
                layeredAuthKeyIdMessageCreatedIntegrationEventDataSerializer.Serialize(writer, layeredAuthKeyIdMessageCreatedIntegrationEvent);
                break;
            //case LayeredPushMessageCreatedIntegrationEvent<TODO> layeredPushMessageCreatedIntegrationEvent:
            //    break;
            case LayeredPushMessageCreatedIntegrationEvent layeredPushMessageCreatedIntegrationEvent1:
                layeredPushMessageCreatedIntegrationEventDataSerializer.Serialize(writer, layeredPushMessageCreatedIntegrationEvent1);
                break;
            case PushMessageToPeerEvent pushMessageToPeerEvent:
                pushMessageToPeerEventDataSerializer.Serialize(writer, pushMessageToPeerEvent);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(data));
        }
    }

    public ISessionMessage Deserialize(ReadOnlyMemory<byte> buffer)
    {
        throw new NotImplementedException();
    }
}