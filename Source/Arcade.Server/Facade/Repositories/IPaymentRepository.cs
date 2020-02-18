using BusinessEntities;
using Common.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Repositories
{
    public interface IPaymentRepository : IRepository<Payment, string, IPaymentRepository>
    {
    }
}
