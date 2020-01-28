using SharedEntities;

namespace Infrastructure.Api
{
    public class UnexpectedFault : FaultResponse
    {

        public UnexpectedFault() : base(0, "Unexpected error occurred, please contact your administrator.", null, 0)
        {

        }
    }
}
