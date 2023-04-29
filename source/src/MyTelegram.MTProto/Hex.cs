namespace MyTelegram.MTProto;

public class Hex
{
    private readonly byte[] _bytes;
    private readonly int _bytesPerLine;

    private readonly int _length;
    private readonly StringBuilder _sb = new();
    private readonly bool _showAscii;
    private readonly bool _showHeader;
    private readonly bool _showOffset;

    private int _index;

    private Hex(byte[] bytes,
        int bytesPerLine,
        bool showHeader,
        bool showOffset,
        bool showAscii,
        int length)
    {
        _bytes = bytes;
        _bytesPerLine = bytesPerLine;
        _showHeader = showHeader;
        _showOffset = showOffset;
        _showAscii = showAscii;

        if (length == 0)
        {
            _length = bytes.Length;
        }
        else
        {
            _length = length;
        }
    }

    public static string Dump(byte[]? bytes,
        int bytesPerLine = 16,
        bool showHeader = true,
        bool showOffset = true,
        bool showAscii = true,
        int length = 0)
    {
        if (bytes == null)
        {
            return "<null>";
        }

        return new Hex(bytes,
            bytesPerLine,
            showHeader,
            showOffset,
            showAscii,
            length).Dump();
    }

    private string Dump()
    {
        if (_showHeader)
        {
            WriteHeader();
        }

        WriteBody();
        return _sb.ToString();
    }

    private static string Translate(byte b)
    {
        return b < 32 ? "." : Encoding.ASCII.GetString(new[] { b });
    }

    private void WriteAscii()
    {
        var backtrack = (_index - 1) / _bytesPerLine * _bytesPerLine;
        var length = _index - backtrack;

        // This is to fill up last string of the dump if it's shorter than _bytesPerLine
        _sb.Append(new string(' ', (_bytesPerLine - length) * 3));

        _sb.Append("   ");
        for (var i = 0; i < length; i++)
        {
            _sb.Append(Translate(_bytes[backtrack + i]));
        }
    }

    private void WriteBody()
    {
        while (_index < _length)
        {
            if (_index % _bytesPerLine == 0)
            {
                if (_index > 0)
                {
                    if (_showAscii)
                    {
                        WriteAscii();
                    }

                    _sb.AppendLine();
                }

                if (_showOffset)
                {
                    WriteOffset();
                }
            }

            WriteByte();
            if (_index % _bytesPerLine != 0 && _index < _length)
            {
                _sb.Append(' ');
            }
        }

        if (_showAscii)
        {
            WriteAscii();
        }
    }

    private void WriteByte()
    {
        _sb.Append($"{_bytes[_index]:X2}");
        _index++;
    }

    private void WriteHeader()
    {
        if (_showOffset)
        {
            _sb.Append("Offset(h)  ");
        }

        for (var i = 0; i < _bytesPerLine; i++)
        {
            _sb.Append($"{i & 0xFF:D2}");
            if (i + 1 < _bytesPerLine)
            {
                _sb.Append(' ');
            }
        }

        _sb.AppendLine();
        _sb.AppendLine();
    }

    private void WriteOffset()
    {
        _sb.Append($"{_index:X8}   ");
    }
}
