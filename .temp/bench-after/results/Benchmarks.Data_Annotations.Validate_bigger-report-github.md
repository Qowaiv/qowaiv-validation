```

BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26200.9448/25H2/2025Update/HudsonValley2)
12th Gen Intel Core i7-12700H 2.69GHz, 1 CPU, 20 logical and 14 physical cores
Memory: 31,68 GB Total, 5,63 GB Available
.NET SDK 11.0.100-rc.2.26504.105
  [Host]     : .NET 10.0.13 (10.0.13, 10.0.1326.50504), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.13 (10.0.13, 10.0.1326.50504), X64 RyuJIT x86-64-v3


```
| Method        | Bericht | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD |
|-------------- |-------- |----------:|----------:|----------:|----------:|------:|--------:|
| **Qowaiv**        | **AX**      | **59.657 μs** | **1.0378 μs** | **1.0657 μs** | **59.414 μs** |  **0.62** |    **0.01** |
| MiniValidator | AX      | 96.877 μs | 1.4794 μs | 1.3114 μs | 96.936 μs |  1.00 |    0.00 |
|               |         |           |           |           |           |       |         |
| **Qowaiv**        | **MX**      |  **8.892 μs** | **0.1770 μs** | **0.3324 μs** |  **8.793 μs** |  **0.77** |    **0.03** |
| MiniValidator | MX      | 11.554 μs | 0.1786 μs | 0.1671 μs | 11.604 μs |  1.00 |    0.00 |
|               |         |           |           |           |           |       |         |
| **Qowaiv**        | **SX**      |  **2.536 μs** | **0.0503 μs** | **0.0783 μs** |  **2.527 μs** |  **0.09** |    **0.02** |
| MiniValidator | SX      | 27.751 μs | 1.2934 μs | 3.7729 μs | 29.422 μs |  1.00 |    0.00 |
