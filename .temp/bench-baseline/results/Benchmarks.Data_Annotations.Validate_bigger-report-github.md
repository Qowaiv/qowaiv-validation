```

BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26200.9448/25H2/2025Update/HudsonValley2)
12th Gen Intel Core i7-12700H 2.69GHz, 1 CPU, 20 logical and 14 physical cores
Memory: 31,68 GB Total, 8,27 GB Available
.NET SDK 11.0.100-rc.2.26504.105
  [Host]     : .NET 10.0.13 (10.0.13, 10.0.1326.50504), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.13 (10.0.13, 10.0.1326.50504), X64 RyuJIT x86-64-v3


```
| Method        | Bericht | Mean       | Error     | StdDev    | Ratio | RatioSD |
|-------------- |-------- |-----------:|----------:|----------:|------:|--------:|
| **Qowaiv**        | **AX**      |  **72.980 μs** | **1.4263 μs** | **2.4980 μs** |  **0.65** |    **0.03** |
| MiniValidator | AX      | 112.070 μs | 2.1994 μs | 2.6182 μs |  1.00 |    0.00 |
|               |         |            |           |           |       |         |
| **Qowaiv**        | **MX**      |   **9.302 μs** | **0.1849 μs** | **0.2056 μs** |  **0.75** |    **0.03** |
| MiniValidator | MX      |  12.467 μs | 0.2438 μs | 0.3497 μs |  1.00 |    0.00 |
|               |         |            |           |           |       |         |
| **Qowaiv**        | **SX**      |   **2.758 μs** | **0.0519 μs** | **0.0692 μs** |  **0.13** |    **0.01** |
| MiniValidator | SX      |  21.156 μs | 0.4224 μs | 0.6450 μs |  1.00 |    0.00 |
