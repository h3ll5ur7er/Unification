namespace Unification
{
    public sealed class TokenDefinition
    {
        public RegexMatcher Matcher { get; private set; }
        public Token Token { get; private set; }

        public TokenDefinition(Token token)
        {
            Token = token;
            Matcher = new RegexMatcher(token.Pattern());
        }
    }
}