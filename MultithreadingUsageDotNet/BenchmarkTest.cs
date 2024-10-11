using BenchmarkDotNet.Attributes;

namespace MultithreadingUsageDotNet
{
    [RankColumn]
    [ThreadingDiagnoser, MemoryDiagnoser]
    public class BenchmarkTest
    {
        private const int TotalOperations = 10;

        [Benchmark]
        public async Task InefficientSequentialExection()
        {
            for (int i = 0; i < TotalOperations; i++)
            {
                await SimulateApiRequest(i);
            }
        }

        [Benchmark]
        public async Task EfficientSequentialExecution()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < TotalOperations; i++)
            {
                tasks.Add(SimulateApiRequest(i));
            }

            await Task.WhenAll(tasks);
        }

        private async Task SimulateApiRequest(int operationId)
        {
            Console.WriteLine($"Iniciando operação {operationId + 1}...");
            await Task.Delay(1000);
            Console.WriteLine($"Operação {operationId + 1} concluída.");
        }
    }
}