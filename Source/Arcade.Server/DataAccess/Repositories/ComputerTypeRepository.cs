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
    public class ComputerTypeRepository : SimpleRepositoryBase<ComputerType, string, IComputerTypeRepository, ArcadeContext>, IComputerTypeRepository
    {
        public ComputerTypeRepository(ArcadeContext context) : base(context)
        {
        }

        public override Expression<Func<ArcadeContext, ITable<ComputerType>>> TableExpression => c => c.ComputerTypes;
    }
}
