using BusinessEntities;
using Common.DataAccess;
using System;

namespace Facade.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee, string, IEmployeeRepository>
    {
    }
}
