namespace Unification
{
    /// <summary>
    /// Definition of the tokens
    /// Use RegexPatternAttribute on the tokens to define the pattern to match
    /// </summary>
    public enum Token
    {
        [RegexPattern(@"([""'])(?:\\\1|.)*?\1")]        QuotedString,
        [RegexPattern(@"[-+]?\d*\.\d+([eE][-+]?\d+)?")] Float,
        [RegexPattern(@"[-+]?\d+")]                     Int,
        [RegexPattern(@"true")]                         True,
        [RegexPattern(@"false")]                        False,
        [RegexPattern(@"[*<>\?\-+/a-z->!]+")]           Function,
        [RegexPattern(@"[*<>\?\-+/A-Z->!]+")]           Variable,
        [RegexPattern(@"\.")]                           Dot,
        [RegexPattern(@",")]                            Comma,
        [RegexPattern(@"\(")]                           LeftBrace,
        [RegexPattern(@"\)")]                           RightBrace,
        [RegexPattern(@"\s")]                           Space
    }
}