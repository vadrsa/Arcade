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
    public class EmployeeActivityRepository : SimpleRepositoryBase<EmployeeActivity, string, IEmployeeActivityRepository, ArcadeContext>, IEmployeeActivityRepository
    {
        public EmployeeActivityRepository(ArcadeContext context) : base(context)
        {
        }

        public override Expression<Func<ArcadeContext, ITable<EmployeeActivity>>> TableExpression => c => c.EmployeeActivity;
    }
}
