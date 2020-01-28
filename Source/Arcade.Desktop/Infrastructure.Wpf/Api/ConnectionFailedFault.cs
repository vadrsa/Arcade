using SharedEntities;

namespace Infrastructure.Api
{
    public class ConnectionFailedFault : FaultResponse
    {

        public ConnectionFailedFault() : base(0, "Failed to connect to the server.", null, 0)
        {
        }
    }
}
