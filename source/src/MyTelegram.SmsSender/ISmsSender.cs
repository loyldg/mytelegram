namespace MyTelegram.SmsSender;

public interface ISmsSender
{
    Task SendAsync(SmsMessage smsMessage);
}