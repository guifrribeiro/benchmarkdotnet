using BenchmarkDotNet.Attributes;
using Npgsql;

namespace DatabaseUsageDotNet
{
    [RankColumn]
    [MemoryDiagnoser]
    public class BenchmarkTest
    {
        private const string ConnectionString = "Host=localhost;Username=stefanini;Password=stefanini;Database=stefanini";

        [Benchmark]
        public void InefficientDatabaseQuery()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM customers WHERE city = 'New York'";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customerName = reader["name"].ToString().ToUpper();
                        }
                    }
                }
            }
        }

        [Benchmark]
        public void EfficientDatabaseQuery()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                int pageSize = 100;
                int pageNumber = 1;
                
                string query = @"
                    SELECT * FROM 
                    (SELECT ROW_NUMBER() OVER (ORDER BY customerid) AS RowNum, * 
                    FROM customers WHERE city = 'New York') AS RowConstrainedResult
                    WHERE RowNum >= @RowStart AND RowNum < @RowEnd
                    ORDER BY RowNum";

                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RowStart", (pageNumber - 1) * pageSize + 1);
                    command.Parameters.AddWithValue("@RowEnd", pageNumber * pageSize);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customerName = reader["name"].ToString().ToUpper();
                        }
                    }
                }
            }
        }
    }
}