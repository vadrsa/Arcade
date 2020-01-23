using BusinessEntities;
using Common.DataAccess;
using Facade.Repository;
using LinqToDB;
using LinqToDB.Data;
using Repositories.LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories.Implementation.Security
{
    public class UserRepository : RepositoryBase<User, RoleRepository>, IUserRepository
    {
        public UserRepository(AcradeDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<User>>> TableExpression => c => ((AcradeDB)c).Users;

        public async Task<List<User>> GetAllAsync()
        {
            return (await ExecuteSelectAsync(t=> t, Context)).ToList();
        }

        public async Task<User> GetByIDAsync(string id)
        {
            return (await ExecuteSelectAsync(t => t.Where(u => u.Id == id), Context)).First();
        }
    }
}
