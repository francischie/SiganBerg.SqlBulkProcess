using System.Data;

namespace SiganBerg.SqlBulkProcess
{
    internal interface ICommandTracker
    {
        void Record(string commandText, IDataParameterCollection parameters);
        void ExecuteAll();
        string GetRecordedSql();
    }
}