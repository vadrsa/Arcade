using BusinessEntities;
using Common.DataAccess;
using Facade.Repositories;
using LinqToDB;
using System;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class EmployeeRepository : SimpleRepositoryBase<Employee, string, IEmployeeRepository, ArcadeContext>, IEmployeeRepository
    {
        public EmployeeRepository(ArcadeContext context) : base(context)
        {
        }

        public override Expression<Func<ArcadeContext, ITable<Employee>>> TableExpression => c => c.Employees;
    }
}
