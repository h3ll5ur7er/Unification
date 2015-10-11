namespace Unification
{
    public class VariableExpression : IExpression
    {
        /// <summary>
        /// IsVariable will try to substitute using VariableRegistry
        /// </summary>
        public bool IsVariable => true;
        /// <summary>
        /// IsFunction will capture parameters in braces following the value
        /// </summary>
        public bool IsFunction => false;
        /// <summary>
        /// Value contains the name of the variable
        /// </summary>
        public string Value { get; }

        public VariableExpression(string value)
        {
            Value = value;
        }

        public override string ToString() => VariableRegistry.Instance.Contains(Value) ? VariableRegistry.Instance[this].ToString() : Value;
    }
}