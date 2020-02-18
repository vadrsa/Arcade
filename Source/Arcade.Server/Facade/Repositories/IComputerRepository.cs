using BusinessEntities;
using Common.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Repositories
{
    public interface IComputerTypeRepository : IRepository<ComputerType, string, IComputerTypeRepository>
    {
    }
}
