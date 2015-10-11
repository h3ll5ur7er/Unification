using System.Linq;

namespace Unification
{
    public static  class Extentions
    {
        public static string Pattern(this Token t)
        {
            return t.GetType().GetMembers()[((int)t) + 11].CustomAttributes.First().ConstructorArguments.First().Value.ToString();
        }

        public static TokenDefinition Definition(this Token t)
        {
            return new TokenDefinition(t);
        }

        public static bool XNOR(this bool b1, bool b2)
        {
            return (b1 && b2) || (!b1 && !b2);
        }
    }
}