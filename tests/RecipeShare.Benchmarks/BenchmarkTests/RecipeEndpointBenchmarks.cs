using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.AspNetCore.Mvc.Testing;
using RecipeShare.Factories;

namespace RecipeShare.Benchmarks.BenchmarkTests
{
    [SimpleJob(launchCount: 1, warmupCount: 2)]
    [MemoryDiagnoser]
    public class RecipeEndpointBenchmarks
    {
        private HttpClient _client;
        private CustomWebAppFactory _factory;
        private const int RequestCount = 500;

        [GlobalSetup]
        public void Setup()
        {
            _factory = new CustomWebAppFactory();
            _client = _factory.CreateClient();
        }

        [Benchmark(Description = "500 Sequential Requests")]
        public async Task GetAllRecipesSequential()
        {
            for (int i = 0; i < RequestCount; i++)
            {
                var response = await _client.GetAsync("api/recipes");
                response.EnsureSuccessStatusCode();
            }
        }

        [Benchmark(Description = "500 Requests in Batches of 50")]
        public async Task GetAllRecipesBatched()
        {
            const int batchSize = 50;
            for (int i = 0; i < RequestCount; i += batchSize)
            {
                var tasks = new List<Task<HttpResponseMessage>>();
                for (int j = 0; j < batchSize && (i + j) < RequestCount; j++)
                {
                    tasks.Add(_client.GetAsync("api/recipes"));
                }
                var responses = await Task.WhenAll(tasks);
                foreach (var response in responses)
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        [Benchmark(Description = "500 Requests All Concurrent")]
        public async Task GetAllRecipesConcurrent()
        {
            var tasks = new List<Task<HttpResponseMessage>>();
            for (int i = 0; i < RequestCount; i++)
            {
                tasks.Add(_client.GetAsync("api/recipes"));
            }
            var responses = await Task.WhenAll(tasks);
            foreach (var response in responses)
            {
                response.EnsureSuccessStatusCode();
            }
        }

        [Benchmark(Description = "Single Request Baseline")]
        public async Task GetAllRecipesSingle()
        {
            var response = await _client.GetAsync("api/recipes");
            response.EnsureSuccessStatusCode();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<RecipeEndpointBenchmarks>();
        }
    }
}