using SharedEntities;

namespace Infrastructure.Api
{
    public class UnexpectedFault : FaultResponse
    {

        public UnexpectedFault(int statusCode) : base(0, "Unexpected error occurred, please contact your administrator.", null, statusCode)
        {

        }
    }
}
