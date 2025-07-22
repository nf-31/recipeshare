```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.5 (24F74) [Darwin 24.5.0]
Apple M4, 1 CPU, 10 logical and 10 physical cores
.NET SDK 8.0.411
  [Host]     : .NET 8.0.17 (8.0.1725.26602), Arm64 RyuJIT AdvSIMD
  Job-RISBLR : .NET 8.0.17 (8.0.1725.26602), Arm64 RyuJIT AdvSIMD

LaunchCount=1  WarmupCount=2  

```
| Method                          | Mean         | Error      | StdDev     | Median       | Gen0       | Gen1      | Gen2      | Allocated    |
|-------------------------------- |-------------:|-----------:|-----------:|-------------:|-----------:|----------:|----------:|-------------:|
| &#39;500 Sequential Requests&#39;       | 1,196.049 ms | 19.1574 ms | 16.9826 ms | 1,195.385 ms | 13000.0000 | 1000.0000 |         - | 107422.84 KB |
| &#39;500 Requests in Batches of 50&#39; |   305.136 ms | 12.5742 ms | 35.8748 ms |   292.528 ms | 15000.0000 | 8000.0000 | 3000.0000 | 113754.14 KB |
| &#39;500 Requests All Concurrent&#39;   |   322.173 ms |  9.6325 ms | 27.1686 ms |   313.922 ms | 14000.0000 | 4000.0000 | 1000.0000 | 108807.39 KB |
| &#39;Single Request Baseline&#39;       |     2.425 ms |  0.0458 ms |  0.0871 ms |     2.410 ms |    23.4375 |    3.9063 |         - |    216.82 KB |
