namespace Common.DataAccess
{
    public interface IIdEntityBase<T>
    {
        T Id { get; set; }
    }
}
