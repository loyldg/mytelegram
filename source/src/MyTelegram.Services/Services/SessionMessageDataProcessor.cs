namespace MyTelegram.Services.Services;

public class SessionMessageDataProcessor(IEventBus eventBus) : IDataProcessor<ISessionMessage>
{
    //private static readonly IGZipHelper gZipHelper = new GZipHelper();

    public async Task ProcessAsync(ISessionMessage data, CancellationToken cancellationToken = default)
    {
        try
        {
            switch (data)
            {
                case DataResultResponseReceivedEvent dataResultResponseReceivedEvent:

                    if (dataResultResponseReceivedEvent.DataObject is TRpcResult rpcResult)
                    {
                        using var writer = new ArrayPoolBufferWriter<byte>();
                        dataResultResponseReceivedEvent.DataObject.Serialize(writer);

                        //if (writer.WrittenCount > 500)
                        //{
                        //    //await eventBus.PublishAsync(newData);
                        //    using var writer2 = new ArrayPoolBufferWriter<byte>();
                        //    rpcResult.Result.Serialize(writer2);
                        //    using var writer3 = new ArrayPoolBufferWriter<byte>();
                        //    gZipHelper.Compress(writer2.WrittenSpan, writer3);
                        //    rpcResult.Result = new TGzipPacked
                        //    {
                        //        PackedData = writer3.WrittenMemory
                        //    };
                        //    using var writer4 = new ArrayPoolBufferWriter<byte>();
                        //    rpcResult.Serialize(writer4);

                        //    await eventBus.PublishAsync(dataResultResponseReceivedEvent with
                        //    {
                        //        DataObject = null,
                        //        Data = writer4.WrittenMemory
                        //    });

                        //}
                        //else
                        {
                            await eventBus.PublishAsync(dataResultResponseReceivedEvent with
                            {
                                DataObject = null,
                                Data = writer.WrittenMemory
                            });
                        }
                        //if (writer.WrittenCount < 280 && writer.WrittenCount > 270)
                        //{
                        //    logger.LogInformation("rpcResult ReqMsgId:{ReqMsgId} :{@Data}",
                        //        dataResultResponseReceivedEvent.ReqMsgId, dataResultResponseReceivedEvent.DataObject);
                        //    writer.WrittenSpan.Dump($"[1]ReqMsgId:{dataResultResponseReceivedEvent.ReqMsgId}");
                        //    using var writer2 = new ArrayPoolBufferWriter<byte>();
                        //    dataResultResponseReceivedEvent.DataObject.Serialize(writer2);
                        //    writer2.WrittenSpan.Dump($"[2]ReqMsgId:{dataResultResponseReceivedEvent.ReqMsgId}");
                        //}
                    }
                    else
                    {
                        if (dataResultResponseReceivedEvent.DataObject != null)
                        {
                            using var writer = new ArrayPoolBufferWriter<byte>();
                            dataResultResponseReceivedEvent.DataObject.Serialize(writer);
                            await eventBus.PublishAsync(dataResultResponseReceivedEvent with
                            {
                                DataObject = null,
                                Data = writer.WrittenMemory
                            });
                        }
                    }

                    break;
                case DataResultResponseWithUserIdReceivedEvent dataResultResponseWithUserIdReceivedEvent:
                    await eventBus.PublishAsync(dataResultResponseWithUserIdReceivedEvent);
                    break;
                case FileDataResultResponseReceivedEvent fileDataResultResponseReceivedEvent:
                    await eventBus.PublishAsync(fileDataResultResponseReceivedEvent);
                    break;
                case LayeredAuthKeyIdMessageCreatedIntegrationEvent layeredAuthKeyIdMessageCreatedIntegrationEvent:
                    await eventBus.PublishAsync(layeredAuthKeyIdMessageCreatedIntegrationEvent);
                    break;
                case LayeredPushMessageCreatedIntegrationEvent layeredPushMessageCreatedIntegrationEvent:
                    await eventBus.PublishAsync(layeredPushMessageCreatedIntegrationEvent);
                    break;
                case PushMessageToPeerEvent pushMessageToPeerEvent:
                    await eventBus.PublishAsync(pushMessageToPeerEvent);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(data));
            }
        }
        finally
        {
            data.MemoryOwner?.Dispose();
        }
    }
}