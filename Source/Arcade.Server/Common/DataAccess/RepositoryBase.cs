using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Common.DataAccess
{
    /// <summary>
    /// Repository base for any type of repsoitory
    /// RepositoryBase doesn't assume anything about the type and quantity of primary keys
    /// </summary>
    /// <typeparam name="T">The entity class/DB model that this repository works with</typeparam>
    /// <typeparam name="R">The repository class that inherits RepositoryBase. Example usage: public class ProductRepository&lt;ProductModel, ProductRepository&gt;</typeparam>
    public abstract class RepositoryBase<T,R,C>
        where T : class
        where R : class
        where C : DataConnection
    {
        #region Private Fields

        List<Expression<Func<T, object>>> LoadWithList = new List<Expression<Func<T, object>>>();
        // the offset to be used for the next select
        int _offset;
        // the limit to be used for the next select
        int _limit;
        // the order by predicate to be used for the next select
        Func<T, object> _orderBy;
        // specifies ascending or desending select
        bool _ascending;
        // the db context
        C _context;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for base class of any repository
        /// </summary>
        public RepositoryBase(C context)
        {
            _context = context;
        }
        #endregion
        /// <summary>
        /// Should be implemeneted in inhereted class to specifiy the db table for Linq2DB DataContext
        /// </summary>
        public abstract Expression<Func<C, ITable<T>>> TableExpression { get; }

        protected C Context
        {
            get
            {
                return _context;
            }
        }

        /// <summary>
        /// Forces the next Select to laod entities with the selected nested / foreign entity
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public R LoadWith(Expression<Func<T, object>> exp)
        {
            LoadWithList.Add(exp);
            return this as R;
        }

        /// <summary>
        /// Sets the offset for the next select.
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public R Offset(int offset)
        {
            _offset = offset;
            return this as R;
        }

        /// <summary>
        /// Sets the limit for the next select.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public R Limit(int limit)
        {
            _limit = limit;
            return this as R;
        }

        private Func<T, object> GetOrderByExpression(string sortColumn)
        {
            Func<T, object> orderByExpr = null;
            if (!String.IsNullOrEmpty(sortColumn))
            {
                Type sponsorResultType = typeof(T);

                if (sponsorResultType.GetProperties().Any(prop => prop.Name == sortColumn))
                {
                    System.Reflection.PropertyInfo pinfo = sponsorResultType.GetProperty(sortColumn);
                    orderByExpr = (data => pinfo.GetValue(data, null));
                }
            }
            return orderByExpr;
        }

        private ITable<T> GetTable(C context)
        {
            ITable<T> table = TableExpression.Compile()(context);
            foreach (var exp in LoadWithList)
            {
                table = table.LoadWith(exp);
            }
            return table;
        }

        /// <summary>
        /// Executes the select operation with the current state.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected IEnumerable<T> ExecuteSelect(Func<ITable<T>, IEnumerable<T>> func, C context)
        {
            IEnumerable<T> res = func(GetTable(context));

            if (_orderBy != null)
            {
                if (_ascending)
                    res = res.OrderBy(_orderBy.Clone() as Func<T, object>);
                else
                    res = res.OrderByDescending(_orderBy.Clone() as Func<T, object>);
            }
            if (_offset != 0)
                res = res.Skip(_offset);
            _offset = 0;

            if (_limit != 0)
                res = res.Take(_limit);
            _limit = 0;

            _orderBy = null;
            LoadWithList.Clear();
            return res;
        }

        /// <summary>
        /// Executes the select operation with the current state asynchronously.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> ExecuteSelectAsync(Func<ITable<T>, IEnumerable<T>> func, C context)
        {
            return await Task.Run(() => ExecuteSelect(func, context));
        }


        public R OrderBy(string orderBy, bool ascending = true)
        {
            _ascending = ascending;
            _orderBy = GetOrderByExpression(orderBy);
            return this as R;
        }

        /// <summary>
        /// Inserts the specified range into the database
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public long InsertRange(List<T> list)
        {
            return Context.BulkCopy(list).RowsCopied;
        }

        /// <summary>
        /// Inserts the specified range into the database asynchronously.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<long> InsertRangeAsync(List<T> list, CancellationToken token = new CancellationToken())
        {
            return Task.Run(() => InsertRange(list));
        }
    }
}
