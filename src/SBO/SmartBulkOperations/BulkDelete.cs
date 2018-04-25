using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SmartBulkOperations
{
    public class BulkDelete : IBulkDelete
    {
        public string BulkDeleteCmd<T>(List<T> items, Expression<Func<T, int>> entityPropertyName) where T : class
        {
            string targetPropertyName = BulkHelper.GetPropertyName(entityPropertyName);

            var ids = items.Select(i => 
                BulkHelper.PropertiesToSql(i)
                    .Where(w => w.Name == targetPropertyName)
                    .Select(s => s.GetValue(i))
                    .FirstOrDefault()
                )
                .ToList()
                .Where(w => w != null)
                .ToList();

            string cmd = default(string);

            if (ids.Count > 0)
            {
                cmd = $"DELETE FROM {typeof(T).Name} WHERE {targetPropertyName} IN ({String.Join(",", ids)});";
            }

            return cmd;
        }
    }
}
