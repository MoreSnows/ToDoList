using System.Data;
using Dapper;
using System.Text;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }

        public async Task<User> GetUser(UserSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException(nameof(criteria));
            }

            var query = new StringBuilder("SELECT TOP 1 * FROM Users WHERE 1 = 1");
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(criteria.Email))
            {
                query.Append(" AND Email = @Email");
                parameters.Add("Email", criteria.Email);
            }

            if (!string.IsNullOrEmpty(criteria.Username))
            {
                query.Append(" AND Username = @Username");
                parameters.Add("Username", criteria.Username);
            }

            if (!string.IsNullOrEmpty(criteria.Phone))
            {
                query.Append(" AND Phone = @Phone");
                parameters.Add("Phone", criteria.Phone);
            }

            var user = await QueryAsync<User>(query.ToString(), parameters);
            return user.FirstOrDefault();
        }

        public async Task Insert(User user)
        {
            await InsertAsync<Guid>(user);
        }

        public async Task Update(User user)
        {
            await UpdateAsync(user);
        }
    }
}