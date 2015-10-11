using System.Text.RegularExpressions;

namespace Unification
{
    public class RegexMatcher
    {
        private Regex m;
        public RegexMatcher(string pattern)
        {
            m = new Regex(string.Format("^{0}", pattern));
        }

        public int MatchLength(string s)
        {
            var r = m.Match(s);

            return r.Success ? r.Length : 0;
        }
    }
}