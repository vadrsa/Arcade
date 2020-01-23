using Common.Enums;
using Common.ResponseHandling;

namespace Common.Validation
{
    public static class LimitValidator
    {
        public static void AssureLimit(int limit)
        {
            if (limit < 1)
                throw new ApiException(FaultCode.InvalidLimit);
        }
    }
}
