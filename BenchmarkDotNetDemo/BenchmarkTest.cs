using System.Text;
using BenchmarkDotNet.Attributes;

namespace BenchmarkDotNetDemo
{
    [MemoryDiagnoser]
    public class BenchmarkTest
    {
        private const int Iterations = 1000;
        private List<string> dataList;

        public BenchmarkTest()
        {
            dataList = new List<string>();
            for (int i = 0; i < Iterations; i++)
            {
                dataList.Add($"Item {i}");
            }
        }

        [Benchmark]
        public string InefficientStringConcatenation()
        {
            string result = "";

            foreach (var item in dataList)
            {
                result += item + ", ";
            }

            return result;
        }

        [Benchmark]
        public string EfficientStringConcatenation()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in dataList)
            {
                stringBuilder.Append(item).Append(", ");
            }

            return stringBuilder.ToString();
        }
    }
}