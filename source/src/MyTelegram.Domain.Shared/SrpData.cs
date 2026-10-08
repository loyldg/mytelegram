namespace MyTelegram;

public class SrpData(
    long srpId,
    byte[] b,
    byte[] gb,
    byte[] salt1,
    byte[] salt2)
{
    public byte[] B { get; init; } = b;
    public byte[] Gb { get; init; } = gb;
    public byte[] Salt1 { get; init; } = salt1;
    public byte[] Salt2 { get; init; } = salt2;

    public long SrpId { get; init; } = srpId;
}