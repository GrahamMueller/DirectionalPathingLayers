using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using NUnit.Framework;


namespace DirectionalPathingLayers.Tests.Benchmarks
{
    class Benchmark_runner
    {

#if RELEASE
        [Test]
        public void ProfilePaths()
        {
            ManualConfig config = new ManualConfig()
                .WithOptions(ConfigOptions.DisableOptimizationsValidator)  //Disabled due to nunit
                .AddValidator(JitOptimizationsValidator.DontFailOnError) //Disabled due to nunit
                .AddLogger(ConsoleLogger.Default)
                .AddExporter()
                .AddColumnProvider(DefaultColumnProviders.Instance);

            //Enables running all benchmarks at once with a single summary.
            BenchmarkRunner.Run(typeof(Benchmark_MapDirectionalLayer).Assembly,
                            config.With(ConfigOptions.JoinSummary)
                                  .With(ConfigOptions.DisableLogFile));

        }
#endif
    }
}
