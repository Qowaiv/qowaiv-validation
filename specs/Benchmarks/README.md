# Qowaiv validation benchmarks

## Allowed Values attribute

| Method                | Mean       | Ratio |
|-----------------------|-----------:|------:|
| AllowedValues         | 321.043 us | 1.000 |
| Allowed&lt;T&gt;      |   2.775 us | 0.009 |


## In Range attribute

| Method                | Mean       | Ratio |
|-----------------------|-----------:|------:|
| ComponentModel Range  |  40.587 us |  1.00 |
| InRange&lt;Amount&gt; |   2.678 us |  0.07 |

## Data Annotations Validation

| Method        | Runtime   | Count | Mean         | Ratio | Gen0      | Allocated   | Alloc Ratio |
|-------------- |---------- |------ |-------------:|------:|----------:|------------:|------------:|
| Qowaiv        | .NET 10.0 | 10    |    244.51 us |  7.60 |         - |    11.66 KB |        0.62 |
| Fluent        | .NET 10.0 | 10    |    223.36 us |  6.94 |         - |    22.77 KB |        1.22 |
| MiniValidator | .NET 10.0 | 10    |     33.14 us |  1.00 |         - |    18.71 KB |        1.00 |
|               |           |       |              |       |           |             |             |
| Qowaiv        | .NET 11.0 | 10    |    200.56 us |  6.98 |         - |    11.56 KB |        0.62 |
| Fluent        | .NET 11.0 | 10    |    217.78 us |  7.57 |         - |    22.77 KB |        1.22 |
| MiniValidator | .NET 11.0 | 10    |     29.89 us |  1.00 |         - |    18.71 KB |        1.00 |
|               |           |       |              |       |           |             |             |
| Qowaiv        | .NET 10.0 | 100   |    178.18 us |  1.46 |         - |    66.63 KB |        0.40 |
| Fluent        | .NET 10.0 | 100   |    234.41 us |  1.92 |         - |    187.3 KB |        1.13 |
| MiniValidator | .NET 10.0 | 100   |    123.19 us |  1.00 |         - |   165.73 KB |        1.00 |
|               |           |       |              |       |           |             |             |
| Qowaiv        | .NET 11.0 | 100   |    188.08 us |  1.65 |         - |    65.84 KB |        0.40 |
| Fluent        | .NET 11.0 | 100   |    264.73 us |  2.33 |         - |    187.3 KB |        1.13 |
| MiniValidator | .NET 11.0 | 100   |    114.57 us |  1.00 |         - |   165.73 KB |        1.00 |
|               |           |       |              |       |           |             |             |
| Qowaiv        | .NET 10.0 | 1000  |    356.08 us |  0.34 |         - |   616.02 KB |        0.31 |
| Fluent        | .NET 10.0 | 1000  |    879.70 us |  0.84 |         - |  1854.48 KB |        0.92 |
| MiniValidator | .NET 10.0 | 1000  |  1,044.27 us |  1.00 |         - |  2016.44 KB |        1.00 |
|               |           |       |              |       |           |             |             |
| Qowaiv        | .NET 11.0 | 1000  |    327.39 us |  0.34 |         - |    608.2 KB |        0.37 |
| Fluent        | .NET 11.0 | 1000  |    774.78 us |  0.80 |         - |  1854.48 KB |        1.13 |
| MiniValidator | .NET 11.0 | 1000  |    970.00 us |  1.00 |         - |  1641.25 KB |        1.00 |
|               |           |       |              |       |           |             |             |
| Qowaiv        | .NET 10.0 | 10000 |  2,205.65 us |  0.18 |         - |  6053.42 KB |        0.30 |
| Fluent        | .NET 10.0 | 10000 |  7,630.83 us |  0.63 | 1000.0000 | 18870.13 KB |        0.95 |
| MiniValidator | .NET 10.0 | 10000 | 12,104.43 us |  1.00 | 1000.0000 | 19962.25 KB |        1.00 |
|               |           |       |              |       |           |             |             |
| Qowaiv        | .NET 11.0 | 10000 |  2,271.69 us |  0.19 |         - |  5975.28 KB |        0.30 |
| Fluent        | .NET 11.0 | 10000 |  7,654.26 us |  0.65 | 1000.0000 | 18870.13 KB |        0.95 |
| MiniValidator | .NET 11.0 | 10000 | 11,790.51 us |  1.00 | 1000.0000 |    19806 KB |        1.00 |
