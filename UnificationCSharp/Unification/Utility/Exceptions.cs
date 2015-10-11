using System;

namespace Unification
{
    public class NotUnifiableException : Exception
    {
        public NotUnifiableException(string value1, string value2, string cause)
            : base($"not unifiable: {value1} + {value2} : {cause}") { }
    }
}