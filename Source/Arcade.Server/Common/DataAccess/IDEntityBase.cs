using LinqToDB.Mapping;

namespace Common.DataAccess
{
    public abstract class IDEntityBase<T> : IIdEntityBase<T>
    {
        [PrimaryKey, Column(Length = 40),NotNull]
        public T Id { get; set; }
    }
}