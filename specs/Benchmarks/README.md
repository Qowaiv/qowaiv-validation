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
| Method        | Runtime   | Count | Mean         | Error      | StdDev     | Median       | Ratio |
|-------------- |---------- |------:|-------------:|-----------:|-----------:|-------------:|------:|
| Qowaiv        | .NET 10.0 | 10    |     13.01 us |   0.901 us |   2.628 us |     12.90 us |  0.46 |
| Fluent        | .NET 10.0 | 10    |    213.41 us |   7.675 us |  21.395 us |    206.85 us |  7.57 |
| MiniValidator | .NET 10.0 | 10    |     28.82 us |   1.543 us |   4.452 us |     28.20 us |  1.00 |
|               |           |       |              |            |            |              |       |
| Qowaiv        | .NET 11.0 | 10    |     10.98 us |   0.747 us |   2.095 us |     10.80 us |  0.40 |
| Fluent        | .NET 11.0 | 10    |    182.55 us |   5.339 us |  14.884 us |    180.75 us |  6.70 |
| MiniValidator | .NET 11.0 | 10    |     27.71 us |   1.342 us |   3.806 us |     27.30 us |  1.00 |
|               |           |       |              |            |            |              |       |
| Qowaiv        | .NET 10.0 | 100   |     23.42 us |   1.710 us |   4.987 us |     21.65 us |  0.19 |
| Fluent        | .NET 10.0 | 100   |    257.52 us |   5.890 us |  16.517 us |    255.90 us |  2.12 |
| MiniValidator | .NET 10.0 | 100   |    122.16 us |   3.584 us |  10.454 us |    120.80 us |  1.00 |
|               |           |       |              |            |            |              |       |
| Qowaiv        | .NET 11.0 | 100   |     21.14 us |   1.174 us |   3.329 us |     20.60 us |  0.18 |
| Fluent        | .NET 11.0 | 100   |    246.57 us |   8.114 us |  22.885 us |    243.30 us |  2.13 |
| MiniValidator | .NET 11.0 | 100   |    116.33 us |   2.326 us |   6.208 us |    115.90 us |  1.00 |
|               |           |       |              |            |            |              |       |
| Qowaiv        | .NET 10.0 | 1000  |    116.57 us |   2.330 us |   6.610 us |    114.80 us |  0.10 |
| Fluent        | .NET 10.0 | 1000  |    928.54 us |  19.736 us |  56.307 us |    916.20 us |  0.81 |
| MiniValidator | .NET 10.0 | 1000  |  1,145.87 us |  22.779 us |  24.373 us |  1,147.80 us |  1.00 |
|               |           |       |              |            |            |              |       |
| Qowaiv        | .NET 11.0 | 1000  |    151.26 us |   3.008 us |   7.766 us |    151.10 us |  0.15 |
| Fluent        | .NET 11.0 | 1000  |    891.27 us |  22.482 us |  65.581 us |    892.05 us |  0.88 |
| MiniValidator | .NET 11.0 | 1000  |  1,012.10 us |  20.184 us |  51.739 us |  1,013.20 us |  1.00 |
|               |           |       |              |            |            |              |       |
| Qowaiv        | .NET 10.0 | 10000 |  1,081.49 us |  21.534 us |  56.730 us |  1,067.80 us |  0.09 |
| Fluent        | .NET 10.0 | 10000 |  8,608.33 us | 169.604 us | 375.830 us |  8,606.60 us |  0.74 |
| MiniValidator | .NET 10.0 | 10000 | 11,671.57 us | 232.114 us | 489.607 us | 11,552.05 us |  1.00 |
|               |           |       |              |            |            |              |       |
| Qowaiv        | .NET 11.0 | 10000 |  1,204.25 us |  24.004 us |  51.671 us |  1,201.45 us |  0.10 |
| Fluent        | .NET 11.0 | 10000 |  9,657.42 us | 186.497 us | 199.550 us |  9,687.95 us |  0.79 |
| MiniValidator | .NET 11.0 | 10000 | 12,320.01 us | 244.262 us | 509.865 us | 12,347.30 us |  1.00 |
