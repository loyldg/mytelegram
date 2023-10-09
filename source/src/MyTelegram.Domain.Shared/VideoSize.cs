namespace MyTelegram;

public record VideoSize(int W, int H, long Size, string Type, double VideoStartTs);
public record VideoSizeEmojiMarkup(long EmojiId, List<int> BackgroundColors);