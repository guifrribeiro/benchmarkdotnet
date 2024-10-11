using BenchmarkDotNet.Running;
using DatabaseUsageDotNet;

public class Program
{
    public static void Main(string[] args)
    {
        // Executa os benchmarks definidos na classe BenchmarkTest
        var summary = BenchmarkRunner.Run<BenchmarkTest>();
    }
}