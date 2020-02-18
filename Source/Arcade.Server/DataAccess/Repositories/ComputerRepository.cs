using BusinessEntities;
using Common.DataAccess;
using Facade.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using LinqToDB;

namespace DataAccess.Repositories
{
    public class ComputerRepository : SimpleRepositoryBase<Computer, string, IComputerRepository, ArcadeContext>, IComputerRepository
    {
        public ComputerRepository(ArcadeContext context) : base(context)
        {
        }

        public override Expression<Func<ArcadeContext, ITable<Computer>>> TableExpression => c => c.Computers;
    }
}
