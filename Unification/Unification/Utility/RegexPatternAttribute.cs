using System;

namespace Unification
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RegexPatternAttribute : Attribute
    {
        public string Pattern { get; private set; }

        public RegexPatternAttribute(string pattern)
        {
            Pattern = pattern;
        }
    }
}