using System;
using System.Data;
using System.Linq;
using System.Text;

namespace SiganBerg.SqlBulkProcess
{
    internal interface IParameterValueConverter
    {
        string Convert(string commandText, IDataParameterCollection parameters);
    }
    internal class ParameterValueConverter : IParameterValueConverter
    {
        private void Convert(StringBuilder sql, IDbDataParameter parameter)
        {
            string result;
            var p = (DataParameter) parameter;

            if (p.Value is DBNull)
                result = "null";
            else
                switch (p.DbType)
                {
                    case DbType.Binary:
                    case DbType.Guid:
                        result = "'" + p.Value.ToString().Replace("'", "''") + "'";
                        break;
                    case DbType.StringFixedLength:
                    case DbType.AnsiString:
                    case DbType.AnsiStringFixedLength:
                    case DbType.String:
                        result = "N'" + p.Value.ToString().Replace("'", "''") + "'";
                        break;
                    case DbType.Time:
                    case DbType.DateTimeOffset:
                    case DbType.Date:
                    case DbType.DateTime:
                    case DbType.DateTime2:
                        var value = (DateTime) p.Value;
                        result = value == DateTime.MinValue ? "null" : $"'{value}'";
                        break;
                    case DbType.Boolean:
                        result = (p.Value.ToBooleanOrDefault(false)) ? "1" : "0";
                        break;
                    default:
                        result = p.Value.ToString().Replace("'", "''");
                        break;
                }
            sql.Replace("@" + p.ParameterName, result);
        }

        public string Convert(string commandText, IDataParameterCollection parameters)
        {
            var sqlParams = parameters.Cast<DataParameter>()
                .OrderByDescending(x => x.ParameterName.Length);

            var sql = new StringBuilder(commandText);
            foreach (var p in sqlParams)
                Convert(sql, p);

            return sql.ToString();
        }
    }
}