using BusinessEntities;
using Common.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Repositories
{
    public interface IComputerRepository : IRepository<Computer, string, IComputerRepository>
    {
    }
}
