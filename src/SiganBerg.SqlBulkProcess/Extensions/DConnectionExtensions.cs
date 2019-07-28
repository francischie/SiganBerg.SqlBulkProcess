using System.Collections.Generic;
using System.Data;
using Dapper.FastCrud;

namespace SiganBerg.SqlBulkProcess.Extensions
{
    public static class DConnectionExtensions
    {
        public static void BulkInsert<T>(this IDbConnection connection, IEnumerable<T> entities) where T : class
        {
            var tracker = new CommandTracker(connection);
            var newConnection = new DbConnection(tracker);
            foreach (var entity in entities)
                newConnection.Insert(entity);
            tracker.ExecuteAll();
        }
        
        public static void BulkUpdate<T>(this IDbConnection connection, IEnumerable<T> entities) where T : class
        {
            var tracker = new CommandTracker(connection);
            var newConnection = new DbConnection(tracker);
            foreach (var entity in entities)
                newConnection.Update(entity);
            tracker.ExecuteAll();
        }
    }
}