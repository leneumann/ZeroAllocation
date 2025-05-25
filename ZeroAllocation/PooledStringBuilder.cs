using System.Buffers;

namespace ZeroAllocation;

internal sealed class PooledStringBuilder(int initialCapacity = 256) : IDisposable
{
    private char[] _buffer = ArrayPool<char>.Shared.Rent(initialCapacity);
    private int _position = 0;

    public void Append(string value)
    {
        EnsureCapacity(value.Length);
        value.CopyTo(0, _buffer, _position, value.Length);
        _position += value.Length;
    }

    public void Append(int value)
    {
        var span = _buffer.AsSpan(_position);
        if (!value.TryFormat(span, out int charsWritten))
            Grow();

        _position += charsWritten;
    }

    public override string ToString()
    {
        return new string(_buffer, 0, _position);
    }

    public void Dispose()
    {
        if (_buffer == null) return;
        ArrayPool<char>.Shared.Return(_buffer);
        _buffer = null!;
    }

    private void EnsureCapacity(int additionalLength)
    {
        if (_position + additionalLength > _buffer.Length)
            Grow();
    }

    private void Grow()
    {
        var newSize = _buffer.Length * 2;
        var newBuffer = ArrayPool<char>.Shared.Rent(newSize);
        _buffer.AsSpan(0, _position).CopyTo(newBuffer);
        ArrayPool<char>.Shared.Return(_buffer);
        _buffer = newBuffer;
    }
}