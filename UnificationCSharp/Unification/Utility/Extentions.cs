using System.Linq;

namespace Unification
{
    public static  class Extentions
    {
        /// <summary>
        /// Retrieves Regex pattern specified in the RegexPatternAttribute
        /// </summary>
        /// <param name="t">target token</param>
        /// <returns>regex pattern</returns>
        public static string Pattern(this Token t)
            => t
                .GetType()
                .GetMembers()[((int) t) + 11] //11 is the offset created by standard functions of enum like the equality opperators, tostring and so far
                .CustomAttributes
                .First()
                .ConstructorArguments
                .Select(x => x.Value.ToString())
                .First();

        /// <summary>
        /// Create a new Tokendefinition from a Token
        /// </summary>
        /// <param name="t">target token</param>
        /// <returns>new TokenDefinition of t</returns>
        //public static Lexer.TokenDefinition Definition(this Token t) => new Lexer.TokenDefinition(t);
    }
}