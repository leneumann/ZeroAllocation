| Method                        | Mean        | Error     | StdDev    | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|------------------------------ |------------:|----------:|----------:|------:|--------:|-------:|-------:|----------:|------------:|
| RegularStringBuilder          | 1,056.27 ns |  9.262 ns |  7.734 ns |  1.00 |    0.01 | 0.8507 | 0.0267 |   14248 B |       1.000 |
| NotOptimalCustomStringBuilder | 1,626.28 ns |  6.853 ns |  6.075 ns |  1.54 |    0.01 | 1.2817 | 0.0381 |   21432 B |       1.504 |
| StackAllocBuilder             |    21.35 ns |  0.158 ns |  0.132 ns |  0.02 |    0.00 | 0.0052 |      - |      88 B |       0.006 |
| StackAllocBuilderLoop         | 2,426.32 ns | 22.552 ns | 17.607 ns |  2.30 |    0.02 | 0.4768 |      - |    8000 B |       0.561 |
| PooledBuilder                 |   963.86 ns |  5.435 ns |  4.244 ns |  0.91 |    0.01 | 0.3242 |      - |    5440 B |       0.382 |

// * Hints *
Outliers
  StringBuilderBenchmarks.RegularStringBuilder: Default          -> 2 outliers were removed, 3 outliers were detected (1.04 us, 1.08 us, 1.09 us)
  StringBuilderBenchmarks.NotOptimalCustomStringBuilder: Default -> 1 outlier  was  removed, 2 outliers were detected (1.61 us, 1.70 us)
  StringBuilderBenchmarks.StackAllocBuilder: Default             -> 2 outliers were removed, 3 outliers were detected (23.23 ns, 23.92 ns, 24.13 ns)
  StringBuilderBenchmarks.StackAllocBuilderLoop: Default         -> 3 outliers were removed (2.58 us..2.70 us)
  StringBuilderBenchmarks.PooledBuilder: Default                 -> 3 outliers were removed (984.37 ns..1.03 us)

// * Legends *
  Mean        : Arithmetic mean of all measurements
  Error       : Half of 99.9% confidence interval
  StdDev      : Standard deviation of all measurements
  Ratio       : Mean of the ratio distribution ([Current]/[Baseline])
  RatioSD     : Standard deviation of the ratio distribution ([Current]/[Baseline])
  Gen0        : GC Generation 0 collects per 1000 operations
  Gen1        : GC Generation 1 collects per 1000 operations
  Allocated   : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  Alloc Ratio : Allocated memory ratio distribution ([Current]/[Baseline])
  1 ns        : 1 Nanosecond (0.000000001 sec)
