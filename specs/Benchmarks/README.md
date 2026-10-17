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
| Method        | Runtime   | Count | Mean         | Error      | StdDev     | Median       | Ratio | RatioSD |
|-------------- |---------- |------ |-------------:|-----------:|-----------:|-------------:|------:|--------:|
| Qowaiv        | .NET 10.0 | 10    |     19.62 us |   1.830 us |   5.367 us |     19.70 us |  0.54 |    0.17 |
| Fluent        | .NET 10.0 | 10    |    197.70 us |   7.859 us |  22.800 us |    191.40 us |  5.44 |    0.98 |
| MiniValidator | .NET 10.0 | 10    |     36.99 us |   1.704 us |   4.889 us |     36.90 us |  1.00 |    0.00 |
|               |           |       |              |            |            |              |       |         |
| Qowaiv        | .NET 11.0 | 10    |     11.71 us |   0.691 us |   1.961 us |     11.20 us |  0.49 |    0.12 |
| Fluent        | .NET 11.0 | 10    |    171.97 us |   7.064 us |  20.269 us |    169.60 us |  7.20 |    1.46 |
| MiniValidator | .NET 11.0 | 10    |     24.58 us |   1.474 us |   4.276 us |     23.70 us |  1.00 |    0.00 |
|               |           |       |              |            |            |              |       |         |
| Qowaiv        | .NET 10.0 | 100   |     21.83 us |   1.646 us |   4.801 us |     19.85 us |  0.20 |    0.05 |
| Fluent        | .NET 10.0 | 100   |    229.93 us |   6.879 us |  19.956 us |    227.10 us |  2.15 |    0.22 |
| MiniValidator | .NET 10.0 | 100   |    107.39 us |   2.307 us |   6.430 us |    106.50 us |  1.00 |    0.00 |
|               |           |       |              |            |            |              |       |         |
| Qowaiv        | .NET 11.0 | 100   |     19.31 us |   1.081 us |   3.032 us |     18.40 us |  0.18 |    0.03 |
| Fluent        | .NET 11.0 | 100   |    235.60 us |   9.097 us |  26.680 us |    230.40 us |  2.23 |    0.31 |
| MiniValidator | .NET 11.0 | 100   |    106.42 us |   2.934 us |   8.513 us |    107.80 us |  1.00 |    0.00 |
|               |           |       |              |            |            |              |       |         |
| Qowaiv        | .NET 10.0 | 1000  |    108.38 us |   2.709 us |   7.729 us |    107.25 us |  0.12 |    0.01 |
| Fluent        | .NET 10.0 | 1000  |    837.31 us |  17.346 us |  49.489 us |    833.90 us |  0.92 |    0.07 |
| MiniValidator | .NET 10.0 | 1000  |    911.76 us |  18.129 us |  47.758 us |    916.00 us |  1.00 |    0.00 |
|               |           |       |              |            |            |              |       |         |
| Qowaiv        | .NET 11.0 | 1000  |    104.83 us |   3.008 us |   8.630 us |    103.50 us |  0.12 |    0.01 |
| Fluent        | .NET 11.0 | 1000  |    798.31 us |  17.697 us |  51.623 us |    795.50 us |  0.93 |    0.07 |
| MiniValidator | .NET 11.0 | 1000  |    863.58 us |  17.202 us |  39.524 us |    865.60 us |  1.00 |    0.00 |
|               |           |       |              |            |            |              |       |         |
| Qowaiv        | .NET 10.0 | 10000 |  1,093.27 us |  27.052 us |  78.911 us |  1,097.55 us |  0.11 |    0.01 |
| Fluent        | .NET 10.0 | 10000 |  7,575.11 us | 150.158 us | 310.102 us |  7,603.10 us |  0.75 |    0.05 |
| MiniValidator | .NET 10.0 | 10000 | 10,055.55 us | 199.433 us | 450.154 us | 10,071.90 us |  1.00 |    0.00 |
|               |           |       |              |            |            |              |       |         |
| Qowaiv        | .NET 11.0 | 10000 |  1,052.07 us |  21.009 us |  53.091 us |  1,059.00 us |  0.10 |    0.01 |
| Fluent        | .NET 11.0 | 10000 |  7,494.37 us | 149.382 us | 308.499 us |  7,531.85 us |  0.74 |    0.05 |
| MiniValidator | .NET 11.0 | 10000 | 10,172.97 us | 205.220 us | 595.379 us | 10,051.70 us |  1.00 |    0.00 |
