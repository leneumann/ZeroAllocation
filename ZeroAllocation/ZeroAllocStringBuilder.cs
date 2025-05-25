namespace ZeroAllocation;

ref struct ZeroAllocStringBuilder
{
    private Span<char> _buffer;
    private int _position;

    public ZeroAllocStringBuilder(Span<char> buffer)
    {
        _buffer = buffer;
        _position = 0;
    }

    public void Append(string value)
    {
        if (!value.AsSpan().TryCopyTo(_buffer.Slice(_position)))
            throw new InvalidOperationException("Buffer overflow");

        _position += value.Length;
    }

    public void Append(int value)
    {
        if (!value.TryFormat(_buffer.Slice(_position), out int charsWritten))
            throw new InvalidOperationException("Buffer overflow");

        _position += charsWritten;
    }

    public override string ToString()
    {
        return new string(_buffer.Slice(0, _position));
    }
}