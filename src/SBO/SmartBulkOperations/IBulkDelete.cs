using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SmartBulkOperations
{
    public interface IBulkDelete
    {
        string BulkDeleteCmd<T>(List<T> items, Expression<Func<T, int>> entityPropertyName) where T : class;
    }
}
