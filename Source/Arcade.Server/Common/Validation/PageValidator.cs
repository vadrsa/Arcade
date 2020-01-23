using Common.Enums;
using Common.ResponseHandling;

namespace Common.Validation
{
    public static class PageValidator
    {
        public static void AssurePage(int page)
        {
            if (page < 1)
                throw new ApiException(FaultCode.InvalidPage);
        }
    }
}
