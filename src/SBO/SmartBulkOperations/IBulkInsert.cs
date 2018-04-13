using System.Collections.Generic;

namespace SmartBulkOperations
{
    public interface IBulkInsert
    {
        string BulkInsertCmd<T>(List<T> items);
    }
}
