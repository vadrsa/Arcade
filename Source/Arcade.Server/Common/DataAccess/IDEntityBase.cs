namespace Common.DataAccess
{
    public abstract class IDEntityBase<T> : IIdEntityBase<T>
    {
        public T Id { get; set; }
    }
}