using System.Linq;

namespace Unification
{
    public class FunctionExpression : IExpression
    {
        public bool IsVariable { get { return false; } }
        public bool IsFunction { get { return true; } }
        public string Value { get; private set; }
        public IExpression[] Expressions { get; private set; }
        public FunctionExpression(string value, params IExpression[] expr)
        {
            Expressions = expr;
            Value = value;
        }
        public override string ToString()
        {
            return Value + "("+string.Join(",", Expressions.Select(x=>x.ToString()))+")";
        }

    }
}