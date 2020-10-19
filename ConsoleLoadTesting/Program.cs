using System;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;

namespace ConsoleLoadTesting
{
    public class Program
    {
        private static string Host => "http://localhost:5000/WeatherForecast/";
        private static int WarmUpDurationInSeconds => 1;
        private static int DurationInSeconds => 30;
        private static int RequestInOneSecond => 100;
        
        private static void Main(string[] args)
        {
            var simpleStep = HttpStep.Create("simple_step",
                context => Http.CreateRequest("GET", $"{Host}Simple"));
            
            var simpleScenario = ScenarioBuilder.CreateScenario("simple_scenario", simpleStep)
                .WithWarmUpDuration(TimeSpan.FromSeconds(WarmUpDurationInSeconds))
                .WithLoadSimulations(Simulation.InjectPerSec(RequestInOneSecond, TimeSpan.FromSeconds(DurationInSeconds)));            
            
            var aspectStep = HttpStep.Create("aspect_step",
                context => Http.CreateRequest("GET", $"{Host}LoadTesting"));
            
            var aspectScenario = ScenarioBuilder.CreateScenario("aspect_scenario", aspectStep)
                .WithWarmUpDuration(TimeSpan.FromSeconds(WarmUpDurationInSeconds))
                .WithLoadSimulations(Simulation.InjectPerSec(RequestInOneSecond, TimeSpan.FromSeconds(DurationInSeconds)));
            
            NBomberRunner
                .RegisterScenarios(simpleScenario)
                .Run();
            
            NBomberRunner
                .RegisterScenarios(aspectScenario)
                .Run();            
        }
    }
}