using BusinessEntities;
using Common.DataAccess;
using Facade.Repositories;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class PaymentRepository : SimpleRepositoryBase<Payment, string, IPaymentRepository, ArcadeContext>, IPaymentRepository
    {
        public PaymentRepository(ArcadeContext context) : base(context)
        {
        }

        public override Expression<Func<ArcadeContext, ITable<Payment>>> TableExpression => c => c.Payments;
    }
}
