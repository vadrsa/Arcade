using BusinessEntities;
using Common.DataAccess;
using Facade.Repositories;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class GameRepository : SimpleRepositoryBase<Game, string, IGameRepository, ArcadeContext>, IGameRepository
    {
        public GameRepository(ArcadeContext context) : base(context)
        {
        }

        public override Expression<Func<ArcadeContext, ITable<Game>>> TableExpression => c => c.Games;
    }
}
