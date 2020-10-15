using System;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;

namespace ConsoleLoadTesting
{
    public class Program
    {
        private static string Host => "http://localhost:5000/WeatherForecast/";
        private static int WarmUpDurationInSeconds => 1;
        private static int DurationInSeconds => 10;
        private static int RequestInOneSecond => 200;
        
        private static void Main(string[] args)
        {
            var simpleStep = HttpStep.Create("simple_step",
                context => Http.CreateRequest("GET", $"{Host}Simple"));
            
            var aspectStep = HttpStep.Create("aspect_step",
                context => Http.CreateRequest("GET", $"{Host}LoadTesting"));
            
            var scenario = ScenarioBuilder.CreateScenario("scenario", simpleStep, aspectStep)
                .WithWarmUpDuration(TimeSpan.FromSeconds(WarmUpDurationInSeconds))
                .WithLoadSimulations(Simulation.InjectPerSec(RequestInOneSecond, TimeSpan.FromSeconds(DurationInSeconds)));
            
            NBomberRunner
                .RegisterScenarios(scenario)
                .Run();
        }
    }
}