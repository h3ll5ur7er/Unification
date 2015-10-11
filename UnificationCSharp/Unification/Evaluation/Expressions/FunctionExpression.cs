using System.Linq;

namespace Unification
{
    /// <summary>
    /// Function expressions have a value and parameters(those are contained in braces and separated by commas)
    /// </summary>
    public class FunctionExpression : IExpression
    {
        /// <summary>
        /// IsVariable will try to substitute using VariableRegistry
        /// </summary>
        public bool IsVariable => false;
        /// <summary>
        /// IsFunction will capture parameters in braces following the value
        /// </summary>
        public bool IsFunction => true;

        /// <summary>
        /// Value contains the name of functions and variables and the value of valuetypes
        /// </summary>
        public string Value { get; }
        /// <summary>
        /// Parameter expressions
        /// </summary>
        public IExpression[] Expressions { get; }
        public FunctionExpression(string value, params IExpression[] expr)
        {
            Expressions = expr;
            Value = value;
        }
        public override string ToString() => $"{Value}({string.Join(",", Expressions.Select(x=>x.ToString()))})";
    }
}