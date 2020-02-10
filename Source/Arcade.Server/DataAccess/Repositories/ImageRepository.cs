using BusinessEntities;
using Common.DataAccess;
using Facade.Repositories;
using LinqToDB;
using System;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class ImageRepository : SimpleRepositoryBase<Image, string, IImageRepository, ArcadeContext>, IImageRepository
    {
        public ImageRepository(ArcadeContext context) : base(context)
        {
        }

        public override Expression<Func<ArcadeContext, ITable<Image>>> TableExpression => c => c.Images;
    }
}
