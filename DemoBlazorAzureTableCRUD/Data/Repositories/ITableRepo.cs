using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DemoBlazorAzureTableCRUD.Data.Repositories
{
    public interface ITableRepo<T> where T : class
    {
        Task<bool> CreateAsync(T newTodo);
        Task<bool> UpdateAsync(T updateTodo);
        Task<bool> DeleteAsync(T deleteTodo);

        Task<List<T>> ListAsync(Expression<Func<T, bool>> filter);
    }
}
