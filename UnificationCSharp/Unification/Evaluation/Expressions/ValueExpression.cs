namespace Unification
{
    public class ValueExpression : IExpression
    {
        /// <summary>
        /// IsVariable will try to substitute using VariableRegistry
        /// </summary>
        public bool IsVariable => false;
        /// <summary>
        /// IsFunction will capture parameters in braces following the value
        /// </summary>
        public bool IsFunction => false;

        /// <summary>
        /// Value contains the name of functions and variables and the value of valuetypes
        /// </summary>
        public string Value { get; }

        public ValueExpression(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;
    }
}