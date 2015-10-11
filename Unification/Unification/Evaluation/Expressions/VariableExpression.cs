namespace Unification
{
    public class VariableExpression : IExpression
    {
        private readonly Evaluator.VariableRegistry registry;
        public bool IsVariable { get { return true; } }
        public bool IsFunction { get { return false; } }
        public VariableExpression(string value, ref Evaluator.VariableRegistry registry)
        {
            this.registry = registry;
            Value = value;
        }

        public string Value { get; private set; }
        public override string ToString()
        {
            return registry.Contains(Value) ? registry[Value].ToString() : Value;
        }
    }
}