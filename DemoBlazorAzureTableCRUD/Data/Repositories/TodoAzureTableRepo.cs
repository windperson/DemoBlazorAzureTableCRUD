using Azure.Data.Tables;
using DemoBlazorAzureTableCRUD.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Azure;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;

namespace DemoBlazorAzureTableCRUD.Data.Repositories
{
    public class TodoAzureTableRepo : ITableRepo<TodoItem>
    {
        private const string TableName = @"Todos";
        private readonly string _conn;

        public TodoAzureTableRepo(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("AzureTableConnection");
        }

        public async Task<bool> CreateAsync(TodoItem newTodo)
        {
            var tableClient = new TableClient(_conn, TableName);

            var result = await tableClient.AddEntityAsync(newTodo);
            return result.Status == StatusCodes.Status204NoContent 
                || result.Status == StatusCodes.Status201Created;
        }

        public async Task<bool> UpdateAsync(TodoItem updateTodo)
        {
            var tableClient = new TableClient(_conn, TableName);

            var result = await tableClient.UpsertEntityAsync(updateTodo);
            return result.Status == StatusCodes.Status204NoContent;
        }

        public async Task<bool> DeleteAsync(TodoItem deleteTodo)
        {
            var tableClient = new TableClient(_conn, TableName);

            var result = await tableClient.DeleteEntityAsync(deleteTodo.PartitionKey, deleteTodo.RowKey);
            return result.Status == StatusCodes.Status204NoContent;
        }

        public async Task<List<TodoItem>> ListAsync(Expression<Func<TodoItem, bool>> filter)
        {
            var tableClient = new TableClient(_conn, TableName);

            AsyncPageable<TodoItem> queryResultsLINQ = tableClient.QueryAsync(filter);

            var result = new List<TodoItem>();
            await foreach(var page in queryResultsLINQ.AsPages())
            {
                result.AddRange(page.Values);
            }

            return result;
        }
    }
}
