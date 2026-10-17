```

BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26200.9448/25H2/2025Update/HudsonValley2)
12th Gen Intel Core i7-12700H 2.69GHz, 1 CPU, 20 logical and 14 physical cores
Memory: 31,68 GB Total, 3,75 GB Available
.NET SDK 11.0.100-rc.2.26504.105
  [Host]     : .NET 10.0.13 (10.0.13, 10.0.1326.50504), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.13 (10.0.13, 10.0.1326.50504), X64 RyuJIT x86-64-v3


```
| Method        | Bericht | Mean       | Error      | StdDev     | Median     | Ratio | RatioSD |
|-------------- |-------- |-----------:|-----------:|-----------:|-----------:|------:|--------:|
| **Qowaiv**        | **AX**      |  **94.498 μs** | **14.5780 μs** | **42.9836 μs** |  **65.537 μs** |  **0.54** |    **0.31** |
| MiniValidator | AX      | 189.994 μs | 14.9466 μs | 43.1243 μs | 198.901 μs |  1.00 |    0.00 |
|               |         |            |            |            |            |       |         |
| **Qowaiv**        | **MX**      |  **22.359 μs** |  **0.4346 μs** |  **0.6637 μs** |  **22.331 μs** |  **0.83** |    **0.03** |
| MiniValidator | MX      |  27.089 μs |  0.5180 μs |  0.5966 μs |  27.108 μs |  1.00 |    0.00 |
|               |         |            |            |            |            |       |         |
| **Qowaiv**        | **SX**      |   **5.793 μs** |  **0.0814 μs** |  **0.0679 μs** |   **5.787 μs** |  **0.19** |    **0.00** |
| MiniValidator | SX      |  31.313 μs |  0.6227 μs |  0.7647 μs |  30.988 μs |  1.00 |    0.00 |
