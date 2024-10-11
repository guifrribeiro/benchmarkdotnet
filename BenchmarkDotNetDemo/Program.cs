using BenchmarkDotNet.Running;
using BenchmarkDotNetDemo;

public class Program
{
  public static void Main(string[] args)
  {
    var summary = BenchmarkRunner.Run<BenchmarkTest>();
  }
}