using BusinessEntities;
using Common.DataAccess;
using Facade.Repositories;
using LinqToDB;
using System;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class SystemSettingRepository : SimpleRepositoryBase<SystemSetting, int, ISystemSettingRepository, ArcadeContext>, ISystemSettingRepository
    {
        public SystemSettingRepository(ArcadeContext context) : base(context)
        {
        }

        public override Expression<Func<ArcadeContext, ITable<SystemSetting>>> TableExpression => c => c.SystemSettings;
    }
}
