using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos.Table;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace timeline_lauka_app
{
    class TableDatabase
    {
        private CloudTable _table;
        private CloudTableClient _client;

        public TableDatabase(string connectionString, string table)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            _client = storageAccount.CreateCloudTableClient();
            _table = _client.GetTableReference(table);
        }

        public CloudTable GetTable(string tablename)
        {
            return _client.GetTableReference(tablename);
        }

        public TimelineItem GetItemByKey(string key)
        {
            TimelineItem item = (TimelineItem)_table.Execute(TableOperation.Retrieve<TimelineItem>(key, key)).Result;

            return item;
        }

        public IEnumerable<TimelineItem> GetAllItems()
        {
            TableQuery<TimelineItem> query = new TableQuery<TimelineItem>();
            return _table.ExecuteQuery(query).OrderBy(o => o.OrderByTime);
        }

        public async Task DeleteItemAsync(TimelineItem item)
        {
            TableOperation op = TableOperation.Delete(item);
            await _table.ExecuteAsync(op);
        }

        public async Task AddItemAsync(TimelineItem item)
        {
            TableOperation op = TableOperation.Insert(item);
            await _table.ExecuteAsync(op);
        }
    }
}
