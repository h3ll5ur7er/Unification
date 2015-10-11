using System;

namespace Unification
{
    /// <summary>
    /// Attribute containing a regex pattern to match target token
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class RegexPatternAttribute : Attribute
    {
        /// <summary>
        /// Attribute containing a regex pattern to match target token.
        /// No accessor needed, because value is read from the constructor arguments using reflection
        /// </summary>
        /// <param name="pattern">regex pattern</param>
        public RegexPatternAttribute(string pattern)
        {
        }
    }
}