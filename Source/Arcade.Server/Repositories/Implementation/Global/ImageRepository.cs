using BusinessEntities;
using Common.DataAccess;
using Repositories.LinqToDB;
using System;
using System.Linq.Expressions;

namespace Core.Repositories.Implementation
{
    public class ImageRepository : SimpleRepositoryBase<Image, int, IImageRepository>, IImageRepository
    {
        public ImageRepository(AcradeDB context) : base(context)
        {
        }

        public override Expression<Func<DataConnection, ITable<Image>>> TableExpression => c => ((AcradeDB)c).Images;

    }
}
