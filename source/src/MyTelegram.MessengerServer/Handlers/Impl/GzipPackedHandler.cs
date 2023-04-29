using MyTelegram.Handlers.Interfaces;

namespace MyTelegram.MessengerServer.Handlers.Impl;

public class GzipPackedHandler : BaseObjectHandler<TGzipPacked, IObject>, IGzipPackedHandler, IProcessedHandler
{
    private readonly IGZipHelper _gZipHelper;
    private readonly IHandlerHelper _handlerHelper;
    private readonly ILogger<GzipPackedHandler> _logger;

    public GzipPackedHandler(IHandlerHelper handlerHelper,
        IGZipHelper gZipHelper,
        ILogger<GzipPackedHandler> logger)
    {
        _handlerHelper = handlerHelper;
        _gZipHelper = gZipHelper;
        _logger = logger;
    }

    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        TGzipPacked obj)
    {
        //obj.PackedData.Dump("before decompress");
        var unpackedData = _gZipHelper.Decompress(obj.PackedData);
        //unpackedData.Dump("after decompress");
        //var unpackedData = obj.PackedData;// _gZipHelper.Decompress(obj.PackedData);
        var br = new BinaryReader(new MemoryStream(unpackedData));
        var objectId = BitConverter.ToUInt32(unpackedData);
        _handlerHelper.TryGetHandlerName(objectId, out var handlerName);
        _logger.LogInformation("Gzip request,objectId={ObjectId:x2} handlerName={HandlerName}", objectId, handlerName);
        if (_handlerHelper.TryGetHandler(objectId, out var handler))
        {
            var data = SerializerFactory.CreateObjectSerializer<IObject>().Deserialize(br);
            return handler.HandleAsync(input, data);
        }

        throw new NotImplementedException();
    }
}
