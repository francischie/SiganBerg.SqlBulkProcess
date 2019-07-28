using System;
using System.Data;

namespace SiganBerg.SqlBulkProcess
{
    internal class DbCommand : IDbCommand
    {
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public string CommandText { get; set; }
        public int CommandTimeout { get; set; }
        public CommandType CommandType { get; set; }
        public IDataParameterCollection Parameters { get; private set; }
        public UpdateRowSource UpdatedRowSource { get; set; }

        private readonly ICommandTracker _commandRecorder;

        public DbCommand(ICommandTracker commandRecorder)
        {
            _commandRecorder = commandRecorder;
            Parameters = new ParameterCollection();
        }

        public IDbDataParameter CreateParameter()
        {
            return new DataParameter();
        }

        public int ExecuteNonQuery()
        {
            _commandRecorder.Record(CommandText, Parameters);
            return 0;
        }

        public void Dispose()
        {

        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader()
        {
            throw new NotSupportedException();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new NotSupportedException();
        }

        public object ExecuteScalar()
        {
            throw new NotSupportedException();
        }
    }
}