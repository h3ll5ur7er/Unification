using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Unification
{
    public class Lexer
    {
        private string input;

        public Token[] Tokens { set { Definitions = value.Select(t => new TokenDefinition(t)).ToArray(); }
        }
        private TokenDefinition[] Definitions { get; set; }

        public Token Token { get; private set; }
        public string Value { get; private set; }

        public Lexer(string input, params Token[] tokens)
        {
            if (tokens.Length == 0)
                tokens = Enum.GetValues(typeof(Token)).Cast<Token>().ToArray();
            
            Definitions = tokens
                .Select(t=> new TokenDefinition(t))
                .ToArray();

            this.input = input;
        }

        public bool Next()
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            foreach (var definition in Definitions)
            {
                var match = definition.Matcher.MatchLength(input);
                if (match <= 0) continue;
                Token = definition.Token;
                Value = input.Substring(0, match);
                input = input.Substring(match);
                return true;
            }
            return false;
        }

        private sealed class TokenDefinition
        {
            public RegexMatcher Matcher { get; }
            public Token Token { get; }

            public TokenDefinition(Token token)
            {
                Token = token;
                Matcher = new RegexMatcher(token.Pattern());
            }

            public class RegexMatcher
            {
                private readonly Regex m;
                public RegexMatcher(string pattern)
                {
                    m = new Regex($"^{pattern}");
                }

                public int MatchLength(string s)
                {
                    var r = m.Match(s);

                    return r.Success ? r.Length : 0;
                }
            }
        }

    }
}