using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Unification
{
    /// <summary>
    /// Lexical analyzer used to tokenize given input string using RegexPatternAttributes attached to the tokens
    /// </summary>
    public class Lexer
    {
        private string input;
        /// <summary>
        /// Tokens to match
        /// </summary>
        public Token[] Tokens { set { Definitions = value.Select(t => new TokenDefinition(t)).ToArray(); } }
        /// <summary>
        /// TokenDefinitions containing RegexMatchers to match for target token
        /// </summary>
        private TokenDefinition[] Definitions { get; set; }
        /// <summary>
        /// Peek detected token
        /// </summary>
        public Token Token { get; private set; }
        /// <summary>
        /// Value within the token
        /// </summary>
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

        /// <summary>
        /// Read next token
        /// </summary>
        /// <returns>has next token</returns>
        public bool Next()
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            //Match for tokens and consume detected input.
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

        /// <summary>
        /// Token definition used for matching
        /// </summary>
        private sealed class TokenDefinition
        {
            /// <summary>
            /// Matcher for target token
            /// </summary>
            public RegexMatcher Matcher { get; }
            /// <summary>
            /// Target token
            /// </summary>
            public Token Token { get; }

            public TokenDefinition(Token token)
            {
                Token = token;
                Matcher = new RegexMatcher(token.Pattern());
            }
            /// <summary>
            /// Matcher checking start of the input string for given pattern
            /// </summary>
            public class RegexMatcher
            {
                private readonly Regex m;
                public RegexMatcher(string pattern)
                {
                    m = new Regex($"^{pattern}");
                }
                /// <summary>
                /// Get the length of the match
                /// </summary>
                /// <param name="s">input string</param>
                /// <returns>match length</returns>
                public int MatchLength(string s)
                {
                    var r = m.Match(s);

                    return r.Success ? r.Length : 0;
                }
            }
        }
    }
}