using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SiganBerg.SqlBulkProcess
{
    internal class CommandTracker : ICommandTracker
    {
        private readonly IDbConnection _conn;
        private readonly List<string> _recordedCommands;
        private int BatchExecutionSizeInBytes { get; set; }

        public CommandTracker(IDbConnection conn)
        {
            _conn = conn;
            _recordedCommands = new List<string>();
            BatchExecutionSizeInBytes = 1000000;
        }

        public void ExecuteAll()
        {
            var sb = new StringBuilder();
            if (_conn.State != ConnectionState.Open)
                _conn.Open();
            foreach (var recordedCommand in _recordedCommands)
            {
                sb.AppendLine(recordedCommand);

                if (sb.Length <= BatchExecutionSizeInBytes) continue;
                
                ExecuteCommand(sb);
                sb.Clear();
            }

            ExecuteCommand(sb);
            
            _conn.Close();
        }

        private string BuildSql(string commandText, IDataParameterCollection parameters)
        {
            IParameterValueConverter converter = new ParameterValueConverter();
            var singleSql = converter.Convert(commandText, parameters);
            return singleSql;
        }

        public string GetRecordedSql()
        {
            var sb = new StringBuilder();
            foreach (var recordedCommand in _recordedCommands)
                sb.AppendLine(recordedCommand);

            return sb.ToString();
        }

        private void ExecuteCommand(StringBuilder sb)
        {
            var command = _conn.CreateCommand();
            command.CommandText = sb.ToString();

            if (command.CommandText == "") return;
            command.ExecuteNonQuery(); 
        }

        public void Record(string commandText, IDataParameterCollection parameters)
        {
            var sql = BuildSql(commandText, parameters);
            _recordedCommands.Add(sql);
        }
    }
}
