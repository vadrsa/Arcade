namespace Infrastructure.Security
{
    public class AnonymousIdentity : AppIdentity
    {
        public AnonymousIdentity() : base(null, null, new System.Collections.Generic.List<string>())
        {
        }
    }
}
