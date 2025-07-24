# Introduction

This repository contains the source code for the RecipeShare challenge.


## Setup

### Prerequisites
- .NET Core 8.0 SDK
- Microsoft SQL Server 
- Docker
- IDE with support for .NET
- Bruno API client

### Instructions
To successfully run the project, a connection string to a SQL Server database must be added to the environment variables/appsettings. On first run, the relevant migrations will run and the database will be seeded with mock data. The API can then be tested using the Bruno collection provided.

Note: A client secret will be required to fetch a token from Auth0. The 'Fetch Token' request must be run before other requests can be run.


## Rationale

### Assumptions
Based on the requirements provided in the specification, the following assumptions were made when designing this solution:

- The API should be able to cater for multiple recipes, with the ability to get,  add, modify and delete recipes.
- The API should be performant, scalable, and easy to maintain.
- The API should provide a user with the ability to use filtering.
- The API should be secure.


## Architecture

Please refer to [ARCHITECTURE.md](ARCHITECTURE.md) for all architecture, design decisions, notes, etc.


## Demo Video

The demo video can be found [here.](https://www.loom.com/share/4443b3843df94e88873caef8045259a1?sid=06ec61b6-1626-4397-945b-2092d216f74b)
## Benchmark Testing

Using BenchmarkDotNet, 500 sequential calls to `GET /api/recipes` in Release mode were run. The results are presented below. Some extra tests were included to provide a comparison.

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

