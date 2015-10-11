using System;
using System.Linq;

namespace Unification
{
    public class Lexer
    {
        private string input;

        public TokenDefinition[] Definitions { get; set; }
        public Token Token { get; private set; }
        public string Value { get; private set; }

        public Lexer(string input, params Token[] tokens)
        {
            if (tokens.Length == 0)
                tokens = Enum.GetValues(typeof(Token)).Cast<Token>().ToArray();
            
            Definitions = tokens.Select(Extentions.Definition).ToArray();
            this.input = input;
        }

        public bool Next()
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            foreach (var definition in Definitions)
            {
                var match = definition.Matcher.MatchLength(input);
                if (match > 0)
                {
                    Token = definition.Token;
                    Value = input.Substring(0, match);
                    input = input.Substring(match);
                    return true;
                }
            }
            return false;
        }
    }
}