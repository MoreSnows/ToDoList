using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Polly;

namespace ToDoList.Data
{
    public class Context : IDisposable
    {
        private readonly string _connectionString;
        public IDbConnection Connection { get; private set; }
        public IDbTransaction Transaction;

        public Context(IConfiguration configuration, SqlConnection conn)
        {
            _connectionString = configuration.GetConnectionString("Default");
            Connection = conn;
            StartConnection();
        }

        public void BeginTransaction()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();

            StartConnection();
            Transaction = Connection.BeginTransaction();
        }

        public void Dispose()
        {
            Transaction?.Dispose();
            Transaction = null;
            Connection?.Close();
        }

        private bool StartConnection()
        {
            var rand = new Random();

            var result = Policy
                .Handle<SqlException>()
                .WaitAndRetry(2, (i) => TimeSpan.FromMilliseconds(i * rand.Next(1, 10) * 10))
                .ExecuteAndCapture(() =>
                {
                    bool connected;
                    int count = 0;
                    do
                    {
                        Connection.Open();
                        connected = Connection.State == ConnectionState.Open;
                        Thread.Sleep(10);
                        count++;
                    } while (!connected && count < (60 * 100));

                    return connected;
                });

            if (result.Outcome == OutcomeType.Failure)
                throw result.FinalException;

            return result.Result;
        }
    }
}
