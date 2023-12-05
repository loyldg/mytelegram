namespace MyTelegram.SmsSender;

public class SmsMessage
{
    public string PhoneNumber { get; }

    public string Text { get; }

    public IDictionary<string, object> Properties { get; }

    public SmsMessage(string phoneNumber, string text)
    {
        PhoneNumber = phoneNumber;
        Text = text;
        Properties = new Dictionary<string, object>();
    }
}