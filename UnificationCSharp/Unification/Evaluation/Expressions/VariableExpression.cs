namespace Unification
{
    public class VariableExpression : IExpression
    {
        private readonly VariableRegistry registry;
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

        public VariableExpression(string value, ref VariableRegistry registry)
        {
            this.registry = registry;
            Value = value;
        }

        public override string ToString() => registry.Contains(Value) ? registry[Value].ToString() : Value;
    }
}