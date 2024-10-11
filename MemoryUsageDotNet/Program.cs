using BenchmarkDotNet.Running;
using MemoryUsageDotNet;

public class Program
{
  public static void Main(string[] args)
  {
    var summary = BenchmarkRunner.Run<BenchmarkTest>();
  }
}