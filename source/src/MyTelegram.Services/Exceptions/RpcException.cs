using MyTelegram.Schema;

namespace MyTelegram.Services.Exceptions;

public class RpcException : Exception
{
    public TRpcError Error { get; set; }
    //public long RequestMessageId { get; }

    public RpcException(int errorCode, string errorMessage) : base(errorMessage)
    {
        //RequestMessageId = requestMessageId;
        Error = new TRpcError
        {
            ErrorCode = errorCode,
            ErrorMessage = errorMessage
        };
    }
}