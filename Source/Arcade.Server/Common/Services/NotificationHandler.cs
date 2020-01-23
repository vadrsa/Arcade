namespace Common.Services
{
    public interface INotificationHandler
    {
        void Handle(string name, object data);
    }
}
