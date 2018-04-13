using System.Collections.Generic;

namespace SmartBulkOperations
{
    public interface IPagedBulk<T>
    {
        int PageSize { get; set; }

        List<T> Items { get; set; }

        int PagesNumber { get; }

        void SetBulkInsert(IBulkInsert bulkInsert);

        bool HasNext();

        string Next();
    }
}
