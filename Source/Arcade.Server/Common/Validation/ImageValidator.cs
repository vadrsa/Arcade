using Common.Enums;
using Common.ResponseHandling;

namespace Common.Validation
{
    public static class ImageValidator
    {
        public static void AssureNonEmpty(byte[] image)
        {
            if (image == null || image.Length == 0)
                throw new ApiException(FaultCode.EmptyImageFault);
        }
    }
}
