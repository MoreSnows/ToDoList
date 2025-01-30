using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task InsertAsync<TKey>(T entity);
        Task InsertManyAsync<TKey>(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task<T> FindAsync<TKey>(TKey key);
        Task<T> FirstOrDefaultAsync(string sql, object prms);
        Task<IEnumerable<T>> GetListAsync(object whereCond = null);
        Task<int> ExecuteAsync(string sql, object prms);
        Task<IEnumerable<TModel>> QueryAsync<TModel>(string sql, object prms = null);
        Task InsertManyAsync<TKey>(List<T> entities);
        Task<IEnumerable<TModel>> QueryByAsync<TModel>(string sql, object prms = null, int? timeout = null);
    }
}
