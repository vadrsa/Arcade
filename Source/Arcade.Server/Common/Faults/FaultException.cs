﻿using SharedEntities;
using System;

namespace Common.Faults
{
    public class FaultException : Exception
    {
        private FaultType _type;

        public FaultType Type => _type;

        public FaultException(FaultType type, string message = "") : base(message)
        {
            _type = type;
        }
    }
}
