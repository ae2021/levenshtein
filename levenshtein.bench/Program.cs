using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace levenshtein.bench
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
			var config = ManualConfig
				.Create(DefaultConfig.Instance);
			config.Add(
				Job.ShortRun.With(
					InProcessEmitToolchain.Instance  // Run in-process so you don't need a solution file. If this is part of a solution, replace with CsProjCoreToolchain.NetCoreApp21.
				)
			);
				
            var summary = BenchmarkRunner.Run<Benchmarks>(config);
        }
    }
	
	/// <summary>
	/// The benchmarks to run
	/// </summary>
	public class Benchmarks
	{
		[Benchmark(Baseline = true)]
		public void A()
		{
			throw new NotImplementedException("Replace this with the code to benchmark");
		}
		
		[Benchmark]
		public void B()
		{
			throw new NotImplementedException("Replace this with the code to benchmark");
		}
	}
}
