
using LinqToDB.Data;

namespace Common.DataAccess
{
    public class AccessorBase<T>
        where T: DataConnection
    {
        protected T Context { get; private set; }

        public AccessorBase(T context)
        {
            Context = context;
        }
    }
}
