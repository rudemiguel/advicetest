using System;
using System.Linq;
using advicetest.Infrastructure;
using advicetest.Infrastructure.Interceptors.Setup;
using advicetest.Services;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleBenchmark
{
    public class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<AdviceBenchmark>();
        }

        public class AdviceBenchmark
        {
            private static readonly IServiceProvider ServiceProvider = GetAdviceServiceProvider();

            private static string _lastResult;

            [Benchmark(Baseline = true)]
            public void WithoutAdvices()
            {
                var serviceWithoutAdvices = ServiceProvider.GetRequiredService<ISimpleWeatherForecastService>();
                var res = serviceWithoutAdvices.GetForecast(10).GetAwaiter().GetResult();
                _lastResult = res.First().Summary;
            }

            [Benchmark]
            public void WithAdvices()
            {
                var serviceWithAdvices = ServiceProvider.GetRequiredService<ILoadTestingWeatherForecastService>();
                var res = serviceWithAdvices.GetForecast(10).GetAwaiter().GetResult();
                _lastResult = res.First().Summary;
            }
        }

        private static IServiceProvider GetAdviceServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddTransient<IWeatherForecastService, WeatherForecastService>()
                .AddTransient<ISimpleWeatherForecastService, SimpleWeatherForecastService>()
                .AddTransient<ILoadTestingWeatherForecastService, LoadTestingWeatherForecastService>()
                .AddSingleton<LoadTestingMockJob>()
                .AddAdvicesOf<IApplicationService>()
                .AddAdvicesOf<ILoadTestingService>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}