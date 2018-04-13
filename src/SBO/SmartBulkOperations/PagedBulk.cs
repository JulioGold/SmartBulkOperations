using System.Collections.Generic;
using System.Linq;

namespace SmartBulkOperations
{
    public class PagedBulk<T> : IPagedBulk<T>
    {
        public PagedBulk() { }

        public PagedBulk(List<T> items, IBulkInsert bulkInsert)
        {
            Items = items;
            _bulkInsert = bulkInsert;
        }

        private int _currentPage = default(int);

        private int _numberOfPages = default(int);

        private IBulkInsert _bulkInsert;

        public int PageSize { get; set; } = 500;

        public List<T> Items { get; set; }

        public int PagesNumber { get { return _numberOfPages; } private set { } }

        public void SetBulkInsert(IBulkInsert bulkInsert)
        {
            _bulkInsert = bulkInsert;
        }

        public bool HasNext()
        {
            _numberOfPages = (Items.Count / PageSize) + ((Items.Count % PageSize) > 0 ? 1 : 0);

            return _currentPage < _numberOfPages;
        }

        public string Next()
        {
            var itemsListPaged = Items.Skip(_currentPage * PageSize).Take(PageSize).ToList();

            _currentPage++;

            string sqlCmd = _bulkInsert.BulkInsertCmd(itemsListPaged);

            return sqlCmd;
        }
    }
}
