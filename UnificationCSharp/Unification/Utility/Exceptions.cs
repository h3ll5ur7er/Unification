using System;

namespace Unification
{
    /// <summary>
    /// Exception thrown by the Parser
    /// </summary>
    public class NotUnifiableException : Exception
    {
        /// <summary>
        /// Exception thrown by the Parser
        /// </summary>
        /// <param name="value1">first value</param>
        /// <param name="value2">second value</param>
        /// <param name="cause">cause of the exception</param>
        public NotUnifiableException(string value1, string value2, string cause)
            : base($"not unifiable: {value1} + {value2} : {cause}") { }
    }
}