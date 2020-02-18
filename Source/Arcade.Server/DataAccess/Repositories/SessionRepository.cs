using BusinessEntities;
using Common.DataAccess;
using Facade.Repositories;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SessionRepository : SimpleRepositoryBase<Session, string, ISessionRepository, ArcadeContext>, ISessionRepository
    {
        public SessionRepository(ArcadeContext context) : base(context)
        {
        }

        public override Expression<Func<ArcadeContext, ITable<Session>>> TableExpression => c => c.Sessions;

        public async Task<List<Session>> GetComputerSessionsAsync(string computerId)
        {
            return (await ExecuteSelectAsync(t => t.Where(s => s.ComputerId == computerId), Context)).ToList();
        }
    }
}
