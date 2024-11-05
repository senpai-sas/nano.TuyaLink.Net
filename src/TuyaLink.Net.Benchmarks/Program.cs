using System;
using System.Diagnostics;
using System.Threading;

using nanoFramework.Benchmark;

namespace TuyaLink.Net.Benchmarks
{
    public class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run(typeof(IAssemblyHandler).Assembly);
            Thread.Sleep(Timeout.Infinite);
        }
    }

    public interface IAssemblyHandler { }
}
