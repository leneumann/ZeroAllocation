using System.Text;
using BenchmarkDotNet.Attributes;

namespace ZeroAllocation;

[MemoryDiagnoser]
public class StringBuilderBenchmarks
{
    private const int Iterations = 100;

    [Benchmark(Baseline = true)]
    public string RegularStringBuilder()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < Iterations; i++)
        {
            sb.Append("User ID: ");
            sb.Append(i);
            sb.Append(", Status: Active");
        }
        return sb.ToString();
    }
    
    [Benchmark]
    public string NotOptimalCustomStringBuilder()
    {
        var mySb = new CustomStringBuilder();
        for (int i = 0; i < Iterations; i++)
        {
            mySb.Append("User ID: ");
            mySb.Append(i.ToString()); // to keep it simple
            mySb.Append(", Status: Active");
        }
        return mySb.ToString();
    }
    
    [Benchmark]
    public string StackAllocBuilder()
    {
        Span<char> buffer = stackalloc char[512];
        var builder = new ZeroAllocStringBuilder(buffer);

        builder.Append("User ID: ");
        builder.Append(12345);
        builder.Append(", Status: Active");

        return builder.ToString();
    }

    [Benchmark]
    public void StackAllocBuilderLoop()
    {
        for (int i = 0; i < 100; i++)
        {
            Span<char> buffer = stackalloc char[512];
            var builder = new ZeroAllocStringBuilder(buffer);

            builder.Append("User ID: ");
            builder.Append(i);
            builder.Append(", Status: Active");
            var _ = builder.ToString();
        }
    }
    
    
    [Benchmark]
    public string PooledBuilder()
    {
        using var builder = new PooledStringBuilder(1024);
        for (int i = 0; i < Iterations; i++)
        {
            builder.Append("User ID: ");
            builder.Append(i);
            builder.Append(", Status: Active");
        }
        return builder.ToString();
    }
}