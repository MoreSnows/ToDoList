using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using ToDoList.Domain.Interfaces.Repositories;

namespace ToDoList.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>
    {
        protected readonly Context Context;

        public RepositoryBase(Context context)
        {
            Context = context;
        }

        public async Task InsertAsync<TKey>(T entity)
        {
            await Context.Connection.InsertAsync<TKey, T>(entity, transaction: Context.Transaction);
        }

        public async Task InsertManyAsync<TKey>(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                await Context.Connection.InsertAsync<TKey, T>(entity, transaction: Context.Transaction);
        }

        public async Task UpdateAsync(T entity)
        {
            await Context.Connection.UpdateAsync(entity, Context.Transaction);
        }

        public async Task<T> FindAsync<TKey>(TKey key)
        {
            return await Context.Connection.GetAsync<T>(key);
        }

        public async Task<T> FirstOrDefaultAsync(string sql, object prms)
        {
            return await Context.Connection.QueryFirstOrDefaultAsync<T>(sql, prms, transaction: Context.Transaction, commandType: System.Data.CommandType.Text);
        }

        public async Task<IEnumerable<T>> GetListAsync(object whereCond = null)
        {
            return await Context.Connection.GetListAsync<T>(whereCond, transaction: Context.Transaction);
        }

        public async Task<int> ExecuteAsync(string sql, object prms)
        {
            return await Context.Connection.ExecuteAsync(sql, prms, transaction: Context.Transaction);
        }

        public async Task<IEnumerable<TModel>> QueryAsync<TModel>(string sql, object prms = null)
        {
            return await Context.Connection.QueryAsync<TModel>(sql, prms, transaction: Context.Transaction);
        }

        public async Task InsertManyAsync<TKey>(List<T> entities)
        {
            foreach (var entity in entities)
                await Context.Connection.InsertAsync<TKey, T>(entity, transaction: Context.Transaction);
        }

        public async Task<IEnumerable<TModel>> QueryByAsync<TModel>(string sql, object prms = null, int? timeout = null)
        {
            return await Context.Connection.QueryAsync<TModel>(sql, prms, transaction: Context.Transaction, commandTimeout: timeout);
        }
    }
}