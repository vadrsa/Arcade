namespace Infrastructure.Security
{
    public class AnonymousIdentity : AppIdentity
    {
        public AnonymousIdentity() : base(null, null)
        {
        }
    }
}
