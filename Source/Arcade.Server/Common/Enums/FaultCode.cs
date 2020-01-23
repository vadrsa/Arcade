using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
    public enum FaultCode
    {
        Undefined = 0,
        InternalError,
        AuthenticationRequired,
        AccessDenied,
        InvalidUserCredentials,

        InvalidInput,
        InvalidID,
        InvalidLimit,
        InvalidPage,
        EmptyImageFault,
        UnsupportedCulture,
        NotAllCulturesProvided,
        NonEmptyID
    }
}
