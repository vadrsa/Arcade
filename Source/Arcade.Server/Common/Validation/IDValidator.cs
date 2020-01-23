using Common.ResponseHandling;
using System.ComponentModel.DataAnnotations;

namespace Common.Validation
{
    public static class IDValidator
    {
        public static void AssureID(int id)
        {
            if (id < 1)
                throw new ApiException(Enums.FaultCode.InvalidID);
        }

        public static void AssureEmpty(int id)
        {
            if (id >= 1)
                throw new ApiException(Enums.FaultCode.NonEmptyID);
        }
    }
}
