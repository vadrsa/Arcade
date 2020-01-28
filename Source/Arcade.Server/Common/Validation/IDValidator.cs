using Common.Faults;
using SharedEntities;

namespace Common.Validation
{
    public static class IDValidator
    {
        public static void AssureID(int id)
        {
            if (id < 1)
                throw new FaultException(FaultType.BadRequest);
        }

        public static void AssureEmpty(int id)
        {
            if (id >= 1)
                throw new FaultException(FaultType.BadRequest);
        }
    }
}
