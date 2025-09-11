namespace MyTelegram.Schema;

public interface ILayeredStarGift : IStarGift
{
    MyTelegram.Schema.IDocument Sticker { get; set; }
}