using BusinessEntities;
using Common.DataAccess;
using Facade.Repository;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Identity;
using Repositories.LinqToDB;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories.Implementation.Security
{
    public class RoleRepository : RepositoryBase<Role, RoleRepository>, IRoleRepository
    {
        public RoleRepository(AcradeDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<Role>>> TableExpression => c => ((AcradeDB)c).Roles;

        public async Task<IdentityRole> GetByIDAsync(string ID)
        {
            return (await ExecuteSelectAsync(t => t.Where(r => r.Id == ID), Context)).First();
        }
    }
}
