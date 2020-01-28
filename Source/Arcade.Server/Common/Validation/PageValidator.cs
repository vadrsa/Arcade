using Common.Enums;
using Common.Faults;
using Common.ResponseHandling;
using SharedEntities;

namespace Common.Validation
{
    public static class PageValidator
    {
        public static void AssurePage(int page)
        {
            if (page < 1)
                throw new FaultException(FaultType.BadRequest);
        }
    }
}
