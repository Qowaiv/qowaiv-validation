```

BenchmarkDotNet v0.16.0-preview.2, Windows 11 (10.0.26200.9448/25H2/2025Update/HudsonValley2)
12th Gen Intel Core i7-12700H 2.69GHz, 1 CPU, 20 logical and 14 physical cores
Memory: 31,68 GB Total, 3,21 GB Available
.NET SDK 11.0.100-rc.2.26504.105
  [Host]     : .NET 10.0.13 (10.0.13, 10.0.1326.50504), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.13 (10.0.13, 10.0.1326.50504), X64 RyuJIT x86-64-v3


```
| Method        | Bericht | Mean       | Error     | StdDev    | Ratio | RatioSD |
|-------------- |-------- |-----------:|----------:|----------:|------:|--------:|
| **Qowaiv**        | **AX**      | **103.050 μs** | **1.9554 μs** | **1.8290 μs** |  **0.51** |    **0.02** |
| MiniValidator | AX      | 200.951 μs | 3.7473 μs | 6.0511 μs |  1.00 |    0.00 |
|               |         |            |           |           |       |         |
| **Qowaiv**        | **MX**      |  **19.256 μs** | **0.2784 μs** | **0.2325 μs** |  **0.70** |    **0.01** |
| MiniValidator | MX      |  27.559 μs | 0.4585 μs | 0.4064 μs |  1.00 |    0.00 |
|               |         |            |           |           |       |         |
| **Qowaiv**        | **SX**      |   **5.144 μs** | **0.0996 μs** | **0.1491 μs** |  **0.17** |    **0.01** |
| MiniValidator | SX      |  30.301 μs | 0.3876 μs | 0.3237 μs |  1.00 |    0.00 |
