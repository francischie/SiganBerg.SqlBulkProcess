using System;
using System.Data;

namespace SiganBerg.SqlBulkProcess
{
    internal class DbConnection : IDbConnection
    {
        public string ConnectionString { get; set; }
        public int ConnectionTimeout => 0;
        public string Database => null;
        public ConnectionState State => ConnectionState.Closed;

        private readonly ICommandTracker _commandRecorder;

        public DbConnection(ICommandTracker commandRecorder)
        {
            _commandRecorder = commandRecorder;
        }

        public IDbCommand CreateCommand()
        {
            return new DbCommand(_commandRecorder) { Connection = this };
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            
        }
    }
}