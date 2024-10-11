using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace MemoryUsageDotNet
{
    [RankColumn]
    [MemoryDiagnoser]
    public class BenchmarkTest
    {
        private const int TotalRecords = 1000000;
        private List<string> customerData;

        public BenchmarkTest()
        {
            customerData = new List<string>();
            
            for (int i = 0; i < TotalRecords; i++)
            {
                customerData.Add($"Customer_{i}");
            }
        }

        [Benchmark]
        public void InefficientMemoryUsage()
        {
            List<string> processedCustomers = new List<string>();

            foreach (var customer in customerData)
            {
                processedCustomers.Add(customer.ToUpper());
            }

            Console.WriteLine($"Total de clientes processados: {processedCustomers.Count}");
        }

        [Benchmark]
        public void EfficientMemoryUsage()
        {
            int pageSize = 10000;
            for (int i = 0; i < TotalRecords; i += pageSize)
            {
                var paginatedData = customerData.Skip(i).Take(pageSize).ToList();
                var processedData = paginatedData.Select(customer => customer.ToUpper()).ToList();

                processedData.Clear();
            }

            Console.WriteLine($"Processamento paginado concluído.");
        }
    }
}