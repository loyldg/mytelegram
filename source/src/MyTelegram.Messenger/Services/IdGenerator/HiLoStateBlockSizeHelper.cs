namespace MyTelegram.Messenger.Services.IdGenerator;

public class HiLoStateBlockSizeHelper : IHiLoStateBlockSizeHelper
{
    private const int BlockSize100 = 100;
    private const int BlockSize1000 = 1000;
    private const int BlockSize10000 = 10000;

    public int GetBlockSize(IdType idType)
    {
        return idType switch
        {
            IdType.GlobalSeqNo => BlockSize10000,
            _ => BlockSize1000
        };
        //switch (idType)
        //{

        //    //case IdType.UserId: return BlockSize100;
        //    //case IdType.MessageId: return BlockSize1000;
        //    //default: return BlockSize1000;
        //}
    }
}