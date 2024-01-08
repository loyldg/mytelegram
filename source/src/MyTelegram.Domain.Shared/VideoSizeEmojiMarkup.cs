// ReSharper disable once CheckNamespace
namespace MyTelegram;

public record VideoSizeEmojiMarkup(long EmojiId, List<int> BackgroundColors);

//public class VideoSize
//{
//    public int W { get; set; }
//    public int H { get; set; }
//    public long Size { get; set; }
//    public string Type { get; set; } = default!;
//    public double VideoStartTs { get; set; }
//}
//public class PhotoSize
//{
//    public int W { get; set; }
//    public int H { get; set; }
//    public long Size { get; set; }
//    public string Type { get; set; } = default!;
//}