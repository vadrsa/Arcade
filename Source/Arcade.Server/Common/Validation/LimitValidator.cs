using Common.Faults;
using SharedEntities;

namespace Common.Validation
{
    public static class LimitValidator
    {
        public static void AssureLimit(int limit)
        {
            if (limit < 1)
                throw new FaultException(FaultType.BadRequest);
        }
    }
}
