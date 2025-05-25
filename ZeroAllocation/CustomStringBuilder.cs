namespace ZeroAllocation;

public class CustomStringBuilder(int capacity = 256)
{
    private char[] _buffer = new char[capacity];
    private int _position = 0;

    public void Append(char c)
    {
        EnsureCapacity(1);
        _buffer[_position++] = c;
    }

    public void Append(string s)
    {
        EnsureCapacity(s.Length);
        s.CopyTo(0, _buffer, _position, s.Length);
        _position += s.Length;
    }

    public override string ToString()
    {
        return new string(_buffer, 0, _position);
    }

    private void EnsureCapacity(int additional)
    {
        if (_position + additional > _buffer.Length)
        {
            int newSize = Math.Max(_buffer.Length * 2, _position + additional);
            var newBuffer = new char[newSize];
            Array.Copy(_buffer, newBuffer, _position);
            _buffer = newBuffer;
        }
    }
}
