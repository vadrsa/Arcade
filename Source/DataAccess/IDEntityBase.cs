namespace DataAccess
{
    public abstract class IDEntityBase<T> : IIdEntityBase<T>
    {
        public T ID { get; set; }
    }
}