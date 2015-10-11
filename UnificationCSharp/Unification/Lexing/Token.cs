namespace Unification
{
    public enum Token
    {
        [RegexPattern(@"([""'])(?:\\\1|.)*?\1")]        QUOTED_STRING,
        [RegexPattern(@"[-+]?\d*\.\d+([eE][-+]?\d+)?")] FLOAT,
        [RegexPattern(@"[-+]?\d+")]                     INT,
        [RegexPattern(@"true")]                         TRUE,
        [RegexPattern(@"false")]                        FALSE,
        [RegexPattern(@"[*<>\?\-+/a-z->!]+")]           FUNCTION,
        [RegexPattern(@"[*<>\?\-+/A-Z->!]+")]           VARIABLE,
        [RegexPattern(@"\.")]                           DOT,
        [RegexPattern(@",")]                            COMMA,
        [RegexPattern(@"\(")]                           LEFT,
        [RegexPattern(@"\)")]                           RIGHT,
        [RegexPattern(@"\s")]                           SPACE
    }
}