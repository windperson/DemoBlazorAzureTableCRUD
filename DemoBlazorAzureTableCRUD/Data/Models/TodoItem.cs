using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlazorAzureTableCRUD.Data.Models
{
    public class TodoItem : ITableEntity
    {
        public int Priority { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public bool Finished { get; set; }
        public DateTime? DueDate { get; set; }

        //ITableEntity implementations
        public string PartitionKey { get; set; } = "Todo ver 1.0";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
